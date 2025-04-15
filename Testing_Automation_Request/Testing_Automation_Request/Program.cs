using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Transactions;
using Testing_Automation_Request.Models;
using Testing_Automation_Request.ServiceLocators;
using Testing_Automation_Request.Services;
using static CloudBanking.Utilities.UtilEnum;

const int SYNC_TRANSACTION_DELAY_MILISECOND = 20;

string logFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
if (!Directory.Exists(logFolderPath))
{
    Directory.CreateDirectory(logFolderPath);  
}

string filePath = Path.Combine(logFolderPath, "log.csv");

File.WriteAllText(filePath, string.Empty);
File.AppendAllText(filePath, "Index,Transaction Type,Initializing Transaction,Waiting for Card,Card Processing,Transaction Processing,Transaction Processed,Transaction Completed,Transaction Result, Total Time\n");

HttpPosInterfaceClient _posInterfaceClient = new HttpPosInterfaceClient(new Uri(string.Format("https://{0}:5643", "192.168.68.163")), string.Empty);
string _sessionId;

string text = "";
string log = "";
Stopwatch stopwatch = Stopwatch.StartNew();

Stopwatch stopWatchStage = new Stopwatch();

List<int> delayTransaction = new List<int>
        {
            10 * 1000,   
            30 * 1000,   
            60 * 1000,   
            5 * 60 * 1000,  
            10 * 60 * 1000, 
            15 * 60 * 1000, 
            30 * 60 * 1000 
        };

int i = 1;
while (true)
{
    text = "Start Do Transaction: " + i;
    log = i + ",";
    Console.WriteLine(text);

    stopwatch.Restart();

    stopWatchStage.Restart();

    _sessionId = Guid.NewGuid().ToString();

    var result = await DoTransaction();

    if (result == null)
    {
        await Task.Delay(10 * 60 * 1000);
        continue;
    }

    stopwatch.Stop();

    var statusResult = (result?.Data?.Response?.Transaction?.Success) == true ? "APPROVAL" : "DECLINED";

    log += statusResult + ",";

    text = statusResult + " : "  + stopwatch.Elapsed.ToString() + "ms";
    log += stopwatch.Elapsed.ToString() + ",";
    Console.WriteLine(text);
    Console.WriteLine(log);
    WriteLog(log);

    text = "\\................................................................./";
    Console.WriteLine(text);

    var delay = delayTransaction[new Random().Next(delayTransaction.Count)];
    Console.WriteLine("Delay Time" + delay);

    await Task.Delay(delay);

    i++;
}

async Task<StatusResponse<TransactionResponseModel>> DoTransaction()
{
    int randomvalue = new Random().Next(2);
    TransactionRequestModel transactionRequestModel = new TransactionRequestModel()
    {
        Merchant = "01",
        TxnType = randomvalue == 0 ? "P" : "PA",
        Amount = new Random().Next(10, 10000),
        AsyncMode = true,
    };

    text = "Transaction Type: " + (randomvalue == 0 ? "Purchase" : "Pre Auth");
    log += (randomvalue == 0 ? "Purchase" : "Pre Auth") + ",";
    Console.WriteLine(text);

    var trans = await _posInterfaceClient.Transaction(transactionRequestModel, _sessionId);

    if (trans?.Data?.Response == null)
        return null;

    return await SyncTransactionStatusAsync();
}

async Task<StatusResponse<TransactionResponseModel>> SyncTransactionStatusAsync()
{
    //ManualResetEventSlim mres = new ManualResetEventSlim(true);

    var oldStatusText = "";
   
    while (true)
    {
        //mres.Wait();
        //mres.Reset();
        var transaction = await _posInterfaceClient.QueryTransaction(_sessionId);

        if (transaction?.Data?.Response == null)
            return null;

        bool isChangeStatus = false;

        if (oldStatusText != transaction.Data.Response.StatusText && (TransactionStage)transaction.Data.Response.Status != TransactionStage.WaitingForCardRemove)
        {
            stopWatchStage.Stop();
            text = transaction.Data.Response.StatusText + ": " + stopWatchStage.ElapsedMilliseconds.ToString() + "ms";
            log += stopWatchStage.ElapsedMilliseconds.ToString() + ",";
            Console.WriteLine(text);
            oldStatusText = transaction.Data.Response.StatusText;
            isChangeStatus = true;
        }

        switch ((TransactionStage)transaction.Data.Response.Status)
        {
            case TransactionStage.TransactionCompleted:
                return transaction;

        }

        //mres.Set();

        await Task.Delay(SYNC_TRANSACTION_DELAY_MILISECOND);

        if (isChangeStatus)
            stopWatchStage.Restart();
    }
}

void WriteLog(string text)
{
    File.AppendAllText(filePath,text + Environment.NewLine);
}
