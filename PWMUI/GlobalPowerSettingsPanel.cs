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

    public class GlobalPowerSettingsPanel : System.Windows.Controls.UserControl, IPMTabPanel, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        internal Expander advancedOptionsExpander;
        internal AdvancedOptionsPanel advancedOptionsPanel;
        internal Expander alarmsExpander;
        internal GlobalAlarmsPanel alarmsPanel;
        protected bool Disposed;
        internal Expander dynamicBrightnessControlExpander;
        internal DynamicBrightnessControlPanel dynamicBrightnessControlPanel;
        internal Expander eventsExpander;
        internal GlobalEventsPanel eventsPanel;
        internal GlobalPowerSettingInformer informer = new GlobalPowerSettingInformer();
        private bool isRegisteredEvent;
        internal Grid LayoutRoot;
        internal TabItem OnFocusControl;
        internal Expander powerManagementOptionsExpander;
        internal PowerManagementOptionsPanel powerManagementOptionsPanel;
        private RapidResume rapidresume = new RapidResume();
        private int rowPosition;
        internal GlobalPowerSettingsPanel UserControl;

        public GlobalPowerSettingsPanel()
        {
            this.InitializeComponent();
        }

        public bool Apply()
        {
            this.Save();
            this.informer.Apply();
            if (this.rapidresume.IsCapable() && this.eventsPanel.checkNeverOff.IsEnabled)
            {
                bool? isChecked = this.eventsPanel.checkNeverOff.IsChecked;
                if (isChecked.HasValue ? isChecked.GetValueOrDefault() : false)
                {
                    this.rapidresume.Enable();
                }
                else
                {
                    this.rapidresume.Disable();
                }
            }
            return true;
        }

        private void applyAllPlan_Click(object sender, RoutedEventArgs e)
        {
        }

        public void Create()
        {
            this.powerManagementOptionsPanel.Create();
            this.advancedOptionsPanel.Create();
            this.dynamicBrightnessControlPanel.Create();
            this.eventsPanel.Create();
            this.alarmsPanel.Create();
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

        ~GlobalPowerSettingsPanel()
        {
            this.Dispose(false);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/globalpowersettingspanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public void LidCloseAdvancedButton_Click()
        {
            this.Save();
            this.informer.ShowAdvancedLidClosedDialog();
            this.powerManagementOptionsPanel.beepPowerCtrl.IsEnabled = this.informer.MyPowerManagementOptions.BeepPowerCtl.IsEnabled;
            this.eventsPanel.checkNeverOff.IsEnabled = this.rapidresume.IsActive();
            if (this.rapidresume.IsCapable() && !this.rapidresume.IsActive())
            {
                this.eventsPanel.checkNeverOff.IsChecked = false;
            }
        }

        private void OnChangeActivePowerScheme()
        {
            this.informer.Refresh();
            this.eventsPanel.Refresh(this.informer.MyGlobalEvents);
            this.alarmsPanel.Refresh(this.informer.MyGlobalAlarms);
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaed);
            base.OnInitialized(e);
        }

        private void OnLoaed(object sender, RoutedEventArgs e)
        {
            if (!this.isRegisteredEvent)
            {
                MainWindow.Instance.ChangeActivePowerSchemeEvent += new ChangeActivePowerSchemeEventHandler(this.OnChangeActivePowerScheme);
                this.eventsPanel.LidCloseAdvancedClickEvent += new LidCloseAdvancedClickEventHandler(this.LidCloseAdvancedButton_Click);
                this.isRegisteredEvent = true;
            }
        }

        public void Refresh()
        {
            this.informer.Refresh();
            this.rowPosition = 1;
            this.RefreshPowerManagementOptons();
            this.RefreshDynamicBrightnessControl();
            this.RefreshEvents();
            this.RefreshAlarms();
            this.RefreshAdvancedOptons();
        }

        public void RefreshAdvancedOptons()
        {
            this.advancedOptionsPanel.Refresh(this.informer.MyAdvancedOptions);
            if (this.informer.MyAdvancedOptions.IsCapable())
            {
                this.advancedOptionsExpander.Visibility = Visibility.Visible;
                Grid.SetRow(this.advancedOptionsExpander, this.rowPosition);
                this.rowPosition++;
            }
            else
            {
                this.advancedOptionsExpander.Visibility = Visibility.Collapsed;
            }
        }

        public void RefreshAlarms()
        {
            this.alarmsPanel.Refresh(this.informer.MyGlobalAlarms);
            Grid.SetRow(this.alarmsExpander, this.rowPosition);
            this.rowPosition++;
        }

        public void RefreshDynamicBrightnessControl()
        {
            this.dynamicBrightnessControlPanel.Refresh(this.informer.MyPowerManagementOptions);
            if (this.informer.MyPowerManagementOptions.DynamicBrightness.IsCapable())
            {
                this.dynamicBrightnessControlExpander.Visibility = Visibility.Visible;
                Grid.SetRow(this.dynamicBrightnessControlExpander, this.rowPosition);
                this.rowPosition++;
            }
            else
            {
                this.dynamicBrightnessControlExpander.Visibility = Visibility.Collapsed;
            }
        }

        public void RefreshEvents()
        {
            this.eventsPanel.Refresh(this.informer.MyGlobalEvents);
            Grid.SetRow(this.eventsExpander, this.rowPosition);
            this.rowPosition++;
        }

        public void RefreshPowerManagementOptons()
        {
            this.powerManagementOptionsPanel.Refresh(this.informer.MyPowerManagementOptions);
            if (this.informer.MyPowerManagementOptions.IsCapable())
            {
                this.powerManagementOptionsExpander.Visibility = Visibility.Visible;
                Grid.SetRow(this.powerManagementOptionsExpander, this.rowPosition);
                this.rowPosition++;
            }
            else
            {
                this.powerManagementOptionsExpander.Visibility = Visibility.Collapsed;
            }
        }

        private void Save()
        {
            PowerManagementOptions myPowerManagementOptions = this.informer.MyPowerManagementOptions;
            this.powerManagementOptionsPanel.Save(ref myPowerManagementOptions);
            this.dynamicBrightnessControlPanel.Save(ref myPowerManagementOptions);
            AdvancedOptions myAdvancedOptions = this.informer.MyAdvancedOptions;
            this.advancedOptionsPanel.Save(ref myAdvancedOptions);
            GlobalEvents myGlobalEvents = this.informer.MyGlobalEvents;
            this.eventsPanel.Save(ref myGlobalEvents);
            GlobalAlarms myGlobalAlarms = this.informer.MyGlobalAlarms;
            this.alarmsPanel.Save(ref myGlobalAlarms);
        }

        public void ShowInstantResume()
        {
            if (!this.eventsExpander.IsExpanded)
            {
                this.eventsExpander.IsExpanded = true;
            }
            Keyboard.Focus(this.eventsExpander);
        }

        [DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (GlobalPowerSettingsPanel) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.OnFocusControl = (TabItem) target;
                    return;

                case 4:
                    this.powerManagementOptionsExpander = (Expander) target;
                    return;

                case 5:
                    this.powerManagementOptionsPanel = (PowerManagementOptionsPanel) target;
                    return;

                case 6:
                    this.dynamicBrightnessControlExpander = (Expander) target;
                    return;

                case 7:
                    this.dynamicBrightnessControlPanel = (DynamicBrightnessControlPanel) target;
                    return;

                case 8:
                    this.eventsExpander = (Expander) target;
                    return;

                case 9:
                    this.eventsPanel = (GlobalEventsPanel) target;
                    return;

                case 10:
                    this.alarmsExpander = (Expander) target;
                    return;

                case 11:
                    this.alarmsPanel = (GlobalAlarmsPanel) target;
                    return;

                case 12:
                    this.advancedOptionsExpander = (Expander) target;
                    return;

                case 13:
                    this.advancedOptionsPanel = (AdvancedOptionsPanel) target;
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

