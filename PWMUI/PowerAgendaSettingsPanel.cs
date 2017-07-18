namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class PowerAgendaSettingsPanel : System.Windows.Controls.UserControl, ISubWindowPanel2, IComponentConnector
    {
        private bool _contentLoaded;
        private IdleTimerValue _idleTimerValue = new IdleTimerValue();
        private PowerAgendaSettingInformer _informer = new PowerAgendaSettingInformer();
        private bool _isNewAction = true;
        private PowerAgendaSettings _powerAgendaSettings = new PowerAgendaSettings();
        internal ComboBox actionComboBox;
        internal Grid bodyPanel;
        internal Label brightnessLable;
        internal Slider brightnessSlider;
        internal CheckBox checkBlockWindows;
        internal CheckBox checkFriday;
        internal CheckBox checkMonday;
        internal CheckBox checkMonitoringBatteryUsage;
        internal CheckBox checkPrenotice;
        internal CheckBox checkRemainingBatteryLevel;
        internal CheckBox checkSaturday;
        internal CheckBox checkSunday;
        internal CheckBox checkThursday;
        internal CheckBox checkTuesday;
        internal CheckBox checkWednesday;
        internal RadioButton dailyBtn;
        internal Label disableChargingLabel;
        internal TimePicker disableChargingTimePicker;
        internal GroupBox disableGrpPeakShift;
        internal DatePicker endDatePicker;
        internal TextBlock endDateText;
        internal Label endTimeLabel;
        internal TimePicker endTimePicker;
        internal PMSpinControl IdleTimer;
        internal Label idleTimerLable;
        internal Grid LayoutRoot;
        internal PMLinkTextBlock learnAboutPeakshift;
        internal TextBlock maxText;
        internal TextBlock minText;
        internal TextBox nameTextBox;
        internal ComboBox powerPlanComboBox;
        internal Label powerPlanLable;
        internal TextBox prenoticeTextBox;
        internal ValuePicker remainingBatteryLevelPicker;
        internal Label runOnBatteryTimeLabel;
        internal TimePicker runOnBatteryTimePicker;
        internal DatePicker startDatePicker;
        internal TextBlock startDateText;
        internal Label startTimeLabel;
        internal TimePicker startTimePicker;
        internal Grid termOfValidityGrid;
        internal TextBlock termOfValidityText;
        internal Label thinkPadbrightnessLable;
        internal Slider thinkPadBrightnessSlider;
        internal TextBlock thinkPadMaxText;
        internal TextBlock thinkPadMinText;
        internal Grid timeGrid;
        internal PowerAgendaSettingsPanel UserControl;
        internal RadioButton weeklyBtn;

        public PowerAgendaSettingsPanel()
        {
            this.InitializeComponent();
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void actionComboBox_SelectedIndexChanged(object sender, SelectionChangedEventArgs e)
        {
            eAction actionFrom = this.GetActionFrom(this.actionComboBox.SelectedIndex);
            this._powerAgendaSettings.Action = this.GetActionFrom(this.actionComboBox.SelectedIndex);
            switch (actionFrom)
            {
                case eAction.SwitchPowerPlan:
                    this.DetermineActionParamVisibility(actionFrom);
                    this.checkPrenotice.IsChecked = false;
                    break;

                case eAction.SetBrightness:
                    this.DetermineActionParamVisibility(actionFrom);
                    this.checkPrenotice.IsChecked = false;
                    break;

                case eAction.Shutdown:
                    this.DetermineActionParamVisibility(actionFrom);
                    this.checkPrenotice.IsChecked = true;
                    this.checkBlockWindows.IsChecked = true;
                    break;

                case eAction.Sleep:
                case eAction.Hibernate:
                    this._idleTimerValue.SetMin(1);
                    this.IdleTimer.SetSettableValue(this._idleTimerValue);
                    this.DetermineActionParamVisibility(actionFrom);
                    this.checkPrenotice.IsChecked = true;
                    break;

                default:
                    if (actionFrom == eAction.MonitorOff)
                    {
                        this._idleTimerValue.SetMin(0);
                        this.IdleTimer.SetSettableValue(this._idleTimerValue);
                        this.DetermineActionParamVisibility(actionFrom);
                        this.checkPrenotice.IsChecked = true;
                    }
                    else
                    {
                        this.DetermineActionParamVisibility(actionFrom);
                        this.checkPrenotice.IsChecked = true;
                    }
                    break;
            }
            this.changeOKButtonVisibility();
        }

        private void AdjustEndTime()
        {
            if (this._powerAgendaSettings.Action == eAction.Shutdown)
            {
                this._powerAgendaSettings.EndTime = this._powerAgendaSettings.StartTime;
            }
            if ((((this._powerAgendaSettings.Action == eAction.HibernateImmediately) || (this._powerAgendaSettings.Action == eAction.SleepImmediately)) || (this._powerAgendaSettings.Action == eAction.MonitorOff)) && (this._powerAgendaSettings.IdleTimerValue == 0))
            {
                this._powerAgendaSettings.EndTime = this._powerAgendaSettings.StartTime;
            }
            if (this._powerAgendaSettings.Action == eAction.FastHibernation)
            {
                this._powerAgendaSettings.EndTime = this._powerAgendaSettings.StartTime;
            }
            if (this._powerAgendaSettings.Action == eAction.Peakshift)
            {
                this._powerAgendaSettings.EndTime = this._powerAgendaSettings.DisableChargingTime;
            }
        }

        public void CancelClick()
        {
        }

        private void changeOKButtonVisibility()
        {
            if ((((this._powerAgendaSettings.Action == eAction.Sleep) || (this._powerAgendaSettings.Action == eAction.Hibernate)) || ((this._powerAgendaSettings.Action == eAction.MonitorOff) && (this._powerAgendaSettings.IdleTimerValue != 0))) || ((this._powerAgendaSettings.Action == eAction.SwitchPowerPlan) || (this._powerAgendaSettings.Action == eAction.SetBrightness)))
            {
                string str = this.startTimePicker.SelectedTime.ToString();
                string strB = this.endTimePicker.SelectedTime.ToString();
                if (str.CompareTo(strB) == 0)
                {
                    this.EnableDisableOkBtn(false);
                }
                else
                {
                    this.EnableDisableOkBtn(true);
                }
            }
            else if (this._powerAgendaSettings.Action == eAction.Peakshift)
            {
                string str3 = this.startTimePicker.SelectedTime.ToString();
                string str4 = this.runOnBatteryTimePicker.SelectedTime.ToString();
                string str5 = this.disableChargingTimePicker.SelectedTime.ToString();
                if ((str3.CompareTo(str4) == 0) && (str3.CompareTo(str5) == 0))
                {
                    this.EnableDisableOkBtn(false);
                }
                else
                {
                    this.EnableDisableOkBtn(true);
                }
            }
            else
            {
                this.EnableDisableOkBtn(true);
            }
        }

        private void checkDaysOfWeek_OnChecked(object sender, RoutedEventArgs e)
        {
            this.EnableDisableOkBtn(true);
        }

        private void checkDaysOfWeek_OnUnchecked(object sender, RoutedEventArgs e)
        {
            this.ValidateWeeklyCheckboxs();
        }

        private void CheckUncheckDaysOfWeek(bool check)
        {
            this.checkSunday.IsChecked = new bool?(check);
            this.checkMonday.IsChecked = new bool?(check);
            this.checkTuesday.IsChecked = new bool?(check);
            this.checkWednesday.IsChecked = new bool?(check);
            this.checkThursday.IsChecked = new bool?(check);
            this.checkFriday.IsChecked = new bool?(check);
            this.checkSaturday.IsChecked = new bool?(check);
        }

        private void CreateActionComboBox()
        {
            ComboBoxItem newItem = new ComboBoxItem {
                Content = (string) base.FindResource("ActionSleep"),
                Tag = eAction.Sleep
            };
            ComboBoxItem item2 = new ComboBoxItem {
                Content = (string) base.FindResource("ActionHibernate"),
                Tag = eAction.Hibernate
            };
            ComboBoxItem item3 = new ComboBoxItem {
                Content = (string) base.FindResource("ActionMonitorOff"),
                Tag = eAction.MonitorOff
            };
            ComboBoxItem item4 = new ComboBoxItem {
                Content = (string) base.FindResource("ActionSleepImmediately"),
                Tag = eAction.SleepImmediately
            };
            ComboBoxItem item5 = new ComboBoxItem {
                Content = (string) base.FindResource("ActionHibernateImmediately"),
                Tag = eAction.HibernateImmediately
            };
            ComboBoxItem item6 = new ComboBoxItem {
                Content = (string) base.FindResource("ActionShutdown"),
                Tag = eAction.Shutdown
            };
            ComboBoxItem item7 = new ComboBoxItem {
                Content = (string) base.FindResource("ActionSwitchPowerPlan"),
                Tag = eAction.SwitchPowerPlan
            };
            ComboBoxItem item8 = new ComboBoxItem {
                Content = (string) base.FindResource("ActionSetBrightness"),
                Tag = eAction.SetBrightness
            };
            ComboBoxItem item9 = new ComboBoxItem {
                Content = (string) base.FindResource("ActionPeakshift"),
                Tag = eAction.Peakshift
            };
            this.actionComboBox.Items.Add(newItem);
            this.actionComboBox.Items.Add(item2);
            this.actionComboBox.Items.Add(item3);
            this.actionComboBox.Items.Add(item4);
            this.actionComboBox.Items.Add(item5);
            this.actionComboBox.Items.Add(item6);
            this.actionComboBox.Items.Add(item7);
            BrighenessItem item10 = new BrighenessItem();
            if (item10.IsCapable() || (!this._isNewAction && (this._powerAgendaSettings.Action == eAction.SetBrightness)))
            {
                this.actionComboBox.Items.Add(item8);
            }
            BatteryInformer informer = new BatteryInformer();
            if (informer.IsPeakshiftCapable())
            {
                this.actionComboBox.Items.Add(item9);
            }
        }

        private void dailyBtn_OnChecked(object sender, RoutedEventArgs e)
        {
            this.CheckUncheckDaysOfWeek(true);
            this.EnableDisableOkBtn(true);
        }

        private void DetermineActionParamVisibility(eAction action)
        {
            Visibility visibility = (action == eAction.SwitchPowerPlan) ? Visibility.Visible : Visibility.Hidden;
            Visibility visibility2 = (((action == eAction.Sleep) || (action == eAction.Hibernate)) || (action == eAction.MonitorOff)) ? Visibility.Visible : Visibility.Hidden;
            Visibility visibility3 = (action == eAction.SetBrightness) ? Visibility.Visible : Visibility.Hidden;
            Visibility visibility4 = (action == eAction.Shutdown) ? Visibility.Visible : Visibility.Hidden;
            Visibility visibility5 = ((((action == eAction.Shutdown) || (action == eAction.SleepImmediately)) || ((action == eAction.HibernateImmediately) || (action == eAction.FastHibernation))) || ((action == eAction.MonitorOff) && (this.IdleTimer.Value == 0))) ? Visibility.Hidden : Visibility.Visible;
            Visibility visibility6 = ((((action == eAction.Shutdown) || (action == eAction.SleepImmediately)) || ((action == eAction.HibernateImmediately) || (action == eAction.FastHibernation))) || (((action == eAction.MonitorOff) && (this.IdleTimer.Value == 0)) || (action == eAction.Peakshift))) ? Visibility.Hidden : Visibility.Visible;
            Visibility visibility7 = (action == eAction.Peakshift) ? Visibility.Visible : Visibility.Hidden;
            string str = (string) base.FindResource("TitleStartTime");
            if (action == eAction.Shutdown)
            {
                str = (string) base.FindResource("TitleStartTimeForShutdown");
            }
            this.startTimeLabel.Content = str;
            this.powerPlanLable.Visibility = visibility;
            this.powerPlanComboBox.Visibility = visibility;
            this.checkBlockWindows.Visibility = visibility4;
            this.brightnessLable.Visibility = visibility3;
            this.brightnessSlider.Visibility = visibility3;
            this.minText.Visibility = visibility3;
            this.maxText.Visibility = visibility3;
            this.thinkPadbrightnessLable.Visibility = visibility3;
            this.thinkPadBrightnessSlider.Visibility = visibility3;
            this.thinkPadMinText.Visibility = visibility3;
            this.thinkPadMaxText.Visibility = visibility3;
            this.idleTimerLable.Visibility = visibility2;
            this.IdleTimer.Visibility = visibility2;
            this.learnAboutPeakshift.Visibility = visibility7;
            this.runOnBatteryTimeLabel.Visibility = visibility7;
            this.runOnBatteryTimePicker.Visibility = visibility7;
            this.disableChargingLabel.Visibility = visibility7;
            this.disableChargingTimePicker.Visibility = visibility7;
            this.termOfValidityGrid.Visibility = visibility7;
            this.disableGrpPeakShift.Visibility = visibility7;
            this.endTimeLabel.Visibility = visibility5;
            this.endTimePicker.Visibility = visibility6;
        }

        private void EnableDisableOkBtn(bool enable)
        {
            Grid parent = (Grid) base.Parent;
            Button button = (Button) parent.FindName("okBtn");
            if (button != null)
            {
                button.IsEnabled = enable;
            }
        }

        private eAction GetActionFrom(int indexOfActionComboBox)
        {
            ComboBoxItem item = (ComboBoxItem) this.actionComboBox.Items[indexOfActionComboBox];
            return (eAction) item.Tag;
        }

        private int GetIndexOfActionComboBoxFrom(eAction action)
        {
            for (int i = 0; i < this.actionComboBox.Items.Count; i++)
            {
                ComboBoxItem item = (ComboBoxItem) this.actionComboBox.Items[i];
                eAction tag = (eAction) item.Tag;
                if (tag == action)
                {
                    return i;
                }
            }
            return -1;
        }

        public PowerAgendaSettings GetPowerAgendaSettings() => 
            this._powerAgendaSettings;

        private void IdleTimer_ValueChanged(object sender, RoutedPropertyChangedEventArgs<uint> e)
        {
            this.GetActionFrom(this.actionComboBox.SelectedIndex);
            this._powerAgendaSettings.IdleTimerValue = Convert.ToUInt32(this.IdleTimer.Value);
            this.changeOKButtonVisibility();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/poweragendasettingspanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public bool OkClick()
        {
            if (!this.ValidateInput())
            {
                return false;
            }
            this.RefreshPowerAgenda();
            this._informer.Apply();
            return true;
        }

        private void PeakshiftLearnAbout_LinkTextClickedEvent()
        {
            MainWindow.Instance.pwmuiCtlDll.ShowHelpFile("PMVPAP.HTM");
        }

        public void RefreshPowerAgenda()
        {
            try
            {
                this._powerAgendaSettings.Action = this.GetActionFrom(this.actionComboBox.SelectedIndex);
                switch (this._powerAgendaSettings.Action)
                {
                    case eAction.Sleep:
                    case eAction.Hibernate:
                    case eAction.MonitorOff:
                        this._powerAgendaSettings.IdleTimerValue = Convert.ToUInt32(this.IdleTimer.Value);
                        goto Label_023A;

                    case eAction.Shutdown:
                    {
                        bool? nullable = this.checkBlockWindows.IsChecked;
                        if (!(nullable.HasValue ? nullable.GetValueOrDefault() : true))
                        {
                            break;
                        }
                        this._powerAgendaSettings.iBlockWindows = 1;
                        goto Label_023A;
                    }
                    case eAction.SwitchPowerPlan:
                        this._powerAgendaSettings.PowerPlanName = this.powerPlanComboBox.SelectedItem.ToString();
                        goto Label_023A;

                    case eAction.SetBrightness:
                        this._powerAgendaSettings.BrightnessLevel = Convert.ToUInt32(this.brightnessSlider.Value);
                        this._powerAgendaSettings.ThinkPadBrightnessLevel = Convert.ToUInt32(this.thinkPadBrightnessSlider.Value);
                        goto Label_023A;

                    case eAction.Peakshift:
                    {
                        this._powerAgendaSettings.StartDate = Convert.ToDateTime(this.startDatePicker.SelectedDate.ToString());
                        this._powerAgendaSettings.EndDate = Convert.ToDateTime(this.endDatePicker.SelectedDate.ToString());
                        this._powerAgendaSettings.RunOnBatteryTime = Convert.ToDateTime(this.runOnBatteryTimePicker.SelectedTime.ToString());
                        this._powerAgendaSettings.DisableChargingTime = Convert.ToDateTime(this.disableChargingTimePicker.SelectedTime.ToString());
                        bool? nullable2 = this.checkRemainingBatteryLevel.IsChecked;
                        this._powerAgendaSettings.IsCheckedRemainingBatteryLevel = nullable2.HasValue ? nullable2.GetValueOrDefault() : false;
                        this._powerAgendaSettings.RemainingBatteryLevel = this.remainingBatteryLevelPicker.CurrentValue;
                        bool? nullable3 = this.checkMonitoringBatteryUsage.IsChecked;
                        this._powerAgendaSettings.IsCheckedMonitoringBatteryUsage = nullable3.HasValue ? nullable3.GetValueOrDefault() : false;
                        goto Label_023A;
                    }
                    case eAction.SleepImmediately:
                    case eAction.HibernateImmediately:
                        this._powerAgendaSettings.IdleTimerValue = 0;
                        goto Label_023A;

                    default:
                        goto Label_023A;
                }
                this._powerAgendaSettings.iBlockWindows = 0;
            Label_023A:
                this._powerAgendaSettings.Name = this.nameTextBox.Text;
                bool? isChecked = this.checkPrenotice.IsChecked;
                if (isChecked.HasValue ? isChecked.GetValueOrDefault() : true)
                {
                    uint num = Convert.ToUInt32(this.prenoticeTextBox.Text);
                    if (num > 0)
                    {
                        this._powerAgendaSettings.Prenotice = num;
                    }
                    else
                    {
                        this._powerAgendaSettings.Prenotice = 0;
                    }
                }
                else
                {
                    this._powerAgendaSettings.Prenotice = 0;
                }
                bool? nullable5 = this.dailyBtn.IsChecked;
                if (nullable5.HasValue ? nullable5.GetValueOrDefault() : true)
                {
                    this._powerAgendaSettings.Frequency = eFrequency.Daily;
                }
                else
                {
                    bool? nullable6 = this.weeklyBtn.IsChecked;
                    if (nullable6.HasValue ? nullable6.GetValueOrDefault() : true)
                    {
                        this._powerAgendaSettings.Frequency = eFrequency.Weekly;
                    }
                    else
                    {
                        this._powerAgendaSettings.Frequency = eFrequency.Unknown;
                    }
                }
                bool? nullable7 = this.weeklyBtn.IsChecked;
                if (nullable7.HasValue ? nullable7.GetValueOrDefault() : true)
                {
                    if (this._powerAgendaSettings.DayOfWeek == null)
                    {
                        this._powerAgendaSettings.DayOfWeek = new ObservableCollection<eDayOfWeek>();
                    }
                    this._powerAgendaSettings.DayOfWeek.Clear();
                    bool? nullable8 = this.checkSunday.IsChecked;
                    if (nullable8.HasValue ? nullable8.GetValueOrDefault() : true)
                    {
                        this._powerAgendaSettings.DayOfWeek.Add(eDayOfWeek.Sunday);
                    }
                    bool? nullable9 = this.checkMonday.IsChecked;
                    if (nullable9.HasValue ? nullable9.GetValueOrDefault() : true)
                    {
                        this._powerAgendaSettings.DayOfWeek.Add(eDayOfWeek.Monday);
                    }
                    bool? nullable10 = this.checkTuesday.IsChecked;
                    if (nullable10.HasValue ? nullable10.GetValueOrDefault() : true)
                    {
                        this._powerAgendaSettings.DayOfWeek.Add(eDayOfWeek.Tuesday);
                    }
                    bool? nullable11 = this.checkWednesday.IsChecked;
                    if (nullable11.HasValue ? nullable11.GetValueOrDefault() : true)
                    {
                        this._powerAgendaSettings.DayOfWeek.Add(eDayOfWeek.Wednesday);
                    }
                    bool? nullable12 = this.checkThursday.IsChecked;
                    if (nullable12.HasValue ? nullable12.GetValueOrDefault() : true)
                    {
                        this._powerAgendaSettings.DayOfWeek.Add(eDayOfWeek.Thursday);
                    }
                    bool? nullable13 = this.checkFriday.IsChecked;
                    if (nullable13.HasValue ? nullable13.GetValueOrDefault() : true)
                    {
                        this._powerAgendaSettings.DayOfWeek.Add(eDayOfWeek.Friday);
                    }
                    bool? nullable14 = this.checkSaturday.IsChecked;
                    if (nullable14.HasValue ? nullable14.GetValueOrDefault() : true)
                    {
                        this._powerAgendaSettings.DayOfWeek.Add(eDayOfWeek.Saturday);
                    }
                    if (this._powerAgendaSettings.DayOfWeek.Count == 0)
                    {
                        this._powerAgendaSettings.DayOfWeek.Add(eDayOfWeek.Unknown);
                    }
                    this.ValidateWeeklyCheckboxs();
                }
                string str = this.startTimePicker.SelectedTime.ToString();
                this._powerAgendaSettings.StartTime = Convert.ToDateTime(str);
                str = this.endTimePicker.SelectedTime.ToString();
                this._powerAgendaSettings.EndTime = Convert.ToDateTime(str);
                this.AdjustEndTime();
            }
            catch (Exception)
            {
            }
        }

        internal void SetActionMode(bool isNewAction)
        {
            this._isNewAction = isNewAction;
        }

        internal void SetPowerAgendaSettings(PowerAgendaSettings powerAgendaSettings)
        {
            this._powerAgendaSettings = powerAgendaSettings;
        }

        private void StartToEndTimer_ValueChanged(object sender, TimeSelectedChangedRoutedEventArgs e)
        {
            this.changeOKButtonVisibility();
        }

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (PowerAgendaSettingsPanel) target;
                    this.UserControl.Loaded += new RoutedEventHandler(this.UserControl_Loaded);
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.bodyPanel = (Grid) target;
                    return;

                case 4:
                    this.nameTextBox = (TextBox) target;
                    return;

                case 5:
                    this.actionComboBox = (ComboBox) target;
                    this.actionComboBox.SelectionChanged += new SelectionChangedEventHandler(this.actionComboBox_SelectedIndexChanged);
                    return;

                case 6:
                    this.idleTimerLable = (Label) target;
                    return;

                case 7:
                    this.IdleTimer = (PMSpinControl) target;
                    return;

                case 8:
                    this.powerPlanLable = (Label) target;
                    return;

                case 9:
                    this.powerPlanComboBox = (ComboBox) target;
                    return;

                case 10:
                    this.thinkPadbrightnessLable = (Label) target;
                    return;

                case 11:
                    this.thinkPadMinText = (TextBlock) target;
                    return;

                case 12:
                    this.thinkPadMaxText = (TextBlock) target;
                    return;

                case 13:
                    this.thinkPadBrightnessSlider = (Slider) target;
                    return;

                case 14:
                    this.brightnessLable = (Label) target;
                    return;

                case 15:
                    this.minText = (TextBlock) target;
                    return;

                case 0x10:
                    this.maxText = (TextBlock) target;
                    return;

                case 0x11:
                    this.brightnessSlider = (Slider) target;
                    return;

                case 0x12:
                    this.checkBlockWindows = (CheckBox) target;
                    return;

                case 0x13:
                    this.learnAboutPeakshift = (PMLinkTextBlock) target;
                    return;

                case 20:
                    this.dailyBtn = (RadioButton) target;
                    this.dailyBtn.Checked += new RoutedEventHandler(this.dailyBtn_OnChecked);
                    return;

                case 0x15:
                    this.weeklyBtn = (RadioButton) target;
                    this.weeklyBtn.Checked += new RoutedEventHandler(this.weeklyBtn_OnChecked);
                    return;

                case 0x16:
                    this.termOfValidityGrid = (Grid) target;
                    return;

                case 0x17:
                    this.termOfValidityText = (TextBlock) target;
                    return;

                case 0x18:
                    this.startDateText = (TextBlock) target;
                    return;

                case 0x19:
                    this.startDatePicker = (DatePicker) target;
                    return;

                case 0x1a:
                    this.endDateText = (TextBlock) target;
                    return;

                case 0x1b:
                    this.endDatePicker = (DatePicker) target;
                    return;

                case 0x1c:
                    this.checkSunday = (CheckBox) target;
                    this.checkSunday.Checked += new RoutedEventHandler(this.checkDaysOfWeek_OnChecked);
                    this.checkSunday.Unchecked += new RoutedEventHandler(this.checkDaysOfWeek_OnUnchecked);
                    return;

                case 0x1d:
                    this.checkMonday = (CheckBox) target;
                    this.checkMonday.Checked += new RoutedEventHandler(this.checkDaysOfWeek_OnChecked);
                    this.checkMonday.Unchecked += new RoutedEventHandler(this.checkDaysOfWeek_OnUnchecked);
                    return;

                case 30:
                    this.checkTuesday = (CheckBox) target;
                    this.checkTuesday.Checked += new RoutedEventHandler(this.checkDaysOfWeek_OnChecked);
                    this.checkTuesday.Unchecked += new RoutedEventHandler(this.checkDaysOfWeek_OnUnchecked);
                    return;

                case 0x1f:
                    this.checkWednesday = (CheckBox) target;
                    this.checkWednesday.Checked += new RoutedEventHandler(this.checkDaysOfWeek_OnChecked);
                    this.checkWednesday.Unchecked += new RoutedEventHandler(this.checkDaysOfWeek_OnUnchecked);
                    return;

                case 0x20:
                    this.checkThursday = (CheckBox) target;
                    this.checkThursday.Checked += new RoutedEventHandler(this.checkDaysOfWeek_OnChecked);
                    this.checkThursday.Unchecked += new RoutedEventHandler(this.checkDaysOfWeek_OnUnchecked);
                    return;

                case 0x21:
                    this.checkFriday = (CheckBox) target;
                    this.checkFriday.Checked += new RoutedEventHandler(this.checkDaysOfWeek_OnChecked);
                    this.checkFriday.Unchecked += new RoutedEventHandler(this.checkDaysOfWeek_OnUnchecked);
                    return;

                case 0x22:
                    this.checkSaturday = (CheckBox) target;
                    this.checkSaturday.Checked += new RoutedEventHandler(this.checkDaysOfWeek_OnChecked);
                    this.checkSaturday.Unchecked += new RoutedEventHandler(this.checkDaysOfWeek_OnUnchecked);
                    return;

                case 0x23:
                    this.timeGrid = (Grid) target;
                    return;

                case 0x24:
                    this.startTimeLabel = (Label) target;
                    return;

                case 0x25:
                    this.startTimePicker = (TimePicker) target;
                    return;

                case 0x26:
                    this.endTimeLabel = (Label) target;
                    return;

                case 0x27:
                    this.endTimePicker = (TimePicker) target;
                    return;

                case 40:
                    this.runOnBatteryTimeLabel = (Label) target;
                    return;

                case 0x29:
                    this.runOnBatteryTimePicker = (TimePicker) target;
                    return;

                case 0x2a:
                    this.disableChargingLabel = (Label) target;
                    return;

                case 0x2b:
                    this.disableChargingTimePicker = (TimePicker) target;
                    return;

                case 0x2c:
                    this.disableGrpPeakShift = (GroupBox) target;
                    return;

                case 0x2d:
                    this.checkRemainingBatteryLevel = (CheckBox) target;
                    return;

                case 0x2e:
                    this.remainingBatteryLevelPicker = (ValuePicker) target;
                    return;

                case 0x2f:
                    this.checkMonitoringBatteryUsage = (CheckBox) target;
                    return;

                case 0x30:
                    this.checkPrenotice = (CheckBox) target;
                    return;

                case 0x31:
                    this.prenoticeTextBox = (TextBox) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this._informer.Refresh();
            AccessText text = PMControlFinder.FindVisualChild<AccessText>(this.checkBlockWindows);
            if (text != null)
            {
                AutomationProperties.SetName(this.checkBlockWindows, text.Text);
            }
            this.CreateActionComboBox();
            this.actionComboBox.SelectedIndex = this.GetIndexOfActionComboBoxFrom(this._powerAgendaSettings.Action);
            bool flag = false;
            PowerPlanInformer informer = new PowerPlanInformer();
            foreach (string str in informer.GetAllPlanName())
            {
                int num = this.powerPlanComboBox.Items.Add(str);
                if ((this._powerAgendaSettings.Action == eAction.SwitchPowerPlan) && (this._powerAgendaSettings.PowerPlanName == str))
                {
                    flag = true;
                    this.powerPlanComboBox.SelectedIndex = num;
                }
            }
            if (!flag)
            {
                this.powerPlanComboBox.SelectedIndex = 0;
            }
            BrighenessItem item = new BrighenessItem();
            this.nameTextBox.Text = this._powerAgendaSettings.Name;
            this.brightnessSlider.Minimum = 0.0;
            this.brightnessSlider.Maximum = 100.0;
            this.brightnessSlider.Value = this._powerAgendaSettings.BrightnessLevel;
            this.thinkPadBrightnessSlider.Minimum = item.GetSettableValue().GetMin();
            this.thinkPadBrightnessSlider.Maximum = item.GetSettableValue().GetMax();
            this.thinkPadBrightnessSlider.Value = this._powerAgendaSettings.ThinkPadBrightnessLevel;
            if (this._powerAgendaSettings.Prenotice > 0)
            {
                this.checkPrenotice.IsChecked = true;
                this.prenoticeTextBox.Text = Convert.ToString(this._powerAgendaSettings.Prenotice);
            }
            else
            {
                this.checkPrenotice.IsChecked = false;
                this.prenoticeTextBox.Text = "60";
            }
            if (this._powerAgendaSettings.iBlockWindows == 1)
            {
                this.checkBlockWindows.IsChecked = true;
            }
            else
            {
                this.checkBlockWindows.IsChecked = false;
            }
            this.dailyBtn.IsChecked = new bool?(this._powerAgendaSettings.IsDaily);
            this.weeklyBtn.IsChecked = new bool?(this._powerAgendaSettings.IsWeekly);
            bool? isChecked = this.weeklyBtn.IsChecked;
            if (isChecked.HasValue ? isChecked.GetValueOrDefault() : true)
            {
                using (IEnumerator<eDayOfWeek> enumerator = this._powerAgendaSettings.DayOfWeek.GetEnumerator())
                {
                    while (enumerator.MoveNext())
                    {
                        switch (enumerator.Current)
                        {
                            case eDayOfWeek.Sunday:
                                this.checkSunday.IsChecked = true;
                                break;

                            case eDayOfWeek.Monday:
                                this.checkMonday.IsChecked = true;
                                break;

                            case eDayOfWeek.Tuesday:
                                this.checkTuesday.IsChecked = true;
                                break;

                            case eDayOfWeek.Wednesday:
                                this.checkWednesday.IsChecked = true;
                                break;

                            case eDayOfWeek.Thursday:
                                this.checkThursday.IsChecked = true;
                                break;

                            case eDayOfWeek.Friday:
                                this.checkFriday.IsChecked = true;
                                break;

                            case eDayOfWeek.Saturday:
                                this.checkSaturday.IsChecked = true;
                                break;
                        }
                    }
                }
            }
            this.IdleTimer.SetSettableValue(this._idleTimerValue);
            this.IdleTimer.Value = this._powerAgendaSettings.IdleTimerValue;
            this.IdleTimer.ValueChanged += new RoutedPropertyChangedEventHandler<uint>(this.IdleTimer_ValueChanged);
            this.startTimePicker.SelectedHour = this._powerAgendaSettings.StartTime.Hour;
            this.startTimePicker.SelectedMinute = this._powerAgendaSettings.StartTime.Minute;
            if (this._powerAgendaSettings.StartTime == this._powerAgendaSettings.EndTime)
            {
                this._powerAgendaSettings.EndTime = this._powerAgendaSettings.EndTime.AddMinutes(1.0);
            }
            this.endTimePicker.SelectedHour = this._powerAgendaSettings.EndTime.Hour;
            this.endTimePicker.SelectedMinute = this._powerAgendaSettings.EndTime.Minute;
            this.startTimePicker.SelectedTimeChanged += new TimeSelectedChangedEventHandler(this.StartToEndTimer_ValueChanged);
            this.endTimePicker.SelectedTimeChanged += new TimeSelectedChangedEventHandler(this.StartToEndTimer_ValueChanged);
            this.runOnBatteryTimePicker.SelectedTimeChanged += new TimeSelectedChangedEventHandler(this.StartToEndTimer_ValueChanged);
            this.disableChargingTimePicker.SelectedTimeChanged += new TimeSelectedChangedEventHandler(this.StartToEndTimer_ValueChanged);
            this.startDatePicker.SelectedMonth = this._powerAgendaSettings.StartDate.Month;
            this.startDatePicker.SelectedDay = this._powerAgendaSettings.StartDate.Day;
            this.endDatePicker.SelectedMonth = this._powerAgendaSettings.EndDate.Month;
            this.endDatePicker.SelectedDay = this._powerAgendaSettings.EndDate.Day;
            if ((this._powerAgendaSettings.StartTime == this._powerAgendaSettings.RunOnBatteryTime) && (this._powerAgendaSettings.StartTime == this._powerAgendaSettings.DisableChargingTime))
            {
                this._powerAgendaSettings.RunOnBatteryTime = this._powerAgendaSettings.RunOnBatteryTime.AddMinutes(1.0);
                this._powerAgendaSettings.DisableChargingTime = this._powerAgendaSettings.DisableChargingTime.AddMinutes(1.0);
            }
            this.runOnBatteryTimePicker.SelectedHour = this._powerAgendaSettings.RunOnBatteryTime.Hour;
            this.runOnBatteryTimePicker.SelectedMinute = this._powerAgendaSettings.RunOnBatteryTime.Minute;
            this.disableChargingTimePicker.SelectedHour = this._powerAgendaSettings.DisableChargingTime.Hour;
            this.disableChargingTimePicker.SelectedMinute = this._powerAgendaSettings.DisableChargingTime.Minute;
            this.checkRemainingBatteryLevel.IsChecked = new bool?(this._powerAgendaSettings.IsCheckedRemainingBatteryLevel);
            this.remainingBatteryLevelPicker.CurrentValue = this._powerAgendaSettings.RemainingBatteryLevel;
            this.checkMonitoringBatteryUsage.IsChecked = new bool?(this._powerAgendaSettings.IsCheckedMonitoringBatteryUsage);
            this.DetermineActionParamVisibility(this._powerAgendaSettings.Action);
        }

        public bool ValidateInput()
        {
            bool flag = true;
            this.nameTextBox.Text = this.nameTextBox.Text.Trim();
            if ((this.nameTextBox.Text.Contains("\"") || this.nameTextBox.Text.Contains("'")) || (this.nameTextBox.Text.Contains(@"\") || this.nameTextBox.Text.Contains("/")))
            {
                this.nameTextBox.Focus();
                this.nameTextBox.SelectAll();
                return false;
            }
            bool? isChecked = this.checkPrenotice.IsChecked;
            if (isChecked.HasValue ? isChecked.GetValueOrDefault() : true)
            {
                int result = -1;
                if ((int.TryParse(this.prenoticeTextBox.Text, NumberStyles.Integer, null, out result) && (result >= 1)) && (result <= 60))
                {
                    return flag;
                }
                flag = false;
                string text1 = (string) base.FindResource("CaptionPowerManager");
                string text = (string) base.FindResource("MessageInvalidPrenotice");
                Resource resource = new Resource();
                if (resource != null)
                {
                    resource.ShowOkQuestionMessageBox(text);
                }
                this.prenoticeTextBox.Focus();
                this.prenoticeTextBox.SelectAll();
            }
            return flag;
        }

        private void ValidateWeeklyCheckboxs()
        {
            bool? isChecked = this.weeklyBtn.IsChecked;
            if (isChecked.HasValue ? isChecked.GetValueOrDefault() : true)
            {
                bool flag = false;
                bool? nullable2 = this.checkSunday.IsChecked;
                if (nullable2.HasValue ? nullable2.GetValueOrDefault() : true)
                {
                    flag = true;
                }
                bool? nullable3 = this.checkMonday.IsChecked;
                if (nullable3.HasValue ? nullable3.GetValueOrDefault() : true)
                {
                    flag = true;
                }
                bool? nullable4 = this.checkTuesday.IsChecked;
                if (nullable4.HasValue ? nullable4.GetValueOrDefault() : true)
                {
                    flag = true;
                }
                bool? nullable5 = this.checkWednesday.IsChecked;
                if (nullable5.HasValue ? nullable5.GetValueOrDefault() : true)
                {
                    flag = true;
                }
                bool? nullable6 = this.checkThursday.IsChecked;
                if (nullable6.HasValue ? nullable6.GetValueOrDefault() : true)
                {
                    flag = true;
                }
                bool? nullable7 = this.checkFriday.IsChecked;
                if (nullable7.HasValue ? nullable7.GetValueOrDefault() : true)
                {
                    flag = true;
                }
                bool? nullable8 = this.checkSaturday.IsChecked;
                if (nullable8.HasValue ? nullable8.GetValueOrDefault() : true)
                {
                    flag = true;
                }
                if (!flag)
                {
                    this.EnableDisableOkBtn(false);
                }
            }
        }

        private void weeklyBtn_OnChecked(object sender, RoutedEventArgs e)
        {
            this.CheckUncheckDaysOfWeek(false);
            this.ValidateWeeklyCheckboxs();
        }
    }
}

