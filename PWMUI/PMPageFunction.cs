namespace PWMUI
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Navigation;

    public class PMPageFunction : PageFunction<CreatePlanWizardResult>
    {
        public PMPageFunction()
        {
            base.Focusable = true;
            base.Loaded += new RoutedEventHandler(this.PMPageFunction_Loaded);
        }

        private void PMPageFunction_Loaded(object sender, RoutedEventArgs e)
        {
            this.SetFirstFocus();
        }

        protected void SetFirstFocus()
        {
            Keyboard.Focus(this);
            FocusNavigationDirection next = FocusNavigationDirection.Next;
            TraversalRequest request = new TraversalRequest(next);
            this.MoveFocus(request);
        }
    }
}

