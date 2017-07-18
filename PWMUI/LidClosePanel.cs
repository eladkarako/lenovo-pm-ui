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

    public class LidClosePanel : System.Windows.Controls.UserControl, ISubWindowPanel, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        internal AccessText AccessText_1;
        internal Grid bodyPanel;
        internal Path borderNeverOff;
        internal CheckBox checkNeverOff;
        public const int DELAY_TIME_MAX = 0x63;
        public const int DELAY_TIME_MIN = 1;
        protected bool Disposed;
        internal RadioButton doNothingBtn;
        internal RadioButton fastHibernationBtn;
        internal RadioButton hibernateBtn;
        private GlobalPowerSettingInformer informer = new GlobalPowerSettingInformer();
        internal Grid LayoutRoot;
        internal Label learnAboutNeverOff;
        internal Button neverOffBtn;
        private RapidResume rapidresume = new RapidResume();
        private MinuteValue settableValue = new MinuteValue();
        internal PMSpinControl settingNeverOffDuration;
        internal RadioButton shutdownBtn;
        internal RadioButton sleepBtn;
        internal LidClosePanel UserControl;

        public LidClosePanel()
        {
            this.InitializeComponent();
            this.settingNeverOffDuration.CanEnableApplyButton = false;
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void ArrangeRadioButton()
        {
            int num = 1;
            this.doNothingBtn.Visibility = Visibility.Visible;
            num++;
            this.sleepBtn.Visibility = Visibility.Visible;
            num++;
            this.fastHibernationBtn.Visibility = Visibility.Hidden;
            this.hibernateBtn.Visibility = Visibility.Visible;
            Grid.SetRow(this.hibernateBtn, num);
            num++;
            this.shutdownBtn.Visibility = Visibility.Visible;
            Grid.SetRow(this.shutdownBtn, num);
            num++;
        }

        public void CancelClick()
        {
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

        ~LidClosePanel()
        {
            this.Dispose(false);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/lidclosepanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void lidCloseAction_Checked(object sender, RoutedEventArgs e)
        {
            bool? isChecked = this.doNothingBtn.IsChecked;
            if (!(isChecked.HasValue ? isChecked.GetValueOrDefault() : true))
            {
                bool? nullable2 = this.shutdownBtn.IsChecked;
                if (!(nullable2.HasValue ? nullable2.GetValueOrDefault() : true))
                {
                    this.borderNeverOff.IsEnabled = this.rapidresume.IsActive();
                    this.checkNeverOff.IsEnabled = this.rapidresume.IsActive();
                    this.settingNeverOffDuration.IsEnabled = this.rapidresume.IsActive();
                    this.neverOffBtn.IsEnabled = this.rapidresume.IsActive();
                    this.learnAboutNeverOff.IsEnabled = this.rapidresume.IsActive();
                    return;
                }
            }
            this.borderNeverOff.IsEnabled = false;
            this.checkNeverOff.IsEnabled = false;
            this.settingNeverOffDuration.IsEnabled = false;
            this.neverOffBtn.IsEnabled = false;
            this.learnAboutNeverOff.IsEnabled = false;
        }

        private void neverOffBtn_Click(object sender, RoutedEventArgs e)
        {
            uint maxCarryTime = this.rapidresume.GetMaxCarryTime();
            this.rapidresume.ShowSettingDialog(false);
            uint num2 = this.rapidresume.GetMaxCarryTime();
            if (maxCarryTime != num2)
            {
                this.settingNeverOffDuration.Value = num2;
            }
        }

        public void OkClick()
        {
            this.informer.Refresh();
            PowerAction action = new PowerAction();
            uint maxValue = uint.MaxValue;
            bool? isChecked = this.doNothingBtn.IsChecked;
            if (isChecked.HasValue ? isChecked.GetValueOrDefault() : false)
            {
                maxValue = action.GetSettingValueOfDoNothing();
            }
            bool? nullable2 = this.sleepBtn.IsChecked;
            if (nullable2.HasValue ? nullable2.GetValueOrDefault() : false)
            {
                maxValue = action.GetSettingValueOfSleep();
            }
            bool? nullable3 = this.hibernateBtn.IsChecked;
            if (nullable3.HasValue ? nullable3.GetValueOrDefault() : false)
            {
                maxValue = action.GetSettingValueOfHibernate();
            }
            bool? nullable4 = this.shutdownBtn.IsChecked;
            if (nullable4.HasValue ? nullable4.GetValueOrDefault() : false)
            {
                maxValue = action.GetSettingValueOfShutDown();
            }
            bool? nullable5 = this.fastHibernationBtn.IsChecked;
            if (nullable5.HasValue ? nullable5.GetValueOrDefault() : false)
            {
                maxValue = action.GetSettingValueOfFastHibernation();
            }
            if (maxValue == uint.MaxValue)
            {
                return;
            }
            this.informer.MyGlobalEvents.ApplyToAllPlanIsChecked = true;
            this.informer.MyGlobalEvents.ValueOfLidClose = maxValue;
            bool? nullable6 = this.sleepBtn.IsChecked;
            if (!(nullable6.HasValue ? nullable6.GetValueOrDefault() : false))
            {
                bool? nullable7 = this.hibernateBtn.IsChecked;
                if (!(nullable7.HasValue ? nullable7.GetValueOrDefault() : false))
                {
                    bool? nullable8 = this.fastHibernationBtn.IsChecked;
                    if (!(nullable8.HasValue ? nullable8.GetValueOrDefault() : false))
                    {
                        goto Label_01A5;
                    }
                }
            }
            bool? nullable9 = this.checkNeverOff.IsChecked;
            if (nullable9.HasValue ? nullable9.GetValueOrDefault() : false)
            {
                this.rapidresume.Enable();
            }
            else
            {
                this.rapidresume.Disable();
            }
        Label_01A5:
            this.rapidresume.SetMaxCarryTime(this.settingNeverOffDuration.Value);
            this.informer.Apply();
        }

        protected override void OnInitialized(EventArgs e)
        {
            this.doNothingBtn.Checked += new RoutedEventHandler(this.lidCloseAction_Checked);
            this.sleepBtn.Checked += new RoutedEventHandler(this.lidCloseAction_Checked);
            this.hibernateBtn.Checked += new RoutedEventHandler(this.lidCloseAction_Checked);
            this.shutdownBtn.Checked += new RoutedEventHandler(this.lidCloseAction_Checked);
            this.fastHibernationBtn.Checked += new RoutedEventHandler(this.lidCloseAction_Checked);
            base.OnInitialized(e);
        }

        [DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (LidClosePanel) target;
                    this.UserControl.Loaded += new RoutedEventHandler(this.UserControl_Loaded);
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.bodyPanel = (Grid) target;
                    return;

                case 4:
                    this.doNothingBtn = (RadioButton) target;
                    return;

                case 5:
                    this.sleepBtn = (RadioButton) target;
                    return;

                case 6:
                    this.fastHibernationBtn = (RadioButton) target;
                    return;

                case 7:
                    this.hibernateBtn = (RadioButton) target;
                    return;

                case 8:
                    this.shutdownBtn = (RadioButton) target;
                    return;

                case 9:
                    this.borderNeverOff = (Path) target;
                    return;

                case 10:
                    this.checkNeverOff = (CheckBox) target;
                    return;

                case 11:
                    this.learnAboutNeverOff = (Label) target;
                    return;

                case 12:
                    this.AccessText_1 = (AccessText) target;
                    return;

                case 13:
                    this.settingNeverOffDuration = (PMSpinControl) target;
                    return;

                case 14:
                    this.neverOffBtn = (Button) target;
                    this.neverOffBtn.Click += new RoutedEventHandler(this.neverOffBtn_Click);
                    return;
            }
            this._contentLoaded = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.informer.Refresh();
            PowerAction action = new PowerAction();
            action.SetSettingValue(this.informer.MyGlobalEvents.ValueOfLidClose);
            this.doNothingBtn.IsChecked = new bool?(action.IsDoNothing());
            this.sleepBtn.IsChecked = new bool?(action.IsSleep());
            this.hibernateBtn.IsChecked = new bool?(action.IsHibernate());
            this.shutdownBtn.IsChecked = new bool?(action.IsShutDown());
            this.fastHibernationBtn.IsChecked = new bool?(action.IsFastHibernation());
            this.ArrangeRadioButton();
            this.borderNeverOff.Visibility = this.rapidresume.IsCapable() ? Visibility.Visible : Visibility.Hidden;
            this.checkNeverOff.Visibility = this.rapidresume.IsCapable() ? Visibility.Visible : Visibility.Hidden;
            this.settingNeverOffDuration.Visibility = this.rapidresume.IsCapable() ? Visibility.Visible : Visibility.Hidden;
            this.neverOffBtn.Visibility = this.rapidresume.IsCapable() ? Visibility.Visible : Visibility.Hidden;
            this.learnAboutNeverOff.Visibility = this.rapidresume.IsCapable() ? Visibility.Visible : Visibility.Hidden;
            this.borderNeverOff.IsEnabled = this.rapidresume.IsActive();
            this.checkNeverOff.IsEnabled = this.rapidresume.IsActive();
            this.settingNeverOffDuration.IsEnabled = this.rapidresume.IsActive();
            this.neverOffBtn.IsEnabled = this.rapidresume.IsActive();
            this.learnAboutNeverOff.IsEnabled = this.rapidresume.IsActive();
            if (this.rapidresume.IsCapable())
            {
                this.settableValue.SetMin(1);
                this.settableValue.SetMax(0x63);
                this.settingNeverOffDuration.SetSettableValue(this.settableValue);
                this.checkNeverOff.IsChecked = new bool?(this.rapidresume.IsEnable());
                this.settingNeverOffDuration.Value = this.rapidresume.GetMaxCarryTime();
            }
            bool? isChecked = this.doNothingBtn.IsChecked;
            if (!(isChecked.HasValue ? isChecked.GetValueOrDefault() : false))
            {
                bool? nullable2 = this.shutdownBtn.IsChecked;
                if (!(nullable2.HasValue ? nullable2.GetValueOrDefault() : false))
                {
                    return;
                }
            }
            this.borderNeverOff.IsEnabled = false;
            this.checkNeverOff.IsEnabled = false;
            this.settingNeverOffDuration.IsEnabled = false;
            this.neverOffBtn.IsEnabled = false;
            this.learnAboutNeverOff.IsEnabled = false;
        }
    }
}

