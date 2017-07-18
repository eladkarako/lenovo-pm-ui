namespace PWMUI
{
    using System;

    public class CreatePlanWizardReturnEventArguments
    {
        private CreatePlanWizardResult result;

        public CreatePlanWizardReturnEventArguments(CreatePlanWizardResult result)
        {
            this.result = result;
        }

        public CreatePlanWizardResult Result =>
            this.result;
    }
}

