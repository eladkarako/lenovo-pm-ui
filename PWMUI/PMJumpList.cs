namespace PWMUI
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class PMJumpList : FrameworkElement
    {
        private static string strAppID = "ThinkPad.PowerManager.PWMUI";

        public bool SetPMAppID()
        {
            string appID = null;
            UnsafeNativeMethods.GetCurrentProcessExplicitAppUserModelID(out appID);
            if (string.IsNullOrEmpty(appID) || (appID.CompareTo(strAppID) != 0))
            {
                UnsafeNativeMethods.SetCurrentProcessExplicitAppUserModelID(strAppID);
            }
            UnsafeNativeMethods.GetCurrentProcessExplicitAppUserModelID(out appID);
            return (!string.IsNullOrEmpty(appID) && (appID.CompareTo(strAppID) == 0));
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct CALPWSTR
        {
            internal uint cElems;
            internal IntPtr pElems;
        }

        [ComImport, Guid("886d8eeb-8cf2-4446-8d02-cdba1dbdcf99"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        internal interface IPropertyStore
        {
            void GetCount(out uint cProps);
            void GetAt(uint iProp, [MarshalAs(UnmanagedType.Struct)] out PMJumpList.PropertyKey pkey);
            void GetValue([In, MarshalAs(UnmanagedType.Struct)] ref PMJumpList.PropertyKey pkey, [MarshalAs(UnmanagedType.Struct)] out PMJumpList.PropVariant pv);
            void SetValue([In, MarshalAs(UnmanagedType.Struct)] ref PMJumpList.PropertyKey pkey, [In, MarshalAs(UnmanagedType.Struct)] ref PMJumpList.PropVariant pv);
            void Commit();
        }

        [Flags]
        internal enum KNOWNDESTCATEGORY
        {
            KDC_FREQUENT = 1,
            KDC_RECENT = 2
        }

        [StructLayout(LayoutKind.Sequential, Pack=4)]
        internal struct PropertyKey
        {
            public Guid fmtid;
            public uint pid;
            public static PMJumpList.PropertyKey PKEY_Title;
            public static PMJumpList.PropertyKey PKEY_AppUserModel_ID;
            public static PMJumpList.PropertyKey PKEY_AppUserModel_IsDestListSeparator;
            public static PMJumpList.PropertyKey PKEY_AppUserModel_RelaunchCommand;
            public static PMJumpList.PropertyKey PKEY_AppUserModel_RelaunchDisplayNameResource;
            public static PMJumpList.PropertyKey PKEY_AppUserModel_RelaunchIconResource;
            public PropertyKey(Guid fmtid, uint pid)
            {
                this.fmtid = fmtid;
                this.pid = pid;
            }

            static PropertyKey()
            {
                PKEY_Title = new PMJumpList.PropertyKey(new Guid("F29F85E0-4FF9-1068-AB91-08002B27B3D9"), 2);
                PKEY_AppUserModel_ID = new PMJumpList.PropertyKey(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), 5);
                PKEY_AppUserModel_IsDestListSeparator = new PMJumpList.PropertyKey(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), 6);
                PKEY_AppUserModel_RelaunchCommand = new PMJumpList.PropertyKey(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), 2);
                PKEY_AppUserModel_RelaunchDisplayNameResource = new PMJumpList.PropertyKey(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), 4);
                PKEY_AppUserModel_RelaunchIconResource = new PMJumpList.PropertyKey(new Guid("9F4C2855-9F79-4B39-A8D0-E1D42DE1D5F3"), 3);
            }
        }

        [StructLayout(LayoutKind.Explicit)]
        internal struct PropVariant : IDisposable
        {
            [FieldOffset(8)]
            private short boolValue;
            [FieldOffset(8)]
            private byte byteValue;
            [MarshalAs(UnmanagedType.Struct), FieldOffset(8)]
            private PMJumpList.CALPWSTR calpwstr;
            [FieldOffset(8)]
            private long longValue;
            [FieldOffset(8)]
            private IntPtr pointerValue;
            [FieldOffset(0)]
            private ushort vt;

            public void Clear()
            {
                PropVariantClear(ref this);
            }

            public void Dispose()
            {
                this.pointerValue = IntPtr.Zero;
            }

            public string GetValue() => 
                Marshal.PtrToStringUni(this.pointerValue);

            [DllImport("ole32.dll")]
            private static extern int PropVariantClear(ref PMJumpList.PropVariant pvar);
            public void SetValue(bool val)
            {
                this.Clear();
                this.vt = 11;
                this.boolValue = val ? ((short) (-1)) : ((short) 0);
            }

            public void SetValue(string val)
            {
                this.Clear();
                this.vt = 0x1f;
                this.pointerValue = Marshal.StringToCoTaskMemUni(val);
            }

            public VarEnum VarType =>
                ((VarEnum) this.vt);
        }

        internal static class UnsafeNativeMethods
        {
            [DllImport("shell32.dll")]
            public static extern void GetCurrentProcessExplicitAppUserModelID([MarshalAs(UnmanagedType.LPWStr)] out string AppID);
            [DllImport("shell32.dll")]
            public static extern void SetCurrentProcessExplicitAppUserModelID([MarshalAs(UnmanagedType.LPWStr)] string AppID);
            [DllImport("shell32.dll")]
            public static extern int SHGetPropertyStoreForWindow(IntPtr hwnd, ref Guid iid, [MarshalAs(UnmanagedType.Interface)] out PMJumpList.IPropertyStore propertyStore);
        }
    }
}

