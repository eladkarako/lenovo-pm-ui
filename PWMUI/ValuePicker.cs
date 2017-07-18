namespace PWMUI
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    [TemplatePart(Name="PART_DecrementDay", Type=typeof(ButtonBase)), TemplatePart(Name="PART_CurrentValue", Type=typeof(TextBox)), TemplatePart(Name="PART_IncreaseDay", Type=typeof(ButtonBase))]
    public class ValuePicker : Control
    {
        public static readonly RoutedEvent CurrentValueChangedEvent = EventManager.RegisterRoutedEvent("CurrentValueChanged", RoutingStrategy.Bubble, typeof(CurrentValueChangedEventHandler), typeof(ValuePicker));
        public static readonly DependencyProperty CurrentValueProperty = DependencyProperty.Register("CurrentValue", typeof(uint), typeof(ValuePicker), new UIPropertyMetadata(50, new PropertyChangedCallback(ValuePicker.CurrentValuePropertyChanged)));
        private TextBox currentValueText;
        private ButtonBase decrementButton;
        private ButtonBase increaseButton;
        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(uint), typeof(ValuePicker), new UIPropertyMetadata(100));
        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(uint), typeof(ValuePicker), new UIPropertyMetadata(0));
        public static readonly DependencyProperty UnitProperty = DependencyProperty.Register("Unit", typeof(string), typeof(ValuePicker), new UIPropertyMetadata("%"));

        public event CurrentValueChangedEventHandler CurrentValueChanged
        {
            add
            {
                base.AddHandler(CurrentValueChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(CurrentValueChangedEvent, value);
            }
        }

        static ValuePicker()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(ValuePicker), new FrameworkPropertyMetadata(typeof(ValuePicker)));
        }

        private static string AdjustText(TextBox textBox, string newText) => 
            textBox.Text.Insert(textBox.CaretIndex, newText);

        private static void CurrentValuePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ValuePicker picker = (ValuePicker) sender;
            uint newValue = (uint) e.NewValue;
            if (newValue > picker.MaxValue)
            {
                picker.CurrentValue = picker.MaxValue;
            }
            if (newValue < picker.MinValue)
            {
                picker.CurrentValue = picker.MinValue;
            }
        }

        private void CurrentValueTextChanged(object sender, TextChangedEventArgs e)
        {
            e.Handled = true;
        }

        private void CurrentValueTextGotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox) sender).SelectAll();
        }

        private void CurrentValueTextInput(object sender, TextCompositionEventArgs e)
        {
            TrimSelectedText(this.currentValueText);
            string newNum = AdjustText(this.currentValueText, e.Text);
            this.CurrentValue = ValidateNumber(newNum);
            this.currentValueText.Select(this.currentValueText.Text.Length, 0);
            e.Handled = true;
        }

        private void CurrentValueTextKeyUp(object sender, KeyEventArgs e)
        {
            this.IncrementDecrementValue(e.Key);
        }

        private void DecrementValue(object sender, RoutedEventArgs e)
        {
            this.IncrementDecrementValue(false);
        }

        private void IncreaseValue(object sender, RoutedEventArgs e)
        {
            this.IncrementDecrementValue(true);
        }

        private void IncrementDecrementValue(bool increment)
        {
            uint num = ValidateNumber(this.currentValueText.Text);
            uint num2 = increment ? ++num : --num;
            this.CurrentValue = num2;
        }

        private bool IncrementDecrementValue(Key selectedKey)
        {
            if (selectedKey == Key.Up)
            {
                this.IncrementDecrementValue(true);
            }
            else if (selectedKey == Key.Down)
            {
                this.IncrementDecrementValue(false);
            }
            else
            {
                return false;
            }
            return true;
        }

        public override void OnApplyTemplate()
        {
            this.currentValueText = (TextBox) base.GetTemplateChild("PART_CurrentValue");
            this.currentValueText.PreviewTextInput += new TextCompositionEventHandler(this.CurrentValueTextInput);
            this.currentValueText.TextChanged += new TextChangedEventHandler(this.CurrentValueTextChanged);
            this.currentValueText.KeyUp += new KeyEventHandler(this.CurrentValueTextKeyUp);
            this.currentValueText.GotFocus += new RoutedEventHandler(this.CurrentValueTextGotFocus);
            this.currentValueText.GotMouseCapture += new MouseEventHandler(this.CurrentValueTextGotFocus);
            this.increaseButton = (ButtonBase) base.GetTemplateChild("PART_IncreaseDate");
            this.increaseButton.Click += new RoutedEventHandler(this.IncreaseValue);
            this.decrementButton = (ButtonBase) base.GetTemplateChild("PART_DecrementDate");
            this.decrementButton.Click += new RoutedEventHandler(this.DecrementValue);
        }

        private static void TrimSelectedText(TextBox textBox)
        {
            if (textBox.SelectionLength > 0)
            {
                textBox.Text = textBox.Text.Remove(textBox.SelectionStart, textBox.SelectionLength);
            }
        }

        private static uint ValidateNumber(string newNum)
        {
            uint num;
            if (!uint.TryParse(newNum, out num))
            {
                return 0;
            }
            return num;
        }

        public uint CurrentValue
        {
            get => 
                ((uint) base.GetValue(CurrentValueProperty));
            set
            {
                base.SetValue(CurrentValueProperty, value);
                string str = value.ToString() + this.Unit;
                if (this.currentValueText != null)
                {
                    AutomationProperties.SetName(this.currentValueText, str);
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

        public uint MaxValue
        {
            get => 
                ((uint) base.GetValue(MaxValueProperty));
            set
            {
                base.SetValue(MaxValueProperty, value);
            }
        }

        public uint MinValue
        {
            get => 
                ((uint) base.GetValue(MinValueProperty));
            set
            {
                base.SetValue(MinValueProperty, value);
            }
        }

        public string Unit
        {
            get => 
                ((string) base.GetValue(UnitProperty));
            set
            {
                base.SetValue(UnitProperty, value);
            }
        }

        public delegate void CurrentValueChangedEventHandler(object sender, ValuePicker.CurrentValueChangedRoutedEventArgs e);

        public class CurrentValueChangedRoutedEventArgs : RoutedEventArgs
        {
            private int newValue;
            private int oldValue;

            public CurrentValueChangedRoutedEventArgs(RoutedEvent routedEvent) : base(routedEvent)
            {
            }

            public int NewValue
            {
                get => 
                    this.newValue;
                set
                {
                    this.newValue = value;
                }
            }

            public int OldValue
            {
                get => 
                    this.oldValue;
                set
                {
                    this.oldValue = value;
                }
            }
        }
    }
}

