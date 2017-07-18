namespace PWMUI
{
    using System;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    [TemplatePart(Name="PART_Month", Type=typeof(TextBox)), TemplatePart(Name="PART_DecrementDay", Type=typeof(ButtonBase)), TemplatePart(Name="PART_Day", Type=typeof(TextBox)), TemplatePart(Name="PART_IncreaseDay", Type=typeof(ButtonBase))]
    public class DatePicker : Control
    {
        private TextBox currentlySelectedTextBox;
        private TextBox day;
        private ButtonBase decrementButton;
        private ButtonBase increaseButton;
        private bool isUpdatingDate;
        private TextBox month;
        public static readonly RoutedEvent SelectedDateChangedEvent = EventManager.RegisterRoutedEvent("SelectedDateChanged", RoutingStrategy.Bubble, typeof(DateSelectedChangedEventHandler), typeof(DatePicker));
        public static readonly DependencyProperty SelectedDateProperty = DependencyProperty.Register("SelectedDate", typeof(DateTime), typeof(DatePicker), new UIPropertyMetadata(DateTime.Today, new PropertyChangedCallback(DatePicker.SelectedDatePropertyChanged)));
        private int selectedDay;
        private int selectedMonth;

        public event DateSelectedChangedEventHandler SelectedDateChanged
        {
            add
            {
                base.AddHandler(SelectedDateChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(SelectedDateChangedEvent, value);
            }
        }

        static DatePicker()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(DatePicker), new FrameworkPropertyMetadata(typeof(DatePicker)));
        }

        public DatePicker()
        {
            this.SelectedDate = DateTime.Today;
            this.SelectedMonth = this.SelectedDate.Month;
            this.SelectedDay = this.SelectedDate.Day;
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

        private void BeginUpdateSelectedDate()
        {
            this.isUpdatingDate = true;
        }

        private void DayKeyUp(object sender, KeyEventArgs e)
        {
            TryFocusNeighbourControl(this.month, this.day, null, e.Key);
            if (!this.IncrementDecrementDate(e.Key))
            {
                this.ValidateAndSetDay(this.day.Text);
            }
        }

        private void DayTextChanged(object sender, TextCompositionEventArgs e)
        {
            TrimSelectedText(this.day);
            string text = AdjustText(this.day, e.Text);
            this.ValidateAndSetDay(text);
            this.day.Select(this.day.Text.Length, 0);
            e.Handled = true;
        }

        private void DecrementDate(object sender, RoutedEventArgs e)
        {
            this.IncrementDecrementDate(false);
        }

        private void EndUpdateSelectedDate()
        {
            this.isUpdatingDate = false;
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

        private void IncreaseDate(object sender, RoutedEventArgs e)
        {
            this.IncrementDecrementDate(true);
        }

        private void IncrementDecrementDate(bool increment)
        {
            int currentMonth = ValidateNumber(this.month.Text);
            int num2 = ValidateNumber(this.day.Text);
            if (this.month == this.currentlySelectedTextBox)
            {
                int newMonth = increment ? ++currentMonth : --currentMonth;
                this.SelectedMonth = ValidateMonth(newMonth);
            }
            if (this.day == this.currentlySelectedTextBox)
            {
                int newDay = increment ? ++num2 : --num2;
                this.SelectedDay = ValidateDay(currentMonth, newDay);
            }
        }

        private bool IncrementDecrementDate(Key selectedKey)
        {
            if (selectedKey == Key.Up)
            {
                this.IncrementDecrementDate(true);
            }
            else if (selectedKey == Key.Down)
            {
                this.IncrementDecrementDate(false);
            }
            else
            {
                return false;
            }
            return true;
        }

        private void MonthKeyUp(object sender, KeyEventArgs e)
        {
            TryFocusNeighbourControl(this.month, null, this.day, e.Key);
            if (!this.IncrementDecrementDate(e.Key))
            {
                this.ValidateAndSetMonth(this.month.Text);
            }
        }

        private void MonthTextChanged(object sender, TextCompositionEventArgs e)
        {
            TrimSelectedText(this.month);
            string text = AdjustText(this.month, e.Text);
            this.ValidateAndSetMonth(text);
            AdjustCarretIndexOrMoveToNeighbour(this.month, this.day);
            this.month.Select(this.month.Text.Length, 0);
            e.Handled = true;
        }

        public override void OnApplyTemplate()
        {
            this.month = (TextBox) base.GetTemplateChild("PART_Month");
            this.month.PreviewTextInput += new TextCompositionEventHandler(this.MonthTextChanged);
            this.month.KeyUp += new KeyEventHandler(this.MonthKeyUp);
            this.month.GotFocus += new RoutedEventHandler(this.TextGotFocus);
            this.month.GotMouseCapture += new MouseEventHandler(this.TextGotFocus);
            this.day = (TextBox) base.GetTemplateChild("PART_Day");
            this.day.PreviewTextInput += new TextCompositionEventHandler(this.DayTextChanged);
            this.day.KeyUp += new KeyEventHandler(this.DayKeyUp);
            this.day.GotFocus += new RoutedEventHandler(this.TextGotFocus);
            this.day.GotMouseCapture += new MouseEventHandler(this.TextGotFocus);
            this.increaseButton = (ButtonBase) base.GetTemplateChild("PART_IncreaseDate");
            this.increaseButton.Click += new RoutedEventHandler(this.IncreaseDate);
            this.decrementButton = (ButtonBase) base.GetTemplateChild("PART_DecrementDate");
            this.decrementButton.Click += new RoutedEventHandler(this.DecrementDate);
        }

        private void OnDateSelectedChanged(DateTime newDate, DateTime oldDate)
        {
            DateSelectedChangedRoutedEventArgs e = new DateSelectedChangedRoutedEventArgs(SelectedDateChangedEvent) {
                NewDate = newDate,
                OldDate = oldDate
            };
            base.RaiseEvent(e);
        }

        private static void SelectedDatePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            DatePicker picker = (DatePicker) sender;
            DateTime newValue = (DateTime) e.NewValue;
            DateTime oldValue = (DateTime) e.OldValue;
            if (!picker.isUpdatingDate)
            {
                picker.BeginUpdateSelectedDate();
                if (picker.SelectedMonth != newValue.Month)
                {
                    picker.SelectedMonth = newValue.Month;
                }
                if (picker.SelectedDay != newValue.Day)
                {
                    picker.SelectedDay = newValue.Day;
                }
                picker.EndUpdateSelectedDate();
                picker.OnDateSelectedChanged(picker.SelectedDate, oldValue);
            }
        }

        private static void SetNewDate(DatePicker datePicker)
        {
            if (!datePicker.isUpdatingDate)
            {
                DateTime time = new DateTime(DateTime.Today.Year, datePicker.SelectedMonth, datePicker.SelectedDay);
                datePicker.SelectedDate = time;
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

        private int ValidateAndSetDay(string text)
        {
            int currentMonth = ValidateNumber(this.month.Text);
            int newDay = ValidateNumber(text);
            this.SelectedDay = ValidateDay(currentMonth, newDay);
            return this.SelectedDay;
        }

        private int ValidateAndSetMonth(string text)
        {
            int newMonth = ValidateNumber(text);
            this.SelectedMonth = ValidateMonth(newMonth);
            return this.SelectedMonth;
        }

        private static int ValidateDay(int currentMonth, int newDay)
        {
            int num = DateTime.DaysInMonth(DateTime.Today.Year, currentMonth);
            if (newDay > num)
            {
                return 1;
            }
            if (newDay < 1)
            {
                return num;
            }
            return newDay;
        }

        private static int ValidateMonth(int newMonth)
        {
            if (newMonth > 12)
            {
                return 1;
            }
            if (newMonth < 1)
            {
                return 12;
            }
            return newMonth;
        }

        private static int ValidateNumber(string newNum)
        {
            int num;
            if (!int.TryParse(newNum, out num))
            {
                return 1;
            }
            return num;
        }

        public DateTime SelectedDate
        {
            get => 
                ((DateTime) base.GetValue(SelectedDateProperty));
            set
            {
                base.SetValue(SelectedDateProperty, value);
                string str = value.ToString("M");
                if (this.month != null)
                {
                    AutomationProperties.SetName(this.month, str);
                }
                if (this.day != null)
                {
                    AutomationProperties.SetName(this.day, str);
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

        public int SelectedDay
        {
            get => 
                this.selectedDay;
            set
            {
                this.selectedDay = value;
                if (this.day != null)
                {
                    this.day.Text = this.selectedDay.ToString();
                }
                SetNewDate(this);
            }
        }

        public int SelectedMonth
        {
            get => 
                this.selectedMonth;
            set
            {
                this.selectedMonth = value;
                if (this.month != null)
                {
                    this.month.Text = this.selectedMonth.ToString();
                }
                int num = ValidateDay(this.selectedMonth, this.selectedDay);
                if (num != this.selectedDay)
                {
                    this.selectedDay = num;
                }
                SetNewDate(this);
            }
        }
    }
}

