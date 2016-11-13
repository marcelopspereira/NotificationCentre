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

        public static readonly DependencyProperty StartAnimationProperty = DependencyProperty.Register("StartAnimation", typeof(bool), typeof(AnimateAlertBehavior), new PropertyMetadata(default(bool), OnStartAnimationChanged));

        public static readonly DependencyProperty TimedOutStoryboardProperty = DependencyProperty.Register("TimedOutStoryboard", typeof(Storyboard), typeof(AnimateAlertBehavior), new PropertyMetadata(default(Storyboard), OnTimedOutStoaryboardChanged));

        public static readonly DependencyProperty TimedOutCommandProperty = DependencyProperty.Register("TimedOutCommand", typeof(ICommand), typeof(AnimateAlertBehavior), new PropertyMetadata(default(ICommand)));

        public static readonly DependencyProperty TimedOutCommandParameterProperty = DependencyProperty.Register("TimedOutCommandParameter", typeof(object), typeof(AnimateAlertBehavior), new PropertyMetadata(default(object)));

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

        public Storyboard TimedOutStoryboard
        {
            get { return (Storyboard) GetValue(TimedOutStoryboardProperty); }
            set { SetValue(TimedOutStoryboardProperty, value); }
        }

        public object TimedOutCommandParameter
        {
            get { return GetValue(TimedOutCommandParameterProperty); }
            set { SetValue(TimedOutCommandParameterProperty, value); }
        }

        public ICommand TimedOutCommand
        {
            get { return (ICommand)GetValue(TimedOutCommandProperty); }
            set { SetValue(TimedOutCommandProperty, value); }
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
            TimedOutStoryboard.Resume();
        }

        private void OnMouseEnter(object sender, MouseEventArgs args)
        {
            TimedOutStoryboard.Pause();
            TimedOutStoryboard.Seek(TimeSpan.Zero);
        }

        private static void OnOpeningStoryboardChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as AnimateAlertBehavior;
            if (behavior == null)
                return;

            var storyboard = e.NewValue as Storyboard;
            if (storyboard == null)
                return;

            storyboard.Completed += (o, args) => behavior.TimedOutStoryboard.Begin();
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

        private static void OnTimedOutStoaryboardChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var behavior = sender as AnimateAlertBehavior;
            if (behavior == null)
                return;

            var storyboard = e.NewValue as Storyboard;
            if (storyboard == null)
                return;

            storyboard.Completed += (o, args) =>
            {
                var parameter = behavior.TimedOutCommandParameter;
                var command = behavior.TimedOutCommand;
                if (command == null)
                    return;

                if (command.CanExecute(parameter))
                    command.Execute(parameter);
            };
        }
    }
}