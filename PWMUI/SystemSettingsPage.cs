namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Navigation;

    public class SystemSettingsPage : PMPageFunction, IComponentConnector
    {
        private bool _contentLoaded;
        internal Button cancelBtn;
        internal Grid effectGrid;
        internal StackPanel fanSoundLevelAC;
        internal StackPanel fanSoundLevelDC;
        internal Grid footer;
        internal Grid LayoutRoot;
        internal Button nextBtn;
        internal StackPanel performancePanelAC;
        internal StackPanel performancePanelDC;
        internal TextBox planName;
        internal Grid planNameGrid;
        internal StackPanel powerUsageAC;
        internal StackPanel powerUsageDC;
        internal Grid settingsGrid;
        private CreatePlanWizardStartParam startParam;
        internal SystemSettingsPanel systemSettingsPanel;
        internal StackPanel systemTemperatureAC;
        internal StackPanel systemTemperatureDC;

        public event CreatePlanWizardModeChangeEventHandler CreatePlanWizardModeChangeEvent;

        public SystemSettingsPage(CreatePlanWizardStartParam startParam)
        {
            this.InitializeComponent();
            this.startParam = startParam;
            this.startParam.SystemSettingsState.SetStateToDefault();
            this.systemSettingsPanel.Create(this.startParam.CreatePlan);
            this.systemSettingsPanel.Refresh(this.startParam.CreatePlan);
            this.RefreshEffectPanel(this.startParam.CreatePlan);
            this.systemSettingsPanel.EffectOfSettingChangeEvent = (EffectOfSettingChangeEventHandler) Delegate.Combine(this.systemSettingsPanel.EffectOfSettingChangeEvent, new EffectOfSettingChangeEventHandler(this.EffectOfSettingChangeEvent));
            this.planName.MaxLength = (int) this.startParam.CreatePlan.GetMaxLengthOfName();
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.OnReturn(new ReturnEventArgs<CreatePlanWizardResult>(CreatePlanWizardResult.Canceled));
        }

        private void EffectOfSettingChangeEvent()
        {
            PowerPlan createPlan = this.startParam.CreatePlan;
            this.systemSettingsPanel.Apply(ref createPlan);
            createPlan.Refresh();
            this.RefreshEffectPanel(this.startParam.CreatePlan);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/systemsettingspage.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            PowerPlan createPlan = this.startParam.CreatePlan;
            createPlan.Name = this.planName.Text;
            if (!this.startParam.CreatePlan.NameIsValid())
            {
                this.planName.Focus();
            }
            else
            {
                if (this.startParam.CreatePlan.NameIsExist() && (this.CreatePlanWizardModeChangeEvent != null))
                {
                    this.CreatePlanWizardModeChangeEvent(this, new CreatePlanWizardModeChangeEventArguments(CreatePlanWizardMode.Edit));
                }
                this.startParam.SystemSettingsState.SetStateToDone();
                this.systemSettingsPanel.Apply(ref createPlan);
                IdleTimersPage root = new IdleTimersPage(this.startParam, this.systemSettingsPanel.brightnessPlanItem);
                root.Return += new ReturnEventHandler<CreatePlanWizardResult>(this.page_Return);
                base.NavigationService.Navigate(root);
            }
        }

        private void page_Return(object sender, ReturnEventArgs<CreatePlanWizardResult> e)
        {
            if (((CreatePlanWizardResult) e.Result) == CreatePlanWizardResult.Backed)
            {
                this.startParam.SystemSettingsState.SetStateToNow();
                base.SetFirstFocus();
                if (this.CreatePlanWizardModeChangeEvent != null)
                {
                    this.CreatePlanWizardModeChangeEvent(this, new CreatePlanWizardModeChangeEventArguments(CreatePlanWizardMode.Create));
                }
            }
            else
            {
                this.OnReturn(e);
            }
        }

        private void planName_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.nextBtn.IsEnabled = this.planName.Text.Length != 0;
        }

        private void RefreshEffectPanel(PowerPlan createPlan)
        {
            EffectImage image = new EffectImage();
            this.performancePanelDC.Children.Clear();
            image.AddToPanel(this.performancePanelDC, createPlan.MyEffectSettings.PerformanceDC);
            this.performancePanelAC.Children.Clear();
            image.AddToPanel(this.performancePanelAC, createPlan.MyEffectSettings.PerformanceAC);
            this.systemTemperatureDC.Children.Clear();
            image.AddToPanel(this.systemTemperatureDC, createPlan.MyEffectSettings.SystemTempratureDC);
            this.systemTemperatureAC.Children.Clear();
            image.AddToPanel(this.systemTemperatureAC, createPlan.MyEffectSettings.SystemTempratureAC);
            this.fanSoundLevelDC.Children.Clear();
            image.AddToPanel(this.fanSoundLevelDC, createPlan.MyEffectSettings.FanSoundLevelDC);
            this.fanSoundLevelAC.Children.Clear();
            image.AddToPanel(this.fanSoundLevelAC, createPlan.MyEffectSettings.FanSoundLevelAC);
            this.powerUsageDC.Children.Clear();
            image.AddToPanel(this.powerUsageDC, createPlan.MyEffectSettings.PowerUsageDC);
            this.powerUsageAC.Children.Clear();
            image.AddToPanel(this.powerUsageAC, createPlan.MyEffectSettings.PowerUsageAC);
        }

        protected override void Start()
        {
            base.Start();
            this.startParam.SystemSettingsState.SetStateToNow();
            this.nextBtn.IsEnabled = false;
            if (this.startParam.CreatePlan.IsExport())
            {
                this.nextBtn.IsEnabled = true;
                this.planName.Text = this.startParam.CreatePlan.Name;
            }
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 2:
                    this.planNameGrid = (Grid) target;
                    return;

                case 3:
                    this.planName = (TextBox) target;
                    this.planName.TextChanged += new TextChangedEventHandler(this.planName_TextChanged);
                    return;

                case 4:
                    this.effectGrid = (Grid) target;
                    return;

                case 5:
                    this.performancePanelDC = (StackPanel) target;
                    return;

                case 6:
                    this.performancePanelAC = (StackPanel) target;
                    return;

                case 7:
                    this.systemTemperatureDC = (StackPanel) target;
                    return;

                case 8:
                    this.systemTemperatureAC = (StackPanel) target;
                    return;

                case 9:
                    this.fanSoundLevelDC = (StackPanel) target;
                    return;

                case 10:
                    this.fanSoundLevelAC = (StackPanel) target;
                    return;

                case 11:
                    this.powerUsageDC = (StackPanel) target;
                    return;

                case 12:
                    this.powerUsageAC = (StackPanel) target;
                    return;

                case 13:
                    this.settingsGrid = (Grid) target;
                    return;

                case 14:
                    this.systemSettingsPanel = (SystemSettingsPanel) target;
                    return;

                case 15:
                    this.footer = (Grid) target;
                    return;

                case 0x10:
                    this.nextBtn = (Button) target;
                    this.nextBtn.Click += new RoutedEventHandler(this.nextBtn_Click);
                    return;

                case 0x11:
                    this.cancelBtn = (Button) target;
                    this.cancelBtn.Click += new RoutedEventHandler(this.cancelBtn_Click);
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

