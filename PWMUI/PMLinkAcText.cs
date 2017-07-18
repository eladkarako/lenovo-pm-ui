namespace PWMUI
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;

    public class PMLinkAcText : AccessText
    {
        private bool isUnderLineInMouseOver = true;
        private TextDecoration underLine = new TextDecoration();

        public PMLinkAcText()
        {
            base.Cursor = Cursors.Hand;
            this.underLine.Location = TextDecorationLocation.Underline;
            base.TextDecorations.Clear();
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

