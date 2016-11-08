using System.ComponentModel.Composition;
using MaterialDesignThemes.Wpf;
using NotificationCentre.SideBar.ViewModels;
using Presentation.Commands;

namespace NotificationCentre.SideBar.Controllers
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class SideBarViewModelController : IPartImportsSatisfiedNotification, ISideBarViewModelController
    {
        [Export]
        public ISideBarViewModel ViewModel { get; } = new SideBarViewModel();

        public void OnImportsSatisfied()
        {
            ViewModel.SwitchTheme = new DelegateCommand<bool?>(isLight =>
            {
                if (isLight.HasValue)
                    new PaletteHelper().SetLightDark(!isLight.Value);
            });
        }
    }
}