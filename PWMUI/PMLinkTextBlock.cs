namespace PWMUI
{
    using System;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class PMLinkTextBlock : TextBlock
    {
        private bool isUnderLineInMouseOver = true;
        private TextDecoration underLine = new TextDecoration();

        public event LinkTextClickedEventHandler LinkTextClickedEvent;

        public PMLinkTextBlock()
        {
            base.Cursor = Cursors.Hand;
            this.underLine.Location = TextDecorationLocation.Underline;
            base.TextDecorations.Clear();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (((e.Key == Key.Space) && base.IsFocused) && (this.LinkTextClickedEvent != null))
            {
                this.LinkTextClickedEvent();
            }
            base.OnKeyUp(e);
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            if (this.IsUnderLineInMouseOver)
            {
                base.TextDecorations.Add(this.underLine);
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            if (this.IsUnderLineInMouseOver)
            {
                base.TextDecorations.Remove(this.underLine);
            }
            base.OnMouseLeave(e);
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            if (this.LinkTextClickedEvent != null)
            {
                this.LinkTextClickedEvent();
            }
            base.OnMouseLeftButtonUp(e);
        }

        public bool IsUnderLineInMouseOver
        {
            get => 
                this.isUnderLineInMouseOver;
            set
            {
                this.isUnderLineInMouseOver = value;
            }
        }
    }
}

