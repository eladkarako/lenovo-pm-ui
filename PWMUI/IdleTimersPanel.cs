namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    internal class IdleTimersPanel : StackPanel, IPlanSettingPlane, IDisposable
    {
        private DimBrighenessItem dimBrightnessItem = new DimBrighenessItem();
        internal SliderPlanItem dimBrightnessPlanItem = new SliderPlanItem();
        private DiscreteGpuTimeoutItem discreteGpuTimeoutItem = new DiscreteGpuTimeoutItem();
        internal ComboBoxPlanItem discreteGpuTimeoutPlanItem = new ComboBoxPlanItem();
        protected bool Disposed;
        private HDDTimeoutItem hDDTimeoutItem = new HDDTimeoutItem();
        internal SpinPlanItem hddTimeoutPlanItem = new SpinPlanItem();
        private HiberTimeoutItem hiberTimeoutItem = new HiberTimeoutItem();
        internal SpinPlanItem hiberTimeoutPlanItem = new SpinPlanItem();
        private List<IPlanItem> items = new List<IPlanItem>();
        private LowerBrighenessItem lowerBrightnessItem = new LowerBrighenessItem();
        internal SliderPlanItem lowerBrightnessPlanItem = new SliderPlanItem();
        private LowerBrigtnessTimeItem lowerBrightnessTimeItem = new LowerBrigtnessTimeItem();
        internal ComboBoxPlanItem lowerBrightnessTimePlanItem = new ComboBoxPlanItem();
        private LowerRefreshRateChecker lowerRefreshRateChecker;
        private LowerRefreshRateItem lowerRefreshRateItem = new LowerRefreshRateItem();
        internal ComboBoxPlanItem lowerRefreshRatePlanItem = new ComboBoxPlanItem();
        private LowerRefreshRateTimeItem lowerRefreshRateTimeItem = new LowerRefreshRateTimeItem();
        internal ComboBoxPlanItem lowerRefreshRateTimePlanItem = new ComboBoxPlanItem();
        private SuspendTimeoutItem suspendTimeoutItem = new SuspendTimeoutItem();
        internal SpinPlanItem suspendTimeoutPlanItem = new SpinPlanItem();
        private VideoDimTimeoutItem videoDimTimeoutItem = new VideoDimTimeoutItem();
        internal SpinPlanItem videoDimTimeoutPlanItem = new SpinPlanItem();
        private VideoTimeoutItem videoTimeoutItem = new VideoTimeoutItem();
        internal SpinPlanItem videoTimeoutPlanItem = new SpinPlanItem();

        public IdleTimersPanel()
        {
            this.items.Add(this.videoDimTimeoutPlanItem);
            this.items.Add(this.dimBrightnessPlanItem);
            this.items.Add(this.lowerBrightnessTimePlanItem);
            this.items.Add(this.lowerBrightnessPlanItem);
            this.items.Add(this.lowerRefreshRateTimePlanItem);
            this.items.Add(this.lowerRefreshRatePlanItem);
            this.items.Add(this.discreteGpuTimeoutPlanItem);
            this.items.Add(this.videoTimeoutPlanItem);
            this.items.Add(this.hddTimeoutPlanItem);
            this.items.Add(this.suspendTimeoutPlanItem);
            this.items.Add(this.hiberTimeoutPlanItem);
            this.lowerRefreshRateChecker = new LowerRefreshRateChecker(this.lowerRefreshRateTimePlanItem, this.lowerRefreshRatePlanItem);
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
            IdleTimers myIdleTimers = activePlan.MyIdleTimers;
            myIdleTimers.ValueOfLowerBrightnessTimeAC = this.lowerBrightnessTimePlanItem.GetCurrentAC();
            myIdleTimers.ValueOfLowerBrightnessTimeDC = this.lowerBrightnessTimePlanItem.GetCurrentDC();
            myIdleTimers.ValueOfLowerBrightnessAC = this.lowerBrightnessPlanItem.GetCurrentAC();
            myIdleTimers.ValueOfLowerBrightnessDC = this.lowerBrightnessPlanItem.GetCurrentDC();
            myIdleTimers.ValueOfLowerRefreshRateTimeAC = this.lowerRefreshRateTimePlanItem.GetCurrentAC();
            myIdleTimers.ValueOfLowerRefreshRateTimeDC = this.lowerRefreshRateTimePlanItem.GetCurrentDC();
            myIdleTimers.ValueOfLowerRefreshRateAC = this.lowerRefreshRatePlanItem.GetCurrentAC();
            myIdleTimers.ValueOfLowerRefreshRateDC = this.lowerRefreshRatePlanItem.GetCurrentDC();
            myIdleTimers.ValueOfDiscreteGpuTimeoutAC = this.discreteGpuTimeoutPlanItem.GetCurrentAC();
            myIdleTimers.ValueOfDiscreteGpuTimeoutDC = this.discreteGpuTimeoutPlanItem.GetCurrentDC();
            myIdleTimers.ValueOfVideoTimeoutAC = this.videoTimeoutPlanItem.GetCurrentAC();
            myIdleTimers.ValueOfVideoTimeoutDC = this.videoTimeoutPlanItem.GetCurrentDC();
            myIdleTimers.ValueOfHddTimeoutAC = this.hddTimeoutPlanItem.GetCurrentAC();
            myIdleTimers.ValueOfHddTimeoutDC = this.hddTimeoutPlanItem.GetCurrentDC();
            myIdleTimers.ValueOfSuspendTimeoutAC = this.suspendTimeoutPlanItem.GetCurrentAC();
            myIdleTimers.ValueOfSuspendTimeoutDC = this.suspendTimeoutPlanItem.GetCurrentDC();
            myIdleTimers.ValueOfHiberTimeoutAC = this.hiberTimeoutPlanItem.GetCurrentAC();
            myIdleTimers.ValueOfHiberTimeoutDC = this.hiberTimeoutPlanItem.GetCurrentDC();
            myIdleTimers.ValueOfDimBrightnessAC = this.dimBrightnessPlanItem.GetCurrentAC();
            myIdleTimers.ValueOfDimBrightnessDC = this.dimBrightnessPlanItem.GetCurrentDC();
            myIdleTimers.ValueOfVideoDimTimeoutAC = this.videoDimTimeoutPlanItem.GetCurrentAC();
            myIdleTimers.ValueOfVideoDimTimeoutDC = this.videoDimTimeoutPlanItem.GetCurrentDC();
        }

        public void Create(WizardPowerPlan powerPlan)
        {
            this.lowerBrightnessTimePlanItem.SetRelatedItem(this.lowerBrightnessTimeItem);
            this.lowerBrightnessTimePlanItem.SetTitle("TitleLowerBrightnessTime");
            this.lowerBrightnessPlanItem.SetRelatedItem(this.lowerBrightnessItem);
            this.lowerBrightnessPlanItem.SetTitle("TitleLowerBrightness");
            this.lowerBrightnessPlanItem.SetMinText("BrightnessSliderMin");
            this.lowerBrightnessPlanItem.SetMaxText("BrightnessSliderMax");
            this.lowerRefreshRateTimePlanItem.SetRelatedItem(this.lowerRefreshRateTimeItem);
            this.lowerRefreshRateTimePlanItem.SetTitle("TitleLowerRefreshRateTime");
            this.lowerRefreshRatePlanItem.SetRelatedItem(this.lowerRefreshRateItem);
            this.lowerRefreshRatePlanItem.SetTitle("TitleLowerRefreshRate");
            this.discreteGpuTimeoutPlanItem.SetRelatedItem(this.discreteGpuTimeoutItem);
            this.discreteGpuTimeoutPlanItem.SetTitle("TitleDiscreteGpuTimeout");
            this.videoTimeoutPlanItem.SetRelatedItem(this.videoTimeoutItem);
            this.videoTimeoutPlanItem.SetTitle("TitleVideoTimeout");
            this.hddTimeoutPlanItem.SetRelatedItem(this.hDDTimeoutItem);
            this.hddTimeoutPlanItem.SetTitle("TitleHDDTimeout");
            this.suspendTimeoutPlanItem.SetRelatedItem(this.suspendTimeoutItem);
            this.suspendTimeoutPlanItem.SetTitle("TitleSuspendTimeout");
            this.hiberTimeoutPlanItem.SetRelatedItem(this.hiberTimeoutItem);
            this.hiberTimeoutPlanItem.SetTitle("TitleHiberTimeout");
            this.dimBrightnessPlanItem.SetRelatedItem(this.dimBrightnessItem);
            this.dimBrightnessPlanItem.SetTitle("TitleDimBrightness");
            this.dimBrightnessPlanItem.SetMinText("BrightnessSliderMin");
            this.dimBrightnessPlanItem.SetMaxText("BrightnessSliderMax");
            this.videoDimTimeoutPlanItem.SetRelatedItem(this.videoDimTimeoutItem);
            this.videoDimTimeoutPlanItem.SetTitle("TitleVideoDimTimeout");
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

        ~IdleTimersPanel()
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
            IdleTimers myIdleTimers = activePlan.MyIdleTimers;
            this.lowerBrightnessTimePlanItem.SetCurrentAC(myIdleTimers.ValueOfLowerBrightnessTimeAC);
            this.lowerBrightnessTimePlanItem.SetCurrentDC(myIdleTimers.ValueOfLowerBrightnessTimeDC);
            this.lowerBrightnessPlanItem.SetCurrentAC(myIdleTimers.ValueOfLowerBrightnessAC);
            this.lowerBrightnessPlanItem.SetCurrentDC(myIdleTimers.ValueOfLowerBrightnessDC);
            this.lowerRefreshRateTimePlanItem.SetCurrentAC(myIdleTimers.ValueOfLowerRefreshRateTimeAC);
            this.lowerRefreshRateTimePlanItem.SetCurrentDC(myIdleTimers.ValueOfLowerRefreshRateTimeDC);
            this.lowerRefreshRatePlanItem.SetCurrentAC(myIdleTimers.ValueOfLowerRefreshRateAC);
            this.lowerRefreshRatePlanItem.SetCurrentDC(myIdleTimers.ValueOfLowerRefreshRateDC);
            this.discreteGpuTimeoutPlanItem.SetCurrentAC(myIdleTimers.ValueOfDiscreteGpuTimeoutAC);
            this.discreteGpuTimeoutPlanItem.SetCurrentDC(myIdleTimers.ValueOfDiscreteGpuTimeoutDC);
            this.videoTimeoutPlanItem.SetCurrentAC(myIdleTimers.ValueOfVideoTimeoutAC);
            this.videoTimeoutPlanItem.SetCurrentDC(myIdleTimers.ValueOfVideoTimeoutDC);
            this.hddTimeoutPlanItem.SetCurrentAC(myIdleTimers.ValueOfHddTimeoutAC);
            this.hddTimeoutPlanItem.SetCurrentDC(myIdleTimers.ValueOfHddTimeoutDC);
            this.suspendTimeoutPlanItem.SetCurrentAC(myIdleTimers.ValueOfSuspendTimeoutAC);
            this.suspendTimeoutPlanItem.SetCurrentDC(myIdleTimers.ValueOfSuspendTimeoutDC);
            this.hiberTimeoutPlanItem.SetCurrentAC(myIdleTimers.ValueOfHiberTimeoutAC);
            this.hiberTimeoutPlanItem.SetCurrentDC(myIdleTimers.ValueOfHiberTimeoutDC);
            this.dimBrightnessPlanItem.SetCurrentAC(myIdleTimers.ValueOfDimBrightnessAC);
            this.dimBrightnessPlanItem.SetCurrentDC(myIdleTimers.ValueOfDimBrightnessDC);
            this.videoDimTimeoutPlanItem.SetCurrentAC(myIdleTimers.ValueOfVideoDimTimeoutAC);
            this.videoDimTimeoutPlanItem.SetCurrentDC(myIdleTimers.ValueOfVideoDimTimeoutDC);
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

