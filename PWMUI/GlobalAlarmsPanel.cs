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

    public class GlobalAlarmsPanel : System.Windows.Controls.UserControl, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        internal AccessText AccessText_1;
        internal AccessText AccessText_2;
        internal AccessText AccessText_3;
        internal AccessText AccessText_4;
        internal AccessText AccessText_5;
        internal PMCheckBox applyAllPlan;
        internal PMComboBox criticalAction;
        private CriticalActionItem criticalActionItem = new CriticalActionItem();
        internal PMSpinControl criticalAt;
        private CriticalAtItem criticalAtItem = new CriticalAtItem();
        protected bool Disposed;
        internal Grid LayoutRoot;
        internal PMComboBox lowBatteryAction;
        private GlobalLowBatteryActionItem lowBatteryActionItem = new GlobalLowBatteryActionItem();
        internal PMSpinControl lowBatteryAt;
        private LowBatteryAtItem lowBatteryAtItem = new LowBatteryAtItem();
        internal PMComboBox lowBatteryNotify;
        private LowBatteryNotifyItem lowBatteryNotifyItem = new LowBatteryNotifyItem();
        internal GlobalAlarmsPanel UserControl;

        public GlobalAlarmsPanel()
        {
            this.InitializeComponent();
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

        public void Create()
        {
            this.lowBatteryAction.SetSettableValue(this.lowBatteryActionItem.GetSettableValueDC());
            this.lowBatteryNotify.SetSettableValue(this.lowBatteryNotifyItem.GetSettableValueDC());
            this.lowBatteryAt.SetSettableValue(this.lowBatteryAtItem.GetSettableValue());
            this.criticalAction.SetSettableValue(this.criticalActionItem.GetSettableValueDC());
            this.criticalAt.SetSettableValue(this.criticalAtItem.GetSettableValue());
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

        ~GlobalAlarmsPanel()
        {
            this.Dispose(false);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/globalalarmspanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            this.applyAllPlan.Checked += new RoutedEventHandler(this.applyAllPlan_Checked);
            this.applyAllPlan.Unchecked += new RoutedEventHandler(this.applyAllPlan_Unchecked);
            base.OnInitialized(e);
        }

        public void Refresh(GlobalAlarms grp)
        {
            this.lowBatteryAction.SetCurrentValue(grp.ValueOfLowBatteryAction);
            this.lowBatteryNotify.SetCurrentValue(grp.ValueOfLowBatteryNotify);
            this.lowBatteryAt.Value = grp.ValueOfLowBatteryAt;
            this.criticalAction.SetCurrentValue(grp.ValueOfCriticalAction);
            this.criticalAt.Value = grp.ValueOfCriticalAt;
            this.applyAllPlan.IsChecked = new bool?(grp.ApplyToAllPlanIsChecked);
            this.UpdateStatusOfContorol();
        }

        public void Save(ref GlobalAlarms grp)
        {
            bool? isChecked = this.applyAllPlan.IsChecked;
            grp.ApplyToAllPlanIsChecked = isChecked.HasValue ? isChecked.GetValueOrDefault() : false;
            bool? nullable2 = this.applyAllPlan.IsChecked;
            bool? nullable4 = nullable2.HasValue ? new bool?(!nullable2.GetValueOrDefault()) : null;
            if (!(nullable4.HasValue ? nullable4.GetValueOrDefault() : false))
            {
                grp.ValueOfLowBatteryAction = this.lowBatteryAction.GetCurrentValue();
                grp.ValueOfLowBatteryNotify = this.lowBatteryNotify.GetCurrentValue();
                grp.ValueOfLowBatteryAt = this.lowBatteryAt.Value;
                grp.ValueOfCriticalAction = this.criticalAction.GetCurrentValue();
                grp.ValueOfCriticalAt = this.criticalAt.Value;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (GlobalAlarmsPanel) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.applyAllPlan = (PMCheckBox) target;
                    return;

                case 4:
                    this.AccessText_1 = (AccessText) target;
                    return;

                case 5:
                    this.lowBatteryAt = (PMSpinControl) target;
                    return;

                case 6:
                    this.AccessText_2 = (AccessText) target;
                    return;

                case 7:
                    this.lowBatteryNotify = (PMComboBox) target;
                    return;

                case 8:
                    this.AccessText_3 = (AccessText) target;
                    return;

                case 9:
                    this.lowBatteryAction = (PMComboBox) target;
                    return;

                case 10:
                    this.AccessText_4 = (AccessText) target;
                    return;

                case 11:
                    this.criticalAt = (PMSpinControl) target;
                    return;

                case 12:
                    this.AccessText_5 = (AccessText) target;
                    return;

                case 13:
                    this.criticalAction = (PMComboBox) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void UpdateStatusOfContorol()
        {
            bool? isChecked = this.applyAllPlan.IsChecked;
            bool flag = isChecked.HasValue ? isChecked.GetValueOrDefault() : false;
            MainWindow.Instance.IsApplyAlarmsToAllPlansChecked = flag;
            foreach (UIElement element in this.LayoutRoot.Children)
            {
                if (!element.Equals(this.applyAllPlan))
                {
                    element.IsEnabled = flag;
                }
            }
        }
    }
}

