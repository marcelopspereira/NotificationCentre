using System;
using System.ComponentModel.Composition;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using MaterialDesignThemes.Wpf;
using NotificationCentre.Interfaces;
using NotificationCentre.SideBar.Models;
using NotificationCentre.SideBar.ViewModels;
using Presentation.Commands;
using Presentation.Core;
using Presentation.Reactive.Concurrency;

namespace NotificationCentre.SideBar.Controllers
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class SideBarViewModelController : IPartImportsSatisfiedNotification, ISideBarViewModelController, IDisposable
    {
        private readonly INotificationService _notificationService;
        private readonly ISchedulerProvider _schedulerProvider;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        [ImportingConstructor]
        public SideBarViewModelController(INotificationService notificationService, ISchedulerProvider schedulerProvider)
        {
            _notificationService = notificationService;
            _schedulerProvider = schedulerProvider;
        }

        [Export]
        public ISideBarViewModel ViewModel { get; } = new SideBarViewModel();

        public void OnImportsSatisfied()
        {
            ViewModel.ClearSearch = new DelegateCommand(() => ViewModel.SearchString = string.Empty);
            ViewModel.SwitchTheme = new DelegateCommand<bool?>(isLight =>
            {
                if (isLight.HasValue)
                    new PaletteHelper().SetLightDark(!isLight.Value);
            });
            _notificationService.ObserveNotifications()
                                .Select(notification => notification.ToModel())
                                .SubscribeOn(_schedulerProvider.TaskPool)
                                .ObserveOn(_schedulerProvider.Dispatcher)
                                .Subscribe(notificationModel => ViewModel.Notifications.Add(notificationModel))
                                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();   
        }
    }
}