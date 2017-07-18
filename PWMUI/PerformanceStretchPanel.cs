namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Shapes;
    using System.Windows.Threading;

    public class PerformanceStretchPanel : System.Windows.Controls.UserControl, IComponentConnector
    {
        private bool _contentLoaded;
        private CpuInformer cpuInformer = new CpuInformer();
        internal Rectangle cpuMeter;
        internal Rectangle cpuNeedle;
        private double cpuOfRatio;
        internal TextBlock cpuRatio;
        internal StackPanel cpuRatioPanel;
        internal TextBlock cpuRatioUnit;
        internal TextBlock cpuTitle;
        private double cpuTurboAngleSize;
        private double cpuTurboMax;
        protected bool Disposed;
        private GpuInformer gpuInformer = new GpuInformer();
        internal Rectangle gpuMeter;
        internal Rectangle gpuNeedle;
        private double gpuOfRatio;
        internal TextBlock gpuRatio;
        internal StackPanel gpuRatioPanel;
        internal TextBlock gpuRatioUnit;
        internal TextBlock gpuTitle;
        private double gpuTurboAngleSize;
        private double gpuTurboMax;
        private bool isRegisteredEvent;
        internal Grid LayoutRoot;
        private const double METER_ANGLE_SIZE = 1.8;
        private const double METER_END = 116.0;
        private const double METER_MAX = 100.0;
        private const double METER_START = -64.0;
        private const double NVIDIA_ANGLE = 194.0;
        internal Ellipse nvidiaCircle;
        internal Ellipse nvidiaCircle2;
        internal Image nvidiaImage;
        private const double ONE_HUNDRED_PERCENT = 100.0;
        private PerformanceStretch performanceStretch = new PerformanceStretch();
        internal Button stretchIcon;
        internal Grid stretchPanel;
        internal TextBlock stretchState;
        internal TextBlock stretchText;
        private const double TURBO_END = 171.0;
        private const double TURBO_START = 117.0;
        private IntelCpuArchitecture turboGeneration = IntelCpuArchitecture.Unknown;
        internal TextBlock turboMessage;
        private DispatcherTimer turboMeterUpdateTimer = new DispatcherTimer();
        internal PerformanceStretchPanel UserControl;

        public PerformanceStretchPanel()
        {
            this.InitializeComponent();
            this.turboGeneration = this.cpuInformer.GetIntelTurboBoostGeneration();
        }

        private void ChangeCpuTurboStateEvent(uint isReady)
        {
            this.RefreshCpuMeter();
            this.RefreshPerformanceStretch();
        }

        private void ChangeGpuTurboStateEvent(uint isReady)
        {
            this.RefreshGpuMeter();
            this.RefreshPerformanceStretch();
        }

        private void ChangePerformanceStretchState()
        {
            if (this.performanceStretch.IsEnabled())
            {
                this.performanceStretch.Disable();
            }
            else
            {
                this.performanceStretch.Enable();
            }
            this.RefreshPerformanceStretch();
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
                this.turboMeterUpdateTimer.Stop();
            }
            this.Disposed = true;
        }

        ~PerformanceStretchPanel()
        {
            this.Dispose(false);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/performancestretchpanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void InitializeTurboMeter()
        {
            this.turboMeterUpdateTimer.Interval = TimeSpan.FromMilliseconds((double) this.cpuInformer.GetPollingInterval());
            this.turboMeterUpdateTimer.Tick += new EventHandler(this.TurboMeterUpdateTimer_Tick);
            this.turboMeterUpdateTimer.Start();
            this.cpuTurboMax = this.cpuInformer.GetMaxRatioOfFrequency() - 100.0;
            this.cpuTurboAngleSize = 54.0 / this.cpuTurboMax;
            this.gpuTurboMax = this.gpuInformer.GetMaxRatioOfFrequency() - 100.0;
            this.gpuTurboAngleSize = 54.0 / this.gpuTurboMax;
        }

        private void Refresh()
        {
            this.cpuOfRatio = this.cpuInformer.GetRatioOfFrequency();
            this.gpuOfRatio = this.gpuInformer.GetRatioOfFrequency();
            this.RefreshRatioOfCpuFrequency();
            this.RefreshRatioOfGpuFrequency();
            this.RefreshCpuMeter();
            this.RefreshGpuMeter();
            this.RefreshPerformanceStretch();
        }

        private void RefreshCpuMeter()
        {
            this.cpuMeter.Fill = (Brush) base.FindResource("_01_meter");
            this.turboMessage.Visibility = Visibility.Hidden;
            if (this.cpuInformer.IsTurboEnabled())
            {
                this.turboMessage.Visibility = Visibility.Visible;
                if (this.cpuOfRatio > 100.0)
                {
                    this.cpuMeter.Fill = (Brush) base.FindResource("_03_turbo_on_o100");
                }
                else
                {
                    this.cpuMeter.Fill = (Brush) base.FindResource("_02_turbo_on_u100");
                }
            }
            if (this.gpuInformer.IsOptimus())
            {
                this.cpuInformer.UpdateCPUTurboState();
            }
        }

        private void RefreshGpuMeter()
        {
            this.gpuMeter.Fill = (Brush) base.FindResource("_01_meter");
            this.gpuMeter.Visibility = Visibility.Visible;
            this.gpuNeedle.Visibility = Visibility.Visible;
            this.gpuTitle.Visibility = Visibility.Visible;
            this.gpuRatioPanel.Visibility = Visibility.Visible;
            this.nvidiaCircle.Visibility = Visibility.Hidden;
            this.nvidiaCircle2.Visibility = Visibility.Hidden;
            this.nvidiaImage.Visibility = Visibility.Hidden;
            if (this.gpuInformer.IsPureDiscrete())
            {
                this.gpuMeter.Visibility = Visibility.Hidden;
                this.gpuNeedle.Visibility = Visibility.Hidden;
                this.gpuTitle.Visibility = Visibility.Hidden;
                this.gpuRatioPanel.Visibility = Visibility.Hidden;
            }
            else
            {
                if (this.gpuInformer.IsOptimus())
                {
                    if (this.gpuInformer.IsGPUOverclocked())
                    {
                        this.gpuMeter.Fill = (Brush) base.FindResource("_07_turbo_gps");
                        this.gpuRatioPanel.Visibility = Visibility.Hidden;
                        this.nvidiaImage.Visibility = Visibility.Visible;
                        this.nvidiaCircle2.Visibility = Visibility.Visible;
                        return;
                    }
                    this.nvidiaCircle.Visibility = Visibility.Visible;
                    if (this.gpuInformer.IsWorkingNVIDIA())
                    {
                        this.gpuMeter.Fill = (Brush) base.FindResource("_03_turbo_on_o100");
                        this.gpuRatioPanel.Visibility = Visibility.Hidden;
                        this.nvidiaImage.Visibility = Visibility.Visible;
                        return;
                    }
                    this.gpuRatioPanel.Visibility = Visibility.Visible;
                    this.nvidiaImage.Visibility = Visibility.Hidden;
                }
                if (this.gpuInformer.IsTurboEnabled())
                {
                    if (this.gpuOfRatio > 100.0)
                    {
                        this.gpuMeter.Fill = (Brush) base.FindResource("_03_turbo_on_o100");
                    }
                    else
                    {
                        this.gpuMeter.Fill = (Brush) base.FindResource("_02_turbo_on_u100");
                    }
                    this.gpuNeedle.Visibility = Visibility.Visible;
                    this.gpuRatioPanel.Visibility = Visibility.Visible;
                }
            }
        }

        private void RefreshPerformanceStretch()
        {
            if (!this.performanceStretch.IsCapable())
            {
                this.stretchPanel.Visibility = Visibility.Hidden;
            }
            else
            {
                this.stretchPanel.Visibility = Visibility.Visible;
                if (this.performanceStretch.IsEnabled())
                {
                    this.stretchIcon.Style = (Style) base.FindResource("_12_fan-on_button");
                    this.stretchState.Text = (string) base.FindResource("PerformanceStretchOn");
                }
                else
                {
                    this.stretchIcon.Style = (Style) base.FindResource("_12_fan-off_button");
                    this.stretchState.Text = (string) base.FindResource("PerformanceStretchOff");
                }
                string str = this.stretchText.Text + " " + this.stretchState.Text;
                AutomationProperties.SetName(this.stretchIcon, str);
            }
        }

        private void RefreshRatioOfCpuFrequency()
        {
            double angle = 0.0;
            if (this.cpuOfRatio <= 100.0)
            {
                angle = -64.0 + (1.8 * this.cpuOfRatio);
            }
            else
            {
                angle = 117.0 + (this.cpuTurboAngleSize * (this.cpuOfRatio - 100.0));
            }
            this.cpuNeedle.RenderTransform = new RotateTransform(angle);
            this.cpuRatio.Text = this.cpuOfRatio.ToString();
        }

        private void RefreshRatioOfGpuFrequency()
        {
            double angle = 0.0;
            if (this.gpuOfRatio <= 100.0)
            {
                angle = -64.0 + (1.8 * this.gpuOfRatio);
            }
            else
            {
                angle = 117.0 + (this.gpuTurboAngleSize * (this.gpuOfRatio - 100.0));
            }
            if (this.gpuInformer.IsWorkingNVIDIA())
            {
                angle = 194.0;
            }
            if (this.gpuInformer.IsGPUOverclocked())
            {
                angle = 194.0;
            }
            this.gpuNeedle.RenderTransform = new RotateTransform(angle);
            this.gpuRatio.Text = this.gpuOfRatio.ToString();
        }

        private void stretchIcon_Click(object sender, RoutedEventArgs e)
        {
            this.ChangePerformanceStretchState();
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (PerformanceStretchPanel) target;
                    this.UserControl.Loaded += new RoutedEventHandler(this.UserControl_Loaded);
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.gpuMeter = (Rectangle) target;
                    return;

                case 4:
                    this.nvidiaCircle = (Ellipse) target;
                    return;

                case 5:
                    this.nvidiaCircle2 = (Ellipse) target;
                    return;

                case 6:
                    this.nvidiaImage = (Image) target;
                    return;

                case 7:
                    this.gpuNeedle = (Rectangle) target;
                    return;

                case 8:
                    this.gpuTitle = (TextBlock) target;
                    return;

                case 9:
                    this.gpuRatioPanel = (StackPanel) target;
                    return;

                case 10:
                    this.gpuRatio = (TextBlock) target;
                    return;

                case 11:
                    this.gpuRatioUnit = (TextBlock) target;
                    return;

                case 12:
                    this.cpuMeter = (Rectangle) target;
                    return;

                case 13:
                    this.cpuNeedle = (Rectangle) target;
                    return;

                case 14:
                    this.cpuTitle = (TextBlock) target;
                    return;

                case 15:
                    this.cpuRatioPanel = (StackPanel) target;
                    return;

                case 0x10:
                    this.cpuRatio = (TextBlock) target;
                    return;

                case 0x11:
                    this.cpuRatioUnit = (TextBlock) target;
                    return;

                case 0x12:
                    this.turboMessage = (TextBlock) target;
                    return;

                case 0x13:
                    this.stretchPanel = (Grid) target;
                    return;

                case 20:
                    this.stretchIcon = (Button) target;
                    this.stretchIcon.Click += new RoutedEventHandler(this.stretchIcon_Click);
                    return;

                case 0x15:
                    this.stretchText = (TextBlock) target;
                    return;

                case 0x16:
                    this.stretchState = (TextBlock) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void TurboMeterUpdateTimer_Tick(object sender, EventArgs e)
        {
            this.cpuOfRatio = this.cpuInformer.GetRatioOfFrequency();
            this.gpuOfRatio = this.gpuInformer.GetRatioOfFrequency();
            this.RefreshRatioOfCpuFrequency();
            this.RefreshRatioOfGpuFrequency();
            this.RefreshCpuMeter();
            this.RefreshGpuMeter();
            AutomationProperties.SetName(this, this.cpuTitle.Text + " " + this.cpuRatio.Text + this.cpuRatioUnit.Text + " " + this.gpuTitle.Text + " " + this.gpuRatio.Text + this.gpuRatioUnit.Text + " " + ((this.turboMessage.Visibility == Visibility.Visible) ? this.turboMessage.Text : ""));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (!this.isRegisteredEvent)
            {
                if ((this.turboGeneration < IntelCpuArchitecture.SandyBridge) || (this.turboGeneration == IntelCpuArchitecture.Unknown))
                {
                    base.Visibility = Visibility.Hidden;
                }
                else
                {
                    MainWindow.Instance.ChangeCpuTurboStateEvent += new ChangeCpuTurboStateEventHandler(this.ChangeCpuTurboStateEvent);
                    MainWindow.Instance.ChangeGpuTurboStateEvent += new ChangeGpuTurboStateEventHandler(this.ChangeGpuTurboStateEvent);
                    this.isRegisteredEvent = true;
                    this.InitializeTurboMeter();
                    this.Refresh();
                }
            }
        }
    }
}

