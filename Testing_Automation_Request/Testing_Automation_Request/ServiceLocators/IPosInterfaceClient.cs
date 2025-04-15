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
        Task<StatusResponse<TransactionResponseModel>> Transaction(object model, string sessionId);
        Task<StatusResponse<TransactionResponseModel>> QueryTransaction(string sessionId);
        void Init(string data);
        void CancelRequest();
        POSMediaType MediaType { get; set; }

        bool UseNumericBoolean { get; set; }
    }
}
