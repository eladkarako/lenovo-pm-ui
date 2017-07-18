namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Windows.Controls;

    public class LowerRefreshRateChecker : IDisposable
    {
        protected bool Disposed;
        private ComboBoxPlanItem lowerRefreshRatePlanItem;
        private LowerRefreshRateTimeItem lowerRefreshRateTimeItem = new LowerRefreshRateTimeItem();
        private ComboBoxPlanItem lowerRefreshRateTimePlanItem;

        public LowerRefreshRateChecker(ComboBoxPlanItem lowerRefreshRateTimePlanItem, ComboBoxPlanItem lowerRefreshRatePlanItem)
        {
            this.lowerRefreshRateTimePlanItem = lowerRefreshRateTimePlanItem;
            this.lowerRefreshRatePlanItem = lowerRefreshRatePlanItem;
            lowerRefreshRateTimePlanItem.settingAC.SelectionChanged += new SelectionChangedEventHandler(this.TimeItem_SelectionChanged_AC);
            lowerRefreshRateTimePlanItem.settingDC.SelectionChanged += new SelectionChangedEventHandler(this.TimeItem_SelectionChanged_DC);
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

        ~LowerRefreshRateChecker()
        {
            this.Dispose(false);
        }

        private void TimeItem_SelectionChanged_AC(object sender, SelectionChangedEventArgs e)
        {
            uint currentValue = this.lowerRefreshRateTimePlanItem.settingAC.GetCurrentValue();
            this.lowerRefreshRatePlanItem.settingAC.IsEnabled = currentValue != 0;
        }

        private void TimeItem_SelectionChanged_DC(object sender, SelectionChangedEventArgs e)
        {
            uint currentValue = this.lowerRefreshRateTimePlanItem.settingDC.GetCurrentValue();
            this.lowerRefreshRatePlanItem.settingDC.IsEnabled = currentValue != 0;
        }
    }
}

