namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class GlobalEventsPanel : System.Windows.Controls.UserControl, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        internal AccessText AccessText_1;
        internal AccessText AccessText_2;
        internal AccessText AccessText_3;
        internal AccessText AccessText_4;
        internal PMCheckBox applyAllPlan;
        internal PMCheckBox checkNeverOff;
        protected bool Disposed;
        internal PMComboBox fnf4;
        private FnF4Item fnf4Item = new FnF4Item();
        internal Label fnf4Label;
        public const int IDOK = 1;
        internal Grid LayoutRoot;
        internal Button lidCloseAdvancedBtn;
        internal PMComboBox lidClosed;
        private LidCloseItem lidClosedItem = new LidCloseItem();
        internal PMComboBox powerBtn;
        private PowerButtonItem powerBtnItem = new PowerButtonItem();
        private RapidResume rapidresume = new RapidResume();
        private ReqPassItem reqPassItem = new ReqPassItem();
        internal PMCheckBox requirePassword;
        private int rowPosition;
        internal PMComboBox startMenu;
        private StartMenuItem startMenuItem = new StartMenuItem();
        internal Label startMenuLabel;
        internal GlobalEventsPanel UserControl;
        internal PMCheckBox wakeOnGrab;
        private WakeOnGrabItem wakeOnGrabItem = new WakeOnGrabItem();

        public event LidCloseAdvancedClickEventHandler LidCloseAdvancedClickEvent;

        public GlobalEventsPanel()
        {
            this.InitializeComponent();
            this.requirePassword.Click += new RoutedEventHandler(this.requirePassword_Click);
            this.wakeOnGrab.Click += new RoutedEventHandler(this.wakeOnGrab_Click);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void applyAllPlan_Checked(object sender, RoutedEventArgs e)
        {
            this.UpdateStatusOfContorol();
        }

        private void applyAllPlan_Unchecked(object sender, RoutedEventArgs e)
        {
            this.UpdateStatusOfContorol();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.LidCloseAdvancedClickEvent != null)
            {
                this.LidCloseAdvancedClickEvent();
                PowerAction action = new PowerAction();
                if ((this.lidClosed.GetCurrentValue() == action.GetSettingValueOfDoNothing()) || (this.lidClosed.GetCurrentValue() == action.GetSettingValueOfShutDown()))
                {
                    this.checkNeverOff.IsEnabled = false;
                }
            }
        }

        public void Create()
        {
            this.fnf4.SetSettableValue(this.fnf4Item.GetSettableValueDC());
            this.powerBtn.SetSettableValue(this.powerBtnItem.GetSettableValueDC());
            this.lidClosed.SetSettableValue(this.lidClosedItem.GetSettableValueDC());
            this.startMenu.SetSettableValue(this.startMenuItem.GetSettableValueDC());
            if (this.fnf4Item.IsAssignedFnF1)
            {
                this.AccessText_1.Text = (string) base.FindResource("TitleFnF1OfSleepButton");
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

        ~GlobalEventsPanel()
        {
            this.Dispose(false);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/globaleventspanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void lidClosed_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            PowerAction action = new PowerAction();
            if ((this.lidClosed.GetCurrentValue() == action.GetSettingValueOfDoNothing()) || (this.lidClosed.GetCurrentValue() == action.GetSettingValueOfShutDown()))
            {
                this.checkNeverOff.IsEnabled = false;
            }
            else
            {
                this.checkNeverOff.IsEnabled = this.rapidresume.IsActive();
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            this.applyAllPlan.Checked += new RoutedEventHandler(this.applyAllPlan_Checked);
            this.applyAllPlan.Unchecked += new RoutedEventHandler(this.applyAllPlan_Unchecked);
            base.OnInitialized(e);
        }

        public void Refresh(GlobalEvents grp)
        {
            this.requirePassword.IsChecked = new bool?(grp.RequirePassword.IsChecked);
            this.fnf4.SetCurrentValue(grp.ValueOfFnf4);
            this.powerBtn.SetCurrentValue(grp.ValueOfPowerButton);
            this.lidClosed.SetCurrentValue(grp.ValueOfLidClose);
            this.startMenu.SetCurrentValue(grp.ValueOfStartMenu);
            this.wakeOnGrab.IsChecked = new bool?(grp.WakeOnGrab.IsChecked);
            this.rowPosition = 5;
            this.lidCloseAdvancedBtn.Visibility = this.rapidresume.IsCapable() ? Visibility.Visible : Visibility.Hidden;
            this.checkNeverOff.Visibility = this.rapidresume.IsCapable() ? Visibility.Visible : Visibility.Hidden;
            this.checkNeverOff.IsEnabled = this.rapidresume.IsActive();
            if (this.rapidresume.IsCapable())
            {
                this.checkNeverOff.IsChecked = new bool?(this.rapidresume.IsEnable());
            }
            if (this.rapidresume.IsCapable())
            {
                this.rowPosition++;
            }
            this.RefreshStartMenu();
            this.RefreshFnF4();
            this.RefreshWakeOnGrab();
            this.applyAllPlan.IsChecked = new bool?(grp.ApplyToAllPlanIsChecked);
            this.UpdateStatusOfContorol();
        }

        private void RefreshFnF4()
        {
            if (this.fnf4Item.IsCapable())
            {
                this.fnf4.Visibility = Visibility.Visible;
                this.fnf4Label.Visibility = Visibility.Visible;
            }
            else
            {
                this.fnf4.Visibility = Visibility.Hidden;
                this.fnf4Label.Visibility = Visibility.Hidden;
            }
        }

        private void RefreshStartMenu()
        {
            if (this.startMenuItem.IsCapable())
            {
                this.startMenu.Visibility = Visibility.Visible;
                this.startMenuLabel.Visibility = Visibility.Visible;
                Grid.SetRow(this.startMenu, this.rowPosition);
                this.rowPosition++;
            }
            else
            {
                this.startMenu.Visibility = Visibility.Hidden;
                this.startMenuLabel.Visibility = Visibility.Hidden;
            }
        }

        private void RefreshWakeOnGrab()
        {
            if (this.wakeOnGrabItem.IsCapable())
            {
                this.wakeOnGrab.Visibility = Visibility.Visible;
                Grid.SetRow(this.wakeOnGrab, this.rowPosition);
                this.rowPosition++;
            }
            else
            {
                this.wakeOnGrab.Visibility = Visibility.Hidden;
            }
        }

        private void requirePassword_Click(object sender, RoutedEventArgs e)
        {
            if (base.IsVisible && !this.reqPassItem.CanAccess())
            {
                string mainProgram = "PWMUI.exe";
                Resource resource = new Resource();
                if (resource.ShowElevationDialog(mainProgram) == 1)
                {
                    MainWindow.Instance.Close();
                }
                else
                {
                    bool? isChecked = this.requirePassword.IsChecked;
                    this.requirePassword.IsChecked = new bool?(!(isChecked.HasValue ? isChecked.GetValueOrDefault() : false));
                }
            }
        }

        public void Save(ref GlobalEvents grp)
        {
            bool? isChecked = this.applyAllPlan.IsChecked;
            grp.ApplyToAllPlanIsChecked = isChecked.HasValue ? isChecked.GetValueOrDefault() : false;
            bool? nullable2 = this.applyAllPlan.IsChecked;
            bool? nullable4 = nullable2.HasValue ? new bool?(!nullable2.GetValueOrDefault()) : null;
            if (!(nullable4.HasValue ? nullable4.GetValueOrDefault() : false))
            {
                bool? nullable5 = this.requirePassword.IsChecked;
                grp.RequirePassword.IsChecked = nullable5.HasValue ? nullable5.GetValueOrDefault() : false;
                grp.ValueOfFnf4 = this.fnf4.GetCurrentValue();
                grp.ValueOfPowerButton = this.powerBtn.GetCurrentValue();
                grp.ValueOfLidClose = this.lidClosed.GetCurrentValue();
                grp.ValueOfStartMenu = this.startMenu.GetCurrentValue();
                bool? nullable6 = this.wakeOnGrab.IsChecked;
                grp.WakeOnGrab.IsChecked = nullable6.HasValue ? nullable6.GetValueOrDefault() : false;
            }
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (GlobalEventsPanel) target;
                    this.UserControl.Loaded += new RoutedEventHandler(this.UserControl_Loaded);
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.applyAllPlan = (PMCheckBox) target;
                    return;

                case 4:
                    this.requirePassword = (PMCheckBox) target;
                    return;

                case 5:
                    this.fnf4Label = (Label) target;
                    return;

                case 6:
                    this.AccessText_1 = (AccessText) target;
                    return;

                case 7:
                    this.fnf4 = (PMComboBox) target;
                    return;

                case 8:
                    this.AccessText_2 = (AccessText) target;
                    return;

                case 9:
                    this.powerBtn = (PMComboBox) target;
                    return;

                case 10:
                    this.AccessText_3 = (AccessText) target;
                    return;

                case 11:
                    this.lidClosed = (PMComboBox) target;
                    return;

                case 12:
                    this.lidCloseAdvancedBtn = (Button) target;
                    this.lidCloseAdvancedBtn.Click += new RoutedEventHandler(this.Button_Click);
                    return;

                case 13:
                    this.checkNeverOff = (PMCheckBox) target;
                    return;

                case 14:
                    this.startMenuLabel = (Label) target;
                    return;

                case 15:
                    this.AccessText_4 = (AccessText) target;
                    return;

                case 0x10:
                    this.startMenu = (PMComboBox) target;
                    return;

                case 0x11:
                    this.wakeOnGrab = (PMCheckBox) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void UpdateStatusOfContorol()
        {
            bool? isChecked = this.applyAllPlan.IsChecked;
            bool flag = isChecked.HasValue ? isChecked.GetValueOrDefault() : false;
            MainWindow.Instance.IsApplyEventsToAllPlansChecked = flag;
            foreach (UIElement element in this.LayoutRoot.Children)
            {
                if (!element.Equals(this.applyAllPlan) && ((!element.Equals(this.checkNeverOff) || !flag) || this.rapidresume.IsActive()))
                {
                    element.IsEnabled = flag;
                }
            }
            PowerAction action = new PowerAction();
            if ((this.lidClosed.GetCurrentValue() == action.GetSettingValueOfDoNothing()) || (this.lidClosed.GetCurrentValue() == action.GetSettingValueOfShutDown()))
            {
                this.checkNeverOff.IsEnabled = false;
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void wakeOnGrab_Click(object sender, RoutedEventArgs e)
        {
            if (base.IsVisible && !this.wakeOnGrabItem.CanAccess())
            {
                bool? isChecked = this.wakeOnGrab.IsChecked;
                this.wakeOnGrab.IsChecked = new bool?(!(isChecked.HasValue ? isChecked.GetValueOrDefault() : false));
            }
        }
    }
}

