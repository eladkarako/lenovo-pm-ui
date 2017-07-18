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

    public class DynamicBrightnessControlPanel : System.Windows.Controls.UserControl, IComponentConnector
    {
        private bool _contentLoaded;
        internal PMCheckBox checkLogoff;
        internal PMCheckBox checkScreenLock;
        internal PMCheckBox checkScreenSaver;
        internal PMCheckBox checkShutdown;
        internal PMCheckBox checkSwitchUser;
        internal Grid LayoutRoot;
        internal DynamicBrightnessControlPanel UserControl;

        public DynamicBrightnessControlPanel()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        public void Create()
        {
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/dynamicbrightnesscontrolpanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public void Refresh(PowerManagementOptions grp)
        {
            this.checkShutdown.IsChecked = new bool?(grp.DynamicBrightness.ShutDownIsChecked);
            this.checkLogoff.IsChecked = new bool?(grp.DynamicBrightness.LogOffIsChecked);
            this.checkSwitchUser.IsChecked = new bool?(grp.DynamicBrightness.SwitchUserIsChecked);
            this.checkScreenLock.IsChecked = new bool?(grp.DynamicBrightness.ScreenLockIsChecked);
            this.checkScreenSaver.IsChecked = new bool?(grp.DynamicBrightness.ScreenSaverIsChecked);
        }

        public void Save(ref PowerManagementOptions grp)
        {
            bool? isChecked = this.checkShutdown.IsChecked;
            grp.DynamicBrightness.ShutDownIsChecked = isChecked.HasValue ? isChecked.GetValueOrDefault() : false;
            bool? nullable2 = this.checkLogoff.IsChecked;
            grp.DynamicBrightness.LogOffIsChecked = nullable2.HasValue ? nullable2.GetValueOrDefault() : false;
            bool? nullable3 = this.checkSwitchUser.IsChecked;
            grp.DynamicBrightness.SwitchUserIsChecked = nullable3.HasValue ? nullable3.GetValueOrDefault() : false;
            bool? nullable4 = this.checkScreenLock.IsChecked;
            grp.DynamicBrightness.ScreenLockIsChecked = nullable4.HasValue ? nullable4.GetValueOrDefault() : false;
            bool? nullable5 = this.checkScreenSaver.IsChecked;
            grp.DynamicBrightness.ScreenSaverIsChecked = nullable5.HasValue ? nullable5.GetValueOrDefault() : false;
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (DynamicBrightnessControlPanel) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.checkShutdown = (PMCheckBox) target;
                    return;

                case 4:
                    this.checkLogoff = (PMCheckBox) target;
                    return;

                case 5:
                    this.checkSwitchUser = (PMCheckBox) target;
                    return;

                case 6:
                    this.checkScreenLock = (PMCheckBox) target;
                    return;

                case 7:
                    this.checkScreenSaver = (PMCheckBox) target;
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

