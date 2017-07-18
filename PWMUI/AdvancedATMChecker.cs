namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Windows.Controls;

    public class AdvancedATMChecker : IDisposable
    {
        private ComboBoxPlanItem cpuSpeedPlanItem;
        protected bool Disposed;
        private OptiSchemeItem optiSchemeItem = new OptiSchemeItem();
        private ComboBoxPlanItem optiSchemePlanItem;

        public AdvancedATMChecker(ComboBoxPlanItem cpuSpeedPlanItem, ComboBoxPlanItem optiSchemePlanItem)
        {
            this.cpuSpeedPlanItem = cpuSpeedPlanItem;
            this.optiSchemePlanItem = optiSchemePlanItem;
            cpuSpeedPlanItem.settingAC.SelectionChanged += new SelectionChangedEventHandler(this.CpuSpeed_SelectionChanged_AC);
            cpuSpeedPlanItem.settingDC.SelectionChanged += new SelectionChangedEventHandler(this.CpuSpeed_SelectionChanged_DC);
        }

        private void AddAdvencedATMItem(PMComboBox target)
        {
            target.AddSettableValue(100);
        }

        private void CheckForAdvencedATM(PMComboBox target, uint valueOfCpuSpeed)
        {
            if (this.optiSchemeItem.AdvancedATMIsCapable())
            {
                if ((valueOfCpuSpeed == 1) || (valueOfCpuSpeed == 2))
                {
                    this.AddAdvencedATMItem(target);
                }
                else
                {
                    this.DeleteAdvancedATMItem(target);
                }
            }
        }

        private void CpuSpeed_SelectionChanged_AC(object sender, SelectionChangedEventArgs e)
        {
            this.CheckForAdvencedATM(this.optiSchemePlanItem.settingAC, this.cpuSpeedPlanItem.GetCurrentAC());
        }

        private void CpuSpeed_SelectionChanged_DC(object sender, SelectionChangedEventArgs e)
        {
            this.CheckForAdvencedATM(this.optiSchemePlanItem.settingDC, this.cpuSpeedPlanItem.GetCurrentDC());
        }

        private void DeleteAdvancedATMItem(PMComboBox target)
        {
            target.DeleteSettableValue(100);
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

        ~AdvancedATMChecker()
        {
            this.Dispose(false);
        }
    }
}

