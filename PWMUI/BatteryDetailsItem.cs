namespace PWMUI
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;

    public class BatteryDetailsItem : System.Windows.Controls.UserControl, IComponentConnector
    {
        private bool _contentLoaded;
        internal TextBlock data;
        internal Grid detailsGrid;
        internal Grid LayoutRoot;
        internal TextBlock title;
        internal BatteryDetailsItem UserControl;

        public BatteryDetailsItem()
        {
            this.InitializeComponent();
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/batterydetailsitem.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public void ResetBackground()
        {
            this.detailsGrid.Background = null;
        }

        public void SetBackgroundToGray()
        {
            this.detailsGrid.Background = (Brush) base.FindResource("GrayBackgroundBrush");
        }

        public void SetCurrentData(string data)
        {
            this.data.Text = data;
        }

        public void SetTitle(string titleKey)
        {
            this.title.Text = (string) base.FindResource(titleKey);
        }

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (BatteryDetailsItem) target;
                    this.UserControl.GotFocus += new RoutedEventHandler(this.UserControl_GotFocus);
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.detailsGrid = (Grid) target;
                    return;

                case 4:
                    this.title = (TextBlock) target;
                    return;

                case 5:
                    this.data = (TextBlock) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            AutomationProperties.SetName(this, this.title.Text + " " + this.data.Text);
        }
    }
}

