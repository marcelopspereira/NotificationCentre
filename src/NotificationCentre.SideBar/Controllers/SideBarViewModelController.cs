using System.ComponentModel.Composition;
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
            
        }
    }
}