using System;
using System.Windows.Interop;

namespace NotificationCentre.Alerts.Views
{
    /// <summary>
    /// Interaction logic for AlertsWindow.xaml
    /// </summary>
    public partial class AlertsWindow : IAlertsWindow
    {
        public AlertsWindow()
        {
            InitializeComponent();
        }

        public IntPtr RetrieveHandle()
        {
            var windowHelper = new WindowInteropHelper(this);

            return windowHelper.EnsureHandle();
        }
    }
}
