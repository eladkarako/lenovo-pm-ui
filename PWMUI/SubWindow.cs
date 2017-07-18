namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Threading;

    public class SubWindow : System.Windows.Window, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        internal Button cancelBtn;
        internal TextBlock caption;
        internal Button closeBtn;
        internal Grid contentArea;
        internal DispatcherTimer deactivateTimer = new DispatcherTimer();
        internal Border deactiveInnerFrame;
        protected bool Disposed;
        internal UserControl footer;
        internal Border frameOutline;
        internal UserControl header;
        internal Border innerFrame;
        internal Border innerFrameOutline;
        private bool isMouseEnter;
        internal Grid LayoutRoot;
        internal Button okBtn;
        private bool onlyOkButton;
        internal PWMUICtlDll pwmuiCtlDll = new PWMUICtlDll();
        internal SubWindow Window;
        public const int WM_DWMCOMPOSITIONCHANGED = 0x31e;
        public const int WM_MOUSELEAVE = 0x2a3;
        public const int WM_MOUSEMOVE = 0x200;

        public event SubWindowCancelClickEventHandler SubWindowCancelClickEvent;

        public event SubWindowOkClickEventHandler SubWindowOkClickEvent;

        public event SubWindowOkClickEventHandler2 SubWindowOkClickEvent2;

        public SubWindow()
        {
            this.InitializeComponent();
            this.deactivateTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            this.deactivateTimer.Tick += new EventHandler(this.OnDeactivateTimer);
            this.okBtn.Focus();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.SubWindowCancelClickEvent != null)
            {
                this.SubWindowCancelClickEvent();
            }
            base.Close();
        }

        private void CloseBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.SubWindowCancelClickEvent != null)
            {
                this.SubWindowCancelClickEvent();
            }
            base.Close();
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
            }
            this.Disposed = true;
        }

        [DllImport("dwmapi.dll", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
        private static extern int DwmIsCompositionEnabled(ref uint enabled);
        ~SubWindow()
        {
            this.Dispose(false);
        }

        private void header_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.DragMove();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/subwindow.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private IntPtr MessageProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            switch (msg)
            {
                case 0x200:
                    if (!this.isMouseEnter)
                    {
                        this.OnMouseEnter();
                        this.isMouseEnter = true;
                    }
                    return IntPtr.Zero;

                case 0x2a3:
                    this.OnMouseLeave();
                    this.isMouseEnter = false;
                    return IntPtr.Zero;

                case 0x31e:
                    this.SetWindowOpacity();
                    return IntPtr.Zero;
            }
            return IntPtr.Zero;
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.SubWindowOkClickEvent != null)
            {
                this.SubWindowOkClickEvent();
            }
            if ((this.SubWindowOkClickEvent2 == null) || this.SubWindowOkClickEvent2())
            {
                base.DialogResult = true;
                base.Close();
            }
        }

        private void OnDeactivateTimer(object sender, EventArgs e)
        {
            this.deactiveInnerFrame.Opacity += 0.01;
            if (this.deactiveInnerFrame.Opacity >= 0.2)
            {
                this.deactivateTimer.Stop();
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.Loaded += new RoutedEventHandler(this.RegisterNotification);
            base.Closing += new CancelEventHandler(this.UnregisterNotification);
            MainWindow.Instance.SuspendEvent += new SuspendEventHandler(this.SuspendEvent);
            base.OnInitialized(e);
        }

        private void OnMouseEnter()
        {
            if (!base.IsActive)
            {
                this.StopDeactivateTimer();
            }
        }

        private void OnMouseLeave()
        {
            if (!base.IsActive)
            {
                this.StartDeactivateTimer();
            }
        }

        private void RegisterNotification(object sender, RoutedEventArgs e)
        {
            WindowInteropHelper helper = new WindowInteropHelper(this);
            HwndSource.FromHwnd(helper.Handle).AddHook(new HwndSourceHook(this.MessageProc));
        }

        public void SetCaption(string caption)
        {
            this.caption.Text = caption;
            base.Title = caption;
        }

        public void SetContentArea(UserControl control)
        {
            this.contentArea.Children.Add(control);
        }

        public void SetTitle(string title)
        {
            base.Title = title;
        }

        private void SetWindowOpacity()
        {
            uint enabled = 0;
            DwmIsCompositionEnabled(ref enabled);
            if (enabled != 0)
            {
                this.innerFrame.Opacity = 0.75;
                this.caption.Background = (Brush) base.FindResource("CaptionBkBrush");
            }
            else
            {
                this.innerFrame.Opacity = 1.0;
                this.caption.Background = null;
            }
        }

        public void SetWindowSize(double Width, double Height)
        {
            base.Width = Width;
            base.Height = Height;
        }

        private void StartDeactivateTimer()
        {
            this.deactiveInnerFrame.Opacity = 0.0;
            this.deactiveInnerFrame.Visibility = Visibility.Visible;
            this.deactivateTimer.Start();
        }

        private void StopDeactivateTimer()
        {
            this.deactiveInnerFrame.Opacity = 0.0;
            this.deactiveInnerFrame.Visibility = Visibility.Hidden;
            this.deactivateTimer.Stop();
        }

        private void SuspendEvent()
        {
            base.Close();
        }

        [EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.Window = (SubWindow) target;
                    this.Window.Activated += new EventHandler(this.Window_Activated);
                    this.Window.Deactivated += new EventHandler(this.Window_Deactivated);
                    this.Window.Loaded += new RoutedEventHandler(this.Window_Loaded);
                    this.Window.KeyUp += new KeyEventHandler(this.Window_KeyUp);
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
                    this.header = (UserControl) target;
                    this.header.MouseLeftButtonDown += new MouseButtonEventHandler(this.header_MouseLeftButtonDown);
                    return;

                case 7:
                    this.caption = (TextBlock) target;
                    return;

                case 8:
                    this.closeBtn = (Button) target;
                    this.closeBtn.Click += new RoutedEventHandler(this.CloseBtn_Click);
                    return;

                case 9:
                    this.contentArea = (Grid) target;
                    return;

                case 10:
                    this.footer = (UserControl) target;
                    return;

                case 11:
                    this.okBtn = (Button) target;
                    this.okBtn.Click += new RoutedEventHandler(this.okBtn_Click);
                    return;

                case 12:
                    this.cancelBtn = (Button) target;
                    this.cancelBtn.Click += new RoutedEventHandler(this.cancelBtn_Click);
                    return;

                case 13:
                    this.deactiveInnerFrame = (Border) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void UnregisterNotification(object sender, CancelEventArgs e)
        {
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            this.frameOutline.BorderBrush = (Brush) base.FindResource("AlmostCyanBrush");
            this.innerFrameOutline.BorderBrush = (Brush) base.FindResource("LightGrayBrush");
            this.StopDeactivateTimer();
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            this.frameOutline.BorderBrush = (Brush) base.FindResource("SilverBrush");
            this.innerFrameOutline.BorderBrush = (Brush) base.FindResource("DkGrayBrush");
            this.StartDeactivateTimer();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                this.pwmuiCtlDll.ShowHelpFile();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.onlyOkButton)
            {
                Grid.SetColumn(this.okBtn, Grid.GetColumn(this.cancelBtn));
                this.cancelBtn.Visibility = Visibility.Hidden;
            }
            this.SetWindowOpacity();
        }

        public bool OnlyOkButton
        {
            get => 
                this.onlyOkButton;
            set
            {
                this.onlyOkButton = value;
            }
        }
    }
}

