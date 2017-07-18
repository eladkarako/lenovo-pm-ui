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

    public class SliderPlanItem : System.Windows.Controls.UserControl, IPlanItem, IComponentConnector
    {
        private bool _contentLoaded;
        internal Grid LayoutRoot;
        private ValueWithRangeItem relatedItem;
        internal Grid settingPanel;
        internal Label settingTitle;
        internal Label settingTitle2;
        internal PMSliderControl sliderAC;
        internal PMSliderControl sliderDC;
        internal AccessText titleText;
        internal TextBlock titleText2;
        internal SliderPlanItem UserControl;

        public SliderPlanItem()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        public bool CanShowExport() => 
            this.relatedItem.CanShowExport();

        public uint GetCurrentAC() => 
            this.sliderAC.GetCurrentValue();

        public uint GetCurrentDC() => 
            this.sliderDC.GetCurrentValue();

        public void HideSettingsItem()
        {
            this.sliderAC.Visibility = Visibility.Hidden;
            this.sliderDC.Visibility = Visibility.Hidden;
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
                Uri resourceLocator = new Uri("/PWMUI;component/sliderplanitem.xaml", UriKind.Relative);
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
            this.sliderAC.SetCurrentValue(settingValue);
        }

        public void SetCurrentDC(uint settingValue)
        {
            this.sliderDC.SetCurrentValue(settingValue);
        }

        public void SetMaxText(string maxKey)
        {
            this.sliderAC.SetMaxText((string) base.FindResource(maxKey));
            this.sliderDC.SetMaxText((string) base.FindResource(maxKey));
        }

        public void SetMinText(string minKey)
        {
            this.sliderAC.SetMinText((string) base.FindResource(minKey));
            this.sliderDC.SetMinText((string) base.FindResource(minKey));
        }

        public void SetRelatedItem(ValueWithRangeItem relatedItem)
        {
            this.relatedItem = relatedItem;
            this.sliderAC.SetSettableValue(relatedItem.GetSettableValue());
            this.sliderDC.SetSettableValue(relatedItem.GetSettableValue());
        }

        public void SetTitle(string titleKey)
        {
            this.titleText.Text = (string) base.FindResource(titleKey);
        }

        public void ShowSettingsItem()
        {
            this.sliderAC.Visibility = Visibility.Visible;
            this.sliderDC.Visibility = Visibility.Visible;
        }

        public void ShowTitle2()
        {
            this.titleText2.Visibility = Visibility.Visible;
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (SliderPlanItem) target;
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
                    this.sliderDC = (PMSliderControl) target;
                    return;

                case 9:
                    this.sliderAC = (PMSliderControl) target;
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

