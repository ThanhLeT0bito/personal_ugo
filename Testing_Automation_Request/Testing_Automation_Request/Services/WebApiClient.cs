using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Testing_Automation_Request.Models;
using Testing_Automation_Request.ServiceLocators;

namespace Testing_Automation_Request.Services
{
    public abstract class WebApiClient : IWebApiClient
    {
        private const string EP_SERVERTIME = "/api/ServerUtilities/ServerTime";

        public static string ProxyHost { get; set; }
        public static string ProxyPort { get; set; }
        public static string ProxyUser { get; set; }
        public static string ProxyPass { get; set; }

        public Uri BaseUrl { get; private set; }
        public string UserAgent { get; private set; }

        protected string _mediaType = HttpRequesMessageBuilder.JSON;

        public POSMediaType MediaType
        {
            get
            {
                return _mediaType == HttpRequesMessageBuilder.JSON ? POSMediaType.Json : POSMediaType.Xml;
            }
            set
            {
                _mediaType = value == POSMediaType.Json ? HttpRequesMessageBuilder.JSON : HttpRequesMessageBuilder.XML;
            }
        }

        public WebApiClient(Uri baseurl, string useragent)
        {
            BaseUrl = baseurl;
            UserAgent = useragent;
        }

        public void Init(Uri uri, string userAgent)
        {
            BaseUrl = uri;
            UserAgent = userAgent;
        }

        public WebApiClient() { }

        HttpClient GetHttpClient()
        {
            if (!string.IsNullOrWhiteSpace(ProxyHost) && !string.IsNullOrWhiteSpace(ProxyPort))
            {
                WebProxy proxy = new WebProxy(new Uri(string.Format("http://{0}:{1}", ProxyHost.TrimEnd('/'), ProxyPort)));

                HttpClientHandler httpClientHandler = new HttpClientHandler
                {
                    Proxy = proxy,
                    UseProxy = true,
                    UseDefaultCredentials = true
                };

                if (!string.IsNullOrWhiteSpace(ProxyUser) && !string.IsNullOrWhiteSpace(ProxyPass))
                {
                    httpClientHandler.Credentials = new NetworkCredential(ProxyUser, ProxyPass);
                    httpClientHandler.UseDefaultCredentials = false;
                }

                return new HttpClient(httpClientHandler);
            }

            return new HttpClient();
        }

