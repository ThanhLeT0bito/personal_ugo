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
        const string URL_QUERY_TRANSACTION = "/v1/sessions/{0}/querytransaction";
        const string URL_REMOTE_KEY_INJECTION = "/v1/sessions/{0}/remotekeyinjection";
        const string URL_BRING_APP_TO_FOREGROUND = "/v1/sessions/{0}/bringapptoforeground";
        const string URL_OFFLINE_TRANMISSION = "/v1/sessions/{0}/transmitofflinetrans";

        string _token = string.Empty;

        protected override string CAFileName => "ca.pem";

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

        public async Task<StatusResponse<TransactionResponseModel>> Transaction(object model, string sessionId)
        {
            var request = new HttpRequesMessageBuilder()
                .SetBaseUrl(BaseUrl.ToString())
                .SetApiEndpoint(string.Format(URL_END_POINT_TRANSACTION, sessionId))
                .SetMethod(HttpMethod.Post)
                .SetHttpContent(model, HttpContentType.StringContent, _mediaType, UseNumericBoolean)
                .Build();

            return await Request<TransactionResponseModel>(request);
        }

        public async Task<StatusResponse<TransactionResponseModel>> QueryTransaction(string sessionId)
        {
            var request = new HttpRequesMessageBuilder()
                .SetBaseUrl(BaseUrl.ToString())
                .SetApiEndpoint(string.Format(URL_QUERY_TRANSACTION, sessionId))
                .SetMethod(HttpMethod.Get)
                .Build();

            return await Request<TransactionResponseModel>(request);
        }

        public void CancelRequest()
        {
            _cts?.Cancel();
        }
    }
}
