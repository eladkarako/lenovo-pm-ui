namespace PWMUI
{
    using System;
    using System.Threading;
    using System.Windows.Navigation;

    public class CreatePlanWizardStartPage : PMPageFunction
    {
        private CreatePlanWizardStartParam startParam;

        public event CreatePlanWizardModeChangeEventHandler CreatePlanWizardModeChangeEvent;

        public event CreatePlanWizardReturnEventHandler CreatePlanWizardReturnEvent;

        public CreatePlanWizardStartPage(CreatePlanWizardStartParam startParam)
        {
            this.startParam = startParam;
        }

        private void page_CreatePlanWizardModeChangeEvent(object sender, CreatePlanWizardModeChangeEventArguments e)
        {
            if (this.CreatePlanWizardModeChangeEvent != null)
            {
                this.CreatePlanWizardModeChangeEvent(sender, e);
            }
        }

        private void page_Return(object sender, ReturnEventArgs<CreatePlanWizardResult> e)
        {
            if (this.CreatePlanWizardReturnEvent != null)
            {
                this.CreatePlanWizardReturnEvent(this, new CreatePlanWizardReturnEventArguments(e.Result));
            }
            this.OnReturn(null);
        }

        protected override void Start()
        {
            base.Start();
            base.KeepAlive = true;
            SystemSettingsPage root = new SystemSettingsPage(this.startParam);
            root.Return += new ReturnEventHandler<CreatePlanWizardResult>(this.page_Return);
            root.CreatePlanWizardModeChangeEvent += new CreatePlanWizardModeChangeEventHandler(this.page_CreatePlanWizardModeChangeEvent);
            base.NavigationService.Navigate(root);
        }
    }
}