        protected async Task<Stream> SendRequest(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;
            ErrorResponseMessage error = null;
            Stream content = null;

            var client = GetHttpClient();

            if (!string.IsNullOrEmpty(UserAgent))
            {
                client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            }

            try
            {
                response = await client.SendAsync(request);
                content = await response.Content.ReadAsStreamAsync();

                if (!response.IsSuccessStatusCode)
                {
                    try
                    {
                        string stringContent = response.Content.ReadAsStringAsync().Result;

                        error = JsonConvert.DeserializeObject<ErrorResponseMessage>(stringContent);

                        throw new Exception(stringContent);
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            catch (ServiceInvokeException sie)
            {
                throw sie;
            }
            catch (AggregateException ae)
            {
                throw ae;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                client?.Dispose();
            }

            return content;
        }

        T DeserializeResponse<T>(string content)
        {
            try
            {
                T data = default(T);

                switch (_mediaType)
                {
                    case HttpRequesMessageBuilder.JSON:

                        data = JsonConvert.DeserializeObject<T>(content);

                        break;

                    case HttpRequesMessageBuilder.XML:

                        content = Regex.Replace(content, @"[^\u0009\u000A\u000D\u0020-\u007E]", "");

                        data = CustomXMLSerializer.Deserialize<T>(content);

                        break;

                    default:

                        return default(T);
                }

                ConvertToDoubleField(data);

                return data;
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public bool IsGenericList(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            foreach (Type @interface in type.GetInterfaces())
            {
                if (@interface.IsGenericType)
                {
                    if (@interface.GetGenericTypeDefinition() == typeof(ICollection<>))
                    {
                        // if needed, you can also return the type used as generic argument
                        return true;
                    }
                }
            }
            return false;
        }

        void ConvertToDoubleField(object data)
        {
            if (data == null)
                return;

            if (IsGenericList(data.GetType()))
                return;

            foreach (var prop in data.GetType().GetProperties())
            {
                var att = prop.GetCustomAttribute<LongNumberAttribute>();

                if (att == null)
                {
                    var type = prop.PropertyType;

                    if (type.Equals(typeof(string)) || type.IsValueType || IsGenericList(type))
                        continue;

                    ConvertToDoubleField(prop.GetValue(data));

                    continue;
                }

                prop.SetValue(data, Math.Round(((double)prop.GetValue(data)) / 100, 2));
            }

            return;
        }


        protected CancellationTokenSource _cts = null;

        protected async Task<StatusResponse<T>> Request<T>(HttpRequestMessage request, string mediaType = HttpRequesMessageBuilder.JSON)
        {
            HttpResponseMessage response = null;
            ErrorResponseMessage error = null;
            String content;

            StatusResponse<T> value = new StatusResponse<T>();

            //todo Lam add to pass ssl exception
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };

            _cts = new CancellationTokenSource();
            //cts.CancelAfter(10000);

            var client = new HttpClient(clientHandler);

            if (!string.IsNullOrEmpty(UserAgent))
            {
                client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            }

            if (request.Content != null && request.Content.Headers != null)
            {
                request.Content.Headers.Remove("Content-Type");
                request.Content.Headers.Add("Content-Type", _mediaType);
            }

            try
            {
                response = await client.SendAsync(request, _cts.Token);
                content = await response.Content.ReadAsStringAsync();

                value.StatusCode = response.StatusCode;

                if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(content))
                {
                    try
                    {
                        value.Data = DeserializeResponse<T>(content);
                    }
                    catch (Exception)
                    {
                        value.Data = default(T);
                    }
                }
                else if (!response.IsSuccessStatusCode && !string.IsNullOrEmpty(content))
                {
                    ErrorResponseModel model = DeserializeResponse<ErrorResponseModel>(content);
                    throw new POSErrorResponseExeption(model);
                }
            }
            catch (TaskCanceledException ex)
            {
                if (_cts.Token.IsCancellationRequested)
                {
                    throw new POSErrorResponseExeption(new ErrorResponseModel()
                    {
                        Response = new ErrorResponse()
                        {
                            ErrMessage = "Request Is canceled",
                            StatusCode = "400"
                        }
                    });
                }
                else
                {
                    throw ex;
                }

            }
            catch (POSErrorResponseExeption sie)
            {
                throw sie;
            }
            catch (AggregateException ae)
            {

            }
            catch (Exception e)
            {

            }
            finally
            {
                _cts = null;
                client?.Dispose();
            }

            return value;
        }

        protected async Task<T> SendRequest<T>(HttpRequestMessage request)
        {
            HttpResponseMessage response = null;
            ErrorResponseMessage error = null;
            String content;

            T value = default(T);

            //var client = GetHttpClient();

            //todo Lam add to pass ssl exception
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };
            var client = new HttpClient(clientHandler);

            if (!string.IsNullOrEmpty(UserAgent))
            {
                client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            }

            try
            {
                response = await client.SendAsync(request);
                content = await response.Content.ReadAsStringAsync();

                if (string.IsNullOrEmpty(content))
                {
                }

                if (response.IsSuccessStatusCode && !string.IsNullOrEmpty(content))
                {
                    value = JsonConvert.DeserializeObject<T>(content);
                }
                else if (!response.IsSuccessStatusCode && !string.IsNullOrEmpty(content))
                {
                    error = JsonConvert.DeserializeObject<ErrorResponseMessage>(content);

                    throw new Exception(content);
                }
            }
            catch (TaskCanceledException canceledEx)
            {

            }
            catch (ServiceInvokeException sie)
            {
                throw sie;
            }
            catch (AggregateException ae)
            {
                throw ae;
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                client?.Dispose();
            }

            return value;
        }

    }
}
