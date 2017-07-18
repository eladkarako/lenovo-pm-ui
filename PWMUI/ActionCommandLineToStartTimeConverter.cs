namespace PWMUI
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    [ValueConversion(typeof(object), typeof(ActionCommandLine))]
    public class ActionCommandLineToStartTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ActionCommandLine line = (ActionCommandLine) value;
            if (line == null)
            {
                throw new ArgumentException("ActionCommandLineToStartTimeConverter: ActionCommandLine is null");
            }
            if (Application.Current.MainWindow == null)
            {
                throw new ArgumentException("ActionCommandLineToStartTimeConverter: Window is null");
            }
            return $"{line.StartTime.ToString("HH:mm")}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ActionCommandLineToStartTimeConverter: ConvertBack not supported");
        }
    }
}

