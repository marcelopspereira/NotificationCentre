using System.Collections.Concurrent;
using System.ComponentModel.Composition;

namespace NotificationCentre.Alerts
{
    [Export(typeof(IAlertsQueue))]
    internal sealed class BlockingAlertsQueue : IAlertsQueue
    {
        private readonly BlockingCollection<IAlert> _blockingCollection = new BlockingCollection<IAlert>();

        public void Enqueue(IAlert alert)
        {
            _blockingCollection.Add(alert);
        }

        public IAlert Dequeue()
        {
            return _blockingCollection.Take();
        }
    }
}