namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Markup;

    public class CreatePlanWizard : System.Windows.Window, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        internal WizardStateItem advancedSettingsItem;
        internal WizardStateItem alarmsItem;
        protected bool Disposed;
        internal WizardStateItem eventsItem;
        internal Label ExportLabel;
        internal Button hlpBtn;
        internal WizardStateItem idleTimersItem;
        internal Grid LayoutRoot;
        private WizardPowerPlan newPowerPlan;
        private CreatePlanWizardStartParam startParam = new CreatePlanWizardStartParam();
        internal WizardStateItem systemSettingsItem;
        internal CreatePlanWizard Window;
        internal StackPanel wizardItem;
        internal Frame wizardPage;

        public CreatePlanWizard(WizardPowerPlan newPlan)
        {
            this.newPowerPlan = newPlan;
            this.InitializeComponent();
            this.CreateWizardPage();
            this.ExportLabel.Visibility = this.newPowerPlan.IsExport() ? Visibility.Visible : Visibility.Hidden;
            SystemSettings settings = new SystemSettings();
            IdleTimers timers = new IdleTimers();
            Events events = new Events();
            Alarms alarms = new Alarms();
            AdvancedSettings settings2 = new AdvancedSettings();
            this.systemSettingsItem.SetTitle2(this.newPowerPlan.IsExport(), settings.IsCapable());
            this.idleTimersItem.SetTitle2(this.newPowerPlan.IsExport(), timers.IsCapable());
            this.eventsItem.SetTitle2(this.newPowerPlan.IsExport(), events.IsCapable());
            this.alarmsItem.SetTitle2(this.newPowerPlan.IsExport(), alarms.IsCapable());
            this.advancedSettingsItem.SetTitle2(this.newPowerPlan.IsExport(), settings2.IsCapable());
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void CreateWizardPage()
        {
            this.startParam.CreatePlan = this.newPowerPlan;
            this.startParam.SystemSettingsState = this.systemSettingsItem;
            this.startParam.IdleTimersState = this.idleTimersItem;
            this.startParam.AdvancedSettingsState = this.advancedSettingsItem;
            this.startParam.EventsState = this.eventsItem;
            this.startParam.AlarmsState = this.alarmsItem;
            CreatePlanWizardStartPage content = new CreatePlanWizardStartPage(this.startParam);
            content.CreatePlanWizardReturnEvent += new CreatePlanWizardReturnEventHandler(this.page_CreatePlanWizardReturnEvent);
            content.CreatePlanWizardModeChangeEvent += new CreatePlanWizardModeChangeEventHandler(this.page_CreatePlanWizardModeChangeEvent);
            this.wizardPage.Navigate(content);
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

        ~CreatePlanWizard()
        {
            this.Dispose(false);
        }

        private void hlpBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.pwmuiCtlDll.ShowHelpFile("PMVPS.HTM");
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/createplanwizard.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void page_CreatePlanWizardModeChangeEvent(object sender, CreatePlanWizardModeChangeEventArguments e)
        {
            if (e.Mode == CreatePlanWizardMode.Create)
            {
                base.Title = (string) base.FindResource("CaptionCreateWizard");
            }
            else if (e.Mode == CreatePlanWizardMode.Edit)
            {
                base.Title = (string) base.FindResource("CaptionEditWizard");
            }
        }

        private void page_CreatePlanWizardReturnEvent(object sender, CreatePlanWizardReturnEventArguments e)
        {
            if (e.Result == CreatePlanWizardResult.Finished)
            {
                this.newPowerPlan.Apply();
                base.DialogResult = true;
            }
            else if (e.Result == CreatePlanWizardResult.Saved)
            {
                base.DialogResult = true;
            }
            else
            {
                base.DialogResult = false;
            }
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.Window = (CreatePlanWizard) target;
                    this.Window.KeyUp += new KeyEventHandler(this.Window_KeyUp);
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.wizardPage = (Frame) target;
                    return;

                case 4:
                    this.wizardItem = (StackPanel) target;
                    return;

                case 5:
                    this.systemSettingsItem = (WizardStateItem) target;
                    return;

                case 6:
                    this.idleTimersItem = (WizardStateItem) target;
                    return;

                case 7:
                    this.eventsItem = (WizardStateItem) target;
                    return;

                case 8:
                    this.alarmsItem = (WizardStateItem) target;
                    return;

                case 9:
                    this.advancedSettingsItem = (WizardStateItem) target;
                    return;

                case 10:
                    this.ExportLabel = (Label) target;
                    return;

                case 11:
                    this.hlpBtn = (Button) target;
                    this.hlpBtn.Click += new RoutedEventHandler(this.hlpBtn_Click);
                    return;
            }
            this._contentLoaded = true;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                MainWindow.Instance.pwmuiCtlDll.ShowHelpFile("PMVPS.HTM");
            }
        }
    }
}

