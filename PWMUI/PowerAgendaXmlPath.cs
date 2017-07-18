namespace PWMUI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class PowerAgendaXmlPath
    {
        private const int CSIDL_APPDATA = 0x1a;
        private const int MAX_PATH = 0xff;

        [DllImport("kernel32.dll")]
        public static extern int CreateDirectory(StringBuilder lpPathName, IntPtr lpSecurityAttributes);
        public static string GetPath()
        {
            StringBuilder pszPath = new StringBuilder(0x100);
            if (SHGetFolderPath(IntPtr.Zero, 0x1a, IntPtr.Zero, 0, pszPath) >= 0)
            {
                if (pszPath[pszPath.Length - 1] != '\\')
                {
                    pszPath.Append('\\');
                }
                pszPath.Append("PwrMgr");
                CreateDirectory(pszPath, IntPtr.Zero);
                pszPath.Append('\\');
            }
            return pszPath.Append("ScheduledTask.xml").ToString();
        }

        [DllImport("shell32.dll")]
        public static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, uint dwFlags, StringBuilder pszPath);
    }
}

