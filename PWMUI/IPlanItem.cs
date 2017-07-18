namespace PWMUI
{
    using System;

    internal interface IPlanItem
    {
        bool CanShowExport();
        void HideSettingsItem();
        void HideTitle2();
        bool IsCapable();
        void SetBackgroundToGray();
        void ShowSettingsItem();
        void ShowTitle2();
    }
}

