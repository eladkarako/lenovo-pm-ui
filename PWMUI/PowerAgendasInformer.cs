namespace PWMUI
{
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.InteropServices;

    internal class PowerAgendasInformer
    {
        private const int WM_APP = 0x8000;
        private const int WM_APP_REFRESH_TASKS = 0x8064;
        private const string WND_CLASS = "ScheduledTaskClass_{F12D0E3D-0AEC-4216-B0F8-4C9D0A407492}";
        private const string WND_TITLE = "ScheduledTask";

        public void Apply(ObservableCollection<PowerAgendaListData> list)
        {
            new PowerAgendaSaver().Save(list);
            int hWnd = FindWindow("ScheduledTaskClass_{F12D0E3D-0AEC-4216-B0F8-4C9D0A407492}", "ScheduledTask");
            if (hWnd > 0)
            {
                PostMessage(hWnd, 0x8064, 0, 0);
            }
        }

        [DllImport("user32.dll")]
        public static extern int FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int PostMessage(int hWnd, uint Msg, int wParam, int lParam);
        public void Refresh()
        {
        }
    }
}

