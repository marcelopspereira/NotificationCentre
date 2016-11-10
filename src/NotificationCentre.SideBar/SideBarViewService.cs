using System;
using System.ComponentModel.Composition;
using NotificationCentre.Interfaces;
using NotificationCentre.SideBar.ViewModels;
using NotificationCentre.SideBar.Views;

namespace NotificationCentre.SideBar
{
    [Export(typeof(ISideBarViewService))]
    internal sealed class SideBarViewService : ISideBarViewService, IPartImportsSatisfiedNotification
    {
        private readonly Lazy<ISideBarWindow> _view;
        private readonly Lazy<ISideBarViewModel> _viewModel;

        [ImportingConstructor]
        public SideBarViewService(Lazy<ISideBarWindow> view, Lazy<ISideBarViewModel> viewModel)
        {
            _view = view;
            _viewModel = viewModel;
        }

        public void ToggleSideBar()
        {
            if (_viewModel.Value.IsOpen)
                HideSideBar();
            else
                ShowSideBar();
        }

        public void ShowSideBar()
        {
            _viewModel.Value.IsOpen = true;
        }

        public void HideSideBar()
        {
            _viewModel.Value.IsOpen = false;
        }

        public void OnImportsSatisfied()
        {
            var view = _view.Value;
            view.DataContext = _viewModel.Value;
            view.Show();
        }
    }
}