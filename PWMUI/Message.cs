namespace PWMUI
{
    using System;
    using System.Runtime.InteropServices;

    public class Message
    {
        private static readonly Message instance = new Message();
        public const string MSG_CHANGE_CALCULATING_REMAINING_BATTERY_EVENT = "PwrmgrChangeCalculatingRemainingBatteryEvent";
        public const string MSG_CHANGE_CPU_TURBO_STATE = "PwrMgrChangeCpuTurboState";
        public const string MSG_CHANGE_GPU_TURBO_STATE = "PwrMgrChangeGpuTurboState";
        public const string MSG_CHANGE_GREEN_STATE = "PwrMgrChangeGreenState";
        public const string MSG_MAIN_UI_WAS_ENDED = "PwrMgrMainUIWasEnded";
        public const string MSG_MAIN_UI_WAS_STARTED = "PwrMgrMainUIWasStarted";
        public const string MSG_POWERUSESLIDER_POSITION_CHANGE = "PowerUseSliderPositionChange";
        public const string MSG_SHOW_BATTERY_PAGE = "PwrMgrShowBatteryPage";
        public const string MSG_SHOW_EDIT_AGENDA_WINDOW = "PwrMgrShowEditAgendaWindow";
        public const string MSG_SHOW_GLOBAL_PAGE = "PwrMgrShowGlobalPage";
        public const string MSG_SHOW_INSTANT_RESUME = "PwrMgrShowInstantResume";
        public const string MSG_SHOW_POWER_AGENDAS_PAGE = "PwrMgrShowPowerAgendasPage";
        public const string MSG_SHOW_POWER_PLAN_PAGE = "PwrMgrShowPowerPlanPage";
        public const string MSG_UPDATE_AC_POWER_CONSUMPTION = "PwrMgrBkGndUpdateACPowerConsumption";
        public const string MSG_UPDATE_BATTERY_REMAININGTIME = "PwrMgrBkGndUpdateBatteryRemainingTime";
        public const string PWRMGR_BKGND_WND_CLASS_NAME = "PwrMgrBkGndWindow";
        public const string PWRMGR_BKGND_WND_WINDOW_NAME = "PwrMgrBkGndWindow";
        private uint wmChangeCalculatingRemainingBatteryEvent;
        private uint wmChangeCpuTurboState;
        private uint wmChangeGpuTurboState;
        private uint wmChangeGreenState;
        private uint wmMainUIWasEnded;
        private uint wmMainUIWasStarted;
        private uint wmPowerSliderPositionChange;
        private uint wmShowBatteryPage;
        private uint wmShowEditAgendaWindow;
        private uint wmShowEditAgendaWindowPanel;
        private uint wmShowGlobalPage;
        private uint wmShowInstantResume;
        private uint wmShowPowerAgendasPage;
        private uint wmShowPowerPlanPage;
        private uint wmUpdateACPowerConsumption;
        private uint wmUpdateBatteryRemainingTime;

        private Message()
        {
            this.RegisterMessage();
        }

        [DllImport("User32.dll", CallingConvention=CallingConvention.StdCall, CharSet=CharSet.Unicode, SetLastError=true)]
        private static extern IntPtr FindWindow(string className, string windowName);
        public void PostMainUIEndedMessage()
        {
            IntPtr hWnd = FindWindow("PwrMgrBkGndWindow", "PwrMgrBkGndWindow");
            if (hWnd != IntPtr.Zero)
            {
                PostMessage(hWnd, this.wmMainUIWasEnded, IntPtr.Zero, IntPtr.Zero);
            }
        }

        public void PostMainUIStartedMessage()
        {
            IntPtr hWnd = FindWindow("PwrMgrBkGndWindow", "PwrMgrBkGndWindow");
            if (hWnd != IntPtr.Zero)
            {
                PostMessage(hWnd, this.wmMainUIWasStarted, IntPtr.Zero, IntPtr.Zero);
            }
        }

        [DllImport("User32.dll", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
        private static extern uint PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        public void PostShowBatteryPageMessage(IntPtr hMainWindow)
        {
            PostMessage(hMainWindow, this.WM_SHOW_BATTERY_PAGE, IntPtr.Zero, IntPtr.Zero);
        }

        public void PostShowEditAgendaWindowMessage(IntPtr hMainWindow, string commandLinePowerAgendaID)
        {
            int num = Convert.ToInt32(commandLinePowerAgendaID);
            PostMessage(hMainWindow, this.WM_SHOW_EDIT_AGENDA_WINDOW, (IntPtr) num, IntPtr.Zero);
        }

        public void PostShowGlobalPageMessage(IntPtr hMainWindow)
        {
            PostMessage(hMainWindow, this.WM_SHOW_GLOBAL_PAGE, IntPtr.Zero, IntPtr.Zero);
        }

        public void PostShowInstantResumeMessage(IntPtr hMainWindow)
        {
            PostMessage(hMainWindow, this.WM_SHOW_INSTANT_RESUME, IntPtr.Zero, IntPtr.Zero);
        }

        public void PostShowPowerAgendasPageMessage(IntPtr hMainWindow)
        {
            PostMessage(hMainWindow, this.WM_SHOW_POWER_AGENDAS_PAGE, IntPtr.Zero, IntPtr.Zero);
        }

        public void PostShowPowerPlanPageMessage(IntPtr hMainWindow)
        {
            PostMessage(hMainWindow, this.WM_SHOW_POWER_PLAN_PAGE, IntPtr.Zero, IntPtr.Zero);
        }

        private void RegisterMessage()
        {
            this.wmPowerSliderPositionChange = RegisterWindowMessage("PowerUseSliderPositionChange");
            this.wmUpdateBatteryRemainingTime = RegisterWindowMessage("PwrMgrBkGndUpdateBatteryRemainingTime");
            this.wmShowBatteryPage = RegisterWindowMessage("PwrMgrShowBatteryPage");
            this.wmChangeCalculatingRemainingBatteryEvent = RegisterWindowMessage("PwrmgrChangeCalculatingRemainingBatteryEvent");
            this.wmUpdateACPowerConsumption = RegisterWindowMessage("PwrMgrBkGndUpdateACPowerConsumption");
            this.wmMainUIWasStarted = RegisterWindowMessage("PwrMgrMainUIWasStarted");
            this.wmMainUIWasEnded = RegisterWindowMessage("PwrMgrMainUIWasEnded");
            this.wmShowInstantResume = RegisterWindowMessage("PwrMgrShowInstantResume");
            this.wmChangeGreenState = RegisterWindowMessage("PwrMgrChangeGreenState");
            this.wmChangeCpuTurboState = RegisterWindowMessage("PwrMgrChangeCpuTurboState");
            this.wmShowPowerAgendasPage = RegisterWindowMessage("PwrMgrShowPowerAgendasPage");
            this.wmShowEditAgendaWindow = RegisterWindowMessage("PwrMgrShowEditAgendaWindow");
            this.wmShowPowerPlanPage = RegisterWindowMessage("PwrMgrShowPowerPlanPage");
            this.wmChangeGpuTurboState = RegisterWindowMessage("PwrMgrChangeGpuTurboState");
            this.wmShowGlobalPage = RegisterWindowMessage("PwrMgrShowGlobalPage");
        }

        [DllImport("User32.dll", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
        private static extern uint RegisterWindowMessage(string msgString);

        public static Message Instance =>
            instance;

        public uint WM_CHANGE_CALCULATING_REMAINING_BATTERY_EVENT =>
            this.wmChangeCalculatingRemainingBatteryEvent;

        public uint WM_CHANGE_CPU_TURBO_STATE =>
            this.wmChangeCpuTurboState;

        public uint WM_CHANGE_GPU_TURBO_STATE =>
            this.wmChangeGpuTurboState;

        public uint WM_CHANGE_GREEN_STATE =>
            this.wmChangeGreenState;

        public uint WM_POWERUSESLIDER_POSITION_CHANGE =>
            this.wmPowerSliderPositionChange;

        public uint WM_SHOW_BATTERY_PAGE =>
            this.wmShowBatteryPage;

        public uint WM_SHOW_EDIT_AGENDA_WINDOW =>
            this.wmShowEditAgendaWindow;

        public uint WM_SHOW_EDIT_AGENDA_WINDOW_PANEL =>
            this.wmShowEditAgendaWindowPanel;

        public uint WM_SHOW_GLOBAL_PAGE =>
            this.wmShowGlobalPage;

        public uint WM_SHOW_INSTANT_RESUME =>
            this.wmShowInstantResume;

        public uint WM_SHOW_POWER_AGENDAS_PAGE =>
            this.wmShowPowerAgendasPage;

        public uint WM_SHOW_POWER_PLAN_PAGE =>
            this.wmShowPowerPlanPage;

        public uint WM_UPDATE_AC_POWER_CONSUMPTION =>
            this.wmUpdateACPowerConsumption;

        public uint WM_UPDATE_BATTERY_REMAININGTIME =>
            this.wmUpdateBatteryRemainingTime;
    }
}

