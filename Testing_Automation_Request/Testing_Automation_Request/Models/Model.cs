using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;
using System.Xml;
using System.Xml.Serialization;

namespace Testing_Automation_Request.Models
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

        public long AmtTip { get; set; }

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

        public long AmtCash { get; set; } //cash amount

        public long Amount { get; set; }

        public long SearchAmount { get; set; }

        public string lszApprovalCode { get; set; }

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

        public bool AsyncMode { get; set; }

        public bool WithReceiptImageData { get; set; }

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


    public class TransactionResponse
    {
        public uint PID { get; set; }
        public string Merchant { get; set; }
        public string TxnType { get; set; }
        public bool Success { get; set; }
        public string ResponseText { get; set; }
        public int IdShift { get; set; }
        public int IdShiftName { get; set; }
        public long lAmount { get; set; }
        public long lTipAmount { get; set; }
        public long lCashOut { get; set; }
        public long lCashOutFee { get; set; }
        public long lSurChargeTax { get; set; }
        public long lDiscount { get; set; }
        public long lDiscountPercent { get; set; }
        public int iReward { get; set; }
        public long lAuthorizedTotal { get; set; }
        public long lDonationAmount { get; set; }
        public int iEntryMode { get; set; }
        public int iAccountType { get; set; }
        public int iCurrencyCode { get; set; }
        public int fsModify { get; set; }
        public int iPaymentType { get; set; }
        public int iCustomerLanguage { get; set; }
        public int fClosed { get; set; }
        public int fBatchError { get; set; }
        public int fVoided { get; set; }
        public bool fRefunded { get; set; }
        public bool fAuthorized { get; set; }
        public bool fSignature { get; set; }
        public bool fCardHolderStatesCVVnotOnCard { get; set; }
        public bool fCardNotPresent { get; set; }
        public bool fCardNotPresentMail { get; set; }
        public bool fCardNotPresentPhone { get; set; }
        public bool fCanAdjust { get; set; }
        public bool fCanVoid { get; set; }
        public bool fCanPreAuthComplete { get; set; }
        public bool fCanVoidPreAuthComplete { get; set; }
        public bool fCanGoOffline { get; set; }
        public string lszCustomerReference { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string szAuthorizationResponseCode { get; set; }
        public string szReferenceNumber { get; set; }
        public string lszSTAN { get; set; }
        public string lszEndCardNumber { get; set; }
        public int iReversalReason { get; set; }
        public string lszCardLogo { get; set; }
        public string szQRCode { get; set; }
        public int byPinType { get; set; }
        public int byPinStatus { get; set; }
        public int iNoCVVOption { get; set; }
        public int byCDCVM { get; set; }
        public string szMaskedCardNumber { get; set; }
        public string lszApprovalCode { get; set; }
        public string ReceiptData { get; set; }
        public string ReceiptLogo { get; set; }
        public string DigitalSig { get; set; }
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
    public class TransactionResponseModel : POSResponse<TransactionModel>
    {
        public TransactionResponseModel()
        {
            Response = new TransactionModel();
        }
    }

    public class TransactionModel
    {
        public int Status { get; set; }

        public string StatusText { get; set; }

        public TransactionResponse Transaction { get; set; }

        public TransactionModel()
        {
            Transaction = new TransactionResponse();
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
        public List<TransactionResponse> Transactions { get; set; }

        public TransactionResponses()
        {
            Transactions = new List<TransactionResponse>();
        }
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
        public bool Success { get; set; }

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

    [PosResponse]
    public class CardResponseModel : POSResponse<CardTransaction>
    {
        public CardResponseModel()
        {
            Response = new CardTransaction();
        }
    }

    public class CardTransaction
    {
        public bool Success { get; set; }

        public string ResponseText { get; set; }

        public string AccountType { get; set; }

        public string CardName { get; set; }

        public string Pan { get; set; }

        public int CardType { get; set; }

        public int EntryMode { get; set; }

        public string AuthCode { get; set; }
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
        public bool Success { get; set; }

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
    }

    public class RePrintResponse
    {
        public string Merchant { get; set; }
        public bool Success { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string ResponseCode { get; set; }
        public string ResponseText { get; set; }
        public string ReceiptData { get; set; }
        public string ReceiptLogo { get; set; }

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
        public int UnattendedMode { get; set; } = -1;
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
        public long Amount { get; set; }
        public long SearchAmount { get; set; }
        public long ExAmount { get; set; }
        public string Stan { get; set; }
        public string Last4Digits { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string AuthId { get; set; }
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

        [XmlElement(ElementName = "Amount")]
        public long Amount { get; set; }

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

    public class GetTokenRequestModel
    {
        public string AppKey { get; set; }

        public string AppSecret { get; set; }

        public string TerminalSerialNumber { get; set; }
    }

    public class GetTokenResponseModel
    {
        public string Token { get; set; }

    }

    public class RemoteKeyInjectionRequestModel
    {
        public string Merchant { get; set; } = "00";

        public string ResellerLogonId { get; set; }

        public string ResellerLogonPasscode { get; set; }
    }

    public class RemoteKeyInjectionResponse
    {
        public string Merchant { get; set; }

        public bool Success { get; set; }

        public string ResponseText { get; set; }

        public string ResponseCode { get; set; }
    }

    [PosResponse]
    public class RemoteKeyInjectionResponseModel : POSResponse<RemoteKeyInjectionResponse>
    {
        public RemoteKeyInjectionResponseModel()
        {
            Response = new RemoteKeyInjectionResponse();
        }
    }

    public class BringAppToForegroundResponse
    {
        public bool Success { get; set; }

        public string ResponseText { get; set; }
    }

    [PosResponse]
    public class BringAppToForegroundResponseModel : POSResponse<BringAppToForegroundResponse>
    {
        public BringAppToForegroundResponseModel()
        {
            Response = new BringAppToForegroundResponse();
        }
    }

    public class TransmitOfflineRequestModel
    {
        public string Merchant { get; set; }
    }

    public class TransmitOfflineResponse
    {
        public bool Success { get; set; }

        [JsonIgnore]
        [XmlIgnore]
        public string ResponseCode { get; set; }

        public string ResponseText { get; set; }

        public string TransmissionDate { get; set; }

        public string TransmissionTime { get; set; }
    }

    [PosResponse]
    public class TransmitOfflineResponseModel : POSResponse<TransmitOfflineResponse>
    {
        public TransmitOfflineResponseModel()
        {
            Response = new TransmitOfflineResponse();
        }
    }
}
