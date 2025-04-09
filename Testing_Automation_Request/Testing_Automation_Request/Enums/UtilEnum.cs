
using static CloudBanking.Utilities.UtilEnum;

namespace CloudBanking.Utilities
{
    public static class UtilEnum
    {
        public enum PrintRenderMode
        {
            None,
            Html,
            PlainText,
            Image,
            Html_DontPrint,
            PlainText_Html_DontPrint
        }

        public enum POSMediaType
        {
            Json,
            Xml
        }

        public enum PosProtocol
        {
            Http,
            SerialPort,
            Socket,
            Bluetooth,
        }

        public enum AppState
        {
            None,
            Foreground,
            Background
        }

        public enum ProcessingAction
        {
            None,
            StartProcessing,
            UpdateMessage,
            StopProcessing
        }

        public enum PinPadType
        {
            Input,
            Error,
            Confirm,
            NoPinPad,
            Cancel,
            PinLength,
            Timeout
        }

        public enum AppTheme
        {
            None,
            Normal,
            CustomerDisplay
        }

        public enum TicketRefType
        {
            None,
            QSale,
            Table,
            Tab
        }

        public enum ManualShiftType
        {
            None = 0,
            Breakfast,
            Lunch,
            Dinner,
            Supper
        }

        public enum FacialDataTypes
        {
            Local,
            Server
        }

        public enum TerminalMode
        {
            None,
            LogOff,
            EOV,
            Online,
            RKIFail
        }

        public enum InfoType
        {
            None = 0,
            Merchant,
            Security,
            Terminal
        }

        public enum TransactionStage
        {
            None,
            TransactionCompleted,
            Initialize,
            AwaitingUserInteraction,
            WaitingForCard,
            CardProcessing,
            TransactionProcessing,
            TransactionProcessed,
            WaitingForCardRemove,
            WaitingForMerchantCard
        }

        public enum FunctionIds
        {
            None,
            StartApp,
            StartUp,            
            Help,       
            Interface,
            MediaManager,
            Setup,
            Exit,
            SelectMerchant,
            MerchantAdvertising,
            Login,
            TimeAttendance,
            SelectPaymentMode,

            SelectFunction,
            ChangeShift,
            Donation,
            Tip,
            PreSurcharge,
            StandaloneSplitPay,
            ManualPay,
            MultiTender,
            MyShare,
            GuestPay,
            BlindPay,

            Purchase,
            PurchaseAndCash,

            Preauth,
            PreauthReferences,
            PaymentOptionsSearch,
            PaymentQrCodeSearch,
            PaymentCardSearch,
            PaymentAdvanceSearch,
            PaymentFilterSearch,
            PreauthFinalCompletion,
            PreauthTopUp,
            PreauthPartialCompletion,
            PreauthDelayedCompletion,
            PreauthCardStatus,
            CancelPreauth,


            MOTO,
            MOTOPurchase,
            MOTORefund,
            MOTOSelectAccountType,
            RatingService,
            MerchantConfirmation,
            ReceiptOptions,
            CashOut,
            PresentCard,
            AliPay,
            WePay,
            GiftAndReward,
            OtherPay,
            BNPL,
            ReportType,
            ReportSales,
            ReportCurrentSales,
            ReportPreviousSales,
            ReportHistorySales,
            ReportShift,
            ReportCurrentShift,
            ReportPreviousShift,
            ReportHistoryShift,
            ReprintReceipt,
            RefundType,
            RefundPurchase,
            RefundAdvanceSearch
        }

        public enum AccessTypes
        {
            None,
            Manual,
            FaceId,
            CardAccess,
            DrawDot
        }

        public enum PickerTypes
        {
            None,
            Date,
            Time,
            DateTime
        }

       [System.Serializable]
        public enum TipOptions
        {
            None,
            ManualTip,
            FixedServiceTip
        }

        public enum ServiceType
        {
            Excellent = 0,
            Good = 1,
            Fair = 2,
            Poor = 3
        }

        public enum ContactType
        {
            None = 0,
            LandLinePhone = 1,
            Cell,
            Email,
            IPorSIP,
            Pager,
            DeviceM2M,
            VPN,
            Fax,
            Website,
            SocialMedia = 10
        }

        public enum ContactDetailsType
        {
            None,
            PersonalDetails,
            WorkDetails
        }


        public enum EntityType
        {
            User = 1,
            JobPositions,
            M2MDevice,
            HostInterface,
            Escaluation
        }

