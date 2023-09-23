using Systmtca.MDPipe.Venues;
using Systmtca.MDPipe.Venues.Core.Data;

namespace Systmtca.MDPipe.Pipes
{
    public interface IEventsPipe
    {
        public Task Subscribe(string venue, List<SubscriptionRequest> newSubscriptions);

        public void OnEvent(string eventData);
    }
}
