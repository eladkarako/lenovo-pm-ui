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

    public class GreenPanel : System.Windows.Controls.UserControl, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        protected bool Disposed;
        public const int FIFTEEN_MINUTES = 900;
        private DispatcherTimer greenCheckTimer = new DispatcherTimer();
        internal Rectangle greenicon_design;
        internal TextBlock greenText1;
        internal TextBlock greenText2;
        private PowerPlanInformer informer = new PowerPlanInformer();
        private bool isRegisteredEvent;
        internal Grid LayoutRoot;
        internal PMLinkTextBlock learnMoreLinkText;
        public const int NEVER = 0;
        public const int THIRTY_MINUTES = 0x708;
        internal GreenPanel UserControl;

        public GreenPanel()
        {
            this.InitializeComponent();
            this.greenCheckTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            this.greenCheckTimer.Tick += new EventHandler(this.greenCheckTimer_Tick);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void ChangeACDCPowerSource()
        {
            this.CheckGreenCondition();
        }

        private void ChangeActivePowerSchemeEvent()
        {
            this.CheckGreenCondition();
        }

        private void ChangeDiskPowerdownTimeoutEvent()
        {
            this.CheckGreenCondition();
        }

        private void ChangeGreenStateEvent(uint greenState)
        {
            this.greenCheckTimer.Start();
        }

        private void ChangeStandbyTimeoutEvent()
        {
            this.CheckGreenCondition();
        }

        private void ChangeVideoPowerdownTimeout()
        {
            this.CheckGreenCondition();
        }

        private void CheckGreenCondition()
        {
            if (this.IsGreenCondition())
            {
                this.greenicon_design.Fill = (Brush) base.FindResource("_14_eco-on");
                this.learnMoreLinkText.Visibility = Visibility.Visible;
                this.greenText1.Visibility = Visibility.Visible;
                this.greenText2.Visibility = Visibility.Visible;
                this.greenText1.Focusable = true;
                this.greenText2.Focusable = true;
                AutomationProperties.SetName(this.greenText1, this.greenText1.Text);
                AutomationProperties.SetName(this.greenText2, this.greenText2.Text);
            }
            else
            {
                this.greenicon_design.Fill = (Brush) base.FindResource("_13_eco-off");
                this.learnMoreLinkText.Visibility = Visibility.Hidden;
                this.greenText1.Visibility = Visibility.Hidden;
                this.greenText2.Visibility = Visibility.Hidden;
                this.greenText1.Focusable = false;
                this.greenText2.Focusable = false;
            }
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

        ~GreenPanel()
        {
            this.Dispose(false);
        }

        private void greenCheckTimer_Tick(object sender, EventArgs e)
        {
            this.greenCheckTimer.Stop();
            this.CheckGreenCondition();
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/greenpanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private bool IsGreenCondition()
        {
            this.informer.Refresh();
            return this.informer.IsGreen();
        }

        private void learnMoreLinkText_LinkTextClickedEvent()
        {
            GreenStandardsPanel control = new GreenStandardsPanel();
            SubWindow window = new SubWindow {
                Owner = MainWindow.Instance
            };
            window.SetCaption((string) base.FindResource("CaptionGreenConfig"));
            window.SetTitle((string) base.FindResource("CaptionGreenConfig"));
            window.SetContentArea(control);
            window.OnlyOkButton = true;
            window.Width = 350.0;
            window.Height = 320.0;
            window.ShowDialog();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
            base.OnInitialized(e);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!this.isRegisteredEvent)
            {
                MainWindow.Instance.ChangeGreenStateEvent += new ChangeGreenStateEventHandler(this.ChangeGreenStateEvent);
                this.isRegisteredEvent = true;
                this.CheckGreenCondition();
            }
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (GreenPanel) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.greenicon_design = (Rectangle) target;
                    return;

                case 4:
                    this.greenText1 = (TextBlock) target;
                    return;

                case 5:
                    this.greenText2 = (TextBlock) target;
                    return;

                case 6:
                    this.learnMoreLinkText = (PMLinkTextBlock) target;
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

