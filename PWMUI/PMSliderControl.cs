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

    public class PMSliderControl : System.Windows.Controls.UserControl, IPMControl, IComponentConnector
    {
        private bool _contentLoaded;
        internal Grid LayoutRoot;
        internal TextBlock maxText;
        internal TextBlock minText;
        private ValueWithRange settableValue;
        internal Slider settingSlider;
        internal PMSliderControl UserControl;

        public PMSliderControl()
        {
            this.InitializeComponent();
        }

        public void EnableApplyButton()
        {
            MainWindow.Instance.EnableApplyButton();
        }

        public uint GetCurrentValue() => 
            ((uint) this.settingSlider.Value);

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/pmslidercontrol.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public void SetCurrentValue(uint settingValue)
        {
            this.settingSlider.Value = settingValue;
        }

        public void SetMaxText(string maxText)
        {
            this.maxText.Text = maxText;
        }

        public void SetMinText(string minText)
        {
            this.minText.Text = minText;
        }

        public void SetSettableValue(ValueWithRange settableValue)
        {
            this.settableValue = settableValue;
            this.settingSlider.Maximum = settableValue.GetMax();
            this.settingSlider.Minimum = settableValue.GetMin();
        }

        private void settingSlider_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (base.IsVisible)
            {
                this.EnableApplyButton();
            }
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (PMSliderControl) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.minText = (TextBlock) target;
                    return;

                case 4:
                    this.maxText = (TextBlock) target;
                    return;

                case 5:
                    this.settingSlider = (Slider) target;
                    this.settingSlider.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.settingSlider_ValueChanged);
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

