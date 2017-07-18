namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class LowerBrightnessChecker : IDisposable
    {
        private SliderPlanItem brightnessPlanItem;
        protected bool Disposed;
        private SliderPlanItem lowerBrightnessPlanItem;
        private LowerBrigtnessTimeItem lowerBrightnessTimeItem = new LowerBrigtnessTimeItem();
        private ComboBoxPlanItem lowerBrightnessTimePlanItem;

        public LowerBrightnessChecker(SliderPlanItem brightnessPlanItem, ComboBoxPlanItem lowerBrightnessTimePlanItem, SliderPlanItem lowerBrightnessPlanItem)
        {
            this.brightnessPlanItem = brightnessPlanItem;
            this.lowerBrightnessTimePlanItem = lowerBrightnessTimePlanItem;
            this.lowerBrightnessPlanItem = lowerBrightnessPlanItem;
            brightnessPlanItem.sliderAC.settingSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.Brightness_ValueChanged_AC);
            brightnessPlanItem.sliderDC.settingSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.Brightness_ValueChanged_DC);
            lowerBrightnessTimePlanItem.settingAC.SelectionChanged += new SelectionChangedEventHandler(this.TimeItem_SelectionChanged_AC);
            lowerBrightnessTimePlanItem.settingDC.SelectionChanged += new SelectionChangedEventHandler(this.TimeItem_SelectionChanged_DC);
            lowerBrightnessPlanItem.sliderAC.settingSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.LowerBrightness_ValueChanged_AC);
            lowerBrightnessPlanItem.sliderDC.settingSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.LowerBrightness_ValueChanged_DC);
        }

        private void Brightness_ValueChanged_AC(object sender, RoutedEventArgs e)
        {
            uint currentAC = this.brightnessPlanItem.GetCurrentAC();
            uint num2 = this.lowerBrightnessPlanItem.GetCurrentAC();
            if (currentAC == 0)
            {
                this.lowerBrightnessPlanItem.SetCurrentAC(0);
                this.lowerBrightnessPlanItem.sliderAC.IsEnabled = false;
                ValueWithIndex settableValueAC = this.lowerBrightnessTimeItem.GetSettableValueAC();
                this.DeleteExceptNever(this.lowerBrightnessTimePlanItem.settingAC, settableValueAC);
                this.lowerBrightnessTimePlanItem.settingAC.SelectedIndex = 0;
            }
            else
            {
                if (currentAC <= num2)
                {
                    uint settingValue = (currentAC == 0) ? 0 : (currentAC - 1);
                    this.lowerBrightnessPlanItem.SetCurrentAC(settingValue);
                }
                this.lowerBrightnessPlanItem.sliderAC.IsEnabled = true;
                this.RestoreExceptNever(this.lowerBrightnessTimePlanItem.settingAC);
            }
        }

        private void Brightness_ValueChanged_DC(object sender, RoutedEventArgs e)
        {
            uint currentDC = this.brightnessPlanItem.GetCurrentDC();
            uint num2 = this.lowerBrightnessPlanItem.GetCurrentDC();
            if (currentDC == 0)
            {
                this.lowerBrightnessPlanItem.SetCurrentDC(0);
                this.lowerBrightnessPlanItem.sliderDC.IsEnabled = false;
                ValueWithIndex settableValueDC = this.lowerBrightnessTimeItem.GetSettableValueDC();
                this.DeleteExceptNever(this.lowerBrightnessTimePlanItem.settingDC, settableValueDC);
                this.lowerBrightnessTimePlanItem.settingDC.SelectedIndex = 0;
            }
            else
            {
                if (currentDC <= num2)
                {
                    uint settingValue = (currentDC == 0) ? 0 : (currentDC - 1);
                    this.lowerBrightnessPlanItem.SetCurrentDC(settingValue);
                }
                this.lowerBrightnessPlanItem.sliderDC.IsEnabled = true;
                this.RestoreExceptNever(this.lowerBrightnessTimePlanItem.settingDC);
            }
        }

        public void CheckManually()
        {
            this.Brightness_ValueChanged_AC(this, new RoutedEventArgs());
            this.Brightness_ValueChanged_DC(this, new RoutedEventArgs());
        }

        private void DeleteExceptNever(PMComboBox target, ValueWithIndex settableValue)
        {
            target.UserData = (int) target.GetCurrentValue();
            if (target.Items.Count != 1)
            {
                for (uint i = 0; i < settableValue.GetCount(); i++)
                {
                    uint settingValue = settableValue.GetSettingValue(i);
                    if (settingValue != 0)
                    {
                        target.DeleteSettableValue(settingValue);
                    }
                }
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
            }
            this.Disposed = true;
        }

        ~LowerBrightnessChecker()
        {
            this.Dispose(false);
        }

        private void LowerBrightness_ValueChanged_AC(object sender, RoutedEventArgs e)
        {
            uint currentAC = this.brightnessPlanItem.GetCurrentAC();
            uint num2 = this.lowerBrightnessPlanItem.GetCurrentAC();
            if (currentAC <= num2)
            {
                uint settingValue = (currentAC == 0) ? 0 : (currentAC - 1);
                this.lowerBrightnessPlanItem.SetCurrentAC(settingValue);
            }
        }

        private void LowerBrightness_ValueChanged_DC(object sender, RoutedEventArgs e)
        {
            uint currentDC = this.brightnessPlanItem.GetCurrentDC();
            uint num2 = this.lowerBrightnessPlanItem.GetCurrentDC();
            if (currentDC <= num2)
            {
                uint settingValue = (currentDC == 0) ? 0 : (currentDC - 1);
                this.lowerBrightnessPlanItem.SetCurrentDC(settingValue);
            }
        }

        private void RestoreExceptNever(PMComboBox target)
        {
            if (target.Items.Count <= 1)
            {
                target.RestoreSettableValue();
                target.SetCurrentValue((uint) target.UserData);
            }
        }

        private void TimeItem_SelectionChanged_AC(object sender, SelectionChangedEventArgs e)
        {
            uint currentValue = this.lowerBrightnessTimePlanItem.settingAC.GetCurrentValue();
            this.lowerBrightnessPlanItem.sliderAC.IsEnabled = currentValue != 0;
        }

        private void TimeItem_SelectionChanged_DC(object sender, SelectionChangedEventArgs e)
        {
            uint currentValue = this.lowerBrightnessTimePlanItem.settingDC.GetCurrentValue();
            this.lowerBrightnessPlanItem.sliderDC.IsEnabled = currentValue != 0;
        }
    }
}

