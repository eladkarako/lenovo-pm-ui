namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;

    public class CreatePlanWizardStartParam
    {
        private WizardStateItem advancedSettingsState;
        private WizardStateItem alarmsState;
        private WizardPowerPlan createPlan;
        private WizardStateItem eventsState;
        private WizardStateItem idleTimersState;
        private WizardStateItem systemSettingsState;

        public WizardStateItem AdvancedSettingsState
        {
            get => 
                this.advancedSettingsState;
            set
            {
                this.advancedSettingsState = value;
            }
        }

        public WizardStateItem AlarmsState
        {
            get => 
                this.alarmsState;
            set
            {
                this.alarmsState = value;
            }
        }

        public WizardPowerPlan CreatePlan
        {
            get => 
                this.createPlan;
            set
            {
                this.createPlan = value;
            }
        }

        public WizardStateItem EventsState
        {
            get => 
                this.eventsState;
            set
            {
                this.eventsState = value;
            }
        }

        public WizardStateItem IdleTimersState
        {
            get => 
                this.idleTimersState;
            set
            {
                this.idleTimersState = value;
            }
        }

        public WizardStateItem SystemSettingsState
        {
            get => 
                this.systemSettingsState;
            set
            {
                this.systemSettingsState = value;
            }
        }
    }
}

