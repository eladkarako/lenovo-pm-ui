namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class BatteryConditionItem : System.Windows.Controls.UserControl, IComponentConnector
    {
        private bool _contentLoaded;
        internal TextBlock batteryKind;
        internal TextBlock condition;
        internal Grid conditionGrid;
        internal Rectangle conditionIcon;
        internal TextBlock conditionTitle;
        private bool isActive;
        internal Grid LayoutRoot;
        internal BatteryConditionItem UserControl;

        public BatteryConditionItem()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/batteryconditionitem.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public void SetActiveBackGround()
        {
            this.isActive = true;
            this.batteryKind.Foreground = Brushes.White;
            this.conditionTitle.Foreground = Brushes.White;
            this.condition.Foreground = Brushes.White;
            base.Background = (Brush) base.FindResource("ActiveBackgroundBrush");
            base.BorderBrush = (Brush) base.FindResource("ActiveBackgroundBrush");
        }

        public void SetCondition(Battery battery)
        {
            if (battery.IsInstalled)
            {
                base.Visibility = Visibility.Visible;
            }
            else
            {
                base.Visibility = Visibility.Hidden;
            }
            this.SetConditionString(battery);
            this.SetConditionIcon(battery);
        }

        private void SetConditionIcon(Battery battery)
        {
            string resourceKey = "img_status_Error_design";
            if (battery.Condition.MyCondition == BatteryCondition.Condition.NotInstalled)
            {
                resourceKey = "img_status_NotInstalled_design";
            }
            if (battery.Condition.MyCondition == BatteryCondition.Condition.Green)
            {
                resourceKey = "img_status_G_design";
            }
            if (battery.Condition.MyCondition == BatteryCondition.Condition.Yellow)
            {
                resourceKey = "img_status_Y_design";
            }
            if (battery.Condition.MyCondition == BatteryCondition.Condition.Red)
            {
                resourceKey = "img_status_R_design";
            }
            this.conditionIcon.Fill = (Brush) base.FindResource(resourceKey);
            this.condition.Text = battery.Condition.ToString();
        }

        private void SetConditionString(Battery battery)
        {
            this.condition.Text = battery.Condition.ToString();
        }

        public void SetDefaultBackground()
        {
            this.isActive = false;
            if (!base.IsMouseOver)
            {
                this.batteryKind.Foreground = Brushes.Black;
                this.conditionTitle.Foreground = Brushes.Black;
                this.condition.Foreground = Brushes.Black;
                base.Background = (Brush) base.FindResource("GrayBackgroundBrush");
                base.BorderBrush = (Brush) base.FindResource("GrayBackgroundBrush");
            }
        }

        public void SetMouseOverBackground()
        {
            if (!this.isActive)
            {
                this.batteryKind.Foreground = Brushes.Black;
                this.conditionTitle.Foreground = Brushes.Black;
                this.condition.Foreground = Brushes.Black;
                base.Background = (Brush) base.FindResource("MouseOverBackgroundBrush");
                base.BorderBrush = (Brush) base.FindResource("LightBlueBrush");
            }
        }

        public void SetPrefixToTitleString(string prefix)
        {
            this.batteryKind.Text = prefix + this.batteryKind.Text;
        }

        public void SetTitle(string titleKey)
        {
            this.batteryKind.Text = (string) base.FindResource(titleKey);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (BatteryConditionItem) target;
                    this.UserControl.MouseEnter += new MouseEventHandler(this.UserControl_MouseEnter);
                    this.UserControl.MouseLeave += new MouseEventHandler(this.UserControl_MouseLeave);
                    this.UserControl.GotFocus += new RoutedEventHandler(this.UserControl_GotFocus);
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.conditionGrid = (Grid) target;
                    return;

                case 4:
                    this.batteryKind = (TextBlock) target;
                    return;

                case 5:
                    this.conditionTitle = (TextBlock) target;
                    return;

                case 6:
                    this.conditionIcon = (Rectangle) target;
                    return;

                case 7:
                    this.condition = (TextBlock) target;
                    return;
            }
            this._contentLoaded = true;
        }

        private void UserControl_GotFocus(object sender, RoutedEventArgs e)
        {
            AutomationProperties.SetName(this, this.batteryKind.Text + " " + this.conditionTitle.Text + " " + this.condition.Text);
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.SetMouseOverBackground();
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.isActive)
            {
                this.SetActiveBackGround();
            }
            else
            {
                this.SetDefaultBackground();
            }
        }
    }
}

