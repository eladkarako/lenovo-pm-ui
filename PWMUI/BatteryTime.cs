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
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class BatteryTime : System.Windows.Controls.UserControl, IComponentConnector
    {
        private bool _contentLoaded;
        internal Rectangle colon;
        internal StackPanel hoursPanel;
        internal Grid LayoutRoot;
        internal StackPanel minutesPanel;
        internal TextBlock unit;
        internal BatteryTime UserControl;

        public BatteryTime()
        {
            this.InitializeComponent();
        }

        private Label CreateNumberLabel(uint number) => 
            new Label { 
                Height = 0.0,
                Width = 0.0,
                Margin = new Thickness(0.0, 0.0, 0.0, 0.0),
                Padding = new Thickness(0.0, 0.0, 0.0, 0.0),
                Content = number.ToString()
            };

        private Rectangle CreateNumberRectangle(uint number)
        {
            Rectangle rectangle = new Rectangle {
                Height = 30.0,
                Width = 20.0
            };
            if (number == 1)
            {
                rectangle.Height = 28.0;
                rectangle.Width = 6.0;
            }
            rectangle.Margin = new Thickness(5.0, 0.0, 0.0, 0.0);
            if (number == 1)
            {
                rectangle.Margin = new Thickness(12.0, 1.0, 7.0, 1.0);
            }
            rectangle.Fill = this.GetNumberBrush(number);
            return rectangle;
        }

        private Brush GetNumberBrush(uint number)
        {
            string resourceKey = "time_0_design";
            if (number == 1)
            {
                resourceKey = "time_1_design";
            }
            if (number == 2)
            {
                resourceKey = "time_2_design";
            }
            if (number == 3)
            {
                resourceKey = "time_3_design";
            }
            if (number == 4)
            {
                resourceKey = "time_4_design";
            }
            if (number == 5)
            {
                resourceKey = "time_5_design";
            }
            if (number == 6)
            {
                resourceKey = "time_6_design";
            }
            if (number == 7)
            {
                resourceKey = "time_7_design";
            }
            if (number == 8)
            {
                resourceKey = "time_8_design";
            }
            if (number == 9)
            {
                resourceKey = "time_9_design";
            }
            return (Brush) base.FindResource(resourceKey);
        }

        private string GetUnit(uint hour, uint minute)
        {
            string resourceKey = "UnitMinutes";
            if (minute == 1)
            {
                resourceKey = "UnitMinute";
            }
            if (hour == 1)
            {
                resourceKey = "UnitHour";
            }
            if (hour > 1)
            {
                resourceKey = "UnitHours";
            }
            return (string) base.FindResource(resourceKey);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/batterytime.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public void Refresh(BatteryInformer informer)
        {
            if (!informer.RemainingTime.IsCapable() && !informer.CompletionTime.IsCapable())
            {
                base.Visibility = Visibility.Hidden;
            }
            else
            {
                string time = "";
                this.RefreshTime(informer, ref time);
                base.Visibility = Visibility.Visible;
                AutomationProperties.SetName(this, time);
            }
        }

        private void RefreshHoursPanel(uint hour)
        {
            this.hoursPanel.Children.Clear();
            if (hour == 0)
            {
                this.colon.Visibility = Visibility.Hidden;
            }
            else
            {
                this.colon.Visibility = Visibility.Visible;
                uint number = hour / 10;
                if (number != 0)
                {
                    this.hoursPanel.Children.Add(this.CreateNumberRectangle(number));
                }
                number = hour % 10;
                this.hoursPanel.Children.Add(this.CreateNumberRectangle(number));
                this.hoursPanel.Children.Add(this.CreateNumberLabel(hour));
            }
        }

        private void RefreshMinutesPanel(uint hour, uint minute)
        {
            this.minutesPanel.Children.Clear();
            uint number = minute / 10;
            if (hour != 0)
            {
                this.minutesPanel.Children.Add(this.CreateNumberRectangle(number));
            }
            else if (number != 0)
            {
                this.minutesPanel.Children.Add(this.CreateNumberRectangle(number));
            }
            number = minute % 10;
            this.minutesPanel.Children.Add(this.CreateNumberRectangle(number));
            this.minutesPanel.Children.Add(this.CreateNumberLabel(minute));
        }

        private void RefreshTime(BatteryInformer informer, ref string time)
        {
            uint minutes = informer.RemainingTime.Minutes;
            uint hours = informer.RemainingTime.Hours;
            if (informer.Status.MyStatus == BatteryStatus.Status.charge)
            {
                minutes = informer.CompletionTime.Minutes;
                hours = informer.CompletionTime.Hours;
            }
            this.RefreshHoursPanel(hours);
            this.RefreshMinutesPanel(hours, minutes);
            this.unit.Text = this.GetUnit(hours, minutes);
            string unit = "";
            if (hours != 0)
            {
                unit = this.GetUnit(hours, 0);
            }
            string str2 = "";
            if (minutes != 0)
            {
                str2 = this.GetUnit(0, minutes);
            }
            time = string.Concat(new object[] { hours, unit, minutes, str2 });
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (BatteryTime) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.hoursPanel = (StackPanel) target;
                    return;

                case 4:
                    this.colon = (Rectangle) target;
                    return;

                case 5:
                    this.minutesPanel = (StackPanel) target;
                    return;

                case 6:
                    this.unit = (TextBlock) target;
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

