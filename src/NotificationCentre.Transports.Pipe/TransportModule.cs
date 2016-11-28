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
using NLog;

namespace NotificationCentre.Transports
{
    [Export(typeof(IModule))]
    internal sealed class TransportModule : IModule
    {
        private readonly ILogger _logger = LogManager.GetCurrentClassLogger();
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
            _transportFactory.Create<JsonNotification>(KnownTransports.Pipes.Server)
                             .ThrowOnNullOrEmptyTopic()
                             .Observe(TransportConstants.Topics.Post)
                             .SubscribeOn(_schedulerProvider.TaskPool)
                             .ObserveOn(_schedulerProvider.TaskPool)
                             .Subscribe(notification => _notificationService.Post(notification), 
                                        ex => _logger.Error(ex))
                             .AddTo(_disposable);

            var publishTransport = _transportFactory.Create<JsonNotificationAction>(KnownTransports.Pipes.Server)
                                                    .ThrowOnNullOrEmptyTopic();

            var activated = publishTransport.Publish(TransportConstants.Topics.Activated);

            _notificationService.ObserveActivated()
                                .Select(notification => notification.ToJsonNotification())
                                .SubscribeOn(_schedulerProvider.TaskPool)
                                .ObserveOn(_schedulerProvider.TaskPool)
                                .Subscribe(activated)
                                .AddTo(_disposable);

            var dismissed = publishTransport.Publish(TransportConstants.Topics.Dismissed);

            _notificationService.ObserveDismissed()
                                .Select(notification => notification.ToJsonNotification())
                                .SubscribeOn(_schedulerProvider.TaskPool)
                                .ObserveOn(_schedulerProvider.TaskPool)
                                .Subscribe(dismissed)
                                .AddTo(_disposable);

            var timedOut = publishTransport.Publish(TransportConstants.Topics.TimedOut);

            _notificationService.ObserveTimedOut()
                                .Select(notification => notification.ToJsonNotification())
                                .SubscribeOn(_schedulerProvider.TaskPool)
                                .ObserveOn(_schedulerProvider.TaskPool)
                                .Subscribe(timedOut)
                                .AddTo(_disposable);
        }
    }
}
