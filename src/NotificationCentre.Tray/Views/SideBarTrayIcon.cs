using System.ComponentModel.Composition;
using System.Drawing;
using System.Reactive.Disposables;
using System.Windows.Forms;
using NotificationCentre.Interfaces;

namespace NotificationCentre.Tray.Views
{
    [Export(typeof(ITrayIcon))]
    internal sealed class SideBarTrayIcon : ITrayIcon
    {
        private readonly ISideBarViewService _sideBarViewService;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        [ImportingConstructor]
        public SideBarTrayIcon(ISideBarViewService sideBarViewService)
        {
            _sideBarViewService = sideBarViewService;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public void Initialize(Icon icon)
        {
            var notifyIcon = new NotifyIcon
            {
                Icon = icon,
                Visible = true,
                Text = "Notification Centre"
            };
            notifyIcon.MouseClick += OnMouseClick;

            _disposable.Add(Disposable.Create(() => notifyIcon.MouseClick -= OnMouseClick));
            _disposable.Add(notifyIcon);
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            _sideBarViewService.ToggleSideBar();
        }
    }
}