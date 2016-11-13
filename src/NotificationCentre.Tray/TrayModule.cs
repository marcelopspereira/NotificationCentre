using System;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows;
using NotificationCentre.Tray.Views;
using Presentation.Interfaces;

namespace NotificationCentre.Tray
{
    [Export(typeof(IModule))]
    internal sealed class TrayModule : IModule
    {
        private readonly ITrayIcon _trayIcon;

        [ImportingConstructor]
        public TrayModule(ITrayIcon trayIcon)
        {
            _trayIcon = trayIcon;
        }

        public void Dispose()
        {
            _trayIcon.Dispose();
        }

        public void Initialize()
        {
            var uri = new Uri(TrayConstants.Icon);
            var iconStream = Application.GetResourceStream(uri);
            if (iconStream == null)
                throw new ArgumentException("Failed to get icon from the application resource stream.");

            var icon = new Icon(iconStream.Stream);

            _trayIcon.Show(icon);
        }
    }
}