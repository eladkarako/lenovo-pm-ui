namespace PWMUI
{
    using System;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    [TemplatePart(Name="PART_Minutes", Type=typeof(TextBox)), TemplatePart(Name="PART_IncreaseTime", Type=typeof(ButtonBase)), TemplatePart(Name="PART_DecrementTime", Type=typeof(ButtonBase)), TemplatePart(Name="PART_Hours", Type=typeof(TextBox))]
    public class TimePicker : Control
    {
        private TextBox currentlySelectedTextBox;
        private ButtonBase decrementButton;
        private int HourMaxValue = 0x17;
        private int HourMinValue;
        private TextBox hours;
        private ButtonBase increaseButton;
        private bool isUpdatingTime;
        public static readonly DependencyProperty MaxTimeProperty;
        public static readonly DependencyProperty MinTimeProperty;
        private int MinuteMaxValue = 0x3b;
        private int MinuteMinValue;
        private TextBox minutes;
        public static readonly DependencyProperty SelectedHourProperty;
        public static readonly DependencyProperty SelectedMinuteProperty;
        public static readonly RoutedEvent SelectedTimeChangedEvent;
        public static readonly DependencyProperty SelectedTimeProperty;

        public event TimeSelectedChangedEventHandler SelectedTimeChanged
        {
            add
            {
                base.AddHandler(SelectedTimeChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(SelectedTimeChangedEvent, value);
            }
        }

        static TimePicker()
        {
            MinTimeProperty = DependencyProperty.Register("MinTime", typeof(TimeSpan), typeof(TimePicker), new UIPropertyMetadata(TimeSpan.MinValue, delegate (DependencyObject sender, DependencyPropertyChangedEventArgs e) {
                TimePicker picker = (TimePicker) sender;
                picker.HourMinValue = picker.MinTime.Hours;
                picker.MinuteMinValue = picker.MinTime.Minutes;
                picker.CoerceValue(SelectedTimeProperty);
            }));
            MaxTimeProperty = DependencyProperty.Register("MaxTime", typeof(TimeSpan), typeof(TimePicker), new UIPropertyMetadata(TimeSpan.MaxValue, delegate (DependencyObject sender, DependencyPropertyChangedEventArgs e) {
                TimePicker picker = (TimePicker) sender;
                picker.HourMaxValue = picker.MaxTime.Hours;
                picker.MinuteMaxValue = picker.MaxTime.Minutes;
                picker.CoerceValue(SelectedTimeProperty);
            }));
            SelectedTimeProperty = DependencyProperty.Register("SelectedTime", typeof(TimeSpan), typeof(TimePicker), new UIPropertyMetadata(new TimeSpan(0, 0, 0), new PropertyChangedCallback(TimePicker.SelectedTimePropertyChanged), new CoerceValueCallback(TimePicker.ForceValidSelectedTime)));
            SelectedHourProperty = DependencyProperty.Register("SelectedHour", typeof(int), typeof(TimePicker), new UIPropertyMetadata(0, delegate (DependencyObject sender, DependencyPropertyChangedEventArgs e) {
                TimePicker timePicker = (TimePicker) sender;
                int num = MathUtil.ValidateNumber(timePicker.SelectedHour, timePicker.HourMinValue, timePicker.HourMaxValue);
                if (num != timePicker.SelectedHour)
                {
                    timePicker.SelectedHour = num;
                }
                SetNewTime(timePicker);
            }));
            SelectedMinuteProperty = DependencyProperty.Register("SelectedMinute", typeof(int), typeof(TimePicker), new UIPropertyMetadata(0, delegate (DependencyObject sender, DependencyPropertyChangedEventArgs e) {
                TimePicker timePicker = (TimePicker) sender;
                int num = MathUtil.ValidateNumber(timePicker.SelectedMinute, timePicker.MinuteMinValue, timePicker.MinuteMaxValue);
                if (num != timePicker.SelectedMinute)
                {
                    timePicker.SelectedMinute = num;
                }
                SetNewTime(timePicker);
            }));
            SelectedTimeChangedEvent = EventManager.RegisterRoutedEvent("SelectedTimeChanged", RoutingStrategy.Bubble, typeof(TimeSelectedChangedEventHandler), typeof(TimePicker));
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(TimePicker), new FrameworkPropertyMetadata(typeof(TimePicker)));
        }

        public TimePicker()
        {
            this.SelectedTime = DateTime.Now.TimeOfDay;
        }

        private static void AdjustCarretIndexOrMoveToNeighbour(TextBox current, TextBox neighbour)
        {
            if ((current.CaretIndex == 1) && (neighbour != null))
            {
                neighbour.Focus();
            }
            else if (current.CaretIndex == 0)
            {
                current.CaretIndex++;
            }
        }

        private static string AdjustText(TextBox textBox, string newText)
        {
            if (textBox.Text.Length == 2)
            {
                if (textBox.CaretIndex == 0)
                {
                    return (newText + textBox.Text[1]);
                }
                return (textBox.Text[0] + newText);
            }
            if (textBox.CaretIndex != 0)
            {
                return (textBox.Text + newText);
            }
            return (newText + textBox.Text);
        }

        private void BeginUpdateSelectedTime()
        {
            this.isUpdatingTime = true;
        }

        private void DecrementTime(object sender, RoutedEventArgs e)
        {
            this.IncrementDecrementTime(false);
        }

        private void EndUpdateSelectedTime()
        {
            this.isUpdatingTime = false;
        }

        public static void ExposeAdjustCarretIndexOrMoveToNeighbour(TextBox current, TextBox neighbour)
        {
            AdjustCarretIndexOrMoveToNeighbour(current, neighbour);
        }

        public static void ExposeTrimSelectedText(TextBox textBox)
        {
            TrimSelectedText(textBox);
        }

        public static void ExposeTryFocusNeighbourControl(TextBox currentControl, TextBox leftControl, TextBox rightControl, Key keyPressed)
        {
            TryFocusNeighbourControl(currentControl, leftControl, rightControl, keyPressed);
        }

        private static object ForceValidSelectedTime(DependencyObject sender, object value)
        {
            TimePicker picker = (TimePicker) sender;
            TimeSpan span = (TimeSpan) value;
            if (span < picker.MinTime)
            {
                return picker.MinTime;
            }
            if (span > picker.MaxTime)
            {
                return picker.MaxTime;
            }
            return span;
        }

        private void HoursKeyUp(object sender, KeyEventArgs e)
        {
            TryFocusNeighbourControl(this.hours, null, this.minutes, e.Key);
            if (!this.IncrementDecrementTime(e.Key))
            {
                this.ValidateAndSetHour(this.hours.Text);
            }
        }

        private void HoursTextChanged(object sender, TextCompositionEventArgs e)
        {
            TrimSelectedText(this.hours);
            string text = AdjustText(this.hours, e.Text);
            this.ValidateAndSetHour(text);
            AdjustCarretIndexOrMoveToNeighbour(this.hours, this.minutes);
            this.hours.Select(this.hours.Text.Length, 0);
            e.Handled = true;
        }

        private void IncreaseTime(object sender, RoutedEventArgs e)
        {
            this.IncrementDecrementTime(true);
        }

        private void IncrementDecrementTime(bool increment)
        {
            if (this.hours == this.currentlySelectedTextBox)
            {
                this.SelectedHour = MathUtil.IncrementDecrementNumber(this.hours.Text, this.HourMinValue, this.HourMaxValue, increment);
            }
            else if (this.minutes == this.currentlySelectedTextBox)
            {
                this.SelectedMinute = MathUtil.IncrementDecrementNumber(this.minutes.Text, this.MinuteMinValue, this.MinuteMaxValue, increment);
            }
        }

        private bool IncrementDecrementTime(Key selectedKey)
        {
            if (selectedKey == Key.Up)
            {
                this.IncrementDecrementTime(true);
            }
            else if (selectedKey == Key.Down)
            {
                this.IncrementDecrementTime(false);
            }
            else
            {
                return false;
            }
            return true;
        }

        private void MinutesKeyUp(object sender, KeyEventArgs e)
        {
            TryFocusNeighbourControl(this.minutes, this.hours, null, e.Key);
            if (!this.IncrementDecrementTime(e.Key))
            {
                this.ValidateAndSetMinute(this.minutes.Text);
            }
        }

        private void MinutesTextChanged(object sender, TextCompositionEventArgs e)
        {
            TrimSelectedText(this.minutes);
            string text = AdjustText(this.minutes, e.Text);
            this.ValidateAndSetMinute(text);
            this.minutes.Select(this.minutes.Text.Length, 0);
            e.Handled = true;
        }

        public override void OnApplyTemplate()
        {
            this.hours = (TextBox) base.GetTemplateChild("PART_Hours");
            this.hours.PreviewTextInput += new TextCompositionEventHandler(this.HoursTextChanged);
            this.hours.KeyUp += new KeyEventHandler(this.HoursKeyUp);
            this.hours.GotFocus += new RoutedEventHandler(this.TextGotFocus);
            this.hours.GotMouseCapture += new MouseEventHandler(this.TextGotFocus);
            this.minutes = (TextBox) base.GetTemplateChild("PART_Minutes");
            this.minutes.PreviewTextInput += new TextCompositionEventHandler(this.MinutesTextChanged);
            this.minutes.KeyUp += new KeyEventHandler(this.MinutesKeyUp);
            this.minutes.GotFocus += new RoutedEventHandler(this.TextGotFocus);
            this.minutes.GotMouseCapture += new MouseEventHandler(this.TextGotFocus);
            this.increaseButton = (ButtonBase) base.GetTemplateChild("PART_IncreaseTime");
            this.increaseButton.Click += new RoutedEventHandler(this.IncreaseTime);
            this.decrementButton = (ButtonBase) base.GetTemplateChild("PART_DecrementTime");
            this.decrementButton.Click += new RoutedEventHandler(this.DecrementTime);
            if (DateTime.Now.ToString("HH:mm").Contains("."))
            {
                TextBlock templateChild = (TextBlock) base.GetTemplateChild("timeSplitter");
                if (templateChild != null)
                {
                    templateChild.Text = ".";
                }
            }
        }

        private void OnTimeSelectedChanged(TimeSpan newTime, TimeSpan oldTime)
        {
            TimeSelectedChangedRoutedEventArgs e = new TimeSelectedChangedRoutedEventArgs(SelectedTimeChangedEvent) {
                NewTime = newTime,
                OldTime = oldTime
            };
            base.RaiseEvent(e);
        }

        private static void SelectedTimePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TimePicker picker = (TimePicker) sender;
            TimeSpan newValue = (TimeSpan) e.NewValue;
            TimeSpan oldValue = (TimeSpan) e.OldValue;
            if (!picker.isUpdatingTime)
            {
                picker.BeginUpdateSelectedTime();
                if (picker.SelectedHour != newValue.Hours)
                {
                    picker.SelectedHour = newValue.Hours;
                }
                if (picker.SelectedMinute != newValue.Minutes)
                {
                    picker.SelectedMinute = newValue.Minutes;
                }
                picker.EndUpdateSelectedTime();
                picker.OnTimeSelectedChanged(picker.SelectedTime, oldValue);
            }
        }

        private static void SetNewTime(TimePicker timePicker)
        {
            if (!timePicker.isUpdatingTime)
            {
                TimeSpan span = new TimeSpan(timePicker.SelectedHour, timePicker.SelectedMinute, 0);
                timePicker.SelectedTime = span;
            }
        }

        private void TextGotFocus(object sender, RoutedEventArgs e)
        {
            TextBox box = (TextBox) sender;
            this.currentlySelectedTextBox = box;
            box.SelectAll();
        }

        private static void TrimSelectedText(TextBox textBox)
        {
            if (textBox.SelectionLength > 0)
            {
                textBox.Text = textBox.Text.Remove(textBox.SelectionStart, textBox.SelectionLength);
            }
        }

        private static void TryFocusNeighbourControl(TextBox currentControl, TextBox leftControl, TextBox rightControl, Key keyPressed)
        {
            if (((keyPressed == Key.Left) && (leftControl != null)) && (currentControl.CaretIndex == 0))
            {
                leftControl.Focus();
            }
            else if (((keyPressed == Key.Right) && (rightControl != null)) && (currentControl.CaretIndex == currentControl.Text.Length))
            {
                rightControl.Focus();
            }
        }

        private int ValidateAndSetHour(string text)
        {
            int num = MathUtil.ValidateNumber(text, this.HourMinValue, this.HourMaxValue);
            this.SelectedHour = num;
            return num;
        }

        private int ValidateAndSetMinute(string text)
        {
            int num = MathUtil.ValidateNumber(text, this.MinuteMinValue, this.MinuteMaxValue);
            this.SelectedMinute = num;
            return num;
        }

        public TimeSpan MaxTime
        {
            get => 
                ((TimeSpan) base.GetValue(MaxTimeProperty));
            set
            {
                base.SetValue(MaxTimeProperty, value);
            }
        }

        public TimeSpan MinTime
        {
            get => 
                ((TimeSpan) base.GetValue(MinTimeProperty));
            set
            {
                base.SetValue(MinTimeProperty, value);
            }
        }

        public int SelectedHour
        {
            get => 
                ((int) base.GetValue(SelectedHourProperty));
            set
            {
                base.SetValue(SelectedHourProperty, value);
            }
        }

        public int SelectedMinute
        {
            get => 
                ((int) base.GetValue(SelectedMinuteProperty));
            set
            {
                base.SetValue(SelectedMinuteProperty, value);
            }
        }

        public TimeSpan SelectedTime
        {
            get => 
                ((TimeSpan) base.GetValue(SelectedTimeProperty));
            set
            {
                base.SetValue(SelectedTimeProperty, value);
                TimeSpan span = value;
                string str = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, span.Hours, span.Minutes, 0).ToString("t");
                if (this.hours != null)
                {
                    AutomationProperties.SetName(this.hours, str);
                }
                if (this.minutes != null)
                {
                    AutomationProperties.SetName(this.minutes, str);
                }
                if (this.increaseButton != null)
                {
                    AutomationProperties.SetName(this.increaseButton, str);
                }
                if (this.decrementButton != null)
                {
                    AutomationProperties.SetName(this.decrementButton, str);
                }
            }
        }
    }
}

