namespace TDAmeritradeAPI.Fields
{
    public class Options
    {
        public static class Type
        {
            public const string
                Standard = "S",
                NonStandard = "NS",
                All = "ALL";
        }
        public static class ContractType
        {
            public const string
                Call = "CALL",
                Put = "PUT",
                All = "ALL";
        }
        public static class Range
        {
            public const string
                InTheMoney = "ITM",
                NearTheMoney = "NTM",
                OutOfTheMoney = "OTM",
                StrikesAboveMarket = "SAK",
                StrikesBelowMarket = "SBK",
                StrikesNearMarket = "SNK",
                ALL = "ALL";
        }

        public static class Strategy
        {
            public const string
                Single = "SINGLE",
                Analytical = "ANALYTICAL",
                Covered = "COVERED",
                Vertical = "VERTICAL",
                Calendar = "CALENDAR",
                Strangle = "STRANGLE",
                Straddle = "STRADDLE",
                Butterfly = "BUTTERFLY",
                Condor = "CONDOR",
                Diagonal = "DIAGONAL",
                Collar = "COLLAR",
                Roll = "ROLL";
        }

        public static class ExpMonth
        {
            public const string
                JAN = "JAN",
                FEB = "FEB",
                MAR = "MAR",
                APR = "APR",
                MAY = "MAY",
                JUN = "JUN",
                JUL = "JUL",
                AUG = "AUG",
                SEP = "SEP",
                OCT = "OCT",
                NOV = "NOV",
                DEC = "DEC";
        }
    }
}