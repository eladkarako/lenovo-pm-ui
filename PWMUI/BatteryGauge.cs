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
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    public class BatteryGauge : System.Windows.Controls.UserControl, IComponentConnector
    {
        private bool _contentLoaded;
        internal Image batteryBk;
        internal Image batteryGauge;
        internal Image batteryMask;
        internal Grid LayoutRoot;
        internal TextBlock percentage;
        internal BatteryGauge UserControl;

        public BatteryGauge()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/batterygauge.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public void Refresh(BatteryInformer informer)
        {
            this.RefreshPercentage(informer);
            this.RefreshGaugeStatus(informer);
            this.RefreshGaugeColor(informer);
        }

        private void RefreshGaugeColor(BatteryInformer informer)
        {
            uint percent = informer.RemainingPercent.Percent;
            if (percent > 10)
            {
                this.batteryGauge.Source = (BitmapImage) base.FindResource("Battery2Gauge");
            }
            else if ((percent > 3) && (percent <= 10))
            {
                this.batteryGauge.Source = (BitmapImage) base.FindResource("Battery3GaugeOrange");
            }
            else
            {
                this.batteryGauge.Source = (BitmapImage) base.FindResource("Battery2GaugeRed");
            }
        }

        private void RefreshGaugeStatus(BatteryInformer informer)
        {
            if (((informer.Status.MyStatus == BatteryStatus.Status.detach) || (informer.Status.MyStatus == BatteryStatus.Status.error)) || !informer.RemainingPercent.IsCapable())
            {
                this.batteryGauge.Visibility = Visibility.Hidden;
                this.batteryMask.Visibility = Visibility.Hidden;
                this.batteryBk.Source = (BitmapImage) base.FindResource("BatteryNotInstalled");
                this.percentage.Text = "---";
                this.percentage.Foreground = Brushes.Black;
            }
            else
            {
                this.batteryGauge.Visibility = Visibility.Visible;
                this.batteryMask.Visibility = Visibility.Visible;
                this.batteryBk.Source = (BitmapImage) base.FindResource("Battery3Bk");
                this.percentage.Foreground = Brushes.White;
            }
        }

        private void RefreshPercentage(BatteryInformer informer)
        {
            this.percentage.Text = $"{informer.RemainingPercent.Percent}%";
            this.batteryGauge.Width = informer.RemainingPercent.Percent;
        }

        [EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (BatteryGauge) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.batteryBk = (Image) target;
                    return;

                case 4:
                    this.batteryGauge = (Image) target;
                    return;

                case 5:
                    this.batteryMask = (Image) target;
                    return;

                case 6:
                    this.percentage = (TextBlock) target;
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

