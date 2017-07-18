namespace PWMUI
{
    using System;
    using System.Windows.Controls;

    public class PMComboBoxItem : ComboBoxItem
    {
        private uint settingValue;

        public uint SettingValue
        {
            get => 
                this.settingValue;
            set
            {
                this.settingValue = value;
            }
        }
    }
}

