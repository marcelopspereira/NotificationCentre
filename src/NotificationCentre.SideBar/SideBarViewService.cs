using System;
using System.ComponentModel.Composition;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using NotificationCentre.Interfaces;
using NotificationCentre.SideBar.ViewModels;
using NotificationCentre.SideBar.Views;
using Presentation.Core;
using Presentation.Reactive;
using Presentation.Reactive.Concurrency;

namespace NotificationCentre.SideBar
{
    [Export(typeof(ISideBarViewService))]
    internal sealed class SideBarViewService : ISideBarViewService, IPartImportsSatisfiedNotification, IDisposable
    {
        private readonly ISubject<Unit> _hideStream;
        private readonly ISchedulerProvider _schedulerProvider;
        private readonly ISideBarWindow _view;
        private readonly ISideBarViewModel _viewModel;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        [ImportingConstructor]
        public SideBarViewService(ISideBarWindow view, ISideBarViewModel viewModel, ISchedulerProvider schedulerProvider)
        {
            _view = view;
            _viewModel = viewModel;
            _schedulerProvider = schedulerProvider;
            _hideStream = new Subject<Unit>();
            _disposable.Add((IDisposable)_hideStream);
        }

        public void ToggleSideBar()
        {
            if (_viewModel.IsOpen)
                HideSideBar();
            else
                ShowSideBar();
        }

        public void ShowSideBar()
        {
            _viewModel.IsOpen = true;
            _view.Activate();
        }

        public void HideSideBar()
        {
            _hideStream.OnNext(Unit.Default);
        }

        public void OnImportsSatisfied()
        {
            _hideStream.Throttle(TimeSpan.FromMilliseconds(150), _schedulerProvider.TaskPool)
                       .ObserveOn(_schedulerProvider.Dispatcher)
                       .Subscribe(_ => _viewModel.IsOpen = false)
                       .AddTo(_disposable);

            _view.DataContext = _viewModel;
            _view.OnDeactived()
                 .Skip(1)
                 .Select(_ => Unit.Default)
                 .Subscribe(_hideStream)
                 .AddTo(_disposable);
            _view.Show();
        }

        public void Dispose()
        {
            _disposable.Dispose();   
        }
    }
}