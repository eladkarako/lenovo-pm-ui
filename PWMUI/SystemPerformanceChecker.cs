namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Windows.Controls;

    public class SystemPerformanceChecker : IDisposable
    {
        private SystemPerformanceConverter converter = new SystemPerformanceConverter();
        private ComboBoxPlanItem cpuSpeedPlanItem;
        protected bool Disposed;
        private ComboBoxPlanItem gfxPowerSettingsPlanItem;
        private SystemPerformanceItem systemPerformanceItem = new SystemPerformanceItem();
        private ComboBoxPlanItem systemPerformancePlanItem;

        public SystemPerformanceChecker(ComboBoxPlanItem systemPerformancePlanItem, ComboBoxPlanItem cpuSpeedPlanItem, ComboBoxPlanItem gfxPowerSettingsPlanItem)
        {
            this.systemPerformancePlanItem = systemPerformancePlanItem;
            this.cpuSpeedPlanItem = cpuSpeedPlanItem;
            this.gfxPowerSettingsPlanItem = gfxPowerSettingsPlanItem;
            systemPerformancePlanItem.settingAC.SelectionChanged += new SelectionChangedEventHandler(this.SystemPerformance_SelectionChanged_AC);
            systemPerformancePlanItem.settingDC.SelectionChanged += new SelectionChangedEventHandler(this.SystemPerformance_SelectionChanged_DC);
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

        ~SystemPerformanceChecker()
        {
            this.Dispose(false);
        }

        private void SystemPerformance_SelectionChanged_AC(object sender, SelectionChangedEventArgs e)
        {
            if (this.systemPerformanceItem.IsCapable())
            {
                this.converter.SystemPerformance = this.systemPerformancePlanItem.settingAC.GetCurrentValue();
                this.converter.ToPerformanceParameters();
                this.cpuSpeedPlanItem.settingAC.SetCurrentValue(this.converter.CpuSpeed);
                this.gfxPowerSettingsPlanItem.settingAC.SetCurrentValue(this.converter.GfxPowerPlan);
            }
        }

        private void SystemPerformance_SelectionChanged_DC(object sender, SelectionChangedEventArgs e)
        {
            if (this.systemPerformanceItem.IsCapable())
            {
                this.converter.SystemPerformance = this.systemPerformancePlanItem.settingDC.GetCurrentValue();
                this.converter.ToPerformanceParameters();
                this.cpuSpeedPlanItem.settingDC.SetCurrentValue(this.converter.CpuSpeed);
                this.gfxPowerSettingsPlanItem.settingDC.SetCurrentValue(this.converter.GfxPowerPlan);
            }
        }
    }
}

