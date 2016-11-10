using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media.Animation;

namespace NotificationCentre.Alerts.Behaviors
{
    internal sealed class AnimateAlertBehavior : Behavior<UIElement>
    {
        public static readonly DependencyProperty OpeningStoryboardProperty = DependencyProperty.Register("OpeningStoryboard", typeof(Storyboard), typeof(AnimateAlertBehavior), new PropertyMetadata(default(Storyboard), OnOpeningStoryboardChanged));

        public static readonly DependencyProperty ClosingStoryboardProperty = DependencyProperty.Register("ClosingStoryboard", typeof(Storyboard), typeof(AnimateAlertBehavior), new PropertyMetadata(default(Storyboard)));

        public static readonly DependencyProperty TimingOutStoryboardProperty = DependencyProperty.Register("TimingOutStoryboard", typeof(Storyboard), typeof(AnimateAlertBehavior), new PropertyMetadata(default(Storyboard)));

        public static readonly DependencyProperty StartAnimationProperty = DependencyProperty.Register("StartAnimation", typeof(bool), typeof(AnimateAlertBehavior), new PropertyMetadata(default(bool), OnStartAnimationChanged));

        public bool StartAnimation
        {
            get { return (bool) GetValue(StartAnimationProperty); }
            set { SetValue(StartAnimationProperty, value); }
        }

        public Storyboard OpeningStoryboard
        {
            get { return (Storyboard)GetValue(OpeningStoryboardProperty); }
            set { SetValue(OpeningStoryboardProperty, value); }
        }

        public Storyboard TimingOutStoryboard
        {
            get { return (Storyboard) GetValue(TimingOutStoryboardProperty); }
            set { SetValue(TimingOutStoryboardProperty, value); }
        }

        public Storyboard ClosingStoryboard
        {
            get { return (Storyboard) GetValue(ClosingStoryboardProperty); }
            set { SetValue(ClosingStoryboardProperty, value); }
        }

        protected override void OnAttached()
        {
            AssociatedObject.MouseEnter += OnMouseEnter;
            AssociatedObject.MouseLeave += OnMouseLeave;
            AssociatedObject.PreviewMouseDown += OnMouseDown;
        }

        protected override void OnDetaching()
        {
            AssociatedObject.MouseEnter -= OnMouseEnter;
            AssociatedObject.MouseLeave -= OnMouseLeave;
            AssociatedObject.PreviewMouseDown -= OnMouseDown;
        }

        private void OnMouseDown(object sender, MouseButtonEventArgs args)
        {
            ClosingStoryboard.Begin();
        }

        private void OnMouseLeave(object sender, MouseEventArgs args)
        {
            TimingOutStoryboard.Resume();
        }

        private void OnMouseEnter(object sender, MouseEventArgs args)
        {
            TimingOutStoryboard.Pause();
            TimingOutStoryboard.Seek(TimeSpan.Zero);
        }

        private static void OnOpeningStoryboardChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as AnimateAlertBehavior;
            if (behavior == null)
                return;

            var storyboard = e.NewValue as Storyboard;
            if (storyboard == null)
                return;

            storyboard.Completed += (o, args) => behavior.TimingOutStoryboard.Begin();
        }

        private static void OnStartAnimationChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as AnimateAlertBehavior;
            if (behavior == null)
                return;

            var begin = (bool)e.NewValue;
            if (begin)
                behavior.OpeningStoryboard.Begin();
        }
    }
}