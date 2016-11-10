using System;
using System.Windows.Interop;

namespace NotificationCentre.SideBar.Views
{
    /// <summary>
    /// Interaction logic for SideBarWindow.xaml
    /// </summary>
    internal partial class SideBarWindow : ISideBarWindow
    {
        public SideBarWindow()
        {
            InitializeComponent();            
        }

        public IntPtr RetrieveHandle()
        {
            var helper = new WindowInteropHelper(this);
            return helper.EnsureHandle();
        }
    }
}
