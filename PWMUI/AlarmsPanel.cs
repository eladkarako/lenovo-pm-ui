namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    internal class AlarmsPanel : StackPanel, IPlanSettingPlane, IDisposable
    {
        private CriticalActionItem criticalActionItem = new CriticalActionItem();
        internal ComboBoxPlanItem criticalActionPlanItem = new ComboBoxPlanItem();
        private CriticalAtItem criticalAtItem = new CriticalAtItem();
        internal SpinPlanItem criticalAtPlanItem = new SpinPlanItem();
        internal TitlePlanItem criticalPlanItem = new TitlePlanItem();
        protected bool Disposed;
        private List<IPlanItem> items = new List<IPlanItem>();
        private LowBatteryActionItem lowBatteryActionItem = new LowBatteryActionItem();
        internal ComboBoxPlanItem lowBatteryActionPlanItem = new ComboBoxPlanItem();
        private LowBatteryAtItem lowBatteryAtItem = new LowBatteryAtItem();
        internal SpinPlanItem lowBatteryAtPlanItem = new SpinPlanItem();
        private LowBatteryNotifyItem lowBatteryNotifyItem = new LowBatteryNotifyItem();
        internal ComboBoxPlanItem lowBatteryNotifyPlanItem = new ComboBoxPlanItem();
        internal TitlePlanItem lowBatteryPlanItem = new TitlePlanItem();
        private ReserveBatteryLevelItem reserveBatteryLevelItem = new ReserveBatteryLevelItem();
        internal SpinPlanItem reserveBatteryLevelPlanItem = new SpinPlanItem();

        public AlarmsPanel()
        {
            this.items.Add(this.lowBatteryPlanItem);
            this.items.Add(this.lowBatteryAtPlanItem);
            this.items.Add(this.lowBatteryNotifyPlanItem);
            this.items.Add(this.lowBatteryActionPlanItem);
            this.items.Add(this.reserveBatteryLevelPlanItem);
            this.items.Add(this.criticalPlanItem);
            this.items.Add(this.criticalAtPlanItem);
            this.items.Add(this.criticalActionPlanItem);
        }

        private void AddPlanItem(IPlanItem planItem, bool expflag)
        {
            if (!expflag)
            {
                if (!planItem.IsCapable())
                {
                    return;
                }
                planItem.HideTitle2();
            }
            else if (!planItem.IsCapable())
            {
                if (!planItem.CanShowExport())
                {
                    return;
                }
                planItem.ShowTitle2();
            }
            else
            {
                planItem.HideTitle2();
            }
            if ((base.Children.Count % 2) == 0)
            {
                planItem.SetBackgroundToGray();
            }
            base.Children.Add((UIElement) planItem);
        }

        public void Apply(ref PowerPlan activePlan)
        {
            Alarms myAlarms = activePlan.MyAlarms;
            myAlarms.ValueOfLowBatteryAtAC = this.lowBatteryAtPlanItem.GetCurrentAC();
            myAlarms.ValueOfLowBatteryAtDC = this.lowBatteryAtPlanItem.GetCurrentDC();
            myAlarms.ValueOfLowBatteryNotfyAC = this.lowBatteryNotifyPlanItem.GetCurrentAC();
            myAlarms.ValueOfLowBatteryNotfyDC = this.lowBatteryNotifyPlanItem.GetCurrentDC();
            myAlarms.ValueOfLowBatteryActionAC = this.lowBatteryActionPlanItem.GetCurrentAC();
            myAlarms.ValueOfLowBatteryActionDC = this.lowBatteryActionPlanItem.GetCurrentDC();
            myAlarms.ValueOfCriticalAtAC = this.criticalAtPlanItem.GetCurrentAC();
            myAlarms.ValueOfCriticalAtDC = this.criticalAtPlanItem.GetCurrentDC();
            myAlarms.ValueOfCriticalActionAC = this.criticalActionPlanItem.GetCurrentAC();
            myAlarms.ValueOfCriticalActionDC = this.criticalActionPlanItem.GetCurrentDC();
            myAlarms.ValueOfReserveBatteryLevelAC = this.reserveBatteryLevelPlanItem.GetCurrentAC();
            myAlarms.ValueOfReserveBatteryLevelDC = this.reserveBatteryLevelPlanItem.GetCurrentDC();
            myAlarms.ApplyAlarmsToAllPlansFromUI = MainWindow.Instance.IsApplyAlarmsToAllPlansChecked;
        }

        public void Create(WizardPowerPlan powerPlan)
        {
            this.lowBatteryPlanItem.SetRelatedItem(this.lowBatteryAtItem);
            this.lowBatteryPlanItem.SetTitle("TitleLowBattery");
            this.lowBatteryAtPlanItem.SetRelatedItem(this.lowBatteryAtItem);
            this.lowBatteryAtPlanItem.SetTitle("TitleLowBatteryAt");
            this.lowBatteryNotifyPlanItem.SetRelatedItem(this.lowBatteryNotifyItem);
            this.lowBatteryNotifyPlanItem.SetTitle("TitleLowBatteryNotify");
            this.lowBatteryActionPlanItem.SetRelatedItem(this.lowBatteryActionItem);
            this.lowBatteryActionPlanItem.SetTitle("TitleLowBatteryAction");
            this.criticalPlanItem.SetRelatedItem(this.criticalAtItem);
            this.criticalPlanItem.SetTitle("TitleCritical");
            this.criticalAtPlanItem.SetRelatedItem(this.criticalAtItem);
            this.criticalAtPlanItem.SetTitle("TitleCriticalAt");
            this.criticalActionPlanItem.SetRelatedItem(this.criticalActionItem);
            this.criticalActionPlanItem.SetTitle("TitleCriticalAction");
            this.reserveBatteryLevelPlanItem.SetRelatedItem(this.reserveBatteryLevelItem);
            this.reserveBatteryLevelPlanItem.SetTitle("TitleReserveBatteryLevel");
            bool expflag = powerPlan.IsExport();
            foreach (IPlanItem item in this.items)
            {
                this.AddPlanItem(item, expflag);
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

        ~AlarmsPanel()
        {
            this.Dispose(false);
        }

        public void HideSettingsItem()
        {
            foreach (IPlanItem item in this.items)
            {
                item.HideSettingsItem();
            }
        }

        public void Refresh(PowerPlan activePlan)
        {
            Alarms myAlarms = activePlan.MyAlarms;
            this.lowBatteryAtPlanItem.SetCurrentAC(myAlarms.ValueOfLowBatteryAtAC);
            this.lowBatteryAtPlanItem.SetCurrentDC(myAlarms.ValueOfLowBatteryAtDC);
            this.lowBatteryNotifyPlanItem.SetCurrentAC(myAlarms.ValueOfLowBatteryNotfyAC);
            this.lowBatteryNotifyPlanItem.SetCurrentDC(myAlarms.ValueOfLowBatteryNotfyDC);
            this.lowBatteryActionPlanItem.SetCurrentAC(myAlarms.ValueOfLowBatteryActionAC);
            this.lowBatteryActionPlanItem.SetCurrentDC(myAlarms.ValueOfLowBatteryActionDC);
            this.criticalAtPlanItem.SetCurrentAC(myAlarms.ValueOfCriticalAtAC);
            this.criticalAtPlanItem.SetCurrentDC(myAlarms.ValueOfCriticalAtDC);
            this.criticalActionPlanItem.SetCurrentAC(myAlarms.ValueOfCriticalActionAC);
            this.criticalActionPlanItem.SetCurrentDC(myAlarms.ValueOfCriticalActionDC);
            this.reserveBatteryLevelPlanItem.SetCurrentAC(myAlarms.ValueOfReserveBatteryLevelAC);
            this.reserveBatteryLevelPlanItem.SetCurrentDC(myAlarms.ValueOfReserveBatteryLevelDC);
        }

        public void ShowSettingsItem()
        {
            foreach (IPlanItem item in this.items)
            {
                item.ShowSettingsItem();
            }
        }
    }
}

