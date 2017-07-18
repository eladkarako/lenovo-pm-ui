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

    public class PowerButtonPanel : System.Windows.Controls.UserControl, ISubWindowPanel, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        internal Grid bodyPanel;
        protected bool Disposed;
        internal RadioButton doNothingBtn;
        internal RadioButton hibernateBtn;
        private GlobalPowerSettingInformer informer = new GlobalPowerSettingInformer();
        internal Grid LayoutRoot;
        internal RadioButton shutdownBtn;
        internal RadioButton sleepBtn;
        internal PowerButtonPanel UserControl;

        public PowerButtonPanel()
        {
            this.InitializeComponent();
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

        ~PowerButtonPanel()
        {
            this.Dispose(false);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/powerbuttonpanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
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
            if (maxValue != uint.MaxValue)
            {
                this.informer.MyGlobalEvents.ApplyToAllPlanIsChecked = true;
                this.informer.MyGlobalEvents.ValueOfPowerButton = maxValue;
                this.informer.Apply();
            }
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (PowerButtonPanel) target;
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
                    this.hibernateBtn = (RadioButton) target;
                    return;

                case 7:
                    this.shutdownBtn = (RadioButton) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.informer.Refresh();
            PowerAction action = new PowerAction();
            action.SetSettingValue(this.informer.MyGlobalEvents.ValueOfPowerButton);
            this.doNothingBtn.IsChecked = new bool?(action.IsDoNothing());
            this.sleepBtn.IsChecked = new bool?(action.IsSleep());
            this.hibernateBtn.IsChecked = new bool?(action.IsHibernate());
            this.shutdownBtn.IsChecked = new bool?(action.IsShutDown());
        }
    }
}

