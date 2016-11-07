using System.ComponentModel.Composition;
using Presentation.Interfaces;

namespace NotificationCentre.Alerts
{
    [Export(typeof(IModule))]
    internal sealed class AlertsModule : IModule
    {
        public void Dispose()
        {
            
        }

        public void Initialize()
        {
            
        }
    }
}