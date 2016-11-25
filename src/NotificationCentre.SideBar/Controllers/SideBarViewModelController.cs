using System;
using System.ComponentModel.Composition;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using MaterialDesignThemes.Wpf;
using NotificationCentre.Interfaces;
using NotificationCentre.SideBar.Models;
using NotificationCentre.SideBar.ViewModels;
using Presentation.Commands;
using Presentation.Core;
using Presentation.Reactive.Concurrency;
using NLog;

namespace NotificationCentre.SideBar.Controllers
{
    [PartCreationPolicy(CreationPolicy.NonShared)]
    internal sealed class SideBarViewModelController : IPartImportsSatisfiedNotification, ISideBarViewModelController, IDisposable
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger(); 
        private readonly INotificationManager _notificationManager;
        private readonly ISchedulerProvider _schedulerProvider;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        [ImportingConstructor]
        public SideBarViewModelController(INotificationManager notificationManager, ISchedulerProvider schedulerProvider)
        {
            _notificationManager = notificationManager;
            _schedulerProvider = schedulerProvider;
        }

        [Export]
        public ISideBarViewModel ViewModel { get; } = new SideBarViewModel();

        public void OnImportsSatisfied()
        {
            ViewModel.ExitApplication = new DelegateCommand(() => Application.Current.Shutdown());
            ViewModel.ClearSearch = new DelegateCommand(() => ViewModel.SearchString = string.Empty);
            ViewModel.SwitchTheme = new DelegateCommand<bool?>(isLight =>
            {
                if (isLight.HasValue)
                    new PaletteHelper().SetLightDark(!isLight.Value);
            });

            var actionCommand = new DelegateCommand<INotificationModel>(model =>
            {
                _notificationManager.OnAction(model.Id);
            });

            _notificationManager.ObserveNotifications()
                                .Select(notification => notification.ToModel(actionCommand))
                                .SubscribeOn(_schedulerProvider.TaskPool)
                                .ObserveOn(_schedulerProvider.Dispatcher)
                                .Subscribe(notificationModel => ViewModel.Notifications.Add(notificationModel), ex => _logger.Error(ex))
                                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();   
        }
    }
}