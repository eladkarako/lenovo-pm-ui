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
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Markup;

    public class PMSpinControl : System.Windows.Controls.UserControl, IPMControl, IComponentConnector
    {
        private bool _contentLoaded;
        private bool canEnableApplyButton = true;
        private static RoutedCommand decreaseCommand;
        internal RepeatButton downBtn;
        private static RoutedCommand increaseCommand;
        internal Grid LayoutRoot;
        private uint maxValue = 100;
        private uint minValue;
        internal TextBox output;
        private ValueWithRange settableValue;
        internal RepeatButton upBtn;
        internal PMSpinControl UserControl;
        public static readonly RoutedEvent ValueChangedEvent = EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<uint>), typeof(PMSpinControl));
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(uint), typeof(PMSpinControl), new FrameworkPropertyMetadata(0, new PropertyChangedCallback(PMSpinControl.OnValueChanged)));

        public event RoutedPropertyChangedEventHandler<uint> ValueChanged
        {
            add
            {
                base.AddHandler(ValueChangedEvent, value);
            }
            remove
            {
                base.RemoveHandler(ValueChangedEvent, value);
            }
        }

        public PMSpinControl()
        {
            this.InitializeComponent();
            InitializeCommands();
            this.upBtn.Command = increaseCommand;
            this.downBtn.Command = decreaseCommand;
        }

        public void EnableApplyButton()
        {
            if (this.canEnableApplyButton)
            {
                MainWindow.Instance.EnableApplyButton();
            }
        }

        private static void InitializeCommands()
        {
            increaseCommand = new RoutedCommand("IncreaseCommand", typeof(PMSpinControl));
            CommandManager.RegisterClassCommandBinding(typeof(PMSpinControl), new CommandBinding(increaseCommand, new ExecutedRoutedEventHandler(PMSpinControl.OnIncreaseCommand)));
            CommandManager.RegisterClassInputBinding(typeof(PMSpinControl), new InputBinding(increaseCommand, new KeyGesture(Key.Up)));
            CommandManager.RegisterClassInputBinding(typeof(TextBox), new InputBinding(increaseCommand, new KeyGesture(Key.Up)));
            decreaseCommand = new RoutedCommand("DecreaseCommand", typeof(PMSpinControl));
            CommandManager.RegisterClassCommandBinding(typeof(PMSpinControl), new CommandBinding(decreaseCommand, new ExecutedRoutedEventHandler(PMSpinControl.OnDecreaseCommand)));
            CommandManager.RegisterClassInputBinding(typeof(PMSpinControl), new InputBinding(decreaseCommand, new KeyGesture(Key.Down)));
            CommandManager.RegisterClassInputBinding(typeof(TextBox), new InputBinding(decreaseCommand, new KeyGesture(Key.Down)));
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/pmspincontrol.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        protected virtual void OnDecrease()
        {
            if (this.Value >= this.minValue)
            {
                this.Value = this.settableValue.DecreaseCurrentValue(this.Value);
            }
        }

        private static void OnDecreaseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            PMSpinControl control = sender as PMSpinControl;
            if (control != null)
            {
                control.OnDecrease();
            }
        }

        protected virtual void OnIncrease()
        {
            if (this.Value < this.maxValue)
            {
                this.Value = this.settableValue.IncreaseCurrentValue(this.Value);
            }
        }

        private static void OnIncreaseCommand(object sender, ExecutedRoutedEventArgs e)
        {
            PMSpinControl control = sender as PMSpinControl;
            if (control != null)
            {
                control.OnIncrease();
            }
        }

        public virtual void OnValueChanged(RoutedPropertyChangedEventArgs<uint> args)
        {
            base.RaiseEvent(args);
            this.output.Text = this.settableValue.ToString(this.Value);
            if (base.IsVisible)
            {
                this.EnableApplyButton();
            }
        }

        private static void OnValueChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            PMSpinControl control = (PMSpinControl) obj;
            RoutedPropertyChangedEventArgs<uint> args2 = new RoutedPropertyChangedEventArgs<uint>((uint) args.OldValue, (uint) args.NewValue, ValueChangedEvent);
            control.OnValueChanged(args2);
        }

        private void output_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        public void SetSettableValue(ValueWithRange settableValue)
        {
            this.settableValue = settableValue;
            this.minValue = settableValue.GetMin();
            this.maxValue = settableValue.GetMax();
            if (this.minValue > this.Value)
            {
                this.Value = this.minValue;
            }
            if (this.maxValue < this.Value)
            {
                this.Value = this.maxValue;
            }
            this.output.Text = settableValue.ToString(this.Value);
        }

        [DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (PMSpinControl) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.output = (TextBox) target;
                    this.output.TextChanged += new TextChangedEventHandler(this.output_TextChanged);
                    return;

                case 4:
                    this.upBtn = (RepeatButton) target;
                    return;

                case 5:
                    this.downBtn = (RepeatButton) target;
                    return;
            }
            this._contentLoaded = true;
        }

        public bool CanEnableApplyButton
        {
            get => 
                this.canEnableApplyButton;
            set
            {
                this.canEnableApplyButton = value;
            }
        }

        public static RoutedCommand DecreaseCommand =>
            decreaseCommand;

        public static RoutedCommand IncreaseCommand =>
            increaseCommand;

        public uint Value
        {
            get => 
                ((uint) base.GetValue(ValueProperty));
            set
            {
                base.SetValue(ValueProperty, value);
                if (this.output != null)
                {
                    AutomationProperties.SetName(this, this.output.Text);
                }
            }
        }
    }
}

