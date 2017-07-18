namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Shapes;

    public class CpuMeter : System.Windows.Controls.UserControl, IComponentConnector
    {
        private bool _contentLoaded;
        private CpuInformer cpuInformer = new CpuInformer();
        private bool isRegisteredEvent;
        internal Grid LayoutRoot;
        internal Rectangle meter;
        internal Rectangle meterArrow;
        private IntelCpuArchitecture turboGeneration = IntelCpuArchitecture.Unknown;
        internal TextBlock turboStatus;
        internal CpuMeter UserControl;

        public CpuMeter()
        {
            this.InitializeComponent();
            this.turboGeneration = this.cpuInformer.GetIntelTurboBoostGeneration();
        }

        private void ChangeCpuTurboStateEvent(uint isReady)
        {
            this.Refresh();
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/cpumeter.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
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
                if (this.turboGeneration != IntelCpuArchitecture.WestmereOrEarlier)
                {
                    base.Visibility = Visibility.Hidden;
                }
                else
                {
                    MainWindow.Instance.ChangeCpuTurboStateEvent += new ChangeCpuTurboStateEventHandler(this.ChangeCpuTurboStateEvent);
                    this.isRegisteredEvent = true;
                    this.Refresh();
                }
            }
        }

        private void Refresh()
        {
            if (this.cpuInformer.IsTurboEnabled())
            {
                base.Visibility = Visibility.Visible;
            }
            else
            {
                base.Visibility = Visibility.Hidden;
            }
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (CpuMeter) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.meter = (Rectangle) target;
                    return;

                case 4:
                    this.meterArrow = (Rectangle) target;
                    return;

                case 5:
                    this.turboStatus = (TextBlock) target;
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

