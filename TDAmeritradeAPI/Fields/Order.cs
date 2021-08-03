namespace TDAmeritradeAPI.Fields
{
    public static class Order
    {
        public static class AssetType
        {
            public const string
                Equity = "EQUITY",
                Option = "OPTION",
                Index = "INDEX",
                MutualFund = "MUTUAL_FUND",
                CashEquivalent = "CASH_EQUIVALENT",
                FixedIncome = "FIXED_INCOME",
                Currency = "CURRENCY";
        }

        public static class StrategyType
        {
            public const string
                Single = "SINGLE",
                OCO = "OCO",
                Trigger = "TRIGGER";
        }

        public static class Duration
        {
            public const string
                Day = "DAY",
                GTC = "GOOD_TILL_CANCEL",
                FOK = "FILL_OR_KILL",
                None = "None";
        }

        public static class Instruction
        {
            public const string
                Buy = "BUY",
                Sell = "SELL",
                BuyToCover = "BUY_TO_COVER",
                SellShort = "SELL_SHORT",
                BuyToOpen = "BUY_TO_OPEN",
                BuyToClose = "BUY_TO_CLOSE",
                SellToOpen = "SELL_TO_OPEN",
                SellToClose = "SELL_TO_CLOSE",
                Exchange = "EXCHANGE";
        }

        public static class MarketSession
        {
            public const string
                AM = "AM",
                PM = "PM",
                Normal = "NORMAL",
                Seamless = "SEAMLESS",
                None = "NONE";
        }

        public static class PriceLinkType
        {
            public const string
                Value = "VALUE",
                Percent = "PERCENT",
                None = "NONE";
        }

        public static class Status
        {
            public const string
                Awaiting_Parent_Order = "AWAITING_PARENT_ORDER",
                Awaiting_Condition = "AWAITING_CONDITION",
                Awaiting_Manual_Review = "AWAITING_MANUAL_REVIEW",
                Accepted = "ACCEPTED",
                AwaitingUrOut = "AWAITING_UR_OUT",
                Pending_Activation = "PENDING_ACTIVATION",
                Queued = "QUEUED",
                Working = "WORKING",
                Rejected = "REJECTED",
                PendingCancel = "PENDING_CANCEL",
                Canceled = "CANCELED",
                PendingReplace = "PENDING_REPLACE",
                Replaced = "REPLACED",
                Filled = "FILLED",
                Expired = "EXPIRED";
        }

        public static class Type
        {
            public const string
                Market = "MARKET",
                Limit = "LIMIT",
                Stop = "STOP",
                StopLimit = "STOP_LIMIT",
                TrailingStop = "TRAILING_STOP",
                MarketOnClose = "MARKET_ON_CLOSE",
                Exercise = "EXERCISE",
                TrailingStopLimit = "TRAILING_STOP_LIMIT",
                NetDebit = "NET_DEBIT",
                NetCredit = "NET_CREDIT",
                NetZero = "NET_ZERO";
        }
    }
}