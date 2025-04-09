using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CloudBanking.Utilities.UtilEnum;
using Testing_Automation_Request.Models;

namespace Testing_Automation_Request.ServiceLocators
{
    public interface IPosInterfaceClient
    {
        Task<StatusResponse<POSResponse>> TransactionStatus(string sessionId);
        Task<StatusResponse<LogonResponseModel>> Logon(object requestModel, string sessionId);
        Task<StatusResponse<SettlementResponseModel>> Settlement(object requestModel, string sessionId);
        Task<StatusResponse<SettlementResponseModel>> SettlementEnquiry(object requestModel, string sessionId);
        Task<StatusResponse<POSResponse>> Transaction(object model, string sessionId);
        Task<StatusResponse<POSResponse>> TransactionSearch(object model, string sessionId);
        Task<StatusResponse<POSResponse>> CancelTransaction(string sessionId);
        Task<StatusResponse<POSResponse>> CardStatusCheck(object model, string sessionId);
        Task<StatusResponse<StartVNCLikeResponseModel>> StartVNCLikeServer(string sessionId);
        Task<StatusResponse<RePrintResponseModel>> RePrintReceipt(RePrintRequestModel model, string sessionId);
        Task<StatusResponse<RePrintResponseModel>> RePrint(object model, string sessionId);
        Task<StatusResponse<PrintStoredResponseModel>> PrintStoredTransactions(object model, string sessionId);
        Task<StatusResponse<PrintPendingResponseModel>> PrintPendingTransactions(object model, string sessionId);
        Task<StatusResponse<MerchantsResponseModels>> GetMerchantList(string sessionId);
        void Init(string data);
        void CancelRequest();
        POSMediaType MediaType { get; set; }
    }
}
