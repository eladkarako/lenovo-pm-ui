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
    using System.Windows.Media.Imaging;

    public class PowerManagementOptionsPanel : System.Windows.Controls.UserControl, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        internal PMCheckBox airplaneMode;
        internal PMLinkTextBlock alwasOnUsb2LearnMore;
        internal PMCheckBox alwaysOnUsb;
        internal PMCheckBox alwaysOnUsb2;
        internal PMComboBox alwaysOnUSB2mode;
        internal Grid alwaysOnUsb2Panel;
        internal TextBlock alwaysOnUsb2Text;
        internal StackPanel alwaysOnUsb2TextPanel;
        internal PMCheckBox alwaysOnUsb4;
        internal Image alwaysOnUsb4Icon;
        internal Grid alwaysOnUsb4Panel;
        internal PMCheckBox alwaysOnUsb4S4S5;
        internal PMLinkTextBlock alwaysOnUsb4Text;
        private AlwaysOnUSBItem alwaysOnUSBItem = new AlwaysOnUSBItem();
        private BatteryStretch batteryStretch = new BatteryStretch();
        internal Image batteryStretchIcon;
        internal Grid batteryStretchPanel;
        internal Button batteryStretchSettingBtn;
        internal TextBlock batteryStretchStatus;
        internal PMCheckBox beepPowerCtrl;
        internal PMCheckBox coolQuietMode;
        internal PMCheckBox coolQuietModeAuto;
        internal Grid coolQuietModePanel;
        protected bool Disposed;
        internal PMCheckBox enableIFFS;
        internal PMCheckBox enableISCT;
        internal PMCheckBox enableISSC;
        internal RowDefinition gridAirplaneMode;
        internal RowDefinition gridAlwaysOnUsb;
        internal RowDefinition gridBatteryStretchPanel;
        internal RowDefinition gridBeepPowerCtrl;
        internal RowDefinition gridCoolQuietMode;
        internal RowDefinition gridCoolQuietModeAuto;
        internal RowDefinition gridHybridBoost;
        internal RowDefinition gridIffsPanel;
        internal RowDefinition gridIsctPanel;
        internal RowDefinition gridIsscPanel;
        internal RowDefinition gridVideoPlayback;
        internal RowDefinition gridWiFiAutoDetectPanel;
        internal PMCheckBox hybridBoost;
        public const int IDOK = 1;
        internal Grid iffsPanel;
        internal Grid isctPanel;
        private bool isRegisteredEvent;
        internal Grid isscPanel;
        internal Grid LayoutRoot;
        internal PMLinkTextBlock learnMoreIFFS;
        internal PMLinkTextBlock learnMoreISCT;
        internal PMLinkTextBlock learnMoreISSC;
        internal PMLinkTextBlock learnMoreWiFiDetect;
        internal Grid settingItemGrid;
        internal Image usbPowerd;
        internal PowerManagementOptionsPanel UserControl;
        internal PMCheckBox videoPlayback;
        internal Grid wiFiAutoDetectPanel;
        internal PMCheckBox wiFiDetectCheckBox;

        public PowerManagementOptionsPanel()
        {
            this.InitializeComponent();
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void alwasOnUsb2LearnMore_LinkTextClickedEvent()
        {
            MainWindow.Instance.pwmuiCtlDll.ShowHelpFile("PMVGPA.HTM");
        }

        private void alwasOnUsb4LearnMore_LinkTextClickedEvent()
        {
            MainWindow.Instance.pwmuiCtlDll.ShowHelpFile("PMVGPA.HTM");
        }

        private void alwaysOnUsb2_Click(object sender, RoutedEventArgs e)
        {
            bool? isChecked = this.alwaysOnUsb2.IsChecked;
            this.alwaysOnUSB2mode.IsEnabled = isChecked.HasValue ? isChecked.GetValueOrDefault() : false;
        }

        private void alwaysOnUSB2mode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.alwaysOnUSB2mode.GetCurrentValue() == 1)
            {
                this.alwaysOnUsb2Text.Text = (string) base.FindResource("UsbPortAny");
                this.usbPowerd.Visibility = Visibility.Hidden;
            }
            else
            {
                this.alwaysOnUsb2Text.Text = (string) base.FindResource("UsbPortWithBatteryIcon");
                this.usbPowerd.Visibility = Visibility.Visible;
            }
        }

        private void alwaysOnUsb4_Click(object sender, RoutedEventArgs e)
        {
            bool? isChecked = this.alwaysOnUsb4.IsChecked;
            this.alwaysOnUsb4S4S5.IsEnabled = isChecked.HasValue ? isChecked.GetValueOrDefault() : false;
        }

        private void alwaysOnUsb4S4S5_Click(object sender, RoutedEventArgs e)
        {
        }

        private void batteryStretchSettingBtn_Click(object sender, RoutedEventArgs e)
        {
            this.batteryStretch.ShowSettingDialog();
            this.SetBatteryStretchStatus();
        }

        private void coolQuietModeAuto_Click(object sender, RoutedEventArgs e)
        {
            this.coolQuietMode.IsEnabled = this.coolQuietModeAuto.IsChecked != true;
            if (this.coolQuietModeAuto.IsChecked == true)
            {
                this.coolQuietMode.IsChecked = false;
            }
        }

        public void Create()
        {
            this.alwaysOnUSB2mode.SetSettableValue(this.alwaysOnUSBItem.GetSettableValueOfMode());
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

        ~PowerManagementOptionsPanel()
        {
            this.Dispose(false);
        }

        private void iffsLearnMore_LinkTextClickedEvent()
        {
            MainWindow.Instance.pwmuiCtlDll.ShowHelpFile("PMVGPA.HTM");
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/powermanagementoptionspanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void isctLearnMore_LinkTextClickedEvent()
        {
            MainWindow.Instance.pwmuiCtlDll.ShowHelpFile("PMVGPA.HTM");
        }

        private void isscLearnMore_LinkTextClickedEvent()
        {
            MainWindow.Instance.pwmuiCtlDll.ShowHelpFile("PMVGPA.HTM");
        }

        private void OnAirplaneMode(bool bStatus)
        {
            this.SetAirplaneModeStatus(bStatus);
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!this.isRegisteredEvent)
            {
                MainWindow.Instance.UpdateBatteryStrechEvent += new UpdateBatteryStrechEventHandler(this.OnUpdateBatteryStretch);
                this.isRegisteredEvent = true;
            }
        }

        private void OnUpdateBatteryStretch()
        {
            this.SetBatteryStretchStatus();
        }

        public void Refresh(PowerManagementOptions grp)
        {
            this.RefreshBeepPowerCtl(grp);
            this.RefreshAlwaysOnUsb(grp);
            this.RefreshVideoPlayback(grp);
            this.RefreshIntelligentSleepStateCtl(grp);
            this.RefreshFastHibernation(grp);
            this.RefreshAirplaneMode(grp);
            this.RefreshInstantInternet(grp);
            this.RefreshHybridBoost(grp);
            this.RefreshWiFiDetect(grp);
            this.RefreshBatteryStretch();
            this.RefreshCoolQuietModeAuto(grp);
            this.RefreshCoolQuietMode(grp);
        }

        private void RefreshAirplaneMode(PowerManagementOptions grp)
        {
            this.airplaneMode.Visibility = Visibility.Collapsed;
            this.gridAirplaneMode.MinHeight = 0.0;
        }

        private void RefreshAlwaysOnUsb(PowerManagementOptions grp)
        {
            if (grp.AlwaysOnUSB.AlwaysOnUSB4IsCapable())
            {
                this.alwaysOnUsb4Panel.Visibility = Visibility.Visible;
                this.alwaysOnUsb.Visibility = Visibility.Hidden;
                this.alwaysOnUsb2Panel.Visibility = Visibility.Collapsed;
            }
            else if (grp.AlwaysOnUSB.AlwaysOnUSB2IsCapable())
            {
                this.alwaysOnUsb2Panel.Visibility = Visibility.Visible;
                this.alwaysOnUsb.Visibility = Visibility.Hidden;
                this.alwaysOnUsb4Panel.Visibility = Visibility.Collapsed;
            }
            else if (grp.AlwaysOnUSB.IsCapable())
            {
                this.alwaysOnUsb.Visibility = Visibility.Visible;
                this.alwaysOnUsb2Panel.Visibility = Visibility.Collapsed;
                this.alwaysOnUsb4Panel.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.alwaysOnUsb.Visibility = Visibility.Collapsed;
                this.alwaysOnUsb2Panel.Visibility = Visibility.Collapsed;
                this.alwaysOnUsb4Panel.Visibility = Visibility.Collapsed;
                this.gridAlwaysOnUsb.MinHeight = 0.0;
            }
            this.alwaysOnUsb.IsChecked = new bool?(grp.AlwaysOnUSB.IsChecked);
            this.alwaysOnUsb2.IsChecked = new bool?(grp.AlwaysOnUSB.AlwaysOnUSB2IsChecked);
            this.alwaysOnUSB2mode.SetCurrentValue(grp.AlwaysOnUSB.ModeOfAlwaysOnUSB2);
            bool? isChecked = this.alwaysOnUsb2.IsChecked;
            this.alwaysOnUSB2mode.IsEnabled = isChecked.HasValue ? isChecked.GetValueOrDefault() : false;
            this.alwaysOnUsb4.IsChecked = new bool?(grp.AlwaysOnUSB.AlwaysOnUSB4IsChecked);
            this.alwaysOnUsb4S4S5.IsChecked = new bool?(grp.AlwaysOnUSB.AlwaysOnUSB4S4S5IsChecked);
            bool? nullable2 = this.alwaysOnUsb4.IsChecked;
            this.alwaysOnUsb4S4S5.IsEnabled = nullable2.HasValue ? nullable2.GetValueOrDefault() : false;
            if (grp.AlwaysOnUSB.AlwaysOnUSB5IsCapable())
            {
                this.alwaysOnUsb4S4S5.Content = (string) base.FindResource("CaptionAlwaysOnUSB5CheckBox");
            }
            else
            {
                this.alwaysOnUsb4S4S5.Content = (string) base.FindResource("CaptionAlwaysOnUSB4CheckBox");
            }
        }

        private void RefreshBatteryStretch()
        {
            if (this.batteryStretch.IsCapable())
            {
                this.batteryStretchPanel.Visibility = Visibility.Visible;
            }
            else
            {
                this.batteryStretchPanel.Visibility = Visibility.Collapsed;
                this.gridBatteryStretchPanel.MinHeight = 0.0;
                return;
            }
            this.SetBatteryStretchStatus();
        }

        private void RefreshBeepPowerCtl(PowerManagementOptions grp)
        {
            if (grp.BeepPowerCtl.IsCapable())
            {
                this.beepPowerCtrl.Visibility = Visibility.Visible;
            }
            else
            {
                this.beepPowerCtrl.Visibility = Visibility.Collapsed;
                this.gridBeepPowerCtrl.MinHeight = 0.0;
            }
            this.beepPowerCtrl.IsChecked = new bool?(grp.BeepPowerCtl.IsChecked);
            this.beepPowerCtrl.IsEnabled = grp.BeepPowerCtl.IsEnabled;
        }

        private void RefreshCoolQuietMode(PowerManagementOptions grp)
        {
            if (grp.CoolQuietMode.IsCapable())
            {
                this.coolQuietModePanel.Visibility = Visibility.Visible;
                if ((this.coolQuietModeAuto.IsChecked == true) && this.coolQuietModeAuto.IsEnabled)
                {
                    this.coolQuietMode.IsEnabled = false;
                    this.coolQuietMode.IsChecked = false;
                }
                else
                {
                    this.coolQuietMode.IsEnabled = true;
                    this.coolQuietMode.IsChecked = new bool?(grp.CoolQuietMode.IsChecked);
                }
            }
            else
            {
                this.coolQuietModePanel.Visibility = Visibility.Collapsed;
                this.gridCoolQuietMode.MinHeight = 0.0;
            }
        }

        private void RefreshCoolQuietModeAuto(PowerManagementOptions grp)
        {
            if (grp.CoolQuietMode.IsCapable() && grp.CoolQuietModeAuto.IsCapable())
            {
                this.coolQuietModeAuto.Visibility = Visibility.Visible;
                this.coolQuietModeAuto.IsEnabled = !grp.CoolQuietModeAuto.IsGrayout;
            }
            else
            {
                this.coolQuietModeAuto.Visibility = Visibility.Collapsed;
                this.gridCoolQuietModeAuto.MinHeight = 0.0;
            }
            this.coolQuietModeAuto.IsChecked = new bool?(grp.CoolQuietModeAuto.IsChecked);
        }

        private void RefreshFastHibernation(PowerManagementOptions grp)
        {
            this.iffsPanel.Visibility = Visibility.Collapsed;
            this.gridIffsPanel.MinHeight = 0.0;
            this.enableIFFS.IsChecked = new bool?(grp.FastHibernation.IsChecked);
        }

        private void RefreshHybridBoost(PowerManagementOptions grp)
        {
            if (grp.HybridBoost.IsCapable())
            {
                this.hybridBoost.Visibility = Visibility.Visible;
            }
            else
            {
                this.hybridBoost.Visibility = Visibility.Collapsed;
                this.gridHybridBoost.MinHeight = 0.0;
            }
            this.hybridBoost.IsChecked = new bool?(grp.HybridBoost.IsChecked);
        }

        private void RefreshInstantInternet(PowerManagementOptions grp)
        {
            if (grp.InstantInternet.IsCapable())
            {
                this.isctPanel.Visibility = Visibility.Visible;
            }
            else
            {
                this.isctPanel.Visibility = Visibility.Collapsed;
                this.gridIsctPanel.MinHeight = 0.0;
            }
            this.enableISCT.IsChecked = new bool?(grp.InstantInternet.IsChecked);
        }

        private void RefreshIntelligentSleepStateCtl(PowerManagementOptions grp)
        {
            if (grp.IntelligentSleepStateCtl.IsCapable())
            {
                this.isscPanel.Visibility = Visibility.Visible;
            }
            else
            {
                this.isscPanel.Visibility = Visibility.Collapsed;
                this.gridIsscPanel.MinHeight = 0.0;
            }
            this.enableISSC.IsChecked = new bool?(grp.IntelligentSleepStateCtl.IsChecked);
            this.enableISSC.IsEnabled = grp.IntelligentSleepStateCtl.IsEnabled;
        }

        private void RefreshVideoPlayback(PowerManagementOptions grp)
        {
            if (grp.VideoPlayback.IsCapable())
            {
                this.videoPlayback.Visibility = Visibility.Visible;
            }
            else
            {
                this.videoPlayback.Visibility = Visibility.Collapsed;
                this.gridVideoPlayback.MinHeight = 0.0;
            }
            this.videoPlayback.IsChecked = new bool?(grp.VideoPlayback.IsChecked);
        }

        private void RefreshWiFiDetect(PowerManagementOptions grp)
        {
            if (grp.WiFiDetect.IsCapable())
            {
                this.wiFiAutoDetectPanel.Visibility = Visibility.Visible;
            }
            else
            {
                this.wiFiAutoDetectPanel.Visibility = Visibility.Collapsed;
                this.gridWiFiAutoDetectPanel.MinHeight = 0.0;
            }
            this.wiFiDetectCheckBox.IsChecked = new bool?(grp.WiFiDetect.IsChecked);
        }

        public void Save(ref PowerManagementOptions grp)
        {
            bool? isChecked = this.beepPowerCtrl.IsChecked;
            grp.BeepPowerCtl.IsChecked = isChecked.HasValue ? isChecked.GetValueOrDefault() : false;
            bool? nullable2 = this.alwaysOnUsb.IsChecked;
            grp.AlwaysOnUSB.IsChecked = nullable2.HasValue ? nullable2.GetValueOrDefault() : false;
            bool? nullable3 = this.videoPlayback.IsChecked;
            grp.VideoPlayback.IsChecked = nullable3.HasValue ? nullable3.GetValueOrDefault() : false;
            bool? nullable4 = this.alwaysOnUsb2.IsChecked;
            grp.AlwaysOnUSB.AlwaysOnUSB2IsChecked = nullable4.HasValue ? nullable4.GetValueOrDefault() : false;
            grp.AlwaysOnUSB.ModeOfAlwaysOnUSB2 = this.alwaysOnUSB2mode.GetCurrentValue();
            bool? nullable5 = this.alwaysOnUsb4.IsChecked;
            grp.AlwaysOnUSB.AlwaysOnUSB4IsChecked = nullable5.HasValue ? nullable5.GetValueOrDefault() : false;
            bool? nullable6 = this.alwaysOnUsb4S4S5.IsChecked;
            grp.AlwaysOnUSB.AlwaysOnUSB4S4S5IsChecked = nullable6.HasValue ? nullable6.GetValueOrDefault() : false;
            bool? nullable7 = this.enableISSC.IsChecked;
            grp.IntelligentSleepStateCtl.IsChecked = nullable7.HasValue ? nullable7.GetValueOrDefault() : false;
            bool? nullable8 = this.enableIFFS.IsChecked;
            grp.FastHibernation.IsChecked = nullable8.HasValue ? nullable8.GetValueOrDefault() : false;
            bool? nullable9 = this.enableISCT.IsChecked;
            grp.InstantInternet.IsChecked = nullable9.HasValue ? nullable9.GetValueOrDefault() : false;
            bool? nullable10 = this.hybridBoost.IsChecked;
            grp.HybridBoost.IsChecked = nullable10.HasValue ? nullable10.GetValueOrDefault() : false;
            bool? nullable11 = this.wiFiDetectCheckBox.IsChecked;
            grp.WiFiDetect.IsChecked = nullable11.HasValue ? nullable11.GetValueOrDefault() : false;
            bool? nullable12 = this.coolQuietMode.IsChecked;
            grp.CoolQuietMode.IsChecked = nullable12.HasValue ? nullable12.GetValueOrDefault() : false;
            bool? nullable13 = this.coolQuietModeAuto.IsChecked;
            grp.CoolQuietModeAuto.IsChecked = nullable13.HasValue ? nullable13.GetValueOrDefault() : false;
        }

        private void SetAirplaneModeStatus(bool bStatus)
        {
            this.airplaneMode.IsChecked = new bool?(bStatus);
        }

        private void SetBatteryStretchStatus()
        {
            this.batteryStretchStatus.Text = this.batteryStretch.GetStatus();
            if (this.batteryStretch.IsEnable())
            {
                this.batteryStretchIcon.Source = (BitmapImage) base.FindResource("BatteryStretchIcon");
                this.batteryStretchIcon.Visibility = Visibility.Visible;
            }
            else
            {
                this.batteryStretchIcon.Source = null;
                this.batteryStretchIcon.Visibility = Visibility.Collapsed;
            }
        }

        private void SetHybridBoostStatus(bool bStatus)
        {
            this.hybridBoost.IsChecked = new bool?(bStatus);
        }

        private void SetWiFiDetectStatus(bool bStatus)
        {
            this.wiFiDetectCheckBox.IsChecked = new bool?(bStatus);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (PowerManagementOptionsPanel) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.settingItemGrid = (Grid) target;
                    return;

                case 4:
                    this.gridBeepPowerCtrl = (RowDefinition) target;
                    return;

                case 5:
                    this.gridAlwaysOnUsb = (RowDefinition) target;
                    return;

                case 6:
                    this.gridVideoPlayback = (RowDefinition) target;
                    return;

                case 7:
                    this.gridIsscPanel = (RowDefinition) target;
                    return;

                case 8:
                    this.gridIffsPanel = (RowDefinition) target;
                    return;

                case 9:
                    this.gridAirplaneMode = (RowDefinition) target;
                    return;

                case 10:
                    this.gridIsctPanel = (RowDefinition) target;
                    return;

                case 11:
                    this.gridHybridBoost = (RowDefinition) target;
                    return;

                case 12:
                    this.gridWiFiAutoDetectPanel = (RowDefinition) target;
                    return;

                case 13:
                    this.gridBatteryStretchPanel = (RowDefinition) target;
                    return;

                case 14:
                    this.gridCoolQuietMode = (RowDefinition) target;
                    return;

                case 15:
                    this.gridCoolQuietModeAuto = (RowDefinition) target;
                    return;

                case 0x10:
                    this.beepPowerCtrl = (PMCheckBox) target;
                    return;

                case 0x11:
                    this.alwaysOnUsb = (PMCheckBox) target;
                    return;

                case 0x12:
                    this.alwaysOnUsb2Panel = (Grid) target;
                    return;

                case 0x13:
                    this.alwaysOnUsb2 = (PMCheckBox) target;
                    return;

                case 20:
                    this.alwaysOnUSB2mode = (PMComboBox) target;
                    return;

                case 0x15:
                    this.alwasOnUsb2LearnMore = (PMLinkTextBlock) target;
                    return;

                case 0x16:
                    this.alwaysOnUsb2TextPanel = (StackPanel) target;
                    return;

                case 0x17:
                    this.alwaysOnUsb2Text = (TextBlock) target;
                    return;

                case 0x18:
                    this.usbPowerd = (Image) target;
                    return;

                case 0x19:
                    this.alwaysOnUsb4Panel = (Grid) target;
                    return;

                case 0x1a:
                    this.alwaysOnUsb4 = (PMCheckBox) target;
                    return;

                case 0x1b:
                    this.alwaysOnUsb4Text = (PMLinkTextBlock) target;
                    return;

                case 0x1c:
                    this.alwaysOnUsb4Icon = (Image) target;
                    return;

                case 0x1d:
                    this.alwaysOnUsb4S4S5 = (PMCheckBox) target;
                    return;

                case 30:
                    this.videoPlayback = (PMCheckBox) target;
                    return;

                case 0x1f:
                    this.isscPanel = (Grid) target;
                    return;

                case 0x20:
                    this.enableISSC = (PMCheckBox) target;
                    return;

                case 0x21:
                    this.learnMoreISSC = (PMLinkTextBlock) target;
                    return;

                case 0x22:
                    this.iffsPanel = (Grid) target;
                    return;

                case 0x23:
                    this.enableIFFS = (PMCheckBox) target;
                    return;

                case 0x24:
                    this.learnMoreIFFS = (PMLinkTextBlock) target;
                    return;

                case 0x25:
                    this.airplaneMode = (PMCheckBox) target;
                    return;

                case 0x26:
                    this.isctPanel = (Grid) target;
                    return;

                case 0x27:
                    this.enableISCT = (PMCheckBox) target;
                    return;

                case 40:
                    this.learnMoreISCT = (PMLinkTextBlock) target;
                    return;

                case 0x29:
                    this.hybridBoost = (PMCheckBox) target;
                    return;

                case 0x2a:
                    this.wiFiAutoDetectPanel = (Grid) target;
                    return;

                case 0x2b:
                    this.wiFiDetectCheckBox = (PMCheckBox) target;
                    return;

                case 0x2c:
                    this.learnMoreWiFiDetect = (PMLinkTextBlock) target;
                    return;

                case 0x2d:
                    this.batteryStretchPanel = (Grid) target;
                    return;

                case 0x2e:
                    this.batteryStretchIcon = (Image) target;
                    return;

                case 0x2f:
                    this.batteryStretchStatus = (TextBlock) target;
                    return;

                case 0x30:
                    this.batteryStretchSettingBtn = (Button) target;
                    this.batteryStretchSettingBtn.Click += new RoutedEventHandler(this.batteryStretchSettingBtn_Click);
                    return;

                case 0x31:
                    this.coolQuietModePanel = (Grid) target;
                    return;

                case 50:
                    this.coolQuietMode = (PMCheckBox) target;
                    return;

                case 0x33:
                    this.coolQuietModeAuto = (PMCheckBox) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void wiFiDetectLearnMore_LinkTextClickedEvent()
        {
            MainWindow.Instance.pwmuiCtlDll.ShowHelpFile("PMVGPA.HTM");
        }
    }
}

