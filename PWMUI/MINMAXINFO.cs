namespace PWMUI
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct MINMAXINFO
    {
        public POINTAPI ptReserved;
        public POINTAPI ptMaxSize;
        public POINTAPI ptMaxPosition;
        public POINTAPI ptMinTrackSize;
        public POINTAPI ptMaxTrackSize;
    }
}

