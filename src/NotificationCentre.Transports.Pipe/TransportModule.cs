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
            var transport = _transportFactory.Create<JsonNotification>(KnownTransports.Pipes.Server);

            transport.ThrowOnNullOrEmptyTopic()
                     .Observe(TransportConstants.Topics.Post)
                     .SubscribeOn(_schedulerProvider.TaskPool)
                     .ObserveOn(_schedulerProvider.TaskPool)
                     .Subscribe(notification => _notificationService.Post(notification))
                     .AddTo(_disposable);
        }
    }
}
