namespace PWMUI
{
    using Microsoft.Win32;
    using System;
    using System.Windows;

    public class ThemeChanger
    {
        private ResourceDictionary highcontrastTheme = new ResourceDictionary();

        public ThemeChanger()
        {
            SystemEvents.UserPreferenceChanged += new UserPreferenceChangedEventHandler(this.SystemEvents_UserPreferenceChanged);
        }

        public void Change()
        {
            ResourceDictionary resources = Application.Current.Resources;
            this.highcontrastTheme.Source = new Uri("/themes/highcontrast.xaml", UriKind.Relative);
            if (this.IsHighContrast())
            {
                if (!resources.MergedDictionaries.Contains(this.highcontrastTheme))
                {
                    resources.MergedDictionaries.Add(this.highcontrastTheme);
                }
            }
            else if (resources.MergedDictionaries.Contains(this.highcontrastTheme))
            {
                resources.MergedDictionaries.Remove(this.highcontrastTheme);
            }
        }

        public bool IsHighContrast() => 
            (((SystemColors.ControlColor.ToString() == "#FF000000") && ((SystemColors.ControlTextColor.ToString() == "#FFFFFFFF") || (SystemColors.ControlTextColor.ToString() == "#FF00FF00"))) || ((SystemColors.ControlColor.ToString() == "#FFFFFFFF") && (SystemColors.ControlTextColor.ToString() == "#FF000000")));

        private void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {
            if (e.Category == UserPreferenceCategory.Color)
            {
                this.Change();
            }
        }
    }
}

