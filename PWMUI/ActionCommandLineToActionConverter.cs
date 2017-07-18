namespace PWMUI
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    [ValueConversion(typeof(object), typeof(ActionCommandLine))]
    public class ActionCommandLineToActionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ActionCommandLine line = (ActionCommandLine) value;
            if (line == null)
            {
                throw new ArgumentException("ActionCommandLineToActionConverter: ActionCommandLine is null");
            }
            Window mainWindow = Application.Current.MainWindow;
            if (mainWindow == null)
            {
                throw new ArgumentException("ActionCommandLineToActionConverter: Window is null");
            }
            IdleTimerValue value2 = new IdleTimerValue();
            string str = "";
            string str2 = "";
            switch (line.Action)
            {
                case eAction.Sleep:
                case eAction.SleepImmediately:
                    str2 = (string) mainWindow.TryFindResource("ActionSleep");
                    return $"{str2}: {value2.ToString(line.IdleTimerValue)}";

                case eAction.Hibernate:
                case eAction.HibernateImmediately:
                    str2 = (string) mainWindow.TryFindResource("ActionHibernate");
                    return $"{str2}: {value2.ToString(line.IdleTimerValue)}";

                case eAction.MonitorOff:
                    str2 = (string) mainWindow.TryFindResource("ActionMonitorOff");
                    return $"{str2}: {value2.ToString(line.IdleTimerValue)}";

                case eAction.Shutdown:
                    return (string) mainWindow.TryFindResource("ActionShutdown");

                case eAction.SwitchPowerPlan:
                    if (line.PowerPlanName != null)
                    {
                        str2 = (string) mainWindow.TryFindResource("ActionDescSwitchPowerPlan");
                        str = $"{str2} {line.PowerPlanName.ToString()}";
                    }
                    return str;

                case eAction.SetBrightness:
                    if ((line.BrightnessLevel >= 0) && (line.BrightnessLevel <= 100))
                    {
                        str2 = (string) mainWindow.TryFindResource("ActionDescSetBrightness");
                        str = $"{str2} {line.BrightnessLevel.ToString()},{line.ThinkPadBrightnessLevel.ToString()}";
                    }
                    return str;

                case eAction.Peakshift:
                    return (string) mainWindow.TryFindResource("ActionPeakshift");

                case eAction.FastHibernation:
                    return (string) mainWindow.TryFindResource("ActionFastHibernation");
            }
            return str;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException("ActionCommandLineToActionConverter: ConvertBack not supported");
        }
    }
}

