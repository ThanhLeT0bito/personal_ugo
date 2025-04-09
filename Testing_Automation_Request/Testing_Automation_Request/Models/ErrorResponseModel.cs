using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Testing_Automation_Request.Models
{
    [PosResponse]
    public class ErrorResponseModel : POSResponse<ErrorResponse>
    {
        public ErrorResponseModel()
        {
            Response = new ErrorResponse();
        }
    }

    public class ErrorResponse
    {
        public string ErrMessage { get; set; }
        public string StatusCode { get; set; }
    }

    [XmlRoot(ElementName = "POSResponse")]
    [PosResponse]
    public class POSResponse<T>
    {
        public string SessionId { get; set; }

        public string ResponseType { get; set; }

        public T Response { get; set; }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = true)]
    public class PosResponseAttribute : XmlRootAttribute
    {
        public PosResponseAttribute() : base()
        {
            ElementName = "POSResponse"; IsNullable = true; Namespace = "";
        }
    }
}

[XmlRoot(ElementName = "Transaction")]
public class Transaction
{
    public uint PID { get; set; }

    public string Merchant { get; set; }

    public string AuthCode { get; set; }

    public string TxnRef { get; set; }

    public string TxnType { get; set; }

    public string Success { get; set; }

    public string ResponseText { get; set; }

    public int IdShift { get; set; }

    public int IdShiftName { get; set; }

    public int lAmount { get; set; }

    public int lTipAmount { get; set; }

    public int lCashOut { get; set; }

    public int lCashOutFee { get; set; }

    public int lSurChargeTax { get; set; }

    public int lDiscount { get; set; }

    public int lDiscountPercent { get; set; }

    public int iReward { get; set; }

    public int lAuthorizedTotal { get; set; }

    public int lDonationAmount { get; set; }

    public int iEntryMode { get; set; }

    public int iCardType { get; set; }

    public int iAccountType { get; set; }

    public int iCurrencyCode { get; set; }

    public int fsModify { get; set; }

    public int iPaymentType { get; set; }

    public int iCustomerLanguage { get; set; }

    public int fClosed { get; set; }

    public int fBatchError { get; set; }

    public int fVoided { get; set; }

    public int fRefunded { get; set; }

    public int fAuthorized { get; set; }

    public int fSignature { get; set; }

    public int fCardHolderStatesCVVnotOnCard { get; set; }

    public int fCardNotPresent { get; set; }

    public int fCardNotPresentMail { get; set; }

    public int fCardNotPresentPhone { get; set; }

    public int fCanAdjust { get; set; }

    public int fCanVoid { get; set; }

    public int fCanPreAuthComplete { get; set; }

    public int fCanVoidPreAuthComplete { get; set; }

    public int fCanGoOffline { get; set; }

    public string lszCustomerReference { get; set; }

    public string Date { get; set; }

    public string Time { get; set; }

    public string szAuthorizationResponseCode { get; set; }

    public string lszApprovalCode { get; set; }

    public string szReferenceNumber { get; set; }

    public string lszSTAN { get; set; }

    public string szCardHolderName { get; set; }

    public string lszEndCardNumber { get; set; }

    public int iReversalReason { get; set; }

    public string lszCardLogo { get; set; }

    public string szQRCode { get; set; }

    public int byPinType { get; set; }

    public int byPinStatus { get; set; }

    public int iNoCVVOption { get; set; }

    public int byCDCVM { get; set; }

    public string szMaskedCardNumber { get; set; }

    public string ReceiptData { get; set; }

    public string ReceiptLogo { get; set; }

    public string DigitalSig { get; set; }
}

[XmlRoot(ElementName = "Response")]
public class Response
{
    public Transaction Transaction { get; set; }

    [XmlArray("Transactions")]
    [XmlArrayItem("Transaction")]
    [JsonProperty("Transactions")]
    public List<Transaction> Transactions { get; set; }
}

[XmlRoot(ElementName = "POSResponse")]
public class POSResponse
{
    public string SessionId { get; set; }

    public string ResponseType { get; set; }

    public Response Response { get; set; }
}

public class POSResponseData
{
    public HttpStatusCode Status { get; set; }

    public string SessionId { get; set; }

    public string ResponseType { get; set; }

    public Response Response { get; set; }

    public POSResponseData()
    {

    }
}
