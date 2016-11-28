using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interactivity;
using NotificationCentre.SideBar.Models;
using Presentation.Core;

namespace NotificationCentre.SideBar.Behaviors
{
    internal sealed class AlertModelFilterByStringBehavior : Behavior<CollectionViewSource>
    {
        private readonly ISubject<Unit> _refreshView = new Subject<Unit>();
        private readonly TimeSpan _throttlePeriod = TimeSpan.FromMilliseconds(150);
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        public static readonly DependencyProperty FilterStringProperty = DependencyProperty.Register("FilterString", typeof(string), typeof(AlertModelFilterByStringBehavior), new FrameworkPropertyMetadata(default(string), PropertyChangedCallback) {BindsTwoWayByDefault = true});

        public string FilterString
        {
            get { return (string) GetValue(FilterStringProperty); }
            set { SetValue(FilterStringProperty, value); }
        }

        protected override void OnAttached()
        {
            _refreshView.Throttle(_throttlePeriod)
                        .ObserveOn(Dispatcher)
                        .Select(_ => AssociatedObject.View)
                        .Subscribe(view => view.Refresh())
                        .AddTo(_disposable);

            AssociatedObject.Filter += AssociatedObjectOnFilter;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Filter -= AssociatedObjectOnFilter;

            _disposable.Dispose();
        }

        private void AssociatedObjectOnFilter(object sender, FilterEventArgs e)
        {
            e.Accepted = true;
            if (string.IsNullOrWhiteSpace(FilterString))
                return;

            var item = e.Item as INotificationModel;
            if (item == null)
                return;

            var filterString = FilterString.ToLowerInvariant();

            var title = item.Title.ToLowerInvariant();
            if (title.Contains(filterString))
                return;

            var content = item.Content.ToLowerInvariant();
            if (content.Contains(filterString))
                return;

            e.Accepted = false;
        }

        private static void PropertyChangedCallback(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as AlertModelFilterByStringBehavior;

            behavior?._refreshView.OnNext(Unit.Default);
        }
    }
}