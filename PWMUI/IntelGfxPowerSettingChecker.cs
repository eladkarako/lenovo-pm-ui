namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Windows.Controls;

    public class IntelGfxPowerSettingChecker : IDisposable
    {
        private ComboBoxPlanItem discreteGpuTimeoutItem;
        protected bool Disposed;
        private ComboBoxPlanItem gfxPowerSettingsPlanItem;
        private GraphicsPowerSchemeItem graphicsPowerSchemeItem = new GraphicsPowerSchemeItem();
        private NVidiaHybridGraphicsItem nVidiaHybridGraphicsItem = new NVidiaHybridGraphicsItem();
        private ComboBoxPlanItem nVidiaHybridPlanItem;

        public IntelGfxPowerSettingChecker(ComboBoxPlanItem nVidiaHybridPlanItem, ComboBoxPlanItem discreteGpuTimeoutItem, ComboBoxPlanItem gfxPowerSettingsPlanItem)
        {
            this.nVidiaHybridPlanItem = nVidiaHybridPlanItem;
            this.discreteGpuTimeoutItem = discreteGpuTimeoutItem;
            this.gfxPowerSettingsPlanItem = gfxPowerSettingsPlanItem;
            nVidiaHybridPlanItem.settingAC.SelectionChanged += new SelectionChangedEventHandler(this.nVidia_SelectionChanged_AC);
            nVidiaHybridPlanItem.settingDC.SelectionChanged += new SelectionChangedEventHandler(this.nVidia_SelectionChanged_DC);
            discreteGpuTimeoutItem.settingAC.SelectionChanged += new SelectionChangedEventHandler(this.nVidia_SelectionChanged_AC);
            discreteGpuTimeoutItem.settingDC.SelectionChanged += new SelectionChangedEventHandler(this.nVidia_SelectionChanged_DC);
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

        ~IntelGfxPowerSettingChecker()
        {
            this.Dispose(false);
        }

        private void nVidia_SelectionChanged_AC(object sender, SelectionChangedEventArgs e)
        {
            if (this.nVidiaHybridGraphicsItem.IsCapable() && this.graphicsPowerSchemeItem.IsCapable())
            {
                uint currentValue = this.nVidiaHybridPlanItem.settingAC.GetCurrentValue();
                uint num2 = this.discreteGpuTimeoutItem.settingAC.GetCurrentValue();
                this.gfxPowerSettingsPlanItem.settingAC.IsEnabled = (currentValue != 1) || (num2 != 9);
            }
        }

        private void nVidia_SelectionChanged_DC(object sender, SelectionChangedEventArgs e)
        {
            if (this.nVidiaHybridGraphicsItem.IsCapable() && this.graphicsPowerSchemeItem.IsCapable())
            {
                uint currentValue = this.nVidiaHybridPlanItem.settingDC.GetCurrentValue();
                uint num2 = this.discreteGpuTimeoutItem.settingDC.GetCurrentValue();
                this.gfxPowerSettingsPlanItem.settingDC.IsEnabled = (currentValue != 1) || (num2 != 9);
            }
        }
    }
}

