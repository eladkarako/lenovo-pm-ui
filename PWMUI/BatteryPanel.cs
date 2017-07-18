namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Threading;

    public class BatteryPanel : System.Windows.Controls.UserControl, IPMTabPanel, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        internal BatteryGauge batteryGauge;
        internal TextBlock batteryMessage;
        internal BatteryTime batteryTime;
        internal DispatcherTimer batteryUpdateTimer = new DispatcherTimer();
        internal PMCheckBox checkLongBatteryHealthMode;
        internal BatteryDetailsPanel detailsPanel;
        protected bool Disposed;
        internal Button firmwareUpdateBtn;
        private BatteryInformer informer = new BatteryInformer();
        private bool isRegisteredEvent;
        internal Grid LayoutRoot;
        internal PMLinkTextBlock learnAboutBattery;
        internal BatteryConditionItem mainBattery;
        internal Button maintenanceBtn;
        internal TabItem OnFocusControl;
        private PowerUseInformer pwrInformer = new PowerUseInformer();
        private bool refreshIsCompleted = true;
        internal BatteryConditionItem secondBattery;
        private bool sessionIsConnected = true;
        internal TextBlock titleOfGauge;
        internal TextBlock titleOfTime;
        internal ToolTip toolTipOfHealthMode;
        internal BatteryPanel UserControl;

        public BatteryPanel()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        public bool Apply()
        {
            bool? isChecked = this.checkLongBatteryHealthMode.IsChecked;
            this.informer.LongBatteryHealthMode.IsChecked = isChecked.HasValue ? isChecked.GetValueOrDefault() : false;
            this.informer.Apply();
            return true;
        }

        private void checkLongBatteryHealthMode_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.toolTipOfHealthMode.PlacementTarget = this.checkLongBatteryHealthMode;
            this.toolTipOfHealthMode.Placement = PlacementMode.Relative;
            this.toolTipOfHealthMode.HorizontalOffset = 15.0;
            this.toolTipOfHealthMode.VerticalOffset = 15.0;
            this.toolTipOfHealthMode.IsOpen = true;
        }

        private void checkLongBatteryHealthMode_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            this.toolTipOfHealthMode.IsOpen = false;
            this.toolTipOfHealthMode.Placement = PlacementMode.Mouse;
            this.toolTipOfHealthMode.HorizontalOffset = 0.0;
            this.toolTipOfHealthMode.VerticalOffset = 0.0;
        }

        public void Create()
        {
            PMSystemBuildID did = new PMSystemBuildID();
            if (did.IsConvertible())
            {
                this.mainBattery.SetTitle("TitleTabletBattery");
                this.secondBattery.SetTitle("TitleBaseBattery");
            }
            else
            {
                if (this.informer.MainBattery.IsInstalled && this.informer.MainBattery.IsCS13Battery)
                {
                    if (this.informer.MainBattery.IsNonReplaceable)
                    {
                        this.mainBattery.SetTitle("TitleBuiltInBattery");
                    }
                    else
                    {
                        this.mainBattery.SetTitle("TitleRemovableBattery");
                    }
                }
                else
                {
                    this.mainBattery.SetTitle("TitleMainBattery");
                }
                if (this.informer.SecondBattery.IsInstalled && this.informer.SecondBattery.IsCS13Battery)
                {
                    if (this.informer.SecondBattery.IsNonReplaceable)
                    {
                        this.secondBattery.SetTitle("TitleBuiltInBattery");
                    }
                    else
                    {
                        this.secondBattery.SetTitle("TitleRemovableBattery");
                    }
                }
                else
                {
                    this.secondBattery.SetTitle("TitleSecondBattery");
                }
                if ((this.informer.MainBattery.IsInstalled && this.informer.MainBattery.IsCS13Battery) && (this.informer.SecondBattery.IsInstalled && this.informer.SecondBattery.IsCS13Battery))
                {
                    this.mainBattery.SetPrefixToTitleString("1 - ");
                    this.secondBattery.SetPrefixToTitleString("2 - ");
                }
            }
            this.detailsPanel.Create();
            this.batteryUpdateTimer.Interval = new TimeSpan(0, 0, 0, (int) (this.informer.TimerElapse / 0x3e8));
            this.batteryUpdateTimer.Tick += new EventHandler(this.OnBatteryUpdateTimer);
            this.batteryUpdateTimer.Start();
            this.SetActiveBattery();
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
                this.batteryUpdateTimer.Stop();
                this.informer.EndBatteryStatusUpdateThread();
            }
            this.Disposed = true;
        }

        ~BatteryPanel()
        {
            this.Dispose(false);
        }

        private void firmwareUpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            this.informer.ShowFirmwareUpdateDialog();
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/batterypanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private bool IsBaseToTabletBatteryCharging()
        {
            PMSystemBuildID did = new PMSystemBuildID();
            if (!did.IsConvertible())
            {
                return false;
            }
            if (!this.informer.MainBattery.IsInstalled || !this.informer.SecondBattery.IsInstalled)
            {
                return false;
            }
            if (this.informer.IsACAttached())
            {
                return false;
            }
            if (this.informer.SecondBattery.Status.MyStatus == BatteryStatus.Status.charge)
            {
                return false;
            }
            if (this.informer.MainBattery.Status.MyStatus != BatteryStatus.Status.charge)
            {
                return false;
            }
            return true;
        }

        private void learnAboutBattery_LinkTextClickedEvent()
        {
            this.informer.ShowLearnAboutBatteryDialog();
        }

        private void mainBattery_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Space) || (e.Key == Key.Return))
            {
                this.informer.SelectBattery = BatteryNo.main;
                this.SetActiveBattery();
                this.RefreshBatteryCondition();
            }
        }

        private void mainBattery_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.informer.SelectBattery = BatteryNo.main;
            this.SetActiveBattery();
            this.RefreshBatteryCondition();
        }

        private void maintenanceBtn_Click(object sender, RoutedEventArgs e)
        {
            this.informer.ShowMaintenanceDialog();
        }

        private void OnBatteryUpdateTimer(object sender, EventArgs e)
        {
            this.OnPowerStatusChange();
            this.informer.UpdateBackgroundMonitor();
        }

        public void OnGetBatteryStatus()
        {
            this.informer.Refresh();
            this.Refresh();
            this.refreshIsCompleted = true;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaed);
            this.informer.StartBatteryStatusUpdateThread();
            base.OnInitialized(e);
        }

        private void OnLoaed(object sender, RoutedEventArgs e)
        {
            if (!this.isRegisteredEvent)
            {
                MainWindow.Instance.PowerStatusChangeEvent += new PowerStatusChangeEventHandler(this.OnPowerStatusChange);
                MainWindow.Instance.ResumeEvent += new ResumeEventHandler(this.OnResume);
                MainWindow.Instance.SuspendEvent += new SuspendEventHandler(this.OnSuspend);
                MainWindow.Instance.WtsConsoleConnectEvent += new WtsConsoleConnectEventHandler(this.OnWtsConsoleConnect);
                MainWindow.Instance.WtsConsoleDisconnectEvent += new WtsConsoleDisconnectEventHandler(this.OnWtsConsoleDisconnect);
                MainWindow.Instance.UpdateBatteryRemainingTimeEvent += new UpdateBatteryRemainingTimeEventHandler(this.OnGetBatteryStatus);
                MainWindow.Instance.ChangeCalculatingRemainingBatteryEvent += new ChangeCalculatingRemainingBatteryEventHandler(this.OnGetBatteryStatus);
                this.isRegisteredEvent = true;
            }
        }

        public void OnPowerStatusChange()
        {
            if (this.sessionIsConnected && this.refreshIsCompleted)
            {
                this.informer.RunBatteryStatusUpdateThread();
                this.refreshIsCompleted = false;
            }
        }

        public void OnResume()
        {
            this.batteryUpdateTimer.Start();
        }

        public void OnSuspend()
        {
            this.batteryUpdateTimer.Stop();
        }

        public void OnWtsConsoleConnect()
        {
            this.sessionIsConnected = true;
        }

        public void OnWtsConsoleDisconnect()
        {
            this.sessionIsConnected = false;
        }

        public void Refresh()
        {
            this.RefreshBatterySetting();
            this.RefreshBatteryCondition();
            this.RefreshBatteryGauge();
            this.RefreshBatteryTime();
            this.RefreshBatteryMessage();
            this.RefreshMode();
        }

        private void RefreshBatteryCondition()
        {
            this.mainBattery.SetCondition(this.informer.MainBattery);
            this.secondBattery.SetCondition(this.informer.SecondBattery);
            if (this.informer.SelectBattery == BatteryNo.main)
            {
                this.detailsPanel.Refresh(this.informer.MainBattery, this.IsBaseToTabletBatteryCharging());
            }
            if (this.informer.SelectBattery == BatteryNo.second)
            {
                this.detailsPanel.Refresh(this.informer.SecondBattery, this.IsBaseToTabletBatteryCharging());
            }
        }

        private void RefreshBatteryGauge()
        {
            this.titleOfGauge.Text = this.informer.TitleOfGauge;
            this.batteryGauge.Refresh(this.informer);
        }

        private void RefreshBatteryMessage()
        {
            this.batteryMessage.Text = this.informer.MessageOfBattery;
        }

        private void RefreshBatterySetting()
        {
            if (this.informer.LearnAboutBatteryIsCapable)
            {
                this.learnAboutBattery.Visibility = Visibility.Visible;
            }
            else
            {
                this.learnAboutBattery.Visibility = Visibility.Hidden;
            }
        }

        private void RefreshBatteryTime()
        {
            if ((SystemParameters.PowerLineStatus != PowerLineStatus.Online) && this.pwrInformer.BatteryRemainingTimeIsCalculating())
            {
                this.titleOfTime.Text = (string) base.FindResource("CalculatingBatteryLifeTitle");
                this.titleOfTime.Visibility = Visibility.Visible;
            }
            else if ((this.informer.Status.MyStatus == BatteryStatus.Status.charge) || this.informer.CompletionTime.IsCapable())
            {
                this.titleOfTime.Text = (string) base.FindResource("TimeToFullChargeTitle");
                this.titleOfTime.Visibility = Visibility.Visible;
            }
            else
            {
                this.titleOfTime.Visibility = Visibility.Hidden;
            }
            this.batteryTime.Refresh(this.informer);
        }

        private void RefreshMode()
        {
            if (MainWindow.Instance.IsAdvanced())
            {
                this.checkLongBatteryHealthMode.Visibility = Visibility.Hidden;
                if (this.informer.BatteryMaintenaceIsCapable)
                {
                    this.maintenanceBtn.Visibility = Visibility.Visible;
                    this.maintenanceBtn.IsEnabled = this.informer.BatteryMaintenaceIsEnable;
                }
                else
                {
                    this.maintenanceBtn.Visibility = Visibility.Hidden;
                }
                if (this.informer.IsFirmwareUpdateCapable)
                {
                    this.firmwareUpdateBtn.Visibility = Visibility.Visible;
                    this.firmwareUpdateBtn.IsEnabled = this.informer.IsFirmwareUpdateEnable;
                }
                else
                {
                    this.firmwareUpdateBtn.Visibility = Visibility.Hidden;
                }
            }
            if (MainWindow.Instance.IsBasic())
            {
                this.maintenanceBtn.Visibility = Visibility.Hidden;
                this.firmwareUpdateBtn.Visibility = Visibility.Hidden;
                if (this.informer.LongBatteryHealthMode.IsCapable())
                {
                    this.checkLongBatteryHealthMode.Visibility = Visibility.Visible;
                    this.checkLongBatteryHealthMode.IsEnabled = this.informer.BatteryMaintenaceIsEnable;
                }
                else
                {
                    this.checkLongBatteryHealthMode.Visibility = Visibility.Hidden;
                }
            }
            this.checkLongBatteryHealthMode.IsChecked = new bool?(this.informer.LongBatteryHealthMode.IsChecked);
        }

        private void secondBattery_KeyUp(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.Space) || (e.Key == Key.Return))
            {
                this.informer.SelectBattery = BatteryNo.second;
                this.SetActiveBattery();
                this.RefreshBatteryCondition();
            }
        }

        private void secondBattery_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.informer.SelectBattery = BatteryNo.second;
            this.SetActiveBattery();
            this.RefreshBatteryCondition();
        }

        private void SetActiveBattery()
        {
            if (this.informer.SelectBattery == BatteryNo.main)
            {
                this.mainBattery.SetActiveBackGround();
                this.secondBattery.SetDefaultBackground();
            }
            if (this.informer.SelectBattery == BatteryNo.second)
            {
                this.mainBattery.SetDefaultBackground();
                this.secondBattery.SetActiveBackGround();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (BatteryPanel) target;
                    this.UserControl.MouseLeftButtonDown += new MouseButtonEventHandler(this.UserControl_MouseLeftButtonDown);
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.OnFocusControl = (TabItem) target;
                    return;

                case 4:
                    this.titleOfGauge = (TextBlock) target;
                    return;

                case 5:
                    this.batteryGauge = (BatteryGauge) target;
                    return;

                case 6:
                    this.titleOfTime = (TextBlock) target;
                    return;

                case 7:
                    this.batteryTime = (BatteryTime) target;
                    return;

                case 8:
                    this.batteryMessage = (TextBlock) target;
                    return;

                case 9:
                    this.maintenanceBtn = (Button) target;
                    this.maintenanceBtn.Click += new RoutedEventHandler(this.maintenanceBtn_Click);
                    return;

                case 10:
                    this.firmwareUpdateBtn = (Button) target;
                    this.firmwareUpdateBtn.Click += new RoutedEventHandler(this.firmwareUpdateBtn_Click);
                    return;

                case 11:
                    this.checkLongBatteryHealthMode = (PMCheckBox) target;
                    return;

                case 12:
                    this.toolTipOfHealthMode = (ToolTip) target;
                    return;

                case 13:
                    this.learnAboutBattery = (PMLinkTextBlock) target;
                    return;

                case 14:
                    this.mainBattery = (BatteryConditionItem) target;
                    return;

                case 15:
                    this.secondBattery = (BatteryConditionItem) target;
                    return;

                case 0x10:
                    this.detailsPanel = (BatteryDetailsPanel) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.toolTipOfHealthMode.IsOpen)
            {
                this.toolTipOfHealthMode.IsOpen = false;
                this.toolTipOfHealthMode.Placement = PlacementMode.Mouse;
                this.toolTipOfHealthMode.HorizontalOffset = 0.0;
                this.toolTipOfHealthMode.VerticalOffset = 0.0;
            }
        }
    }
}

