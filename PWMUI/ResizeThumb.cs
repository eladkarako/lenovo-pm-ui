namespace PWMUI
{
    using System;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls.Primitives;
    using System.Windows.Forms;
    using System.Windows.Input;

    public class ResizeThumb : Thumb
    {
        public static readonly DependencyProperty PositionProperty = DependencyProperty.Register("Position", typeof(Positions), typeof(ResizeThumb));
        private System.Windows.Input.Cursor TargetWindowCursor;
        public static readonly DependencyProperty TargetWindowProperty = DependencyProperty.Register("TargetWindow", typeof(Window), typeof(ResizeThumb));

        private System.Windows.Input.Cursor GetMyCursor()
        {
            if (this.TargetWindow.WindowState == WindowState.Maximized)
            {
                return this.TargetWindowCursor;
            }
            if (this.Position == Positions.Left)
            {
                return System.Windows.Input.Cursors.SizeWE;
            }
            if (this.Position == Positions.Right)
            {
                return System.Windows.Input.Cursors.SizeWE;
            }
            if (this.Position == Positions.Top)
            {
                return System.Windows.Input.Cursors.SizeNS;
            }
            if (this.Position == Positions.Bottom)
            {
                return System.Windows.Input.Cursors.SizeNS;
            }
            if (this.Position == Positions.LeftTop)
            {
                return System.Windows.Input.Cursors.SizeNWSE;
            }
            if (this.Position == Positions.RightBottom)
            {
                return System.Windows.Input.Cursors.SizeNWSE;
            }
            if (this.Position == Positions.LeftBottom)
            {
                return System.Windows.Input.Cursors.SizeNESW;
            }
            if (this.Position == Positions.RightTop)
            {
                return System.Windows.Input.Cursors.SizeNESW;
            }
            return System.Windows.Input.Cursors.None;
        }

        protected override AutomationPeer OnCreateAutomationPeer() => 
            new PMResizeThumbPeer(this);

        protected override void OnInitialized(EventArgs e)
        {
            base.DragStarted += new DragStartedEventHandler(this.ResizeThumb_DragStarted);
            base.DragCompleted += new DragCompletedEventHandler(this.ResizeThumb_DragCompleted);
            base.DragDelta += new DragDeltaEventHandler(this.ResizeThumb_DragDelta);
            if (this.TargetWindow != null)
            {
                this.TargetWindowCursor = this.TargetWindow.Cursor;
                base.Cursor = this.GetMyCursor();
                this.TargetWindow.StateChanged += new EventHandler(this.TargetWindow_StateChanged);
            }
            base.OnInitialized(e);
        }

        private void ResizeOfBottom(DragDeltaEventArgs e)
        {
            double minHeight = this.TargetWindow.Height + e.VerticalChange;
            if (minHeight < this.TargetWindow.MinHeight)
            {
                minHeight = this.TargetWindow.MinHeight;
            }
            if (((this.TargetWindow.Top + minHeight) > SystemParameters.WorkArea.Bottom) && (Screen.AllScreens.Length == 1))
            {
                minHeight = SystemParameters.WorkArea.Bottom - this.TargetWindow.Top;
            }
            this.TargetWindow.Height = minHeight;
        }

        private void ResizeOfLeft(DragDeltaEventArgs e)
        {
            double minWidth = this.TargetWindow.Width - e.HorizontalChange;
            double left = this.TargetWindow.Left + e.HorizontalChange;
            if ((left < SystemParameters.WorkArea.Left) && (Screen.AllScreens.Length == 1))
            {
                left = SystemParameters.WorkArea.Left;
                minWidth = (this.TargetWindow.Left - SystemParameters.WorkArea.Left) + this.TargetWindow.Width;
            }
            if (minWidth < this.TargetWindow.MinWidth)
            {
                left = (this.TargetWindow.Width - this.TargetWindow.MinWidth) + this.TargetWindow.Left;
                minWidth = this.TargetWindow.MinWidth;
            }
            this.TargetWindow.Width = minWidth;
            this.TargetWindow.Left = left;
        }

        private void ResizeOfLeftBottom(DragDeltaEventArgs e)
        {
            this.ResizeOfLeft(e);
            this.ResizeOfBottom(e);
        }

        private void ResizeOfLeftTop(DragDeltaEventArgs e)
        {
            this.ResizeOfLeft(e);
            this.ResizeOfTop(e);
        }

        private void ResizeOfRight(DragDeltaEventArgs e)
        {
            double minWidth = this.TargetWindow.Width + e.HorizontalChange;
            if (minWidth < this.TargetWindow.MinWidth)
            {
                minWidth = this.TargetWindow.MinWidth;
            }
            if (((this.TargetWindow.Left + minWidth) > SystemParameters.WorkArea.Right) && (Screen.AllScreens.Length == 1))
            {
                minWidth = SystemParameters.WorkArea.Right - this.TargetWindow.Left;
            }
            this.TargetWindow.Width = minWidth;
        }

        private void ResizeOfRightBottom(DragDeltaEventArgs e)
        {
            this.ResizeOfRight(e);
            this.ResizeOfBottom(e);
        }

        private void ResizeOfRightTop(DragDeltaEventArgs e)
        {
            this.ResizeOfRight(e);
            this.ResizeOfTop(e);
        }

        private void ResizeOfTop(DragDeltaEventArgs e)
        {
            double minHeight = this.TargetWindow.Height - e.VerticalChange;
            double top = this.TargetWindow.Top + e.VerticalChange;
            if ((top < SystemParameters.WorkArea.Top) && (Screen.AllScreens.Length == 1))
            {
                top = SystemParameters.WorkArea.Top;
                minHeight = (this.TargetWindow.Top - SystemParameters.WorkArea.Top) + this.TargetWindow.Height;
            }
            if (minHeight < this.TargetWindow.MinHeight)
            {
                top = (this.TargetWindow.Height - this.TargetWindow.MinHeight) + this.TargetWindow.Top;
                minHeight = this.TargetWindow.MinHeight;
            }
            this.TargetWindow.Height = minHeight;
            this.TargetWindow.Top = top;
        }

        private void ResizeThumb_DragCompleted(object sender, DragCompletedEventArgs e)
        {
            this.TargetWindow.Cursor = this.TargetWindowCursor;
        }

        private void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (this.TargetWindow.WindowState != WindowState.Maximized)
            {
                if (this.Position == Positions.Left)
                {
                    this.ResizeOfLeft(e);
                }
                if (this.Position == Positions.Right)
                {
                    this.ResizeOfRight(e);
                }
                if (this.Position == Positions.Top)
                {
                    this.ResizeOfTop(e);
                }
                if (this.Position == Positions.Bottom)
                {
                    this.ResizeOfBottom(e);
                }
                if (this.Position == Positions.LeftTop)
                {
                    this.ResizeOfLeftTop(e);
                }
                if (this.Position == Positions.LeftBottom)
                {
                    this.ResizeOfLeftBottom(e);
                }
                if (this.Position == Positions.RightTop)
                {
                    this.ResizeOfRightTop(e);
                }
                if (this.Position == Positions.RightBottom)
                {
                    this.ResizeOfRightBottom(e);
                }
            }
        }

        private void ResizeThumb_DragStarted(object sender, DragStartedEventArgs e)
        {
            this.TargetWindow.Cursor = base.Cursor;
        }

        private void TargetWindow_StateChanged(object sender, EventArgs e)
        {
            base.Cursor = this.GetMyCursor();
        }

        public Positions Position
        {
            get => 
                ((Positions) base.GetValue(PositionProperty));
            set
            {
                base.SetValue(PositionProperty, value);
            }
        }

        public Window TargetWindow
        {
            get => 
                (base.GetValue(TargetWindowProperty) as Window);
            set
            {
                base.SetValue(TargetWindowProperty, value);
            }
        }
    }
}

