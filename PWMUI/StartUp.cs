namespace PWMUI
{
    using Microsoft.Win32;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    public class StartUp
    {
        public static bool IsMyAssemblyInstalled(string[] args)
        {
            string executablePath = Application.ExecutablePath;
            string directoryName = Path.GetDirectoryName(executablePath);
            string path = Path.Combine(directoryName, "PWMUICtl.dll");
            if (!File.Exists(path))
            {
                return false;
            }
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall\\{9A25302D-30C0-39D9-BD6F-21E6EC160475}");
            if (key == null)
            {
                return false;
            }
            key.Close();
            DateTime lastWriteTime = File.GetLastWriteTime(executablePath);
            DateTime time2 = File.GetLastWriteTime(path);
            if (lastWriteTime.Date.CompareTo(time2.Date) != 0)
            {
                return false;
            }
            if ((args.Length == 0) || (args[0] != "Stop"))
            {
                DateTime time3;
                string name = "";
                if (CultureInfo.CurrentUICulture.CompareInfo.LCID == 0xc04)
                {
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-TW");
                    name = Thread.CurrentThread.CurrentUICulture.Name;
                }
                else
                {
                    name = CultureInfo.CurrentUICulture.Name;
                }
                string str6 = Path.Combine(Path.Combine(directoryName, name), "PWMUI.resources.dll");
                string str7 = Path.Combine(directoryName, @"EN-US\PWMUI.resources.dll");
                if (!File.Exists(str6))
                {
                    if (!File.Exists(str7))
                    {
                        return false;
                    }
                    time3 = File.GetLastWriteTime(str7);
                }
                else
                {
                    time3 = File.GetLastWriteTime(str6);
                }
                if (lastWriteTime.Date.CompareTo(time3.Date) != 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsNetfx30Installed() => 
            CheckForDotNet.IsDotNetInstalled(DotNet.Framework3, Path.GetFileName(Application.ExecutablePath));

        [STAThread]
        public static void Main(string[] args)
        {
            if (IsNetfx30Installed() && IsMyAssemblyInstalled(args))
            {
                WPFAppStart(args);
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        public static void WPFAppStart(string[] args)
        {
            App.PWMUIMain(args);
        }
    }
}

