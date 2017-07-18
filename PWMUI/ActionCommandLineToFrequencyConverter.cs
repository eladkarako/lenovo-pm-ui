namespace PWMUI
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    [ValueConversion(typeof(object), typeof(ActionCommandLine))]
    public class ActionCommandLineToFrequencyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ActionCommandLine line = (ActionCommandLine) value;
            if (line == null)
            {
                throw new ArgumentException("ActionCommandLineToFrequencyConverter: ActionCommandLine is null");
            }
            Window mainWindow = Application.Current.MainWindow;
            if (mainWindow == null)
            {
                throw new ArgumentException("ActionCommandLineToFrequencyConverter: Window is null");
            }
            if (line.Frequency == eFrequency.Weekly)
            {
                string str2 = "";
                foreach (eDayOfWeek week in line.DayOfWeek)
                {
                    string str3 = "";
                    switch (week)
                    {
                        case eDayOfWeek.Sunday:
                            str3 = (string) mainWindow.TryFindResource("WeekSunday");
                            str2 = str2 + str3 + " ";
                            break;

                        case eDayOfWeek.Monday:
                            str3 = (string) mainWindow.TryFindResource("WeekMonday");
                            str2 = str2 + str3 + " ";
                            break;

                        case eDayOfWeek.Tuesday:
                            str3 = (string) mainWindow.TryFindResource("WeekTuesday");
                            str2 = str2 + str3 + " ";
                            break;

                        case eDayOfWeek.Wednesday:
                            str3 = (string) mainWindow.TryFindResource("WeekWednesday");
                            str2 = str2 + str3 + " ";
                            break;

                        case eDayOfWeek.Thursday:
                            str3 = (string) mainWindow.TryFindResource("WeekThursday");
                            str2 = str2 + str3 + " ";
                            break;

                        case eDayOfWeek.Friday:
                            str3 = (string) mainWindow.TryFindResource("WeekFriday");
                            str2 = str2 + str3 + " ";
                            break;

                        case eDayOfWeek.Saturday:
                            str3 = (string) mainWindow.TryFindResource("WeekSaturday");
                            str2 = str2 + str3 + " ";
                            break;
                    }
                }
                string str4 = (string) mainWindow.TryFindResource("FreqDescWeekly");
                return $"{str4} {str2}";
            }
            string str5 = (string) mainWindow.TryFindResource("FreqDescDaily");
            return $"{str5}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ActionCommandLineToFrequencyConverter: ConvertBack not supported");
        }
    }
}

