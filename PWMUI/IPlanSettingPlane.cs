namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;

    internal interface IPlanSettingPlane
    {
        void Apply(ref PowerPlan activePlan);
        void Create(WizardPowerPlan activePlan);
        void HideSettingsItem();
        void Refresh(PowerPlan activePlan);
        void ShowSettingsItem();
    }
}

