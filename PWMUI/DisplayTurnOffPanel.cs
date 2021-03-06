﻿namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class DisplayTurnOffPanel : System.Windows.Controls.UserControl, ISubWindowPanel, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        internal Grid bodyPanel;
        protected bool Disposed;
        private VideoTimeoutItem item = new VideoTimeoutItem();
        internal Grid LayoutRoot;
        private PowerPlanInformer planInformer = new PowerPlanInformer();
        internal PMSpinControl settingAC;
        internal PMSpinControl settingDC;
        private PowerUseInformer useInformer = new PowerUseInformer();
        internal DisplayTurnOffPanel UserControl;

        public DisplayTurnOffPanel()
        {
            this.InitializeComponent();
            this.settingAC.CanEnableApplyButton = false;
            this.settingDC.CanEnableApplyButton = false;
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

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

        ~DisplayTurnOffPanel()
        {
            this.Dispose(false);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/displayturnoffpanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public void OkClick()
        {
            this.useInformer.SetVideoTimeoutToAllScheme(this.settingAC.Value, this.settingDC.Value);
        }

        [DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (DisplayTurnOffPanel) target;
                    this.UserControl.Loaded += new RoutedEventHandler(this.UserControl_Loaded);
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.bodyPanel = (Grid) target;
                    return;

                case 4:
                    this.settingDC = (PMSpinControl) target;
                    return;

                case 5:
                    this.settingAC = (PMSpinControl) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            this.settingDC.SetSettableValue(this.item.GetSettableValue());
            this.settingAC.SetSettableValue(this.item.GetSettableValue());
            PowerPlan activePlan = this.planInformer.GetActivePlan();
            this.settingDC.Value = activePlan.MyIdleTimers.ValueOfVideoTimeoutDC;
            this.settingAC.Value = activePlan.MyIdleTimers.ValueOfVideoTimeoutAC;
        }
    }
}

