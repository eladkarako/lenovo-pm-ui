namespace PWMUI
{
    using Microsoft.Win32;
    using PWMUI.res;
    using System;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Windows.Forms;

    internal static class CheckForDotNet
    {
        private const string DotNet2DetectionKey = @"Software\Microsoft\NET Framework Setup\NDP\v2.0.50727";
        private const string DotNet35DetectionKey = @"Software\Microsoft\NET Framework Setup\NDP\v3.5";
        private const string DotNet3DetectionKey = @"Software\Microsoft\NET Framework Setup\NDP\v3.0";
        private const string DotNet3DetectionKey2 = @"Software\Microsoft\NET Framework Setup\NDP\v3.0\Setup";
        private const string DotNet4ClientDetectionKey = @"Software\Microsoft\NET Framework Setup\NDP\v4\Client";
        private const string DotNet4FullDetectionKey = @"Software\Microsoft\NET Framework Setup\NDP\v4\Full";
        private const string InstallName = "Install";
        private const string InstallSuccessName = "InstallSuccess";
        private const string ServicePackName = "SP";

        public static void DisplayErrorDlg(DotNet requiredFramework, string appTitle)
        {
            string[] strArray = new string[] { "Microsoft .NET Framework 2.0", "Microsoft .NET Framework 2.0 Service Pack 1", "Microsoft .NET Framework 2.0 Service Pack 2", "Microsoft .NET Framework 3.0", "Microsoft .NET Framework 3.0 Service Pack 1", "Microsoft .NET Framework 3.0 Service Pack 2", "Microsoft .NET Framework 3.5", "Microsoft .NET Framework 3.5 Service Pack 1", "Microsoft .NET Framework 4.0 Client", "Microsoft .NET Framework 4.0 Full" };
            uint index = (uint) requiredFramework;
            if (index >= strArray.Length)
            {
                throw new ArgumentException("Must be a value from the DotNet enumeration.", "requiredFramework");
            }
            MessageBox.Show(string.Format(CultureInfo.CurrentCulture, DotNetErrorMessages.DotNetMissing, new object[] { strArray[index] }), appTitle ?? Path.GetFileName(Assembly.GetExecutingAssembly().Location), MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1, 0);
        }

        public static int GetInstallSuccess(DotNet requiredFramework)
        {
            string name = null;
            switch (requiredFramework)
            {
                case DotNet.Framework3:
                case DotNet.Framework3SP1:
                case DotNet.Framework3SP2:
                    name = @"Software\Microsoft\NET Framework Setup\NDP\v3.0\Setup";
                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(name))
                    {
                        if (key != null)
                        {
                            try
                            {
                                return (int) key.GetValue("InstallSuccess", 0);
                            }
                            catch (InvalidCastException)
                            {
                            }
                        }
                    }
                    return 0;
            }
            return 0;
        }

        public static bool IsDotNetInstalled(DotNet requiredFramework, string appTitle)
        {
            bool flag = false;
            string name = null;
            uint num = 0;
            switch (requiredFramework)
            {
                case DotNet.Framework2:
                case DotNet.Framework2SP1:
                case DotNet.Framework2SP2:
                    name = @"Software\Microsoft\NET Framework Setup\NDP\v2.0.50727";
                    break;

                case DotNet.Framework3:
                case DotNet.Framework3SP1:
                case DotNet.Framework3SP2:
                    name = @"Software\Microsoft\NET Framework Setup\NDP\v3.0";
                    break;

                case DotNet.Framework35:
                case DotNet.Framework35SP1:
                    name = @"Software\Microsoft\NET Framework Setup\NDP\v3.5";
                    break;

                case DotNet.Framework4Client:
                    name = @"Software\Microsoft\NET Framework Setup\NDP\v4\Client";
                    break;

                case DotNet.Framework4Full:
                    name = @"Software\Microsoft\NET Framework Setup\NDP\v4\Full";
                    break;

                default:
                    throw new ArgumentException("Must be a value from the DotNet enumeration.", "requiredFramework");
            }
            switch (requiredFramework)
            {
                case DotNet.Framework2SP1:
                case DotNet.Framework3SP1:
                case DotNet.Framework35SP1:
                    num = 1;
                    break;

                case DotNet.Framework2SP2:
                case DotNet.Framework3SP2:
                    num = 2;
                    break;

                default:
                    num = 0;
                    break;
            }
            if (name != null)
            {
                using (RegistryKey key = Registry.LocalMachine.OpenSubKey(name))
                {
                    if (key != null)
                    {
                        try
                        {
                            int installSuccess = (int) key.GetValue("Install", 0);
                            int num3 = (int) key.GetValue("SP", 0);
                            if (installSuccess == 0)
                            {
                                installSuccess = GetInstallSuccess(requiredFramework);
                            }
                            flag = (installSuccess != 0) && (num3 >= num);
                        }
                        catch (InvalidCastException)
                        {
                        }
                    }
                }
            }
            if (!flag && (appTitle != null))
            {
                DisplayErrorDlg(requiredFramework, appTitle);
            }
            return flag;
        }
    }
}

