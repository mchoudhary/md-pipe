namespace Systmtca.MDPipe.Framework.Core
{
    public static class DataConverters
    {
        public static DateTime ParseDateTimeStr(string rawDate)
        {
            _ = SpanJson.Helpers.DateTimeParser.TryParseDateTime(rawDate, out DateTime dtParsed, out _);
            return dtParsed;
        }
    }
}
