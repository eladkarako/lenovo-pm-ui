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

    public class UICustomizationsPanel : System.Windows.Controls.UserControl, IPMTabPanel, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        internal AccessText AccessText_1;
        internal PMCheckBox autoNotify;
        private const int BRANDID_THINKPADMINI = 2;
        protected bool Disposed;
        internal PMRadioButton fnf3ChoosePlanMenu;
        internal PMRadioButton fnf3PowerOffDisplay;
        internal TextBlock fnf3Title;
        private UICustomizationsInformer informer = new UICustomizationsInformer();
        internal Grid LayoutRoot;
        internal PMComboBox menuTextSize;
        internal Label menuTextSizeLabel;
        internal StackPanel menuTextSizePanel;
        internal TabItem OnFocusControl;
        internal PMCheckBox showGauge;
        internal UICustomizationsPanel UserControl;

        public UICustomizationsPanel()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        public bool Apply()
        {
            bool? isChecked = this.showGauge.IsChecked;
            this.informer.MyShowGauge.IsChecked = isChecked.HasValue ? isChecked.GetValueOrDefault() : false;
            bool? nullable2 = this.autoNotify.IsChecked;
            this.informer.MyAutoNotify.IsChecked = nullable2.HasValue ? nullable2.GetValueOrDefault() : false;
            if (this.fnf3PowerOffDisplay.IsChecked == true)
            {
                this.informer.MyFnF3.MyAction = FnF3.Action.PowerOffDisplay;
            }
            if (this.fnf3ChoosePlanMenu.IsChecked == true)
            {
                this.informer.MyFnF3.MyAction = FnF3.Action.ChoosePowerPlanMenu;
            }
            this.informer.MyMenuTextSize.ValueOfMenuTextSize = this.menuTextSize.GetCurrentValue();
            this.informer.Apply();
            return true;
        }

        public void Create()
        {
            this.menuTextSize.SetSettableValue(this.informer.MyMenuTextSize.GetSettableValueDC());
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

        ~UICustomizationsPanel()
        {
            this.Dispose(false);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/uicustomizationspanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public void Refresh()
        {
            this.informer.Refresh();
            this.RefreshShowGauge();
            this.RefreshAutoNotify();
            this.RefreshTextSize();
            this.RefreshFnF3();
        }

        private void RefreshAutoNotify()
        {
            if (!this.informer.MyAutoNotify.IsCapable())
            {
                this.autoNotify.Visibility = Visibility.Hidden;
            }
            else
            {
                this.autoNotify.Visibility = Visibility.Visible;
                this.autoNotify.IsChecked = new bool?(this.informer.MyAutoNotify.IsChecked);
            }
        }

        private void RefreshFnF3()
        {
            if (!this.informer.MyFnF3.IsCapable())
            {
                this.fnf3Title.Visibility = Visibility.Hidden;
                this.fnf3ChoosePlanMenu.Visibility = Visibility.Hidden;
                this.fnf3PowerOffDisplay.Visibility = Visibility.Hidden;
            }
            else
            {
                if (this.informer.IsAssignedFnEnd)
                {
                    this.fnf3Title.Text = (string) base.FindResource("CaptionFnEnd");
                }
                this.fnf3Title.Visibility = Visibility.Visible;
                this.fnf3ChoosePlanMenu.Visibility = Visibility.Visible;
                this.fnf3PowerOffDisplay.Visibility = Visibility.Visible;
                this.fnf3PowerOffDisplay.IsChecked = false;
                this.fnf3ChoosePlanMenu.IsChecked = false;
                if (this.informer.MyFnF3.MyAction == FnF3.Action.PowerOffDisplay)
                {
                    this.fnf3PowerOffDisplay.IsChecked = true;
                }
                if (this.informer.MyFnF3.MyAction == FnF3.Action.ChoosePowerPlanMenu)
                {
                    this.fnf3ChoosePlanMenu.IsChecked = true;
                }
            }
        }

        private void RefreshShowGauge()
        {
            if (!this.informer.MyShowGauge.IsCapable())
            {
                this.showGauge.Visibility = Visibility.Hidden;
            }
            else
            {
                this.showGauge.Visibility = Visibility.Visible;
                this.showGauge.IsChecked = new bool?(this.informer.MyShowGauge.IsChecked);
            }
        }

        private void RefreshTextSize()
        {
            if (!this.informer.MyMenuTextSize.IsCapable())
            {
                this.menuTextSizePanel.Visibility = Visibility.Hidden;
            }
            else
            {
                this.menuTextSize.Visibility = Visibility.Visible;
                this.menuTextSize.SetCurrentValue(this.informer.MyMenuTextSize.ValueOfMenuTextSize);
            }
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (UICustomizationsPanel) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.OnFocusControl = (TabItem) target;
                    return;

                case 4:
                    this.showGauge = (PMCheckBox) target;
                    return;

                case 5:
                    this.autoNotify = (PMCheckBox) target;
                    return;

                case 6:
                    this.menuTextSizePanel = (StackPanel) target;
                    return;

                case 7:
                    this.menuTextSizeLabel = (Label) target;
                    return;

                case 8:
                    this.AccessText_1 = (AccessText) target;
                    return;

                case 9:
                    this.menuTextSize = (PMComboBox) target;
                    return;

                case 10:
                    this.fnf3Title = (TextBlock) target;
                    return;

                case 11:
                    this.fnf3ChoosePlanMenu = (PMRadioButton) target;
                    return;

                case 12:
                    this.fnf3PowerOffDisplay = (PMRadioButton) target;
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

