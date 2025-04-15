using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using CloudBanking.Utilities;
using CloudBanking.Utilities.CustomFormat;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Testing_Automation_Request.Models
{
    public enum HttpContentType
    {
        StringContent,
        FormUrlEncodedContent,
        ByteArrayContent,
        MultipartForm,
        StreamContent,
        JsonObject
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LongNumberAttribute : Attribute
    {
    }

    public class HttpRequesMessageBuilder
    {
        public const string JSON = "application/json";
        public const string URLENCODED = "application/x-www-form-urlencoded";
        public const string TEXTPLAIN = "text/plain";
        public const string OCTETSTREAM = "octet-stream";
        public const string XML = "application/xml";
        public const string TEXTXML = "text/xml";

        private HttpMethod Method = HttpMethod.Get;

        private string BaseUrl;
        private string ApiEndpoint;
        private List<KeyValuePair<string, IEnumerable<string>>> Headers;
        private List<KeyValuePair<string, string>> Parameters;
        private HttpContent Content = null;
        public HttpRequesMessageBuilder SetBaseUrl(string baseurl)
        {
            this.BaseUrl = baseurl;
            return this;
        }

        public HttpRequesMessageBuilder SetHttpContent(object content, HttpContentType httpContentType = HttpContentType.StringContent, string mediaType = JSON, bool useNumericBoolean = false)
        {
            switch (httpContentType)
            {
                case HttpContentType.StringContent:

                    var stringContent = "";

                    if (mediaType.Contains(URLENCODED))
                        stringContent = "data:" + WebUtility.UrlEncode(content.ToString());
                    else
                    {
                        if (content != null)
                        {
                            if (content is string)
                            {
                                stringContent = (string)content;
                            }
                            else
                            {
                                IList<string> longFieldNames = new List<string>();

                                foreach (var property in content.GetType().GetProperties())
                                {
                                    var att = property.GetCustomAttribute<LongNumberAttribute>();

                                    if (att == null)
                                        continue;

                                    longFieldNames.Add(property.Name);
                                    property.SetValue(content, ((double)property.GetValue(content)) * 100);
                                }

                                if (mediaType == JSON)
                                {
                                    stringContent = CustomJSONSerializer.Serialize(content, useNumericBoolean);

                                    if (longFieldNames.Any())
                                    {
                                        JObject json = JObject.Parse(stringContent);

                                        foreach (var longFied in longFieldNames)
                                        {
                                            if (json.TryGetValue(longFied, out JToken nameToken))
                                            {
                                                var val = json.Value<string>(longFied);

                                                json[longFied] = long.Parse(val);
                                            }
                                        }

                                        stringContent = json.ToString();
                                    }
                                }
                                else
                                {
                                    stringContent = CustomXMLSerializer.Serialize(content, true);

                                    if (longFieldNames.Any())
                                    {
                                        XmlDocument doc = new XmlDocument();
                                        doc.LoadXml(stringContent);

                                        foreach (var field in longFieldNames)
                                        {
                                            XmlNode node = doc.SelectSingleNode($"/Request/{field}");

                                            if (node != null)
                                            {
                                                // Change the value of the node
                                                node.InnerText = long.Parse(node.InnerText).ToString();
                                            }
                                        }

                                        stringContent = doc.OuterXml;
                                    }
                                }
                            }
                        }
                    }

                    Content = string.IsNullOrEmpty(mediaType) ? new StringContent(stringContent, Encoding.UTF8) : new StringContent(stringContent, Encoding.UTF8, mediaType);

                    break;
                case HttpContentType.FormUrlEncodedContent:
                    var dictionary = (Dictionary<string, string>)content;
                    Content = new FormUrlEncodedContent(dictionary);
                    break;
                case HttpContentType.StreamContent:
                    Content = (StreamContent)content;
                    break;

                case HttpContentType.JsonObject:
                    string jsonObject = JsonConvert.SerializeObject(content);
                    Content = new StringContent(jsonObject, Encoding.UTF8, mediaType);
                    break;
            }
            return this;
        }

        public HttpRequesMessageBuilder SetApiEndpoint(string apiendpoint)
        {
            this.ApiEndpoint = apiendpoint;
            return this;
        }

        public HttpRequesMessageBuilder SetMethod(HttpMethod method)
        {
            this.Method = method;
            return this;
        }

        public HttpRequesMessageBuilder AddHeader(string key, string value)
        {
            if (Headers == null)
            {
                Headers = new List<KeyValuePair<string, IEnumerable<string>>>();
            }

            if (!Headers.Any(kvp => kvp.Key.Equals(key)))
            {
                var values = new List<string>();
                var kvp = new KeyValuePair<string, IEnumerable<string>>(key, values);
                Headers.Add(kvp);
            }

            (Headers.First(kvp => kvp.Key.Equals(key)).Value as List<string>).Add(value);

            return this;
        }

        public HttpRequesMessageBuilder AddParameter(string key, string value)
        {
            if (Parameters == null)
            {
                Parameters = new List<KeyValuePair<string, string>>();
            }

            Parameters.Add(new KeyValuePair<string, string>(key, value));

            return this;
        }

        public HttpRequesMessageBuilder AddNonce()
        {
            string nonce = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 15);
            return this.AddParameter("nonce", nonce);
        }

        public HttpRequesMessageBuilder AddTimestamp()
        {
            string timestamp = "" + DateTimeUtils.UnixTime();
            return this.AddParameter("timestamp", timestamp);
        }

        private UriBuilder GetRequestUriBuilder()
        {
            var tmpUri = new Uri(BaseUrl);
            var builder = new UriBuilder(tmpUri);
            builder.Scheme = tmpUri.Scheme;
            builder.Host = tmpUri.Host;
            builder.Port = tmpUri.Port;
            if (!string.IsNullOrEmpty(ApiEndpoint))
                builder.Path = ApiEndpoint;

            return builder;
        }

        /* Not use so far
        public HttpRequesMessageBuilder SignRequest(string key)
        {
            var uri = GetRequestUriBuilder().Uri;
            string baseurl = (string.IsNullOrEmpty(uri.Query)) ? uri.OriginalString : uri.OriginalString.Replace(uri.Query, string.Empty);

            var stringify = HttpRequestMessageUtils.StringifyRequest(
                    Method.ToString().ToUpper(),
                    baseurl,
                    Headers,
                    Parameters,
                    await Content.ReadAsStringAsync());

            var signature = CryptoUtils.CalculateHmacSha1Signature(key, stringify);
            return this.AddParameter("signature", signature);
        }*/

        public HttpRequestMessage Build()
        {
            var builder = GetRequestUriBuilder();

            if (Parameters != null && Parameters.Any())
            {
                var sb = new StringBuilder();
                foreach (var p in Parameters.OrderBy(kvp => kvp.Key))
                {
                    if (sb.Length != 0)
                    {
                        sb.Append("&");
                    }

                    sb.Append(p.Key);
                    sb.Append("=");
                    sb.Append(p.Value);
                }
                builder.Query = sb.ToString();
            }

            var uri = builder.Uri;

            var request = new HttpRequestMessage(this.Method, uri);
            if (Headers != null)
            {
                foreach (var header in Headers)
                {
                    foreach (var headervalue in header.Value)
                    {
                        request.Headers.TryAddWithoutValidation(header.Key, headervalue);
                    }
                }
            }

            if (Content != null)
                request.Content = Content;

            return request;
        }
    }
}
