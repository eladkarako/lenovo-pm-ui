namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public class PowerUseSlider : Slider, IPMControl, IDisposable
    {
        protected bool Disposed;
        private double dragStartPosition;
        public bool IgnoreValueChangeEvent;
        private PowerUseInformer informer = new PowerUseInformer();
        private bool isDragging;
        private bool raisePositionChangeEvent = true;
        public static readonly DependencyProperty ThumbHeightProperty = DependencyProperty.Register("ThumbHeight", typeof(double), typeof(PowerUseSlider));
        public static readonly DependencyProperty ThumbWidthProperty = DependencyProperty.Register("ThumbWidth", typeof(double), typeof(PowerUseSlider));

        public event SliderPositionChangeEventHandler SliderPositionChangeEvent;

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
            }
            this.Disposed = true;
        }

        public void EnableApplyButton()
        {
            MainWindow.Instance.EnableApplyButton();
        }

        ~PowerUseSlider()
        {
            this.Dispose(false);
        }

        protected override void OnDecreaseLarge()
        {
            base.OnDecreaseLarge();
        }

        protected override void OnDecreaseSmall()
        {
            base.OnDecreaseSmall();
        }

        protected override void OnIncreaseLarge()
        {
            base.OnIncreaseLarge();
        }

        protected override void OnIncreaseSmall()
        {
            base.OnIncreaseSmall();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.slider_ValueChanged);
            base.OnInitialized(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
        }

        protected override void OnThumbDragCompleted(DragCompletedEventArgs e)
        {
            this.raisePositionChangeEvent = true;
            this.isDragging = false;
            this.informer.TerminateSliderSimulation();
            if (this.SliderPositionChangeEvent != null)
            {
                this.SliderPositionChangeEvent();
            }
            base.OnThumbDragCompleted(e);
        }

        protected override void OnThumbDragDelta(DragDeltaEventArgs e)
        {
            if (((base.Value == base.Minimum) || (base.Value == base.Maximum)) || (Math.Abs((double) (this.dragStartPosition - base.Value)) >= 1.0))
            {
                this.dragStartPosition = base.Value;
                this.informer.PerformSliderSimulation((uint) this.dragStartPosition);
            }
            base.OnThumbDragDelta(e);
        }

        protected override void OnThumbDragStarted(DragStartedEventArgs e)
        {
            this.raisePositionChangeEvent = false;
            this.isDragging = true;
            this.dragStartPosition = base.Value;
            this.informer.PrepareForSliderSimulation();
            base.OnThumbDragStarted(e);
        }

        private void slider_ValueChanged(object sender, RoutedEventArgs e)
        {
            if (base.IsVisible)
            {
                if (this.IgnoreValueChangeEvent)
                {
                    this.IgnoreValueChangeEvent = false;
                }
                else
                {
                    if (this.raisePositionChangeEvent && (this.SliderPositionChangeEvent != null))
                    {
                        this.SliderPositionChangeEvent();
                    }
                    this.EnableApplyButton();
                }
            }
        }

        public bool IsDragging =>
            this.isDragging;

        public double ThumbHeight
        {
            get => 
                ((double) base.GetValue(ThumbHeightProperty));
            set
            {
                base.SetValue(ThumbHeightProperty, value);
            }
        }

        public double ThumbWidth
        {
            get => 
                ((double) base.GetValue(ThumbWidthProperty));
            set
            {
                base.SetValue(ThumbWidthProperty, value);
            }
        }
    }
}

