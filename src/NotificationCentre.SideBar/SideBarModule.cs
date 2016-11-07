using System;
using System.ComponentModel.Composition;
using NotificationCentre.SideBar.ViewModels;
using NotificationCentre.SideBar.Views;
using Presentation.Interfaces;

namespace NotificationCentre.SideBar
{
    [Export(typeof(IModule))]
    internal sealed class SideBarModule : IModule
    {
        private readonly Lazy<ISideBarWindow> _view;
        private readonly Lazy<ISideBarViewModel> _viewModel;

        [ImportingConstructor]
        public SideBarModule(Lazy<ISideBarWindow> view, Lazy<ISideBarViewModel> viewModel)
        {
            _view = view;
            _viewModel = viewModel;
        }

        public void Dispose()
        {
            
        }

        public void Initialize()
        {
            var viewModel = _viewModel.Value;
            var view = _view.Value;
            view.DataContext = viewModel;
            view.Show();
        }
    }
}