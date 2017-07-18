namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Navigation;

    public class AdvancedSettingsPage : PMPageFunction, IComponentConnector
    {
        private bool _contentLoaded;
        internal AdvancedSettingsPanel advancedSettingsPanel;
        internal Button backBtn;
        internal Button cancelBtn;
        internal Button finishBtn;
        internal Grid footer;
        internal Grid LayoutRoot;
        internal Button saveBtn;
        internal Grid settingsGrid;
        private CreatePlanWizardStartParam startParam;
        internal Grid titleGrid;

        public AdvancedSettingsPage(CreatePlanWizardStartParam startParam)
        {
            this.InitializeComponent();
            this.startParam = startParam;
            this.startParam.AdvancedSettingsState.SetStateToDefault();
            this.advancedSettingsPanel.Create(this.startParam.CreatePlan);
            this.advancedSettingsPanel.Refresh(this.startParam.CreatePlan);
            if (this.startParam.CreatePlan.IsExport())
            {
                this.finishBtn.Visibility = Visibility.Hidden;
                this.saveBtn.Visibility = Visibility.Visible;
            }
            else
            {
                this.finishBtn.Visibility = Visibility.Visible;
                this.saveBtn.Visibility = Visibility.Hidden;
            }
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            this.startParam.AdvancedSettingsState.SetStateToDefault();
            this.OnReturn(new ReturnEventArgs<CreatePlanWizardResult>(CreatePlanWizardResult.Backed));
            base.NavigationService.GoBack();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.OnReturn(new ReturnEventArgs<CreatePlanWizardResult>(CreatePlanWizardResult.Canceled));
        }

        private void finishBtn_Click(object sender, RoutedEventArgs e)
        {
            this.startParam.AdvancedSettingsState.SetStateToDone();
            PowerPlan createPlan = this.startParam.CreatePlan;
            this.advancedSettingsPanel.Apply(ref createPlan);
            this.OnReturn(new ReturnEventArgs<CreatePlanWizardResult>(CreatePlanWizardResult.Finished));
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/advancedsettingspage.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            this.startParam.AdvancedSettingsState.SetStateToDone();
            PowerPlan createPlan = this.startParam.CreatePlan;
            this.advancedSettingsPanel.Apply(ref createPlan);
            this.OnReturn(new ReturnEventArgs<CreatePlanWizardResult>(CreatePlanWizardResult.Saved));
        }

        protected override void Start()
        {
            base.Start();
            this.startParam.AdvancedSettingsState.SetStateToNow();
        }

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 2:
                    this.titleGrid = (Grid) target;
                    return;

                case 3:
                    this.settingsGrid = (Grid) target;
                    return;

                case 4:
                    this.advancedSettingsPanel = (AdvancedSettingsPanel) target;
                    return;

                case 5:
                    this.footer = (Grid) target;
                    return;

                case 6:
                    this.backBtn = (Button) target;
                    this.backBtn.Click += new RoutedEventHandler(this.backBtn_Click);
                    return;

                case 7:
                    this.finishBtn = (Button) target;
                    this.finishBtn.Click += new RoutedEventHandler(this.finishBtn_Click);
                    return;

                case 8:
                    this.saveBtn = (Button) target;
                    this.saveBtn.Click += new RoutedEventHandler(this.saveBtn_Click);
                    return;

                case 9:
                    this.cancelBtn = (Button) target;
                    this.cancelBtn.Click += new RoutedEventHandler(this.cancelBtn_Click);
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

