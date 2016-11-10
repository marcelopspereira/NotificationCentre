using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interactivity;
using NotificationCentre.SideBar.Models;

namespace NotificationCentre.SideBar.Behaviors
{
    internal sealed class AlertModelFilterByStringBehavior : Behavior<CollectionViewSource>
    {
        public static readonly DependencyProperty FilterStringProperty = DependencyProperty.Register("FilterString", typeof(string), typeof(AlertModelFilterByStringBehavior), new FrameworkPropertyMetadata(default(string), PropertyChangedCallback) {BindsTwoWayByDefault = true});

        public string FilterString
        {
            get { return (string) GetValue(FilterStringProperty); }
            set { SetValue(FilterStringProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.Filter += AssociatedObjectOnFilter;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Filter -= AssociatedObjectOnFilter;
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

            behavior?.Dispatcher?.BeginInvoke(new Action(() =>
            {
                behavior.AssociatedObject?.View?.Refresh();
            }));
        }
    }
}