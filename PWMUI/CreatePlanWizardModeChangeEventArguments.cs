namespace PWMUI
{
    using System;

    public class CreatePlanWizardModeChangeEventArguments
    {
        private CreatePlanWizardMode mode;

        public CreatePlanWizardModeChangeEventArguments(CreatePlanWizardMode mode)
        {
            this.mode = mode;
        }

        public CreatePlanWizardMode Mode =>
            this.mode;
    }
}

