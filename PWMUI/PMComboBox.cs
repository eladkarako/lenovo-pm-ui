namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Collections;
    using System.Windows;
    using System.Windows.Controls;

    public class PMComboBox : ComboBox, IPMControl
    {
        private uint currentSettingValue = uint.MaxValue;
        private ValueWithIndex settableValue;
        private int userData;

        public PMComboBox()
        {
            base.ToolTip = new ToolTip();
        }

        public void AddSettableValue(uint settingValue)
        {
            foreach (object obj2 in (IEnumerable) base.Items)
            {
                if (((PMComboBoxItem) obj2).SettingValue == settingValue)
                {
                    return;
                }
            }
            uint index = this.settableValue.GetIndex(settingValue);
            if (index != uint.MaxValue)
            {
                PMComboBoxItem newItem = new PMComboBoxItem {
                    SettingValue = this.settableValue.GetSettingValue(index),
                    Content = this.settableValue.GetText(index)
                };
                base.Items.Add(newItem);
            }
        }

        public void DeleteSettableValue(uint settingValue)
        {
            foreach (object obj2 in (IEnumerable) base.Items)
            {
                PMComboBoxItem removeItem = (PMComboBoxItem) obj2;
                if (removeItem.SettingValue == settingValue)
                {
                    if (base.SelectedItem == obj2)
                    {
                        base.SelectedIndex = 0;
                    }
                    base.Items.Remove(removeItem);
                    break;
                }
            }
        }

        public void EnableApplyButton()
        {
            MainWindow.Instance.EnableApplyButton();
        }

        public uint GetCurrentValue()
        {
            if (base.SelectedIndex < 0)
            {
                return this.currentSettingValue;
            }
            return ((PMComboBoxItem) base.SelectedItem).SettingValue;
        }

        private void NotifyMainWindow(object sender, SelectionChangedEventArgs e)
        {
            if (base.IsVisible)
            {
                this.EnableApplyButton();
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.SelectionChanged += new SelectionChangedEventHandler(this.NotifyMainWindow);
            base.ToolTipOpening += new ToolTipEventHandler(this.PMComboBox_ToolTipOpening);
            base.OnInitialized(e);
        }

        private void PMComboBox_ToolTipOpening(object sender, ToolTipEventArgs e)
        {
            ToolTip toolTip = (ToolTip) base.ToolTip;
            toolTip.Content = base.Text;
            toolTip.Visibility = Visibility.Hidden;
            toolTip.IsOpen = true;
            double actualWidth = toolTip.ActualWidth;
            toolTip.IsOpen = false;
            if (actualWidth < (base.Width - 5.0))
            {
                e.Handled = true;
            }
            else
            {
                toolTip.Visibility = Visibility.Visible;
            }
        }

        public void RestoreSettableValue()
        {
            base.Items.Clear();
            for (uint i = 0; i < this.settableValue.GetCount(); i++)
            {
                PMComboBoxItem newItem = new PMComboBoxItem {
                    SettingValue = this.settableValue.GetSettingValue(i),
                    Content = this.settableValue.GetText(i)
                };
                base.Items.Add(newItem);
            }
        }

        public void SetCurrentValue(uint settingValue)
        {
            this.currentSettingValue = settingValue;
            foreach (object obj2 in (IEnumerable) base.Items)
            {
                PMComboBoxItem item = (PMComboBoxItem) obj2;
                if (item.SettingValue == settingValue)
                {
                    item.IsSelected = true;
                    break;
                }
            }
        }

        public void SetSettableValue(ValueWithIndex settableValue)
        {
            this.settableValue = settableValue;
            for (uint i = 0; i < settableValue.GetCount(); i++)
            {
                PMComboBoxItem newItem = new PMComboBoxItem {
                    SettingValue = settableValue.GetSettingValue(i),
                    Content = settableValue.GetText(i)
                };
                base.Items.Add(newItem);
                if (i == 0)
                {
                    newItem.IsSelected = true;
                }
            }
        }

        public int UserData
        {
            get => 
                this.userData;
            set
            {
                this.userData = value;
            }
        }
    }
}

