namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Shapes;

    public class BatteryStretchPanel : System.Windows.Controls.UserControl, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        private BatteryStretch batteryStretch = new BatteryStretch();
        protected bool Disposed;
        internal Canvas Layer_1;
        internal Grid LayoutRoot;
        internal System.Windows.Shapes.Path Path;
        internal System.Windows.Shapes.Path Path_0;
        internal System.Windows.Shapes.Path Path_1;
        internal Canvas tips_design;
        internal BatteryStretchPanel UserControl;
        internal PMLinkTextBlock viewSettings;

        public BatteryStretchPanel()
        {
            this.InitializeComponent();
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

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

        ~BatteryStretchPanel()
        {
            this.Dispose(false);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/batterystretchpanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public void Refresh()
        {
            if (!this.batteryStretch.IsCapable())
            {
                base.Visibility = Visibility.Hidden;
            }
            else
            {
                base.Visibility = Visibility.Visible;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (BatteryStretchPanel) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.tips_design = (Canvas) target;
                    return;

                case 4:
                    this.Layer_1 = (Canvas) target;
                    return;

                case 5:
                    this.Path = (System.Windows.Shapes.Path) target;
                    return;

                case 6:
                    this.Path_0 = (System.Windows.Shapes.Path) target;
                    return;

                case 7:
                    this.Path_1 = (System.Windows.Shapes.Path) target;
                    return;

                case 8:
                    this.viewSettings = (PMLinkTextBlock) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void viewSettings_LinkTextClickedEvent()
        {
            this.batteryStretch.ShowSettingDialog();
        }

        private void viewSettings_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.batteryStretch.ShowSettingDialog();
        }
    }
}

