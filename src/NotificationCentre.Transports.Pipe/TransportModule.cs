using System;
using System.ComponentModel.Composition;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using NotificationCentre.Interfaces;
using Presentation.Core;
using Presentation.Interfaces;
using Presentation.Reactive.Concurrency;
using Transport.Core;
using Transport.Interfaces;

namespace NotificationCentre.Transports
{
    [Export(typeof(IModule))]
    internal sealed class TransportModule : IModule
    {
        private readonly ITransportFactory _transportFactory;
        private readonly ISchedulerProvider _schedulerProvider;
        private readonly INotificationService _notificationService;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        [ImportingConstructor]
        public TransportModule(ITransportFactory transportFactory, ISchedulerProvider schedulerProvider, INotificationService notificationService)
        {
            _transportFactory = transportFactory;
            _schedulerProvider = schedulerProvider;
            _notificationService = notificationService;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public void Initialize()
        {
            var transport = _transportFactory.Create<JsonNotification>(KnownTransports.Pipes.Server)
                                             .ThrowOnNullOrEmptyTopic();

            transport.Observe(TransportConstants.Topics.Post)
                     .SubscribeOn(_schedulerProvider.TaskPool)
                     .ObserveOn(_schedulerProvider.TaskPool)
                     .Subscribe(notification => _notificationService.Post(notification), ex => {})
                     .AddTo(_disposable);

            var activated = transport.Publish(TransportConstants.Topics.Activated);

            _notificationService.ObserveActivated()
                                .Select(notification => notification.ToJsonNotification())
                                .SubscribeOn(_schedulerProvider.TaskPool)
                                .ObserveOn(_schedulerProvider.TaskPool)
                                .Subscribe(activated)
                                .AddTo(_disposable);

            var dismissed = transport.Publish(TransportConstants.Topics.Dismissed);

            _notificationService.ObserveDismissed()
                                .Select(notification => notification.ToJsonNotification())
                                .SubscribeOn(_schedulerProvider.TaskPool)
                                .ObserveOn(_schedulerProvider.TaskPool)
                                .Subscribe(dismissed)
                                .AddTo(_disposable);

            var timedOut = transport.Publish(TransportConstants.Topics.TimedOut);

            _notificationService.ObserveTimedOut()
                                .Select(notification => notification.ToJsonNotification())
                                .SubscribeOn(_schedulerProvider.TaskPool)
                                .ObserveOn(_schedulerProvider.TaskPool)
                                .Subscribe(timedOut)
                                .AddTo(_disposable);

        }
    }
}
