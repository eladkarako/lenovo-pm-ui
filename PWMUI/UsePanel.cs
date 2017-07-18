namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Markup;
    using System.Windows.Shapes;

    public class UsePanel : System.Windows.Controls.UserControl, IPMTabPanel, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        private AirplaneMode airplaneMode = new AirplaneMode();
        internal Button airplaneModeBtn;
        internal Rectangle airplaneModeIcon;
        internal TextBlock airplaneModeStatus;
        internal StackPanel airplaneModeStatusPanel;
        internal TextBlock airplaneModeTitle;
        internal StackPanel batteryLifePanel;
        internal TextBlock batteryLifeTime;
        internal TextBlock batteryLifeTitle;
        internal TextBlock batteryLifeUnit;
        private BatteryStretch batteryStretch = new BatteryStretch();
        internal Button batteryStretchBtn;
        internal Rectangle batteryStretchIcon;
        internal TextBlock batteryStretchStatus;
        internal StackPanel batteryStretchStatusPanel;
        internal TextBlock batteryStretchTitle;
        internal CpuMeter cpuMeter;
        internal Grid displayTrunOffPanel;
        protected bool Disposed;
        internal Grid greenConfigPanel;
        internal GreenPanel greenPanel;
        private bool IgnoreSliderPositionChangeEvent;
        private PowerUseInformer informer = new PowerUseInformer();
        private bool isRegisteredEvent;
        internal Grid LayoutRoot;
        private bool lidCloseDialogIsShowing;
        internal Grid lidClosePanel;
        internal TextBlock maxBattery;
        internal TextBlock maxPerformance;
        internal TabItem OnFocusControl;
        private uint originalSliderPosition;
        internal PerformanceStretchPanel performanceStretchPanel;
        internal Grid PowerButtonPanel;
        internal StackPanel powerUsedPanel;
        internal TextBlock powerUsedTitle;
        internal TextBlock powerUsedUnit;
        internal TextBlock powerUsedWattage;
        internal PowerUseSlider powerUseSlider;
        internal Button QuickButton_1;
        internal Button QuickButton_1_2;
        internal Button QuickButton_2;
        internal Button QuickButton_3;
        internal Button QuickButton_4;
        internal Button QuickButton_5;
        private RapidResume rapidresume = new RapidResume();
        internal Grid sleepPanel;
        internal UsePanel UserControl;

        public UsePanel()
        {
            this.InitializeComponent();
            this.powerUseSlider.SliderPositionChangeEvent += new SliderPositionChangeEventHandler(this.powerUseSliderControl_PositionChange);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void airplaneModeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.airplaneMode.SetAirplaneMode();
            this.RefreshAirplaneMode();
        }

        public bool Apply()
        {
            this.ApplySlider();
            return true;
        }

        public void ApplySlider()
        {
            this.informer.SliderPosition = (uint) this.powerUseSlider.Value;
            this.IgnoreSliderPositionChangeEvent = true;
            this.informer.Apply();
        }

        private void batteryStretchBtn_Click(object sender, RoutedEventArgs e)
        {
            this.batteryStretch.ShowSettingDialog();
            this.RefreshBatteryStretch();
        }

        private void ChangeACDCPowerSource()
        {
            this.Refresh();
        }

        public void Create()
        {
            this.informer.Refresh();
            this.originalSliderPosition = this.informer.SliderPosition;
            this.SetBatteryLifeTitle();
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

        ~UsePanel()
        {
            this.Dispose(false);
        }

        private string GetTimeUnit(string time)
        {
            uint num = 0;
            uint num2 = 0;
            Regex regex = new Regex(@"(\d+):(\d+)");
            Match match = regex.Match(time);
            if (match.Success)
            {
                num = Convert.ToUInt32(match.Groups[1].Value);
                num2 = Convert.ToUInt32(match.Groups[2].Value);
            }
            else
            {
                match = new Regex(@"(\d+)").Match(time);
                if (match.Success)
                {
                    num2 = Convert.ToUInt32(match.Groups[1].Value);
                }
            }
            string resourceKey = "UnitMinutes";
            if (num2 == 1)
            {
                resourceKey = "UnitMinute";
            }
            if (num == 1)
            {
                resourceKey = "UnitHour";
            }
            if (num > 1)
            {
                resourceKey = "UnitHours";
            }
            return (string) base.FindResource(resourceKey);
        }

        private string GetWattUnit(string wattage)
        {
            uint num = Convert.ToUInt32(wattage);
            string resourceKey = "UnitWatts";
            if (num <= 1)
            {
                resourceKey = "UnitWatt";
            }
            return (string) base.FindResource(resourceKey);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/usepanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void OnAirplaneMode(bool bStatus)
        {
            this.RefreshAirplaneMode();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.OnInitialized(e);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!this.isRegisteredEvent)
            {
                MainWindow.Instance.SettingChangeWasCanceledEvent += new SettingChangeWasCanceledEventHandler(this.SettingChangeWasCanceled);
                MainWindow.Instance.SettingChangeWasAppliedEvent += new SettingChangeWasAppliedEventHandler(this.SettingChangeWasApplied);
                MainWindow.Instance.PowerUseSliderPositionChangeEvent += new PowerUseSliderPositionChangeEventHandler(this.PowerUseSliderPositionChange);
                MainWindow.Instance.ChangeACDCPowerSourceEvent += new ChangeACDCPowerSourceEventHandler(this.ChangeACDCPowerSource);
                MainWindow.Instance.UpdateBatteryRemainingTimeEvent += new UpdateBatteryRemainingTimeEventHandler(this.UpdateBatteryRemainingTime);
                MainWindow.Instance.ChangeCalculatingRemainingBatteryEvent += new ChangeCalculatingRemainingBatteryEventHandler(this.SetBatteryLifeTitle);
                MainWindow.Instance.UpdateACPowerConsumptionEvent += new UpdateACPowerConsumptionEventHandler(this.RefreshUsedWattage);
                MainWindow.Instance.UpdateBatteryStrechEvent += new UpdateBatteryStrechEventHandler(this.RefreshBatteryStretch);
                MainWindow.Instance.UpdateAirplaneModeEvent += new UpdateAirplaneModeEventHandler(this.OnAirplaneMode);
                if (this.rapidresume.IsCapable())
                {
                    this.QuickButton_1.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.QuickButton_1_2.Visibility = Visibility.Hidden;
                }
                this.isRegisteredEvent = true;
                if (this.rapidresume.IsCapable())
                {
                    AutomationProperties.SetName(this.QuickButton_1_2, (string) base.FindResource("CaptionNeverOffSupported"));
                }
                else
                {
                    AutomationProperties.SetName(this.QuickButton_1, (string) base.FindResource("CaptionNeverOffNotSupported"));
                }
                AutomationProperties.SetName(this.QuickButton_2, (string) base.FindResource("CaptionQuickTaskSleep"));
                AutomationProperties.SetName(this.QuickButton_3, (string) base.FindResource("CaptionQuickTaskDisplay"));
                AutomationProperties.SetName(this.QuickButton_4, (string) base.FindResource("CaptionQuickTaskPowerBtn"));
                AutomationProperties.SetName(this.QuickButton_5, (string) base.FindResource("CaptionQuickTaskClimate"));
            }
        }

        private void powerUseSliderControl_PositionChange()
        {
            this.ApplySlider();
        }

        private void PowerUseSliderPositionChange()
        {
            if (this.IgnoreSliderPositionChangeEvent)
            {
                this.IgnoreSliderPositionChangeEvent = false;
            }
            else
            {
                this.informer.Refresh();
                this.powerUseSlider.IgnoreValueChangeEvent = true;
                this.RefreshSlider();
                this.SettingChangeWasApplied();
            }
        }

        private void QuickButton_1_Click(object sender, RoutedEventArgs e)
        {
            if (!this.lidCloseDialogIsShowing)
            {
                LidClosePanel control = new LidClosePanel();
                SubWindow window = new SubWindow {
                    Owner = MainWindow.Instance
                };
                window.SetCaption((string) base.FindResource("CaptionLidClose"));
                window.SetTitle((string) base.FindResource("CaptionLidClose"));
                window.SetContentArea(control);
                window.SubWindowOkClickEvent += new SubWindowOkClickEventHandler(control.OkClick);
                window.SubWindowCancelClickEvent += new SubWindowCancelClickEventHandler(control.CancelClick);
                if (this.rapidresume.IsCapable())
                {
                    window.Width = 650.0;
                    window.Height = 350.0;
                }
                this.lidCloseDialogIsShowing = true;
                window.ShowDialog();
                this.lidCloseDialogIsShowing = false;
            }
        }

        private void QuickButton_2_Click(object sender, RoutedEventArgs e)
        {
            SleepPanel control = new SleepPanel();
            SubWindow window = new SubWindow {
                Owner = MainWindow.Instance
            };
            window.SetCaption((string) base.FindResource("CaptionSleep"));
            window.SetTitle((string) base.FindResource("CaptionSleep"));
            window.SetContentArea(control);
            window.SubWindowOkClickEvent += new SubWindowOkClickEventHandler(control.OkClick);
            window.SubWindowCancelClickEvent += new SubWindowCancelClickEventHandler(control.CancelClick);
            window.ShowDialog();
        }

        private void QuickButton_3_Click(object sender, RoutedEventArgs e)
        {
            DisplayTurnOffPanel control = new DisplayTurnOffPanel();
            SubWindow window = new SubWindow {
                Owner = MainWindow.Instance
            };
            window.SetCaption((string) base.FindResource("CaptionDisplayTurnOff"));
            window.SetTitle((string) base.FindResource("CaptionDisplayTurnOff"));
            window.SetContentArea(control);
            window.SubWindowOkClickEvent += new SubWindowOkClickEventHandler(control.OkClick);
            window.SubWindowCancelClickEvent += new SubWindowCancelClickEventHandler(control.CancelClick);
            window.ShowDialog();
        }

        private void QuickButton_4_Click(object sender, RoutedEventArgs e)
        {
            PWMUI.PowerButtonPanel control = new PWMUI.PowerButtonPanel();
            SubWindow window = new SubWindow {
                Owner = MainWindow.Instance
            };
            window.SetCaption((string) base.FindResource("CaptionPowerButton"));
            window.SetTitle((string) base.FindResource("CaptionPowerButton"));
            window.SetContentArea(control);
            window.SubWindowOkClickEvent += new SubWindowOkClickEventHandler(control.OkClick);
            window.SubWindowCancelClickEvent += new SubWindowCancelClickEventHandler(control.CancelClick);
            window.ShowDialog();
        }

        private void QuickButton_5_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.pwmuiCtlDll.ShowHelpFile("PMVPUC.HTM");
        }

        public void Refresh()
        {
            this.informer.Refresh();
            this.RefreshSlider();
            this.RefreshBatteryStretch();
            this.RefreshAirplaneMode();
            this.RefreshBatteryTime();
            this.RefreshUsedWattage();
        }

        private void RefreshAirplaneMode()
        {
            if (!this.airplaneMode.IsCapable())
            {
                this.airplaneModeBtn.Visibility = Visibility.Hidden;
                this.airplaneModeIcon.Visibility = Visibility.Hidden;
                this.airplaneModeStatusPanel.Visibility = Visibility.Hidden;
            }
            else
            {
                if (this.airplaneMode.IsEnable())
                {
                    this.airplaneModeBtn.Content = (string) base.FindResource("AirplaneModeOff");
                }
                else
                {
                    this.airplaneModeBtn.Content = (string) base.FindResource("AirplaneModeOn");
                }
                this.airplaneModeStatus.Text = this.airplaneMode.GetStatus();
                this.airplaneModeBtn.Visibility = Visibility.Visible;
                this.airplaneModeIcon.Visibility = Visibility.Visible;
                this.airplaneModeStatusPanel.Visibility = Visibility.Visible;
                AutomationProperties.SetName(this.airplaneModeBtn, this.airplaneModeTitle.Text + " " + this.airplaneModeBtn.Content);
                AutomationProperties.SetName(this.airplaneModeStatus, this.airplaneModeTitle.Text + " " + this.airplaneModeStatus.Text);
            }
        }

        private void RefreshBatteryStretch()
        {
            if (!this.batteryStretch.IsCapable())
            {
                this.batteryStretchBtn.Visibility = Visibility.Hidden;
                this.batteryStretchIcon.Visibility = Visibility.Hidden;
                this.batteryStretchStatusPanel.Visibility = Visibility.Hidden;
            }
            else
            {
                this.batteryStretchBtn.Visibility = Visibility.Visible;
                this.batteryStretchIcon.Visibility = Visibility.Visible;
                this.batteryStretchStatusPanel.Visibility = Visibility.Visible;
                this.batteryStretchStatus.Text = this.batteryStretch.GetStatus();
                AutomationProperties.SetName(this.batteryStretchBtn, this.batteryStretchTitle.Text + " " + this.batteryStretchBtn.Content);
                AutomationProperties.SetName(this.batteryStretchStatus, this.batteryStretchTitle.Text + " " + this.batteryStretchStatus.Text);
            }
        }

        private void RefreshBatteryTime()
        {
            string batteryRemainingTime = this.informer.GetBatteryRemainingTime();
            if ((SystemParameters.PowerLineStatus == PowerLineStatus.Online) || (batteryRemainingTime == ""))
            {
                this.batteryLifePanel.Visibility = Visibility.Hidden;
            }
            else
            {
                this.batteryLifePanel.Visibility = Visibility.Visible;
                this.batteryLifeTime.Text = batteryRemainingTime;
                this.batteryLifeUnit.Text = this.GetTimeUnit(batteryRemainingTime);
                AutomationProperties.SetName(this.batteryLifeTime, this.batteryLifeTitle.Text + " " + this.batteryLifeTime.Text + " " + this.batteryLifeUnit.Text);
            }
        }

        private void RefreshSlider()
        {
            if (SystemParameters.PowerLineStatus == PowerLineStatus.Online)
            {
                this.maxBattery.Text = (string) base.FindResource("TitleMaxBatteryAC");
            }
            else
            {
                this.maxBattery.Text = (string) base.FindResource("TitleMaxBatteryDC");
            }
            if ((((uint) this.powerUseSlider.Value) != this.informer.SliderPosition) && !this.powerUseSlider.IsDragging)
            {
                this.powerUseSlider.Value = this.informer.SliderPosition;
            }
        }

        private void RefreshUsedWattage()
        {
            string usedWattage = this.informer.GetUsedWattage();
            if (usedWattage == "")
            {
                this.powerUsedPanel.Visibility = Visibility.Hidden;
            }
            else
            {
                this.powerUsedPanel.Visibility = Visibility.Visible;
                this.powerUsedWattage.Text = usedWattage;
                this.powerUsedUnit.Text = this.GetWattUnit(usedWattage);
                AutomationProperties.SetName(this.powerUsedWattage, this.powerUsedTitle.Text + " " + this.powerUsedWattage.Text + " " + this.powerUsedUnit.Text);
            }
        }

        private void SetBatteryLifeTitle()
        {
            if (this.informer.BatteryRemainingTimeIsCalculating())
            {
                this.batteryLifeTitle.Text = (string) base.FindResource("CalcBatteryLifeTitle");
            }
            else
            {
                this.batteryLifeTitle.Text = (string) base.FindResource("BatteryLifeTitle");
            }
        }

        private void SettingChangeWasApplied()
        {
            this.originalSliderPosition = (uint) this.powerUseSlider.Value;
        }

        private void SettingChangeWasCanceled()
        {
            this.informer.SliderPosition = this.originalSliderPosition;
            this.informer.Cancel();
        }

        public void ShowInstantResume()
        {
            RoutedEventArgs e = new RoutedEventArgs(ButtonBase.ClickEvent);
            this.QuickButton_1.RaiseEvent(e);
        }

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (UsePanel) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.OnFocusControl = (TabItem) target;
                    return;

                case 4:
                    this.lidClosePanel = (Grid) target;
                    return;

                case 5:
                    this.QuickButton_1 = (Button) target;
                    this.QuickButton_1.Click += new RoutedEventHandler(this.QuickButton_1_Click);
                    return;

                case 6:
                    this.QuickButton_1_2 = (Button) target;
                    this.QuickButton_1_2.Click += new RoutedEventHandler(this.QuickButton_1_Click);
                    return;

                case 7:
                    this.sleepPanel = (Grid) target;
                    return;

                case 8:
                    this.QuickButton_2 = (Button) target;
                    this.QuickButton_2.Click += new RoutedEventHandler(this.QuickButton_2_Click);
                    return;

                case 9:
                    this.displayTrunOffPanel = (Grid) target;
                    return;

                case 10:
                    this.QuickButton_3 = (Button) target;
                    this.QuickButton_3.Click += new RoutedEventHandler(this.QuickButton_3_Click);
                    return;

                case 11:
                    this.PowerButtonPanel = (Grid) target;
                    return;

                case 12:
                    this.QuickButton_4 = (Button) target;
                    this.QuickButton_4.Click += new RoutedEventHandler(this.QuickButton_4_Click);
                    return;

                case 13:
                    this.greenConfigPanel = (Grid) target;
                    return;

                case 14:
                    this.QuickButton_5 = (Button) target;
                    this.QuickButton_5.Click += new RoutedEventHandler(this.QuickButton_5_Click);
                    return;

                case 15:
                    this.batteryStretchIcon = (Rectangle) target;
                    return;

                case 0x10:
                    this.airplaneModeIcon = (Rectangle) target;
                    return;

                case 0x11:
                    this.powerUseSlider = (PowerUseSlider) target;
                    return;

                case 0x12:
                    this.maxPerformance = (TextBlock) target;
                    return;

                case 0x13:
                    this.maxBattery = (TextBlock) target;
                    return;

                case 20:
                    this.batteryStretchBtn = (Button) target;
                    this.batteryStretchBtn.Click += new RoutedEventHandler(this.batteryStretchBtn_Click);
                    return;

                case 0x15:
                    this.batteryStretchStatusPanel = (StackPanel) target;
                    return;

                case 0x16:
                    this.batteryStretchTitle = (TextBlock) target;
                    return;

                case 0x17:
                    this.batteryStretchStatus = (TextBlock) target;
                    return;

                case 0x18:
                    this.airplaneModeBtn = (Button) target;
                    this.airplaneModeBtn.Click += new RoutedEventHandler(this.airplaneModeBtn_Click);
                    return;

                case 0x19:
                    this.airplaneModeStatusPanel = (StackPanel) target;
                    return;

                case 0x1a:
                    this.airplaneModeTitle = (TextBlock) target;
                    return;

                case 0x1b:
                    this.airplaneModeStatus = (TextBlock) target;
                    return;

                case 0x1c:
                    this.batteryLifePanel = (StackPanel) target;
                    return;

                case 0x1d:
                    this.batteryLifeTitle = (TextBlock) target;
                    return;

                case 30:
                    this.batteryLifeTime = (TextBlock) target;
                    return;

                case 0x1f:
                    this.batteryLifeUnit = (TextBlock) target;
                    return;

                case 0x20:
                    this.powerUsedPanel = (StackPanel) target;
                    return;

                case 0x21:
                    this.powerUsedTitle = (TextBlock) target;
                    return;

                case 0x22:
                    this.powerUsedWattage = (TextBlock) target;
                    return;

                case 0x23:
                    this.powerUsedUnit = (TextBlock) target;
                    return;

                case 0x24:
                    this.cpuMeter = (CpuMeter) target;
                    return;

                case 0x25:
                    this.performanceStretchPanel = (PerformanceStretchPanel) target;
                    return;

                case 0x26:
                    this.greenPanel = (GreenPanel) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void UpdateBatteryRemainingTime()
        {
            this.RefreshBatteryTime();
            this.RefreshUsedWattage();
        }
    }
}

