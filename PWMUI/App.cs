namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Interop;
    using System.Windows.Markup;

    public class App : Application, IComponentConnector
    {
        private bool _contentLoaded;
        private const string CMD_LINE_ELEVATION = "elevation";
        private const string CMD_LINE_SHOW_BATTERY_ADV_PAGE = "ShowBatteryAdvPage";
        private const string CMD_LINE_SHOW_BATTERY_PAGE = "ShowBatteryPage";
        private const string CMD_LINE_SHOW_GLOBAL_PAGE = "ShowGlobalPowerSettingsPage";
        private const string CMD_LINE_SHOW_INSTANT_RESUME = "ShowInstantResume";
        private const string CMD_LINE_SHOW_POWER_AGENDAS_PAGE = "ShowPowerAgendasPage";
        private const string CMD_LINE_SHOW_POWPLAN_ADV_PAGE = "ShowPowerPlanAdvPage";
        internal const string CMD_LINE_STOP = "Stop";
        private bool commandLineIsElevation;
        private bool commandLineIsNothing;
        private bool commandLineIsShowBatteryAdvPage;
        private bool commandLineIsShowBatteryPage;
        private bool commandLineIsShowGlobalPage;
        private bool commandLineIsShowInstantResume;
        private bool commandLineIsShowPowerAgendasPage;
        private bool commandLineIsShowPowerPlanAdvPage;
        private string commandLinePowerAgendaID = "";
        internal static PWMUICtlDll ctlDll;
        private IntPtr hMainWindow = IntPtr.Zero;
        internal static Process previousProcess = null;
        private const int SW_RESTORE = 9;
        private ThemeChanger themeChanger = new ThemeChanger();

        private void App_Startup(object sender, StartupEventArgs e)
        {
            this.ParseCommandLine(e.Args);
            if (this.commandLineIsElevation)
            {
                this.ExecuteElevation();
            }
            else if (this.commandLineIsShowBatteryPage)
            {
                this.ExecuteShowBatteryPage();
            }
            else if (this.commandLineIsShowInstantResume)
            {
                this.ExecuteShowInstantResume();
            }
            else if (this.commandLineIsShowBatteryAdvPage)
            {
                MainWindow.Instance.SetModeAdvanced();
                this.ExecuteShowBatteryPage();
            }
            else if (this.commandLineIsShowPowerAgendasPage)
            {
                this.ExecuteShowPowerAgendasPage(this.commandLinePowerAgendaID);
            }
            else if (this.commandLineIsShowPowerPlanAdvPage)
            {
                MainWindow.Instance.SetModeAdvanced();
                this.ExecuteNormal();
            }
            else if (this.commandLineIsShowGlobalPage)
            {
                this.ExecuteShowGlobalPage();
            }
            else if (this.commandLineIsNothing)
            {
                this.ExecuteNormal();
            }
            else
            {
                base.Shutdown();
            }
        }

        public static bool CanRun(string[] args)
        {
            if ((args.Length != 0) && (args[0] == "Stop"))
            {
                ExecuteStop();
                return false;
            }
            if (!ctlDll.CanUse())
            {
                return false;
            }
            return true;
        }

        public static bool CanStartSplashWindow() => 
            (previousProcess == null);

        private void ExecuteElevation()
        {
            if (previousProcess != null)
            {
                previousProcess.WaitForExit(100);
                if (!previousProcess.HasExited)
                {
                    previousProcess.Kill();
                }
            }
            this.ShowMainWindow();
        }

        private void ExecuteNormal()
        {
            if (previousProcess != null)
            {
                WakeupWindow(previousProcess.MainWindowHandle);
                base.Shutdown();
            }
            else
            {
                this.ShowMainWindow();
            }
        }

        private void ExecuteShowBatteryPage()
        {
            if (previousProcess != null)
            {
                WakeupWindow(previousProcess.MainWindowHandle);
                Message.Instance.PostShowBatteryPageMessage(previousProcess.MainWindowHandle);
                base.Shutdown();
            }
            else
            {
                this.ShowMainWindow();
                MainWindow.Instance.mainTabControl.SelectedItem = MainWindow.Instance.mainTabControl.batteryTabItem;
            }
        }

        private void ExecuteShowGlobalPage()
        {
            if (previousProcess != null)
            {
                WakeupWindow(previousProcess.MainWindowHandle);
                Message.Instance.PostShowGlobalPageMessage(previousProcess.MainWindowHandle);
                base.Shutdown();
            }
            else
            {
                this.ShowMainWindow();
                MainWindow.Instance.SetModeAdvanced();
                MainWindow.Instance.mainTabControl.SelectedItem = MainWindow.Instance.mainTabControl.globalTabItem;
            }
        }

        private void ExecuteShowInstantResume()
        {
            if (previousProcess != null)
            {
                WakeupWindow(previousProcess.MainWindowHandle);
                Message.Instance.PostShowInstantResumeMessage(previousProcess.MainWindowHandle);
                base.Shutdown();
            }
            else
            {
                MainWindow.Instance.SetModeAdvanced();
                this.ShowMainWindow();
                MainWindow.Instance.mainTabControl.ShowInstantResume();
            }
        }

        private void ExecuteShowPowerAgendasPage(string commandLinePowerAgendaID)
        {
            Process previousProcess = GetPreviousProcess();
            if (previousProcess != null)
            {
                WakeupWindow(previousProcess.MainWindowHandle);
                base.Shutdown();
            }
            else
            {
                this.ShowMainWindow();
                Process currentProcess = Process.GetCurrentProcess();
                if (commandLinePowerAgendaID != "")
                {
                    Message.Instance.PostShowEditAgendaWindowMessage(currentProcess.MainWindowHandle, commandLinePowerAgendaID);
                }
                else
                {
                    Message.Instance.PostShowPowerAgendasPageMessage(currentProcess.MainWindowHandle);
                }
            }
        }

        private static void ExecuteStop()
        {
            if (previousProcess != null)
            {
                previousProcess.CloseMainWindow();
                previousProcess.WaitForExit(500);
                if (!previousProcess.HasExited)
                {
                    previousProcess.Kill();
                }
            }
        }

        public static Process GetPreviousProcess()
        {
            Process currentProcess = Process.GetCurrentProcess();
            foreach (Process process2 in Process.GetProcessesByName(currentProcess.ProcessName))
            {
                if ((process2.Id != currentProcess.Id) && (process2.SessionId == currentProcess.SessionId))
                {
                    return process2;
                }
            }
            return null;
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            base.Startup += new StartupEventHandler(this.App_Startup);
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/app.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [DllImport("User32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);
        protected override void OnExit(ExitEventArgs e)
        {
            ctlDll.EndSplashWindow();
        }

        private void ParseCommandLine(string[] args)
        {
            if (args.Length == 0)
            {
                this.commandLineIsNothing = true;
            }
            else if (args[0] == "elevation")
            {
                this.commandLineIsElevation = true;
            }
            else if (args[0] == "ShowBatteryPage")
            {
                this.commandLineIsShowBatteryPage = true;
            }
            else if (args[0] == "ShowInstantResume")
            {
                this.commandLineIsShowInstantResume = true;
            }
            else if (args[0] == "ShowBatteryAdvPage")
            {
                this.commandLineIsShowBatteryAdvPage = true;
            }
            else if (args[0] == "ShowPowerPlanAdvPage")
            {
                this.commandLineIsShowPowerPlanAdvPage = true;
            }
            else if (args[0] == "ShowPowerAgendasPage")
            {
                this.commandLineIsShowPowerAgendasPage = true;
                if (args.Length == 2)
                {
                    this.commandLinePowerAgendaID = args[1];
                }
            }
            else if (args[0] == "ShowGlobalPowerSettingsPage")
            {
                this.commandLineIsShowGlobalPage = true;
            }
        }

        public static void PWMUIMain(string[] args)
        {
            ctlDll = new PWMUICtlDll();
            previousProcess = GetPreviousProcess();
            if (CanRun(args))
            {
                App app = new App();
                PMWin7 win = new PMWin7();
                if (win.Is7OrLater())
                {
                    new PMJumpList().SetPMAppID();
                }
                if (CanStartSplashWindow())
                {
                    ctlDll.StartSplashWindow();
                }
                app.InitializeComponent();
                app.Run();
            }
        }

        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
        private void ShowMainWindow()
        {
            MainWindow.Instance.Show();
            WindowInteropHelper helper = new WindowInteropHelper(MainWindow.Instance);
            HwndSource source = HwndSource.FromHwnd(helper.Handle);
            ctlDll.SetMainWindow(source.Handle);
            this.themeChanger.Change();
            ctlDll.EndSplashWindow();
        }

        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            this._contentLoaded = true;
        }

        public static void WakeupWindow(IntPtr hWnd)
        {
            if (IsIconic(hWnd))
            {
                ShowWindowAsync(hWnd, 9);
            }
            SetForegroundWindow(hWnd);
        }
    }
}

