namespace PWMUI
{
    using System;
    using System.Windows;

    public class TimeSelectedChangedRoutedEventArgs : RoutedEventArgs
    {
        private TimeSpan newTime;
        private TimeSpan oldTime;

        public TimeSelectedChangedRoutedEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public TimeSpan NewTime
        {
            get => 
                this.newTime;
            set
            {
                this.newTime = value;
            }
        }

        public TimeSpan OldTime
        {
            get => 
                this.oldTime;
            set
            {
                this.oldTime = value;
            }
        }
    }
}

