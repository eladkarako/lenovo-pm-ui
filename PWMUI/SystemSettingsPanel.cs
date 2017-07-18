namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    internal class SystemSettingsPanel : StackPanel, IPlanSettingPlane, IDisposable
    {
        private AdvancedATMChecker advancedATMChecker;
        private AutoOddItem autoOddItem = new AutoOddItem();
        internal ComboBoxPlanItem autoOddPlanItem = new ComboBoxPlanItem();
        private BrighenessItem brighenessItem = new BrighenessItem();
        internal SliderPlanItem brightnessPlanItem = new SliderPlanItem();
        private C4Item c4Item = new C4Item();
        internal ComboBoxPlanItem c4PlanItem = new ComboBoxPlanItem();
        private CPUSpeedItem cpuSpeedItem = new CPUSpeedItem();
        internal ComboBoxPlanItem cpuSpeedPlanItem = new ComboBoxPlanItem();
        protected bool Disposed;
        public EffectOfSettingChangeEventHandler EffectOfSettingChangeEvent;
        internal ComboBoxPlanItem gfxPowerSettingsPlanItem = new ComboBoxPlanItem();
        private GpuSwitchItem gpuSwitchItem = new GpuSwitchItem();
        internal ComboBoxPlanItem gpuSwitchPlanItem = new ComboBoxPlanItem();
        private GraphicsPowerSchemeItem graphicsPowerSchemeItem = new GraphicsPowerSchemeItem();
        private List<IPlanItem> items = new List<IPlanItem>();
        private NVidiaHybridGraphicsItem nVidiaHybridItem = new NVidiaHybridGraphicsItem();
        internal ComboBoxPlanItem nVidiaHybridPlanItem = new ComboBoxPlanItem();
        private OptiSchemeItem optiSchemeItem = new OptiSchemeItem();
        private OptiSchemeItemExp optiSchemeItemExp = new OptiSchemeItemExp();
        internal ComboBoxPlanItem optiSchemePlanItem = new ComboBoxPlanItem();
        private SystemPerformanceChecker systemPerformanceChecker;
        private SystemPerformanceItem systemPerformanceItem = new SystemPerformanceItem();
        internal ComboBoxPlanItem systemPerformancePlanItem = new ComboBoxPlanItem();

        public SystemSettingsPanel()
        {
            this.items.Add(this.systemPerformancePlanItem);
            this.items.Add(this.cpuSpeedPlanItem);
            this.items.Add(this.c4PlanItem);
            this.items.Add(this.optiSchemePlanItem);
            this.items.Add(this.brightnessPlanItem);
            this.items.Add(this.nVidiaHybridPlanItem);
            this.items.Add(this.gfxPowerSettingsPlanItem);
            this.items.Add(this.autoOddPlanItem);
            this.cpuSpeedPlanItem.settingAC.SelectionChanged += new SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            this.cpuSpeedPlanItem.settingDC.SelectionChanged += new SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            this.optiSchemePlanItem.settingAC.SelectionChanged += new SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            this.optiSchemePlanItem.settingDC.SelectionChanged += new SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            this.brightnessPlanItem.sliderAC.settingSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.Slider_ValueChanged);
            this.brightnessPlanItem.sliderDC.settingSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.Slider_ValueChanged);
            this.nVidiaHybridPlanItem.settingAC.SelectionChanged += new SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            this.nVidiaHybridPlanItem.settingDC.SelectionChanged += new SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            this.gfxPowerSettingsPlanItem.settingAC.SelectionChanged += new SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            this.gfxPowerSettingsPlanItem.settingDC.SelectionChanged += new SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            this.advancedATMChecker = new AdvancedATMChecker(this.cpuSpeedPlanItem, this.optiSchemePlanItem);
            this.systemPerformanceChecker = new SystemPerformanceChecker(this.systemPerformancePlanItem, this.cpuSpeedPlanItem, this.gfxPowerSettingsPlanItem);
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
            SystemSettings mySystemSettings = activePlan.MySystemSettings;
            mySystemSettings.ValueOfCpuSpeedAC = this.cpuSpeedPlanItem.GetCurrentAC();
            mySystemSettings.ValueOfCpuSpeedDC = this.cpuSpeedPlanItem.GetCurrentDC();
            mySystemSettings.ValueOfC4AC = this.c4PlanItem.GetCurrentAC();
            mySystemSettings.ValueOfC4DC = this.c4PlanItem.GetCurrentDC();
            mySystemSettings.ValueOfOptiSchemeAC = this.optiSchemePlanItem.GetCurrentAC();
            mySystemSettings.ValueOfOptiSchemeDC = this.optiSchemePlanItem.GetCurrentDC();
            mySystemSettings.ValueOfBrightnessAC = this.brightnessPlanItem.GetCurrentAC();
            mySystemSettings.ValueOfBrightnessDC = this.brightnessPlanItem.GetCurrentDC();
            mySystemSettings.ValueOfNVidiaHybridAC = this.nVidiaHybridPlanItem.GetCurrentAC();
            mySystemSettings.ValueOfNVidiaHybridDC = this.nVidiaHybridPlanItem.GetCurrentDC();
            mySystemSettings.ValueOfGraphicsPowerSchemeAC = this.gfxPowerSettingsPlanItem.GetCurrentAC();
            mySystemSettings.ValueOfGraphicsPowerSchemeDC = this.gfxPowerSettingsPlanItem.GetCurrentDC();
            mySystemSettings.ValueOfAutoOddAC = this.autoOddPlanItem.GetCurrentAC();
            mySystemSettings.ValueOfAutoOddDC = this.autoOddPlanItem.GetCurrentDC();
            mySystemSettings.ValueOfGpuSwitchAC = this.gpuSwitchPlanItem.GetCurrentAC();
            mySystemSettings.ValueOfGpuSwitchDC = this.gpuSwitchPlanItem.GetCurrentDC();
            mySystemSettings.ValueOfSystemPerformanceAC = this.systemPerformancePlanItem.GetCurrentAC();
            mySystemSettings.ValueOfSystemPerformanceDC = this.systemPerformancePlanItem.GetCurrentDC();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.EffectOfSettingChangeEvent != null)
            {
                this.EffectOfSettingChangeEvent();
            }
        }

        public void Create(WizardPowerPlan powerPlan)
        {
            if (powerPlan.IsExport() && !this.optiSchemeItem.IsCapable())
            {
                this.optiSchemePlanItem.SetRelatedItem(this.optiSchemeItemExp);
            }
            else
            {
                this.optiSchemePlanItem.SetRelatedItem(this.optiSchemeItem);
            }
            this.optiSchemePlanItem.SetTitle("TitleOptiScheme");
            this.cpuSpeedPlanItem.SetRelatedItem(this.cpuSpeedItem);
            this.cpuSpeedPlanItem.SetTitle("TitleCPUSpeed");
            this.c4PlanItem.SetRelatedItem(this.c4Item);
            this.c4PlanItem.SetTitle("TitleC4");
            this.brightnessPlanItem.SetRelatedItem(this.brighenessItem);
            this.brightnessPlanItem.SetTitle("TitleBrightness");
            this.brightnessPlanItem.SetMinText("BrightnessSliderMin");
            this.brightnessPlanItem.SetMaxText("BrightnessSliderMax");
            this.nVidiaHybridPlanItem.SetRelatedItem(this.nVidiaHybridItem);
            this.nVidiaHybridPlanItem.SetTitle("TitleNVidiaHybrid");
            this.gfxPowerSettingsPlanItem.SetRelatedItem(this.graphicsPowerSchemeItem);
            this.gfxPowerSettingsPlanItem.SetTitle("TitleGfxPowerSettings");
            this.autoOddPlanItem.SetRelatedItem(this.autoOddItem);
            this.autoOddPlanItem.SetTitle("TitleAutoOdd");
            this.gpuSwitchPlanItem.SetRelatedItem(this.gpuSwitchItem);
            this.gpuSwitchPlanItem.SetTitle("TitleGpuSwitch");
            this.systemPerformancePlanItem.SetRelatedItem(this.systemPerformanceItem);
            this.systemPerformancePlanItem.SetTitle("TitleSystemPerformance");
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

        ~SystemSettingsPanel()
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
            SystemSettings mySystemSettings = activePlan.MySystemSettings;
            this.cpuSpeedPlanItem.SetCurrentAC(mySystemSettings.ValueOfCpuSpeedAC);
            this.cpuSpeedPlanItem.SetCurrentDC(mySystemSettings.ValueOfCpuSpeedDC);
            this.c4PlanItem.SetCurrentAC(mySystemSettings.ValueOfC4AC);
            this.c4PlanItem.SetCurrentDC(mySystemSettings.ValueOfC4DC);
            this.optiSchemePlanItem.SetCurrentAC(mySystemSettings.ValueOfOptiSchemeAC);
            this.optiSchemePlanItem.SetCurrentDC(mySystemSettings.ValueOfOptiSchemeDC);
            this.brightnessPlanItem.SetCurrentAC(mySystemSettings.ValueOfBrightnessAC);
            this.brightnessPlanItem.SetCurrentDC(mySystemSettings.ValueOfBrightnessDC);
            this.nVidiaHybridPlanItem.SetCurrentAC(mySystemSettings.ValueOfNVidiaHybridAC);
            this.nVidiaHybridPlanItem.SetCurrentDC(mySystemSettings.ValueOfNVidiaHybridDC);
            this.gfxPowerSettingsPlanItem.SetCurrentAC(mySystemSettings.ValueOfGraphicsPowerSchemeAC);
            this.gfxPowerSettingsPlanItem.SetCurrentDC(mySystemSettings.ValueOfGraphicsPowerSchemeDC);
            this.autoOddPlanItem.SetCurrentAC(mySystemSettings.ValueOfAutoOddAC);
            this.autoOddPlanItem.SetCurrentDC(mySystemSettings.ValueOfAutoOddDC);
            this.gpuSwitchPlanItem.SetCurrentAC(mySystemSettings.ValueOfGpuSwitchAC);
            this.gpuSwitchPlanItem.SetCurrentDC(mySystemSettings.ValueOfGpuSwitchDC);
            this.systemPerformancePlanItem.SetCurrentAC(mySystemSettings.ValueOfSystemPerformanceAC);
            this.systemPerformancePlanItem.SetCurrentDC(mySystemSettings.ValueOfSystemPerformanceDC);
        }

        public void ShowSettingsItem()
        {
            foreach (IPlanItem item in this.items)
            {
                item.ShowSettingsItem();
            }
        }

        private void Slider_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (this.EffectOfSettingChangeEvent != null)
            {
                this.EffectOfSettingChangeEvent();
            }
        }
    }
}

