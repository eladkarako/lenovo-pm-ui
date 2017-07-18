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
    using System.Windows.Media;

    public class ComboBoxPlanItem : System.Windows.Controls.UserControl, IPlanItem, IComponentConnector
    {
        private bool _contentLoaded;
        internal Grid LayoutRoot;
        private ValueWithIndexItem relatedItem;
        internal PMComboBox settingAC;
        internal PMComboBox settingDC;
        internal Grid settingPanel;
        internal Label settingTitle;
        internal Label settingTitle2;
        internal AccessText titleText;
        internal TextBlock titleText2;
        internal ComboBoxPlanItem UserControl;

        public ComboBoxPlanItem()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        public bool CanShowExport() => 
            this.relatedItem.CanShowExport();

        public uint GetCurrentAC() => 
            this.settingAC.GetCurrentValue();

        public uint GetCurrentDC() => 
            this.settingDC.GetCurrentValue();

        public void HideSettingsItem()
        {
            this.settingAC.Visibility = Visibility.Hidden;
            this.settingDC.Visibility = Visibility.Hidden;
        }

        public void HideTitle2()
        {
            this.titleText2.Visibility = Visibility.Hidden;
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/comboboxplanitem.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public bool IsCapable() => 
            this.relatedItem.IsCapable();

        public void SetBackgroundToGray()
        {
            this.settingPanel.Background = (Brush) base.FindResource("GrayBackgroundBrush");
        }

        public void SetCurrentAC(uint settingValue)
        {
            this.settingAC.SetCurrentValue(settingValue);
        }

        public void SetCurrentDC(uint settingValue)
        {
            this.settingDC.SetCurrentValue(settingValue);
        }

        public void SetRelatedItem(ValueWithIndexItem relatedItem)
        {
            this.relatedItem = relatedItem;
            this.settingDC.SetSettableValue(relatedItem.GetSettableValueDC());
            this.settingAC.SetSettableValue(relatedItem.GetSettableValueAC());
        }

        public void SetTitle(string titleKey)
        {
            this.titleText.Text = (string) base.FindResource(titleKey);
        }

        public void ShowSettingsItem()
        {
            this.settingAC.Visibility = Visibility.Visible;
            this.settingDC.Visibility = Visibility.Visible;
        }

        public void ShowTitle2()
        {
            this.titleText2.Visibility = Visibility.Visible;
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (ComboBoxPlanItem) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.settingPanel = (Grid) target;
                    return;

                case 4:
                    this.settingTitle2 = (Label) target;
                    return;

                case 5:
                    this.titleText2 = (TextBlock) target;
                    return;

                case 6:
                    this.settingTitle = (Label) target;
                    return;

                case 7:
                    this.titleText = (AccessText) target;
                    return;

                case 8:
                    this.settingDC = (PMComboBox) target;
                    return;

                case 9:
                    this.settingAC = (PMComboBox) target;
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

