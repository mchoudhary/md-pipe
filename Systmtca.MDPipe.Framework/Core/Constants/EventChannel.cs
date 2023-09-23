namespace Systmtca.MDPipe.Framework.Core.Constants
{
    public static class EventChannel
    {
        public const string TRADE = "TRADE";
        public const string TRADE_BAR = "TRADE_BAR";
        public const string ORDERBOOK_L2_INCR = "ORDERBOOK_L2_INCR";

        /// <summary>
        /// s-> seconds; m -> minutes; h -> hours; d -> days; w -> weeks; M -> months
        /// </summary>
        public const string TICKER = "TICKER";

        public const string FUNDING_RATE = "FUNDING_RATE";
        public const string OPEN_INTR = "OPEN_INTR";
    }
}
