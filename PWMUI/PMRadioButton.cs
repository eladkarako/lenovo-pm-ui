namespace PWMUI
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class PMRadioButton : RadioButton, IPMControl
    {
        public void EnableApplyButton()
        {
            MainWindow.Instance.EnableApplyButton();
        }

        private void NotifyMainWindow(object sender, RoutedEventArgs e)
        {
            if (base.IsVisible)
            {
                this.EnableApplyButton();
            }
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.Click += new RoutedEventHandler(this.NotifyMainWindow);
            base.OnInitialized(e);
        }
    }
}

