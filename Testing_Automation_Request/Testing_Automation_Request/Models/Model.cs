using CloudBanking.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using System.Xml;
using System.Xml.Serialization;
using CloudBanking.HttpPosInterfaceClient;
using CloudBanking.ApiLocators.Models;

namespace CloudBanking.HttpPosInterfaceClient
{
    [XmlRoot(ElementName = "Request", Namespace = "")]
    public class TransactionRequestModel : BaseObject   
    {
        public string Merchant { get; set; } = "00";

        [JsonIgnore]
        [XmlIgnore]
        public string ReceiptAutoPrint { get; set; } = "0";

        [JsonIgnore]
        [XmlIgnore]
        public string CutReceipt { get; set; } = "0";

        [JsonIgnore]
        [XmlIgnore]
        public string SettlementType { get; set; }

        public string TxnType { get; set; } // "P"

        [LongNumber]
        public double AmtTip { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string FuncType { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string Type { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string ReprintType { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string OriginalTxnRef { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string CurrencyCode { get; set; } // length 3: AUD

        [JsonIgnore]
        [XmlIgnore]
        public string OriginalTxnType { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string Date { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string Time { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public bool TrainingMode { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string Application { get; set; }

        public string EnableTip { get; set; }

        [LongNumber]
        public double AmtCash { get; set; } //cash amount

        [LongNumber]
        public double Amount { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string AuthCode { get; set; }

        public string TxnRef { get; set; } //reference

        [JsonIgnore]
        [XmlIgnore]
        public string PanSource { get; set; }

        public string Pan { get; set; }

        public string DateExpiry { get; set; } //MMYY

        [JsonIgnore]
        [XmlIgnore]
        public string Track2 { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string AccountType { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public long ExAmount { get; set; }

        public string lszSTAN { get; set; }

        public string lszApprovalCode { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string Last4Digits { get; set; }

        public string CVV { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string QueryCardType { get; set; }

        public string CardName { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public uint PID { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string RRN { get; set; } //use for refund

        [JsonIgnore]
        [XmlIgnore]
        [XmlElement("Basket")]
        public BasketModel Basket { get; set; }

        public bool FromSameDevice { get; set; }

        public TransactionRequestModel()
        {
            EnableTip = "0";
            Basket = new BasketModel();
        }
    }

    public class BasketModel
    {
        [XmlElement(ElementName = "id")]
        public string id { get; set; }

        public long amt { get; set; }

        public long tax { get; set; }

        public long dis { get; set; }

        public long sur { get; set; }

        [XmlElement(ElementName = "items")]
        public BasketItems Basket { get; set; }
    }

    [XmlRoot(ElementName = "items")]
    public class BasketItems
    {
        [XmlElement(ElementName = "item")]
        public List<BasketItem> items { get; set; }
    }

    [XmlRoot(ElementName = "item")]
    public class BasketItem
    {
        public string id { get; set; }

        public string sku { get; set; }

        public Int32 qty { get; set; }

        public UInt64 amt { get; set; }
        public UInt64 tax { get; set; }
        public UInt64 dis { get; set; }

        public string ean { get; set; }

        public string upc { get; set; }

        public string gtin { get; set; }

        public string name { get; set; }// 24

        public string desc { get; set; }// 255

        public string srl { get; set; }

        public string img { get; set; }

        public string link { get; set; }

        public string tag { get; set; }// 64
    };

    public class Receipt
    {
        public string Type { get; set; }
        public List<string> ReceiptText { get; set; }
        public bool IsPrePrint { get; set; }
    }

    [XmlRoot(ElementName = "Transaction")]
    public class Transaction
    {
        public string TxnType { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string Merchant { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string CardType { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string CardName { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string RRN { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string DateSettlement { get; set; }

        [LongNumber]
        public double AmtCash { get; set; }
        [JsonIgnore]
        [XmlIgnore]
        [LongNumber]
        public double AmtPurchase { get; set; }
        [LongNumber]
        public double Amount { get; set; }
        [LongNumber]
        public double AmtTip { get; set; }
        public string AuthCode { get; set; }
        public string TxnRef { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string Pan { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string DateExpiry { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string Track2 { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string AccountType { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public bool BalanceReceived { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public int AvailableBalance { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public int ClearedFundsBalance { get; set; }
        public string Success { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string ResponseCode { get; set; }
        public string ResponseText { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string IsTrack1Available { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string IsTrack2Available { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string IsTrack3Available { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string Date { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string Catid { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string Caid { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public int Stan { get; set; }

        public uint PID { get; set; }
        public uint Modify { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public IList<Receipt> Receipts { get; set; }

        public Transaction()
        {
            Receipts = new List<Receipt>();
        }
    }


    [PosResponse]
    public class StartVNCLikeResponseModel : POSResponse<StartVNCLikeResponseResponse>
    {

    }

    public class StartVNCLikeResponseResponse
    {
        public string Result { get; set; }

        public string Error { get; set; }
    }

    [PosResponse]
    [XmlRoot(ElementName = "PosResponse")]
    public class Response : POSResponse<Transaction>
    {
        public Response()
        {
            Response = new Transaction();
        }
    }

    [PosResponse]
    public class TransactionResponseModels : POSResponse<TransactionResponses>
    {
        public TransactionResponseModels()
        {
            Response = new TransactionResponses();
        }
    }
    public class TransactionResponses
    {
        public List<Transaction> Transactions { get; set; }
    }

    [XmlRoot(ElementName = "Request", Namespace = "")]
    public class LogonRequestModel
    {
        public const string LOGON_TYPE_STANDARD = " ";
        public const string LOGON_TYPE_RSA = "4";
        public const string LOGON_TYPE_TMS = "5";
        public const string LOGON_TYPE_TMSPARAMS = "6";
        public const string LOGON_TYPE_TMSSOFTWARE = "7";
        public const string LOGON_TYPE_LOG_OFF = "8";
        public const string LOGON_TYPE_DIAGNOSTICS = "9";

        public string Merchant { get; set; } = "00";

        [JsonIgnore]
        [XmlIgnore]
        public string LogonType { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string Application { get; set; } = "00";

        [JsonIgnore]
        [XmlIgnore]
        public string ReceiptAutoPrint { get; set; } = "0";

        [JsonIgnore]
        [XmlIgnore]
        public string CutReceipt { get; set; } = "0";

        public bool FromSameDevice { get; set; }

        public LogonRequestModel()
        {
        }
    }

    [PosResponse]
    public class LogonResponseModel : POSResponse<LogonResponse>
    {
        public LogonResponseModel()
        {
            Response = new LogonResponse();
        }
    }

    public class LogonResponse
    {
        [JsonIgnore]
        [XmlIgnore]
        public string PinPadVersion { get; set; }
        public string Success { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string ResponseCode { get; set; }
        public string ResponseText { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public DateTime Date { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string Catid { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string Caid { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public int Stan { get; set; }

        public LogonResponse()
        {
        }
    }

    [XmlRoot(ElementName = "Request", Namespace = "")]
    public class SettlementRequestModel
    {
        public const string SETTLEMENT_TYPE_SETTLEMENT = "S";
        public const string SETTLEMENT_TYPE_PRE_SETTLEMENT = "P";
        public const string SETTLEMENT_TYPE_LAST_SETTLEMENT = "L";

        /// <summary>
        /// Sub Totals or Summary Totals for ANZ
        /// </summary>
        public const string SETTLEMENT_TYPE_SUB_OR_SUMMARY_FOR_ANZ = "U";

        /// <summary>
        /// Shift Totals or Subtotals for ANZ
        /// </summary>
        public const string SETTLEMENT_TYPE_SHIFT_OR_SUB_TOTALS_FOR_ANZ = "H";

        /// <summary>
        /// Txn Listing
        /// </summary>
        public const string SETTLEMENT_TYPE_TXN_LISTING = "I";

        /// <summary>
        /// Start Cash
        /// </summary>
        public const string SETTLEMENT_TYPE_START_CASH = "M";


        /// <summary>
        /// Store and forward (SAF) totals report
        /// </summary>
        public const string SETTLEMENT_TYPE_STORE_AND_FORWARD_TOTAL_REPORT = "F";

        /// <summary>
        /// Daily cash statement (STG Agency)
        /// </summary>
        public const string SETTLEMENT_TYPE_DAILY_CASH_STATEMENT = "D";

        public string Merchant { get; set; } = "00";

        public string SettlementType { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string Application { get; set; } = "00";

        [JsonIgnore]
        [XmlIgnore]
        public string ReceiptAutoPrint { get; set; } = "0";

        [JsonIgnore]
        [XmlIgnore]
        public string CutReceipt { get; set; } = "0";

        [JsonIgnore]
        [XmlIgnore]
        public string ResetTotals { get; set; } = "0";
        public string SettlementDate { get; set; }

        public bool FromSameDevice { get; set; }
    }


    public class SettlementResponse
    {
        public string Merchant { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public List<SettlementTotalsDatum> SettlementTotalsData { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public SettlementReport SettlementReport { get; set; }
        public string Success { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string ResponseCode { get; set; }
        public string ResponseText { get; set; }
    }

    [PosResponse]
    public class SettlementResponseModel : POSResponse<SettlementResponse>
    {
        public SettlementResponse Response { get; set; }
    }

    public class SettlementReport
    {
        public string IsPreprint { get; set; }
        public List<string> ReceiptText { get; set; }
        public string Type { get; set; }
    }

    public class SettlementTotalsDatum
    {
        public string CardName { get; set; }
        public int PurchaseAmount { get; set; }
        public int PurchaseCount { get; set; }
        public int CashOutAmount { get; set; }
        public int CashOutCount { get; set; }
        public int RefundAmount { get; set; }
        public int RefundCount { get; set; }
        public int TotalAmount { get; set; }
        public int TotalCount { get; set; }
    }


    [XmlRoot("Request", Namespace = "")]
    public class RePrintRequestModel
    {
        public const string REPRINT_TYPE_MERCHANT_COPY = "M";
        public const string REPRINT_TYPE_CUSTOMER_COPY = "C";

        public string Merchant { get; set; } = "00";

        [JsonIgnore]
        [XmlIgnore]
        public string Application { get; set; } = "00";

        [JsonIgnore]
        [XmlIgnore]
        public string ReceiptAutoPrint { get; set; } = "0";

        [JsonIgnore]
        [XmlIgnore]
        public string CutReceipt { get; set; } = "0";

        [JsonIgnore]
        [XmlIgnore]
        public string ReprintType { get; set; } = "2";

        [JsonIgnore]
        [XmlIgnore]
        public string OriginalTxnRef { get; set; }

        public uint PID { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string Type { get; set; }

        public bool FromSameDevice { get; set; }
    }

    public class RePrintResponse
    {
        public string Merchant { get; set; }
        public string Success { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string ResponseCode { get; set; }
        public string ResponseText { get; set; }

        public string DigitalSig { get; set; }

        public RePrintResponse()
        {
        }
    }

    [PosResponse]
    public class RePrintResponseModel : POSResponse<RePrintResponse>
    {
        public RePrintResponseModel()
        {
            Response = new RePrintResponse();
        }
    }

    public class Merchant
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class MerchantsResponse
    {
        public IList<Merchant> Merchants { get; set; }
    }


    [PosResponse]
    public class MerchantsResponseModels : POSResponse<MerchantsResponse>
    {
        public MerchantsResponseModels()
        {
            Response = new MerchantsResponse();
        }
    }

    [XmlRoot(ElementName = "Request", Namespace = "")]
    public class PrintStoreRequestModel
    {
        public string Merchant { get; set; }

        public bool FromSameDevice { get; set; }

        public PrintStoreRequestModel()
        {

        }
    }

    public class PrintStoredResponse
    {
        public int IdProcessor { get; set; }
        public int EOVCount { get; set; }
        public int EOVTotal { get; set; }
        public int CTLSCount { get; set; }
        public int CTLSTotal { get; set; }
        public int ContactCount { get; set; }
        public int ContactTotal { get; set; }
        public int CurrencyCode { get; set; }
        public int CountryCode { get; set; }
    }

    [PosResponse]
    public class PrintStoredResponseModel : POSResponse<PrintStoredResponse>
    {
        public PrintStoredResponseModel()
        {
            Response = new PrintStoredResponse();
        }
    }

    [XmlRoot(ElementName = "Request", Namespace = "")]
    public class PrintPendingRequestModel
    {
        public string Merchant { get; set; }

        public bool FromSameDevice { get; set; }
    }

    [XmlRoot(ElementName = "Response")]
    public class PrintPendingResponse
    {
        public int PendingCount { get; set; }
        public int OutstandingCount { get; set; }
    }

    [PosResponse]
    public class PrintPendingResponseModel : POSResponse<PrintPendingResponse>
    {
        public PrintPendingResponseModel()
        {
            Response = new PrintPendingResponse();
        }
    }


    [XmlRoot(ElementName = "Request", Namespace = "")]
    public class SearchTransactionRequestModel
    {
        public string Merchant { get; set; }
        public string FuncType { get; set; }
        public string TxnRef { get; set; }
        public string AuthCode { get; set; }
        [LongNumber]
        public double Amount { get; set; }
        public long ExAmount { get; set; }
        public string Stan { get; set; }
        public string Last4Digits { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }

    public class SearchTransactionResponse
    {
        [XmlElement(ElementName = "TxnRef")]
        public string TxnRef { get; set; }

        [XmlElement(ElementName = "AuthCode")]
        public string AuthCode { get; set; }

        [XmlElement(ElementName = "Date")]
        public string Date { get; set; }

        [XmlElement(ElementName = "Time")]
        public string Time { get; set; }

        [XmlElement(ElementName = "lszApprovalCode")]
        public string lszApprovalCode { get; set; }

        [XmlElement(ElementName = "lszSTAN")]
        public string lszSTAN { get; set; }

        [XmlElement(ElementName = "lAmount")]
        [LongNumber]
        public double lAmount { get; set; }

        [XmlElement(ElementName = "PID")]
        public uint PID { get; set; }
    }

    public class Transactions
    {
        [XmlElement("Transaction")]
        public List<SearchTransactionResponse> Transaction { get; set; }
    }

    public class SearchResponse
    {
        [JsonIgnore]
        [XmlElement(ElementName = "Transactions")]
        public Transactions XmlTransaction { get; set; }

        [JsonProperty("Transactions")]
        [XmlIgnore]
        public List<SearchTransactionResponse> JsonTransactions { get; set; }


        [JsonIgnore]
        [XmlIgnore]
        public IList<SearchTransactionResponse> Transactions
        {
            get
            {
                if (XmlTransaction != null && XmlTransaction.Transaction != null)
                {
                    return XmlTransaction.Transaction;
                }

                return JsonTransactions;
            }
        }
    }

    [PosResponse]
    public class SearchTransactionResponseModel : POSResponse<SearchResponse>
    {
        public SearchTransactionResponseModel()
        {
            Response = new SearchResponse();
        }
    }


}
