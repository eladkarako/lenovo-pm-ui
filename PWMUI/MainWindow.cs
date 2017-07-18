namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Forms;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System.Windows.Threading;

    public sealed class MainWindow : Window, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        public readonly int _osVersion = Environment.OSVersion.Version.Major;
        private IntPtr acdcPowerSourceNotify = IntPtr.Zero;
        public const string ACTIVATE_AIRPLANE_MODE = @"Global\PwrmgrActivateAirplaneModeEvent";
        public const string ACTIVATE_BATTERY_STRETCH = "PwrmgrActivateBatteryStretchEvent";
        public EventWaitHandle activateAirplaneModeEvent = new EventWaitHandle(false, EventResetMode.ManualReset, @"Global\PwrmgrActivateAirplaneModeEvent");
        public EventWaitHandle activateBatteryStretchEvent = new EventWaitHandle(false, EventResetMode.ManualReset, "PwrmgrActivateBatteryStretchEvent");
        private IntPtr activePowerSchemeNotify = IntPtr.Zero;
        private bool applyAlarmsToAllPlans;
        internal System.Windows.Controls.Button applyBtn;
        private bool applyEventsToAllPlans;
        internal ResizeThumb bottomThumb;
        internal System.Windows.Controls.Button cancelBtn;
        internal TextBlock caption;
        internal System.Windows.Controls.Button closeBtn;
        internal Grid contentArea;
        internal System.Windows.Controls.MenuItem contextMenuAbout;
        internal System.Windows.Controls.MenuItem contextMenuHelp;
        private Mode currentMode = Mode.Advanced;
        public const string DEACTIVATE_AIRPLANE_MODE = @"Global\PwrmgrDeactivateAirplaneModeEvent";
        public const string DEACTIVATE_BATTERY_STRETCH = "PwrmgrDeactivateBatteryStretchEvent";
        public EventWaitHandle deactivateAirplaneModeEvent = new EventWaitHandle(false, EventResetMode.ManualReset, @"Global\PwrmgrDeactivateAirplaneModeEvent");
        public EventWaitHandle deactivateBatteryStretchEvent = new EventWaitHandle(false, EventResetMode.ManualReset, "PwrmgrDeactivateBatteryStretchEvent");
        internal DispatcherTimer deactivateTimer = new DispatcherTimer();
        internal Path deactiveIconback;
        internal Border deactiveInnerFrame;
        public const int DEVICE_NOTIFY_WINDOW_HANDLE = 0;
        private IntPtr diskPowerDownTimeoutNotify = IntPtr.Zero;
        private bool Disposed;
        private Thread eventCheckThread;
        internal System.Windows.Controls.UserControl footer;
        internal Border frameOutline;
        public static Guid GUID_ACDC_POWER_SOURCE = new Guid("5D3E9A59-E9D5-4B00-A6BD-FF34FF516548");
        public static Guid GUID_ACTIVE_POWERSCHEME = new Guid("31F9F286-5084-42FE-B720-2B0264993763");
        public static Guid GUID_DISK_POWERDOWN_TIMEOUT = new Guid("6738E2C4-E8A5-4A42-B16A-E040E769756E");
        public static Guid GUID_STANDBY_TIMEOUT = new Guid("29F6C1DB-86DA-48C5-9FDB-F2B67B1F44DA");
        public static Guid GUID_VIDEO_POWERDOWN_TIMEOUT = new Guid("3C0BC021-C8A8-4E07-A973-6B14CBCB2B7E");
        internal System.Windows.Controls.UserControl header;
        internal System.Windows.Controls.Button helpBtn;
        internal Border innerFrame;
        internal Border innerFrameOutline;
        private static readonly MainWindow instance = new MainWindow();
        internal Grid LayoutRoot;
        internal ResizeThumb leftBottomThumb;
        internal ResizeThumb leftThumb;
        internal ResizeThumb leftTopThumb;
        internal MainTabControl mainTabControl;
        internal MainWindow mainWindow;
        internal System.Windows.Controls.Button maxBtn;
        internal System.Windows.Controls.Button minBtn;
        internal System.Windows.Controls.Button modeBtn;
        public const int NOTIFY_FOR_ALL_SESSIONS = 1;
        public const int NOTIFY_FOR_THIS_SESSION = 0;
        internal System.Windows.Controls.Button okBtn;
        public const double OPACITY_DEACTIVATE_MAX = 0.2;
        public const double OPACITY_DEFAULT_ON_AERO = 0.75;
        public const int PBT_APMPOWERSTATUSCHANGE = 10;
        public const int PBT_APMRESUMEAUTOMATIC = 0x12;
        public const int PBT_APMRESUMECRITICAL = 6;
        public const int PBT_APMRESUMESUSPEND = 7;
        public const int PBT_APMSUSPEND = 4;
        public const int PBT_POWERSETTINGCHANGE = 0x8013;
        internal PWMUICtlDll pwmuiCtlDll = new PWMUICtlDll();
        public const int PWRMGR_MSG_GET_STATUS = 0x812c;
        internal ResizeThumb rightBottomThumb;
        internal ResizeThumb rightThumb;
        internal ResizeThumb rightTopThumb;
        public const int SC_CLOSE = 0xf060;
        private int sessionNotify;
        private IntPtr standbyPowerDownTimeoutNotify = IntPtr.Zero;
        internal ResizeThumb topIconThumb;
        internal ResizeThumb topThumb;
        private IntPtr videoPowerDownTimeoutNotify = IntPtr.Zero;
        public const int VISTA_OS_VERSION = 6;
        public const int WM_APP = 0x8000;
        public const int WM_DWMCOMPOSITIONCHANGED = 0x31e;
        public const int WM_GETMINMAXINFO = 0x24;
        public const int WM_POWERBROADCAST = 0x218;
        public const int WM_SYSCOMMAND = 0x112;
        public const int WM_WTSSESSION_CHANGE = 0x2b1;
        public const int WTS_CONSOLE_CONNECT = 1;
        public const int WTS_CONSOLE_DISCONNECT = 2;
        public const int WTS_REMOTE_CONNECT = 3;
        public const int WTS_REMOTE_DISCONNECT = 4;
        public const int WTS_SESSION_LOCK = 7;
        public const int WTS_SESSION_LOGOFF = 6;
        public const int WTS_SESSION_LOGON = 5;
        public const int WTS_SESSION_REMOTE_CONTROL = 9;
        public const int WTS_SESSION_UNLOCK = 8;

        public event ChangeACDCPowerSourceEventHandler ChangeACDCPowerSourceEvent;

        public event ChangeActivePowerSchemeEventHandler ChangeActivePowerSchemeEvent;

        public event ChangeCalculatingRemainingBatteryEventHandler ChangeCalculatingRemainingBatteryEvent;

        public event ChangeCpuTurboStateEventHandler ChangeCpuTurboStateEvent;

        public event ChangeDiskPowerdownTimeoutEventHandler ChangeDiskPowerdownTimeoutEvent;

        public event ChangeGpuTurboStateEventHandler ChangeGpuTurboStateEvent;

        public event ChangeGreenStateEventHandler ChangeGreenStateEvent;

        public event ChangeStandbyTimeoutEventHandler ChangeStandbyTimeoutEvent;

        public event ChangeVideoPowerdownTimeoutEventHandler ChangeVideoPowerdownTimeoutEvent;

        public event GetBatteryStatusEventHandler GetBatteryStatusEvent;

        public event PowerStatusChangeEventHandler PowerStatusChangeEvent;

        public event PowerUseSliderPositionChangeEventHandler PowerUseSliderPositionChangeEvent;

        public event ResumeEventHandler ResumeEvent;

        public event SettingChangeWasAppliedEventHandler SettingChangeWasAppliedEvent;

        public event SettingChangeWasCanceledEventHandler SettingChangeWasCanceledEvent;

        public event ShowEditAgendaWindowEventHandler ShowEditAgendaWindowEvent;

        public event SuspendEventHandler SuspendEvent;

        public event UpdateACPowerConsumptionEventHandler UpdateACPowerConsumptionEvent;

        public event UpdateAirplaneModeEventHandler UpdateAirplaneModeEvent;

        public event UpdateBatteryRemainingTimeEventHandler UpdateBatteryRemainingTimeEvent;

        public event UpdateBatteryStrechEventHandler UpdateBatteryStrechEvent;

        public event WtsConsoleConnectEventHandler WtsConsoleConnectEvent;

        public event WtsConsoleDisconnectEventHandler WtsConsoleDisconnectEvent;

        private MainWindow()
        {
            this.InitializeComponent();
            this.deactivateTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            this.deactivateTimer.Tick += new EventHandler(this.OnDeactivateTimer);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        internal Delegate _CreateDelegate(System.Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private bool Apply()
        {
            if (this.applyBtn.IsEnabled)
            {
                if (!this.mainTabControl.Apply())
                {
                    return false;
                }
                if (this.SettingChangeWasAppliedEvent != null)
                {
                    this.SettingChangeWasAppliedEvent();
                }
                this.applyBtn.IsEnabled = false;
            }
            return true;
        }

        private void applyBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Apply();
        }

        private void Cancel()
        {
            if (this.applyBtn.IsEnabled && (this.SettingChangeWasCanceledEvent != null))
            {
                this.SettingChangeWasCanceledEvent();
            }
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Cancel();
            base.Close();
        }

        private void ChangeMode()
        {
            if (this.currentMode == Mode.Basic)
            {
                this.currentMode = Mode.Advanced;
                this.pwmuiCtlDll.IsBasicMode = false;
            }
            else if (this.currentMode == Mode.Advanced)
            {
                this.currentMode = Mode.Basic;
                this.pwmuiCtlDll.IsBasicMode = true;
            }
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Cancel();
            base.Close();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
                this.activePowerSchemeNotify = IntPtr.Zero;
                this.acdcPowerSourceNotify = IntPtr.Zero;
                this.videoPowerDownTimeoutNotify = IntPtr.Zero;
                this.diskPowerDownTimeoutNotify = IntPtr.Zero;
                this.standbyPowerDownTimeoutNotify = IntPtr.Zero;
            }
            this.Disposed = true;
        }

        [DllImport("dwmapi.dll", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
        private static extern int DwmIsCompositionEnabled(ref uint enabled);
        public void EnableApplyButton()
        {
            this.applyBtn.IsEnabled = true;
        }

        private void EndEventCheckThread(object sender, CancelEventArgs e)
        {
            if (this.eventCheckThread != null)
            {
                this.eventCheckThread.Abort();
            }
        }

        public void EventCheckThread()
        {
            WaitHandle[] waitHandles = new WaitHandle[] { this.activateBatteryStretchEvent, this.activateAirplaneModeEvent };
            while (true)
            {
                switch (WaitHandle.WaitAny(waitHandles))
                {
                    case 0:
                        if (waitHandles[0] == this.activateBatteryStretchEvent)
                        {
                            base.Dispatcher.Invoke(DispatcherPriority.Normal, new NoArgDelegate(this.OnActivateBatteryStretch));
                            waitHandles[0] = this.deactivateBatteryStretchEvent;
                        }
                        if (waitHandles[0] == this.deactivateBatteryStretchEvent)
                        {
                            base.Dispatcher.Invoke(DispatcherPriority.Normal, new NoArgDelegate(this.OnDeactivateBatteryStretch));
                            waitHandles[0] = this.activateBatteryStretchEvent;
                        }
                        break;

                    case 1:
                        if (waitHandles[1] != this.activateAirplaneModeEvent)
                        {
                            if (waitHandles[1] == this.deactivateAirplaneModeEvent)
                            {
                                base.Dispatcher.Invoke(DispatcherPriority.Normal, new NoArgDelegate(this.OnDeactivateAirplaneMode));
                                waitHandles[1] = this.activateAirplaneModeEvent;
                            }
                            break;
                        }
                        base.Dispatcher.Invoke(DispatcherPriority.Normal, new NoArgDelegate(this.OnActivateAirplaneMode));
                        waitHandles[1] = this.deactivateAirplaneModeEvent;
                        break;
                }
            }
        }

        ~MainWindow()
        {
            this.Dispose(false);
        }

        [DllImport("User32.dll", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
        private static extern IntPtr GetTopWindow(IntPtr hWnd);
        private void header_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.WindowMaximize();
        }

        private void header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }

        private void helpBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.helpBtn.ContextMenu != null)
            {
                this.helpBtn.ContextMenu.IsOpen = true;
            }
        }

        private void hlpBtnContextMemu_Click(object sender, RoutedEventArgs e)
        {
            if (e.Source == this.contextMenuHelp)
            {
                this.pwmuiCtlDll.ShowHelpFile();
            }
            if (e.Source == this.contextMenuAbout)
            {
                this.pwmuiCtlDll.ShowAboutDialog();
            }
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/mainwindow.xaml", UriKind.Relative);
                System.Windows.Application.LoadComponent(this, resourceLocator);
            }
        }

        public bool IsAdvanced() => 
            (this.currentMode == Mode.Advanced);

        public bool IsBasic() => 
            (this.currentMode == Mode.Basic);

        private bool IsShownChildWindows()
        {
            WindowCollection ownedWindows = Instance.OwnedWindows;
            WindowInteropHelper helper = new WindowInteropHelper(this);
            if ((ownedWindows.Count <= 0) && !(IntPtr.Zero != GetTopWindow(helper.Handle)))
            {
                return false;
            }
            return true;
        }

        private void mainWindow_Activated(object sender, EventArgs e)
        {
            this.frameOutline.BorderBrush = (System.Windows.Media.Brush) base.FindResource("AlmostCyanBrush");
            this.innerFrameOutline.BorderBrush = (System.Windows.Media.Brush) base.FindResource("LightGrayBrush");
            this.StopDeactivateTimer();
        }

        private void mainWindow_Closed(object sender, EventArgs e)
        {
            PWMUI.Message.Instance.PostMainUIEndedMessage();
        }

        private void mainWindow_Deactivated(object sender, EventArgs e)
        {
            this.frameOutline.BorderBrush = (System.Windows.Media.Brush) base.FindResource("SilverBrush");
            this.innerFrameOutline.BorderBrush = (System.Windows.Media.Brush) base.FindResource("DkGrayBrush");
            this.StartDeactivateTimer();
        }

        private void mainWindow_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                this.pwmuiCtlDll.ShowHelpFile();
            }
        }

        private void mainWindow_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!base.IsActive)
            {
                this.StopDeactivateTimer();
            }
        }

        private void mainWindow_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!base.IsActive)
            {
                this.StartDeactivateTimer();
            }
        }

        private void MaxBtn_Click(object sender, RoutedEventArgs e)
        {
            this.WindowMaximize();
        }

        private IntPtr MessageProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x2b1:
                    return this.OnWtsSessionChange(wParam, lParam);

                case 0x31e:
                    this.SetWindowOpacity();
                    return IntPtr.Zero;

                case 0x812c:
                    if (this.GetBatteryStatusEvent != null)
                    {
                        this.GetBatteryStatusEvent();
                    }
                    handled = true;
                    return IntPtr.Zero;

                case 0x24:
                    return this.OnGetMinMaxInfo(wParam, lParam);

                case 0x112:
                    return this.OnSysCommand(wParam, lParam);

                case 0x218:
                    return this.OnPowerBroadcast(wParam, lParam);
            }
            if (msg == PWMUI.Message.Instance.WM_POWERUSESLIDER_POSITION_CHANGE)
            {
                if (this.PowerUseSliderPositionChangeEvent != null)
                {
                    this.PowerUseSliderPositionChangeEvent();
                }
                handled = true;
                return IntPtr.Zero;
            }
            if (msg == PWMUI.Message.Instance.WM_UPDATE_BATTERY_REMAININGTIME)
            {
                if (this.UpdateBatteryRemainingTimeEvent != null)
                {
                    this.UpdateBatteryRemainingTimeEvent();
                }
                handled = true;
                return IntPtr.Zero;
            }
            if (msg == PWMUI.Message.Instance.WM_SHOW_BATTERY_PAGE)
            {
                this.mainTabControl.SelectedItem = this.mainTabControl.batteryTabItem;
                handled = true;
                return IntPtr.Zero;
            }
            if (msg == PWMUI.Message.Instance.WM_CHANGE_CALCULATING_REMAINING_BATTERY_EVENT)
            {
                if (this.ChangeCalculatingRemainingBatteryEvent != null)
                {
                    this.ChangeCalculatingRemainingBatteryEvent();
                }
                handled = true;
                return IntPtr.Zero;
            }
            if (msg == PWMUI.Message.Instance.WM_SHOW_POWER_AGENDAS_PAGE)
            {
                this.OnShowPowerAgendasPage();
                handled = true;
                return IntPtr.Zero;
            }
            if (msg == PWMUI.Message.Instance.WM_SHOW_EDIT_AGENDA_WINDOW)
            {
                this.OnShowEditAgendaWindow(wParam, lParam);
                handled = true;
                return IntPtr.Zero;
            }
            if (msg == PWMUI.Message.Instance.WM_SHOW_POWER_PLAN_PAGE)
            {
                this.OnShowPowerPlanPage();
                handled = true;
                return IntPtr.Zero;
            }
            if (msg == PWMUI.Message.Instance.WM_UPDATE_AC_POWER_CONSUMPTION)
            {
                if (this.UpdateACPowerConsumptionEvent != null)
                {
                    this.UpdateACPowerConsumptionEvent();
                }
                handled = true;
                return IntPtr.Zero;
            }
            if (msg == PWMUI.Message.Instance.WM_SHOW_INSTANT_RESUME)
            {
                this.mainTabControl.ShowInstantResume();
                handled = true;
                return IntPtr.Zero;
            }
            if (msg == PWMUI.Message.Instance.WM_CHANGE_GREEN_STATE)
            {
                if (this.ChangeGreenStateEvent != null)
                {
                    this.ChangeGreenStateEvent((uint) lParam.ToInt32());
                }
                handled = true;
                return IntPtr.Zero;
            }
            if (msg == PWMUI.Message.Instance.WM_CHANGE_CPU_TURBO_STATE)
            {
                if (this.ChangeCpuTurboStateEvent != null)
                {
                    this.ChangeCpuTurboStateEvent((uint) lParam.ToInt32());
                }
                handled = true;
                return IntPtr.Zero;
            }
            if (msg == PWMUI.Message.Instance.WM_CHANGE_GPU_TURBO_STATE)
            {
                if (this.ChangeGpuTurboStateEvent != null)
                {
                    this.ChangeGpuTurboStateEvent((uint) lParam.ToInt32());
                }
                handled = true;
                return IntPtr.Zero;
            }
            if (msg == PWMUI.Message.Instance.WM_SHOW_GLOBAL_PAGE)
            {
                this.OnShowGlobalPage();
                handled = true;
                return IntPtr.Zero;
            }
            return IntPtr.Zero;
        }

        private void MinBtn_Click(object sender, RoutedEventArgs e)
        {
            base.WindowState = WindowState.Minimized;
        }

        private void modeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.ChangeMode();
            this.Refresh();
            if (e.OriginalSource.GetType() == typeof(string))
            {
                string originalSource = e.OriginalSource as string;
                string.Format("PWMUI: modeBtn_Click by code, begin invoke to show window", new object[0]);
                NormalShowMethod method = new NormalShowMethod(this.ShowEditAgendaWindowMethod);
                base.Dispatcher.BeginInvoke(DispatcherPriority.SystemIdle, method, originalSource);
            }
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.Apply())
            {
                base.Close();
            }
        }

        public void OnActivateAirplaneMode()
        {
            if (this.UpdateAirplaneModeEvent != null)
            {
                this.UpdateAirplaneModeEvent(true);
            }
        }

        public void OnActivateBatteryStretch()
        {
            if (this.UpdateBatteryStrechEvent != null)
            {
                this.UpdateBatteryStrechEvent();
            }
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new PMWindowPeer(this);

        public void OnDeactivateAirplaneMode()
        {
            if (this.UpdateAirplaneModeEvent != null)
            {
                this.UpdateAirplaneModeEvent(false);
            }
        }

        public void OnDeactivateBatteryStretch()
        {
            if (this.UpdateBatteryStrechEvent != null)
            {
                this.UpdateBatteryStrechEvent();
            }
        }

        private void OnDeactivateTimer(object sender, EventArgs e)
        {
            this.deactiveInnerFrame.Opacity += 0.01;
            this.deactiveIconback.Opacity += 0.01;
            if (this.deactiveInnerFrame.Opacity >= 0.2)
            {
                this.deactivateTimer.Stop();
            }
        }

        private IntPtr OnGetMinMaxInfo(IntPtr wParam, IntPtr lParam)
        {
            PWMUI.MINMAXINFO structure = (PWMUI.MINMAXINFO) Marshal.PtrToStructure(lParam, typeof(PWMUI.MINMAXINFO));
            System.Windows.Point point = new System.Windows.Point(base.Width / 2.0, base.Height / 2.0);
            System.Windows.Point point2 = base.PointToScreen(point);
            System.Drawing.Point pt = new System.Drawing.Point((int) point2.X, (int) point2.Y);
            System.Drawing.Rectangle workingArea = Screen.GetWorkingArea(pt);
            structure.ptMaxSize.x = workingArea.Width;
            structure.ptMaxSize.y = workingArea.Height;
            structure.ptMaxPosition.x = workingArea.X;
            structure.ptMaxPosition.y = workingArea.Y;
            Marshal.StructureToPtr(structure, lParam, false);
            return IntPtr.Zero;
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.Loaded += new RoutedEventHandler(this.RegisterNotification);
            base.Loaded += new RoutedEventHandler(this.StartEventCheckThread);
            base.Closing += new CancelEventHandler(this.UnregisterNotification);
            base.Closing += new CancelEventHandler(this.EndEventCheckThread);
            base.OnInitialized(e);
        }

        private IntPtr OnPowerBroadcast(IntPtr wParam, IntPtr lParam)
        {
            int num = wParam.ToInt32();
            switch (num)
            {
                case 4:
                    if (this.SuspendEvent != null)
                    {
                        this.SuspendEvent();
                    }
                    break;

                case 5:
                case 6:
                case 8:
                case 9:
                case 0x12:
                    break;

                case 7:
                    if (this.ResumeEvent != null)
                    {
                        this.ResumeEvent();
                    }
                    break;

                case 10:
                    if (this.PowerStatusChangeEvent != null)
                    {
                        this.PowerStatusChangeEvent();
                    }
                    break;

                default:
                    if (num == 0x8013)
                    {
                        this.OnPowerSettingsChange(wParam, lParam);
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private IntPtr OnPowerSettingsChange(IntPtr wParam, IntPtr lParam)
        {
            POWERBROADCAST_SETTING powerbroadcast_setting = (POWERBROADCAST_SETTING) Marshal.PtrToStructure(lParam, typeof(POWERBROADCAST_SETTING));
            if (powerbroadcast_setting.PowerSetting.Equals(GUID_ACTIVE_POWERSCHEME) && (this.ChangeActivePowerSchemeEvent != null))
            {
                this.ChangeActivePowerSchemeEvent();
            }
            if (powerbroadcast_setting.PowerSetting.Equals(GUID_ACDC_POWER_SOURCE) && (this.ChangeACDCPowerSourceEvent != null))
            {
                this.ChangeACDCPowerSourceEvent();
            }
            if (powerbroadcast_setting.PowerSetting.Equals(GUID_VIDEO_POWERDOWN_TIMEOUT) && (this.ChangeVideoPowerdownTimeoutEvent != null))
            {
                this.ChangeVideoPowerdownTimeoutEvent();
            }
            if (powerbroadcast_setting.PowerSetting.Equals(GUID_DISK_POWERDOWN_TIMEOUT) && (this.ChangeDiskPowerdownTimeoutEvent != null))
            {
                this.ChangeDiskPowerdownTimeoutEvent();
            }
            if (powerbroadcast_setting.PowerSetting.Equals(GUID_STANDBY_TIMEOUT) && (this.ChangeStandbyTimeoutEvent != null))
            {
                this.ChangeStandbyTimeoutEvent();
            }
            return IntPtr.Zero;
        }

        private void OnShowEditAgendaWindow(IntPtr wParam, IntPtr lParam)
        {
            if (!this.IsShownChildWindows())
            {
                string str = Convert.ToString(wParam);
                $"PWMUI: WM_SHOW_EDIT_AGENDA_WINDOW - agenda id {str}";
                if (this.mainTabControl.SelectedItem != this.mainTabControl.powerAgendasTabItem)
                {
                    if (Instance.IsBasic())
                    {
                        Instance.modeBtn.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent, str));
                        this.mainTabControl.SelectedItem = this.mainTabControl.powerAgendasTabItem;
                        return;
                    }
                    this.mainTabControl.SelectedItem = this.mainTabControl.powerAgendasTabItem;
                }
                if (this.ShowEditAgendaWindowEvent != null)
                {
                    this.ShowEditAgendaWindowEvent(str);
                }
            }
        }

        private void OnShowGlobalPage()
        {
            if (!this.IsShownChildWindows() && (this.mainTabControl.SelectedItem != this.mainTabControl.globalTabItem))
            {
                if (Instance.IsBasic())
                {
                    Instance.modeBtn.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
                }
                this.mainTabControl.SelectedItem = this.mainTabControl.globalTabItem;
            }
        }

        private void OnShowPowerAgendasPage()
        {
            if (!this.IsShownChildWindows() && (this.mainTabControl.SelectedItem != this.mainTabControl.powerAgendasTabItem))
            {
                if (Instance.IsBasic())
                {
                    Instance.modeBtn.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
                }
                this.mainTabControl.SelectedItem = this.mainTabControl.powerAgendasTabItem;
            }
        }

        private void OnShowPowerPlanPage()
        {
            if (!this.IsShownChildWindows() && ((this.mainTabControl.SelectedItem != this.mainTabControl.planTabItem) && Instance.IsBasic()))
            {
                Instance.modeBtn.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            }
        }

        private IntPtr OnSysCommand(IntPtr wParam, IntPtr lParam)
        {
            if (wParam.ToInt32() == 0xf060)
            {
                this.Cancel();
            }
            return IntPtr.Zero;
        }

        private IntPtr OnWtsSessionChange(IntPtr wParam, IntPtr lParam)
        {
            switch (wParam.ToInt32())
            {
                case 1:
                    if (this.WtsConsoleConnectEvent != null)
                    {
                        this.WtsConsoleConnectEvent();
                    }
                    break;

                case 2:
                    if (this.WtsConsoleDisconnectEvent != null)
                    {
                        this.WtsConsoleDisconnectEvent();
                    }
                    break;
            }
            return IntPtr.Zero;
        }

        private void Refresh()
        {
            if (this.IsBasic())
            {
                this.modeBtn.Content = (string) base.FindResource("ButtonAdvancedMode");
            }
            if (this.IsAdvanced())
            {
                this.modeBtn.Content = (string) base.FindResource("ButtonBasicMode");
            }
            this.mainTabControl.Refresh();
        }

        private void RegisterNotification(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            if (helper.Handle != IntPtr.Zero)
            {
                HwndSource source = HwndSource.FromHwnd(helper.Handle);
                source.AddHook(new HwndSourceHook(this.MessageProc));
                if (this._osVersion >= 6)
                {
                    this.activePowerSchemeNotify = RegisterPowerSettingNotification(source.Handle, ref GUID_ACTIVE_POWERSCHEME, 0);
                    this.acdcPowerSourceNotify = RegisterPowerSettingNotification(source.Handle, ref GUID_ACDC_POWER_SOURCE, 0);
                    this.videoPowerDownTimeoutNotify = RegisterPowerSettingNotification(source.Handle, ref GUID_VIDEO_POWERDOWN_TIMEOUT, 0);
                    this.diskPowerDownTimeoutNotify = RegisterPowerSettingNotification(source.Handle, ref GUID_DISK_POWERDOWN_TIMEOUT, 0);
                    this.standbyPowerDownTimeoutNotify = RegisterPowerSettingNotification(source.Handle, ref GUID_STANDBY_TIMEOUT, 0);
                    this.sessionNotify = WTSRegisterSessionNotification(source.Handle, 0);
                }
            }
        }

        [DllImport("User32.dll", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
        private static extern IntPtr RegisterPowerSettingNotification(IntPtr hRecipient, ref Guid PowerSettingGuid, int Flags);
        public void SetModeAdvanced()
        {
            this.currentMode = Mode.Advanced;
            this.pwmuiCtlDll.IsBasicMode = false;
        }

        private void SetWindowOpacity()
        {
            uint enabled = 0;
            DwmIsCompositionEnabled(ref enabled);
            if (enabled != 0)
            {
                this.innerFrame.Opacity = 0.75;
                this.caption.Background = (System.Windows.Media.Brush) base.FindResource("CaptionBkBrush");
            }
            else
            {
                this.innerFrame.Opacity = 1.0;
                this.caption.Background = null;
            }
        }

        private void ShowEditAgendaWindowMethod(string agendaID)
        {
            $"PWMUI: MainWindow : ShowEditAgendaWindowMethod({agendaID})";
            if (this.ShowEditAgendaWindowEvent != null)
            {
                this.ShowEditAgendaWindowEvent(agendaID);
            }
        }

        private void StartDeactivateTimer()
        {
            this.deactiveInnerFrame.Opacity = 0.0;
            this.deactiveIconback.Opacity = 0.0;
            this.deactiveInnerFrame.Visibility = Visibility.Visible;
            this.deactiveIconback.Visibility = Visibility.Visible;
            this.deactivateTimer.Start();
        }

        private void StartEventCheckThread(object sender, RoutedEventArgs e)
        {
            this.eventCheckThread = new Thread(new ThreadStart(this.EventCheckThread));
            this.eventCheckThread.IsBackground = true;
            this.eventCheckThread.Start();
        }

        private void StopDeactivateTimer()
        {
            this.deactiveInnerFrame.Opacity = 0.0;
            this.deactiveIconback.Opacity = 0.0;
            this.deactiveIconback.Visibility = Visibility.Hidden;
            this.deactiveInnerFrame.Visibility = Visibility.Hidden;
            this.deactivateTimer.Stop();
        }

        [EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.mainWindow = (MainWindow) target;
                    this.mainWindow.Loaded += new RoutedEventHandler(this.Window_Loaded);
                    this.mainWindow.Activated += new EventHandler(this.mainWindow_Activated);
                    this.mainWindow.Deactivated += new EventHandler(this.mainWindow_Deactivated);
                    this.mainWindow.MouseEnter += new System.Windows.Input.MouseEventHandler(this.mainWindow_MouseEnter);
                    this.mainWindow.MouseLeave += new System.Windows.Input.MouseEventHandler(this.mainWindow_MouseLeave);
                    this.mainWindow.KeyUp += new System.Windows.Input.KeyEventHandler(this.mainWindow_KeyUp);
                    this.mainWindow.Closed += new EventHandler(this.mainWindow_Closed);
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.frameOutline = (Border) target;
                    return;

                case 4:
                    this.innerFrameOutline = (Border) target;
                    return;

                case 5:
                    this.innerFrame = (Border) target;
                    return;

                case 6:
                    this.header = (System.Windows.Controls.UserControl) target;
                    this.header.MouseDoubleClick += new MouseButtonEventHandler(this.header_MouseDoubleClick);
                    this.header.MouseLeftButtonDown += new MouseButtonEventHandler(this.header_MouseLeftButtonDown);
                    return;

                case 7:
                    this.topThumb = (ResizeThumb) target;
                    return;

                case 8:
                    this.caption = (TextBlock) target;
                    return;

                case 9:
                    this.topIconThumb = (ResizeThumb) target;
                    return;

                case 10:
                    this.minBtn = (System.Windows.Controls.Button) target;
                    this.minBtn.Click += new RoutedEventHandler(this.MinBtn_Click);
                    return;

                case 11:
                    this.maxBtn = (System.Windows.Controls.Button) target;
                    this.maxBtn.Click += new RoutedEventHandler(this.MaxBtn_Click);
                    return;

                case 12:
                    this.closeBtn = (System.Windows.Controls.Button) target;
                    this.closeBtn.Click += new RoutedEventHandler(this.CloseBtn_Click);
                    return;

                case 13:
                    this.deactiveIconback = (Path) target;
                    return;

                case 14:
                    this.contentArea = (Grid) target;
                    return;

                case 15:
                    this.modeBtn = (System.Windows.Controls.Button) target;
                    this.modeBtn.Click += new RoutedEventHandler(this.modeBtn_Click);
                    return;

                case 0x10:
                    this.helpBtn = (System.Windows.Controls.Button) target;
                    this.helpBtn.Click += new RoutedEventHandler(this.helpBtn_Click);
                    return;

                case 0x11:
                    ((System.Windows.Controls.ContextMenu) target).AddHandler(System.Windows.Controls.MenuItem.ClickEvent, new RoutedEventHandler(this.hlpBtnContextMemu_Click));
                    return;

                case 0x12:
                    this.contextMenuHelp = (System.Windows.Controls.MenuItem) target;
                    return;

                case 0x13:
                    this.contextMenuAbout = (System.Windows.Controls.MenuItem) target;
                    return;

                case 20:
                    this.mainTabControl = (MainTabControl) target;
                    return;

                case 0x15:
                    this.footer = (System.Windows.Controls.UserControl) target;
                    return;

                case 0x16:
                    this.okBtn = (System.Windows.Controls.Button) target;
                    this.okBtn.Click += new RoutedEventHandler(this.okBtn_Click);
                    return;

                case 0x17:
                    this.cancelBtn = (System.Windows.Controls.Button) target;
                    this.cancelBtn.Click += new RoutedEventHandler(this.cancelBtn_Click);
                    return;

                case 0x18:
                    this.applyBtn = (System.Windows.Controls.Button) target;
                    this.applyBtn.Click += new RoutedEventHandler(this.applyBtn_Click);
                    return;

                case 0x19:
                    this.leftThumb = (ResizeThumb) target;
                    return;

                case 0x1a:
                    this.rightThumb = (ResizeThumb) target;
                    return;

                case 0x1b:
                    this.bottomThumb = (ResizeThumb) target;
                    return;

                case 0x1c:
                    this.rightBottomThumb = (ResizeThumb) target;
                    return;

                case 0x1d:
                    this.leftTopThumb = (ResizeThumb) target;
                    return;

                case 30:
                    this.leftBottomThumb = (ResizeThumb) target;
                    return;

                case 0x1f:
                    this.rightTopThumb = (ResizeThumb) target;
                    return;

                case 0x20:
                    this.deactiveInnerFrame = (Border) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void UnregisterNotification(object sender, CancelEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            if (helper.Handle != IntPtr.Zero)
            {
                HwndSource source = HwndSource.FromHwnd(helper.Handle);
                if (this._osVersion >= 6)
                {
                    if (this.activePowerSchemeNotify != IntPtr.Zero)
                    {
                        UnregisterPowerSettingNotification(this.activePowerSchemeNotify);
                    }
                    if (this.acdcPowerSourceNotify != IntPtr.Zero)
                    {
                        UnregisterPowerSettingNotification(this.acdcPowerSourceNotify);
                    }
                    if (this.videoPowerDownTimeoutNotify != IntPtr.Zero)
                    {
                        UnregisterPowerSettingNotification(this.videoPowerDownTimeoutNotify);
                    }
                    if (this.diskPowerDownTimeoutNotify != IntPtr.Zero)
                    {
                        UnregisterPowerSettingNotification(this.diskPowerDownTimeoutNotify);
                    }
                    if (this.standbyPowerDownTimeoutNotify != IntPtr.Zero)
                    {
                        UnregisterPowerSettingNotification(this.standbyPowerDownTimeoutNotify);
                    }
                    if ((this.sessionNotify != 0) && (source.Handle != IntPtr.Zero))
                    {
                        WTSUnRegisterSessionNotification(source.Handle);
                    }
                }
            }
        }

        [DllImport("User32.dll", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
        private static extern IntPtr UnregisterPowerSettingNotification(IntPtr Handle);
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PWMUI.Message.Instance.PostMainUIStartedMessage();
            if (this.pwmuiCtlDll.IsBasicMode)
            {
                this.currentMode = Mode.Basic;
            }
            this.caption.Text = base.Title;
            this.applyBtn.IsEnabled = false;
            this.SetWindowOpacity();
            this.mainTabControl.Create();
            this.Refresh();
        }

        private void WindowMaximize()
        {
            if (base.WindowState == WindowState.Maximized)
            {
                base.WindowState = WindowState.Normal;
                this.maxBtn.Style = (Style) base.FindResource("Button WindowControl Maximize");
            }
            else
            {
                base.WindowState = WindowState.Maximized;
                this.maxBtn.Style = (Style) base.FindResource("Button WindowControl Restore");
            }
        }

        [DllImport("WtsApi32.dll", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
        private static extern int WTSRegisterSessionNotification(IntPtr Handle, uint Flags);
        [DllImport("WtsApi32.dll", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
        private static extern int WTSUnRegisterSessionNotification(IntPtr Handle);

        public static MainWindow Instance =>
            instance;

        public bool IsApplyAlarmsToAllPlansChecked
        {
            get => 
                this.applyAlarmsToAllPlans;
            set
            {
                this.applyAlarmsToAllPlans = value;
            }
        }

        public bool IsApplyEventsToAllPlansChecked
        {
            get => 
                this.applyEventsToAllPlans;
            set
            {
                this.applyEventsToAllPlans = value;
            }
        }

        private enum Mode
        {
            Basic,
            Advanced
        }

        private delegate void NoArgDelegate();

        public delegate void NormalShowMethod(string agendaID);
    }
}

