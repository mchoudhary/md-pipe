namespace Systmtca.MDPipe.Venues.Core.Data
{
    public readonly struct SubscriptionRequest
    {
        public string Id => $"{Symbol}@{Exchange}->{Channel}{(string.IsNullOrWhiteSpace(Interval) ? string.Empty: $"_{Interval}")}";
        public string Symbol { get; }
        public string Channel { get; }
        public string Exchange { get; }
        public DateTime TsSubmission { get; }
        public string? Interval { get;}

        public SubscriptionRequest(string symbol, string channel, string exchange, DateTime tsSubmission, string? interval = null)
        {
            Symbol = symbol;
            Channel = channel;
            Exchange = exchange;
            TsSubmission = tsSubmission;
            Interval = interval;
        }

        public override string ToString() => Id;
    }
}
