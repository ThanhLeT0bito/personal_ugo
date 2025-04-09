using System;
using System.Net.Http;
using System.Threading.Tasks;
using Testing_Automation_Request.Models;
using Testing_Automation_Request.ServiceLocators;

namespace Testing_Automation_Request.Services
{
    public class HttpPosInterfaceClient : WebApiClient, IPosInterfaceClient
    {
        const string URL_END_POINT_TRANSACTION = "/v1/sessions/{0}/transaction";
        const string URL_END_POINT_START_VNC_LIKE = "/v1/sessions/{0}/startVNCLike";
        const string URL_END_POINT_LOGON = "/v1/sessions/{0}/logon";
        const string URL_END_POINT_TRANSACTION_STATUS = "/v1/sessions/{0}/transaction";
        const string URL_END_POINT_SETTLEMENT = "/v1/sessions/{0}/settlement";
        const string URL_END_POINT_REPRINT_RECEIPT = "/v1/sessions/{0}/reprintreceipt&async=false";
        const string URL_GET_MERCHANT_LIST = "/v1/sessions/{0}/merchantlist";
        const string URL_TRANSACTION_SEARCH = "/v1/sessions/{0}/transactionsearch";
        const string URL_REPRINT = "/v1/sessions/{0}/reprint";
        const string URL_CANCEL_TRANSACTION = "/v1/sessions/{0}/canceltransaction";
        const string URL_CARD_STATUS_CHECK = "/v1/sessions/{0}/querycard";
        const string URL_PRINT_STORED_TRANSACTIONS = "/v1/sessions/{0}/printStoredTransactions";
        const string URL_PRINT_PENDING_TRANSACTIONS = "/v1/sessions/{0}/printpendingtransaction";

        string _token = string.Empty;


        public HttpPosInterfaceClient()
        {

        }

        public HttpPosInterfaceClient(Uri baseurl, string useragent) : base(baseurl, useragent)
        {

        }

        public void Init(string ip)
        {
            if (!string.IsNullOrEmpty(ip))
            {
                Uri uri = new Uri(string.Format("https://{0}:5643", ip));
                base.Init(uri, string.Empty);
            }
        }


        public void SetToken(string token)
        {
            _token = token;
        }

        public async Task<StatusResponse<POSResponse>> TransactionStatus(string sessionId)
        {
            var request = new HttpRequesMessageBuilder()
               .SetBaseUrl(BaseUrl.ToString())
               .SetApiEndpoint(string.Format(URL_END_POINT_TRANSACTION_STATUS, sessionId))
               .SetMethod(HttpMethod.Get)
               .Build();

            return await Request<POSResponse>(request);
        }

        public async Task<StatusResponse<LogonResponseModel>> Logon(object requestModel, string sessionId)
        {
            var request = new HttpRequesMessageBuilder()
               .SetBaseUrl(BaseUrl.ToString())
               .SetHttpContent(requestModel, HttpContentType.StringContent, _mediaType)
               .SetApiEndpoint(string.Format(URL_END_POINT_LOGON, sessionId))
               .SetMethod(HttpMethod.Post)
               .Build();

            return await Request<LogonResponseModel>(request);
        }

        public async Task<StatusResponse<SettlementResponseModel>> Settlement(object requestModel, string sessionId)
        {
            var request = new HttpRequesMessageBuilder()
               .SetBaseUrl(BaseUrl.ToString())
               .SetHttpContent(requestModel, HttpContentType.StringContent, _mediaType)
               .SetApiEndpoint(string.Format(URL_END_POINT_SETTLEMENT, sessionId))
               .SetMethod(HttpMethod.Post)
               .Build();

            return await Request<SettlementResponseModel>(request);
        }

        public async Task<StatusResponse<SettlementResponseModel>> SettlementEnquiry(object requestModel, string sessionId)
        {
            var request = new HttpRequesMessageBuilder()
              .SetBaseUrl(BaseUrl.ToString())
              .SetHttpContent(requestModel, HttpContentType.StringContent, _mediaType)
              .SetApiEndpoint(string.Format(URL_END_POINT_SETTLEMENT, sessionId))
              .SetMethod(HttpMethod.Post)
              .Build();

            return await Request<SettlementResponseModel>(request);
        }

        public async Task<StatusResponse<StartVNCLikeResponseModel>> StartVNCLikeServer(string sessionId)
        {
            var request = new HttpRequesMessageBuilder()
                .SetBaseUrl(BaseUrl.ToString())
                .SetApiEndpoint(string.Format(URL_END_POINT_START_VNC_LIKE, sessionId))
                .SetMethod(HttpMethod.Post)
                .Build();

            return await Request<StartVNCLikeResponseModel>(request);
        }

