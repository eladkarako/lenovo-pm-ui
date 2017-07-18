namespace PWMUI
{
    using System;
    using System.Windows;

    public class DateSelectedChangedRoutedEventArgs : RoutedEventArgs
    {
        private DateTime newDate;
        private DateTime oldDate;

        public DateSelectedChangedRoutedEventArgs(RoutedEvent routedEvent) : base(routedEvent)
        {
        }

        public DateTime NewDate
        {
            get => 
                this.newDate;
            set
            {
                this.newDate = value;
            }
        }

        public DateTime OldDate
        {
            get => 
                this.oldDate;
            set
            {
                this.oldDate = value;
            }
        }
    }
}

