using System;
using System.ComponentModel.Composition;
using System.Reactive.Linq;
using NotificationCentre.SideBar.ViewModels;

namespace NotificationCentre.SideBar.Controllers
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class SideBarViewModelController : IPartImportsSatisfiedNotification, ISideBarViewModelController
    {
        [Export]
        public ISideBarViewModel ViewModel { get; } = new SideBarViewModel();

        public void OnImportsSatisfied()
        {
            Observable.Timer(TimeSpan.FromSeconds(5))
                      .Subscribe(_ => ViewModel.IsOpen = true);
        }
    }
}