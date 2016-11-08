using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Interactivity;

namespace NotificationCentre.SideBar.Behaviors
{
    public sealed class FocusTargetOnClickBehavior : Behavior<ButtonBase>
    {
        public static readonly DependencyProperty TargetProperty = DependencyProperty.Register("Target", typeof(UIElement), typeof(FocusTargetOnClickBehavior), new PropertyMetadata(default(UIElement)));

        public UIElement Target
        {
            get { return (UIElement)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.Click += OnClick;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Click -= OnClick;
        }

        private void OnClick(object sender, RoutedEventArgs e)
        {
            if (Target.IsKeyboardFocusWithin)
                return;

            Target.Focus();
        }
    }
}