        public enum RefAuthInputField
        {
            Ref,
            Auth
        }

        public enum RuleType
        {
            RuleSetup,
            MessageFormat
        }

        public enum HostInterfaceType
        {
            DDC,
            EFT,
            WECHAT,
            ALIPAY,
            OTHERPAY
        }

        public enum KeyBoardTypes
        {
            Amount,
            CardNumber,
            Number,
            Date,
            NumberAndText
        }

        public enum FunctionTypes
        {
            License,
            Certified,
            Alert,
            Advertising,
            Timeout,
            Cancel
        }

        [System.Serializable]
        public enum FncSelectDisplayOptions
        {
            SingleLevel,
            SubLevel
        }

        [System.Serializable]
        public enum PaymentStatus
        {
            None,
            Approved,
            Declined,
            Refunded
        }

        [System.Serializable]
        public enum Gender
        {
            None,
            Male,
            Female,
            Others
        }

        public enum FingerPrint
        {
            SCAN_FINGER = 0,
            SCAN_FAILED = 1,
            SCAN_COMPLETE = 2
        }

        public enum IdProofScanType
        {
            SCAN_PASSPORT = 0,
            SCAN_DRIVER_LICENSE = 1,
            SCAN_ID_CARD = 2
        }

        public enum BatterySave
        {
            NONE,
            INACTIVE,
            ALWAYS
        }
       
        public enum OtherPaymentSubType
        {
            BUNNINGS,
            GOOGLE_PLAY,
            BP,
            JB_HI_FI,
            AMAZON,
            DAVID_JONES,
            MEDICARE,
            MEDIBANK,
            BUPA,
            HCF,
            NIB
        }

        public enum PrintReportType
        {
            NONE,
            DETAIL,
            SUMMARY,
            DATETIME,
            ISSUER,
            EMPLOYEE,
            BYCARDTYPE,
        }

        public enum ReceiptType
        {
            Email,
            Text
        }

        public enum RefundReason
        {
            None,
            ReturnedGoods,
            CancelledPurchase,
            CustomerService,
            EmployeeError,
            Other
        }

        public enum OpenTabPhase
        {
            None,
            Opened,
            Canceled,
            Closed,
            Deleted,
        }

        public enum SubFlow
        {
            Standalone,
            ECR,
            PayAtTable,
            GiftCardSale,
            Activata
        }

        public enum PaymentSearchType
        {
            None,
            NormalSearch,
            QRCodeSearch,
            CardSearch,
            AdvancedSearch,
            FilterSearch
        }

        public enum ReportType
        {
            None,
            Transactions,
            Tips,
            Donations,
            Discounts
        }

        public enum PaymentMethod
        {
            None,
            EFTCards,
            AliPay,
            WeChat,
            Other
        }

        public enum RefundOption
        {
            None,
            ExistingCard,
            ManualPay,
            AlternativeCard
        }

        public enum ReportCategory
        {
            Sales,
            Shift
        }

        public enum ChangableListActionType
        {
            None,
            Help,
            Menu,
            Select,
            IdCard
        }

        public enum SwitchWithOptionItemType
        {
            SWITCH,
            SELECT,
            SINGLE_LINE
        }

        public enum ItemSelectDeleteType
        {
            DEFAULT,
            WITH_SUB_ITEM
        }

        public enum CustomerDisplayAuthType
        {
            None,
            Card,
            FaceID,
            FingerPrintID,
            QRCodeID
        }

        public enum RequestCardScreenType
        {
            NoPresentCard,
            OnlyPresentCard,
            Mixture
        }

        public enum PaymentOther
        {
            All,
            AliPay,
            Wechat,
            Other
        }

        public enum RunPrintingAnimationType
        {
            Start,
            Pause,
            Resume,
            End,
            Exit
        }

        public enum PaintCanvasType
        {
            IMAGE,
            TEXT,
            INVERT,
        }

        public enum TerminalPrinterAlignment
        {
            LEFT,
            CENTER,
            RIGHT,
        }

        public enum TerminalPrinterBarcodeSymbology
        {
            LEFT,
            CENTER,
            RIGHT,
        }

        public enum ReadCtlsSwipeMode
        {
            None,
            CtlsDelay,
            MSRReader
        }

        public enum DefaultTransaction
        {
            None,
            TransactionList,
            Purchase,
            Refund,
            Preauth
        }

        public enum ACCOUNTTYPE
        {
            Credit,
            Debit
        }
    }
}
