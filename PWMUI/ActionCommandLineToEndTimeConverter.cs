namespace PWMUI
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    [ValueConversion(typeof(object), typeof(ActionCommandLine))]
    public class ActionCommandLineToEndTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ActionCommandLine line = (ActionCommandLine) value;
            if (line == null)
            {
                throw new ArgumentException("ActionCommandLineToEndTimeConverter: ActionCommandLine is null");
            }
            if (Application.Current.MainWindow == null)
            {
                throw new ArgumentException("ActionCommandLineToEndTimeConverter: Window is null");
            }
            string str = $"{line.EndTime.ToString("HH:mm")}";
            if (line.StartTime == line.EndTime)
            {
                str = "-";
            }
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ActionCommandLineToEndTimeConverter: ConvertBack not supported");
        }
    }
}

