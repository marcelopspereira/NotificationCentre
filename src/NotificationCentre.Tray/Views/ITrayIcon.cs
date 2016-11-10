using System;
using System.Drawing;

namespace NotificationCentre.Tray.Views
{
    internal interface ITrayIcon : IDisposable
    {
        void Initialize(Icon icon);
    }
}