        public async Task<StatusResponse<POSResponse>> Transaction(object model, string sessionId)
        {
            var request = new HttpRequesMessageBuilder()
                .SetBaseUrl(BaseUrl.ToString())
                .SetApiEndpoint(string.Format(URL_END_POINT_TRANSACTION, sessionId))
                .SetMethod(HttpMethod.Post)
                .SetHttpContent(model, HttpContentType.StringContent, _mediaType)
                .Build();

            return await Request<POSResponse>(request);
        }

        public async Task<StatusResponse<POSResponse>> TransactionSearch(object model, string sessionId)
        {
            var request = new HttpRequesMessageBuilder()
                .SetBaseUrl(BaseUrl.ToString())
                .SetApiEndpoint(string.Format(URL_TRANSACTION_SEARCH, sessionId))
                .SetMethod(HttpMethod.Post)
                .SetHttpContent(model, HttpContentType.StringContent, _mediaType)
                .Build();

            return await Request<POSResponse>(request);
        }

        public async Task<StatusResponse<RePrintResponseModel>> RePrintReceipt(RePrintRequestModel model, string sessionId)
        {
            var request = new HttpRequesMessageBuilder()
                .SetBaseUrl(BaseUrl.ToString())
                .SetApiEndpoint(string.Format(URL_REPRINT, sessionId))
                .SetMethod(HttpMethod.Post)
                .SetHttpContent(model, HttpContentType.StringContent, _mediaType)
                .Build();

            return await Request<RePrintResponseModel>(request);
        }

        public async Task<StatusResponse<RePrintResponseModel>> RePrint(object requestModel, string sessionId)
        {
            var request = new HttpRequesMessageBuilder()
                .SetBaseUrl(BaseUrl.ToString())
                .SetApiEndpoint(string.Format(URL_REPRINT, sessionId))
                .SetMethod(HttpMethod.Post)
                .SetHttpContent(requestModel, HttpContentType.StringContent, _mediaType)
                .Build();

            return await Request<RePrintResponseModel>(request);
        }

        public async Task<StatusResponse<MerchantsResponseModels>> GetMerchantList(string sessionId)
        {
            var request = new HttpRequesMessageBuilder()
               .SetBaseUrl(BaseUrl.ToString())
               .SetApiEndpoint(string.Format(URL_GET_MERCHANT_LIST,sessionId))
               .SetMethod(HttpMethod.Post)
               .SetHttpContent(string.Empty, HttpContentType.StringContent, _mediaType)
               .Build();

            return await Request<MerchantsResponseModels>(request, HttpRequesMessageBuilder.JSON);
        }

        public async Task<StatusResponse<POSResponse>> CancelTransaction(string sessionId)
        {
            var request = new HttpRequesMessageBuilder()
               .SetBaseUrl(BaseUrl.ToString())
               .SetApiEndpoint(string.Format(URL_CANCEL_TRANSACTION, sessionId))
               .SetMethod(HttpMethod.Post)
               .SetHttpContent(string.Empty, HttpContentType.StringContent, _mediaType)
               .Build();

            return await Request<POSResponse>(request, HttpRequesMessageBuilder.JSON);
        }

        public async Task<StatusResponse<POSResponse>> CardStatusCheck(object model, string sessionId)
        {
            var request = new HttpRequesMessageBuilder()
                .SetBaseUrl(BaseUrl.ToString())
                .SetApiEndpoint(string.Format(URL_CARD_STATUS_CHECK, sessionId))
                .SetMethod(HttpMethod.Post)
                .SetHttpContent(model, HttpContentType.StringContent, _mediaType)
                .Build();

            return await Request<POSResponse>(request);
        }
        public async Task<StatusResponse<PrintStoredResponseModel>> PrintStoredTransactions(object model, string sessionId)
        {
            var request = new HttpRequesMessageBuilder()
                .SetBaseUrl(BaseUrl.ToString())
                .SetApiEndpoint(string.Format(URL_PRINT_STORED_TRANSACTIONS, sessionId))
                .SetMethod(HttpMethod.Post)
                .SetHttpContent(model, HttpContentType.StringContent, _mediaType)
                .Build();

            return await Request<PrintStoredResponseModel>(request);
        }
        public async Task<StatusResponse<PrintPendingResponseModel>> PrintPendingTransactions(object model, string sessionId)
        {
            var request = new HttpRequesMessageBuilder()
                .SetBaseUrl(BaseUrl.ToString())
                .SetApiEndpoint(string.Format(URL_PRINT_PENDING_TRANSACTIONS, sessionId))
                .SetMethod(HttpMethod.Post)
                .SetHttpContent(model, HttpContentType.StringContent, _mediaType)
                .Build();

            return await Request<PrintPendingResponseModel>(request);
        }

        public void CancelRequest()
        {
            _cts?.Cancel();
        }
    }
}
