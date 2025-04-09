using System.Runtime.CompilerServices;
using Testing_Automation_Request.Models;
using Testing_Automation_Request.ServiceLocators;
using Testing_Automation_Request.Services;

const string SESSION = "12345678";

HttpPosInterfaceClient _posInterfaceClient = new HttpPosInterfaceClient(new Uri(string.Format("https://{0}:5643", "192.168.68.150")), string.Empty);

TransactionRequestModel _transactionRequestModel = new TransactionRequestModel()
{
    Merchant = "01",
    TxnType = "P",
    Amount = 10,
    AsyncMode = "1",
};

var transaction = await _posInterfaceClient.Transaction(_transactionRequestModel, SESSION);

Console.WriteLine(transaction.ToString());

//async Task<POSResponse> SyncTransactionStatusAsync()
//{
//    while (true)
//    {
//        var res = await _posInterfaceClient.QueryTransactionAsync
//    }    
//}
