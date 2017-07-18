namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    internal class AdvancedSettingsPanel : StackPanel, IPlanSettingPlane, IDisposable
    {
        private AdaptiveDisplayItem adaptiveDisplayItem = new AdaptiveDisplayItem();
        internal ComboBoxPlanItem adaptiveDisplayPlanItem = new ComboBoxPlanItem();
        private AllowRTCWakeItem allowWakeTimersItem = new AllowRTCWakeItem();
        internal ComboBoxPlanItem allowWakeTimersPlanItem = new ComboBoxPlanItem();
        internal TitlePlanItem desktopPlanItem = new TitlePlanItem();
        internal TitlePlanItem displayPlanItem = new TitlePlanItem();
        protected bool Disposed;
        private HybridSleepItem hybridsleepItem = new HybridSleepItem();
        internal ComboBoxPlanItem hybridsleepPlanItem = new ComboBoxPlanItem();
        private List<IPlanItem> items = new List<IPlanItem>();
        private LinkStateItem linkStateItem = new LinkStateItem();
        internal ComboBoxPlanItem linkStatePlanItem = new ComboBoxPlanItem();
        internal TitlePlanItem multimediaPlanItem = new TitlePlanItem();
        internal TitlePlanItem pciExpressPlanItem = new TitlePlanItem();
        private PlayingVideoItem playingVideoItem = new PlayingVideoItem();
        internal ComboBoxPlanItem playingVideoPlanItem = new ComboBoxPlanItem();
        internal TitlePlanItem processorPlanItem = new TitlePlanItem();
        internal TitlePlanItem searchAndIndexingPlanItem = new TitlePlanItem();
        private SearchPowerSavingModeItem searchPowerSavingModeItem = new SearchPowerSavingModeItem();
        internal ComboBoxPlanItem searchPowerSavingModePlanItem = new ComboBoxPlanItem();
        private SharingMediaItem sharingMediaItem = new SharingMediaItem();
        internal ComboBoxPlanItem sharingMediaPlanItem = new ComboBoxPlanItem();
        internal TitlePlanItem sleepPlanItem = new TitlePlanItem();
        private SlideShowItem slideShowItem = new SlideShowItem();
        internal ComboBoxPlanItem slideShowPlanItem = new ComboBoxPlanItem();
        private SystemCoolingPolicyItem systemCoolingPolicyItem = new SystemCoolingPolicyItem();
        internal ComboBoxPlanItem systemCoolingPolicyPlanItem = new ComboBoxPlanItem();
        internal TitlePlanItem usbPlanItem = new TitlePlanItem();
        private UsbSelectiveSuspendItem usbSelectiveSuspendItem = new UsbSelectiveSuspendItem();
        internal ComboBoxPlanItem usbSelectiveSuspendPlanItem = new ComboBoxPlanItem();
        internal TitlePlanItem wirelessPlanItem = new TitlePlanItem();
        private WirelessPowerSavingModeItem wirelessPowerSavingModeItem = new WirelessPowerSavingModeItem();
        internal ComboBoxPlanItem wirelessPowerSavingModePlanItem = new ComboBoxPlanItem();

        public AdvancedSettingsPanel()
        {
            this.items.Add(this.sleepPlanItem);
            this.items.Add(this.hybridsleepPlanItem);
            this.items.Add(this.allowWakeTimersPlanItem);
            this.items.Add(this.wirelessPlanItem);
            this.items.Add(this.wirelessPowerSavingModePlanItem);
            this.items.Add(this.pciExpressPlanItem);
            this.items.Add(this.linkStatePlanItem);
            this.items.Add(this.multimediaPlanItem);
            this.items.Add(this.sharingMediaPlanItem);
            this.items.Add(this.playingVideoPlanItem);
            this.items.Add(this.displayPlanItem);
            this.items.Add(this.adaptiveDisplayPlanItem);
            this.items.Add(this.searchAndIndexingPlanItem);
            this.items.Add(this.searchPowerSavingModePlanItem);
            this.items.Add(this.usbPlanItem);
            this.items.Add(this.usbSelectiveSuspendPlanItem);
            this.items.Add(this.desktopPlanItem);
            this.items.Add(this.slideShowPlanItem);
            this.items.Add(this.processorPlanItem);
            this.items.Add(this.systemCoolingPolicyPlanItem);
        }

        private void AddPlanItem(IPlanItem planItem, bool expflag)
        {
            if (!expflag)
            {
                if (!planItem.IsCapable())
                {
                    return;
                }
                planItem.HideTitle2();
            }
            else if (!planItem.IsCapable())
            {
                if (!planItem.CanShowExport())
                {
                    return;
                }
                planItem.ShowTitle2();
            }
            else
            {
                planItem.HideTitle2();
            }
            if ((base.Children.Count % 2) == 0)
            {
                planItem.SetBackgroundToGray();
            }
            base.Children.Add((UIElement) planItem);
        }

        public void Apply(ref PowerPlan activePlan)
        {
            AdvancedSettings myAdvancedSettings = activePlan.MyAdvancedSettings;
            myAdvancedSettings.ValueOfHibridSleepAC = this.hybridsleepPlanItem.GetCurrentAC();
            myAdvancedSettings.ValueOfHibridSleepDC = this.hybridsleepPlanItem.GetCurrentDC();
            myAdvancedSettings.ValueOfAllowWakeTimersAC = this.allowWakeTimersPlanItem.GetCurrentAC();
            myAdvancedSettings.ValueOfAllowWakeTimersDC = this.allowWakeTimersPlanItem.GetCurrentDC();
            myAdvancedSettings.ValueOfWirelessPowerSavingMondeAC = this.wirelessPowerSavingModePlanItem.GetCurrentAC();
            myAdvancedSettings.ValueOfWirelessPowerSavingMondeDC = this.wirelessPowerSavingModePlanItem.GetCurrentDC();
            myAdvancedSettings.ValueOfLinkStateAC = this.linkStatePlanItem.GetCurrentAC();
            myAdvancedSettings.ValueOfLinkStateDC = this.linkStatePlanItem.GetCurrentDC();
            myAdvancedSettings.ValueOfSharingMediaAC = this.sharingMediaPlanItem.GetCurrentAC();
            myAdvancedSettings.ValueOfSharingMediaDC = this.sharingMediaPlanItem.GetCurrentDC();
            myAdvancedSettings.ValueOfVideoPlayingAC = this.playingVideoPlanItem.GetCurrentAC();
            myAdvancedSettings.ValueOfVideoPlayingDC = this.playingVideoPlanItem.GetCurrentDC();
            myAdvancedSettings.ValueOfAdaptiveDisplayAC = this.adaptiveDisplayPlanItem.GetCurrentAC();
            myAdvancedSettings.ValueOfAdaptiveDisplayDC = this.adaptiveDisplayPlanItem.GetCurrentDC();
            myAdvancedSettings.ValueOfSearchAndSavingModeAC = this.searchPowerSavingModePlanItem.GetCurrentAC();
            myAdvancedSettings.ValueOfSearchAndSavingModeDC = this.searchPowerSavingModePlanItem.GetCurrentDC();
            myAdvancedSettings.ValueOfUsbSelectiveSuspendAC = this.usbSelectiveSuspendPlanItem.GetCurrentAC();
            myAdvancedSettings.ValueOfUsbSelectiveSuspendDC = this.usbSelectiveSuspendPlanItem.GetCurrentDC();
            myAdvancedSettings.ValueOfSlideShowAC = this.slideShowPlanItem.GetCurrentAC();
            myAdvancedSettings.ValueOfSlideShowDC = this.slideShowPlanItem.GetCurrentDC();
            myAdvancedSettings.ValueOfSystemCoolingPolicyAC = this.systemCoolingPolicyPlanItem.GetCurrentAC();
            myAdvancedSettings.ValueOfSystemCoolingPolicyDC = this.systemCoolingPolicyPlanItem.GetCurrentDC();
        }

        public void Create(WizardPowerPlan powerPlan)
        {
            this.sleepPlanItem.SetRelatedItem(this.hybridsleepItem);
            this.sleepPlanItem.SetRelatedItem(this.allowWakeTimersItem);
            this.sleepPlanItem.SetTitle("TitleSleep");
            this.hybridsleepPlanItem.SetRelatedItem(this.hybridsleepItem);
            this.hybridsleepPlanItem.SetTitle("TitleHybridSleep");
            this.allowWakeTimersPlanItem.SetRelatedItem(this.allowWakeTimersItem);
            this.allowWakeTimersPlanItem.SetTitle("TitleAllowWakeTimers");
            this.wirelessPlanItem.SetRelatedItem(this.wirelessPowerSavingModeItem);
            this.wirelessPlanItem.SetTitle("TitleWireless");
            this.wirelessPowerSavingModePlanItem.SetRelatedItem(this.wirelessPowerSavingModeItem);
            this.wirelessPowerSavingModePlanItem.SetTitle("TitleWirelessPowerSavingMode");
            this.pciExpressPlanItem.SetRelatedItem(this.linkStateItem);
            this.pciExpressPlanItem.SetTitle("TitlePCIExpress");
            this.linkStatePlanItem.SetRelatedItem(this.linkStateItem);
            this.linkStatePlanItem.SetTitle("TitleLinkState");
            this.multimediaPlanItem.SetRelatedItem(this.sharingMediaItem);
            this.multimediaPlanItem.SetRelatedItem(this.playingVideoItem);
            this.multimediaPlanItem.SetTitle("TitleMultimedia");
            this.sharingMediaPlanItem.SetRelatedItem(this.sharingMediaItem);
            this.sharingMediaPlanItem.SetTitle("TitleSharingMedia");
            this.playingVideoPlanItem.SetRelatedItem(this.playingVideoItem);
            this.playingVideoPlanItem.SetTitle("TitlePlayingVideo");
            this.displayPlanItem.SetRelatedItem(this.adaptiveDisplayItem);
            this.displayPlanItem.SetTitle("TitleDisplay");
            this.adaptiveDisplayPlanItem.SetRelatedItem(this.adaptiveDisplayItem);
            this.adaptiveDisplayPlanItem.SetTitle("TitleAdaptiveDisplay");
            this.searchAndIndexingPlanItem.SetRelatedItem(this.searchPowerSavingModeItem);
            this.searchAndIndexingPlanItem.SetTitle("TitleSearchAndIndexing");
            this.searchPowerSavingModePlanItem.SetRelatedItem(this.searchPowerSavingModeItem);
            this.searchPowerSavingModePlanItem.SetTitle("TitleSearchPowerSavingMode");
            this.usbPlanItem.SetRelatedItem(this.usbSelectiveSuspendItem);
            this.usbPlanItem.SetTitle("TitleUsb");
            this.usbSelectiveSuspendPlanItem.SetRelatedItem(this.usbSelectiveSuspendItem);
            this.usbSelectiveSuspendPlanItem.SetTitle("TitleUsbSelectiveSuspend");
            this.desktopPlanItem.SetTitle("TitleDesktop");
            this.desktopPlanItem.SetRelatedItem(this.slideShowItem);
            this.slideShowPlanItem.SetTitle("TitleSlideShow");
            this.slideShowPlanItem.SetRelatedItem(this.slideShowItem);
            this.processorPlanItem.SetTitle("TitleProcessor");
            this.processorPlanItem.SetRelatedItem(this.systemCoolingPolicyItem);
            this.systemCoolingPolicyPlanItem.SetTitle("TitleSystemCoolingPolicy");
            this.systemCoolingPolicyPlanItem.SetRelatedItem(this.systemCoolingPolicyItem);
            bool expflag = powerPlan.IsExport();
            foreach (IPlanItem item in this.items)
            {
                this.AddPlanItem(item, expflag);
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
            }
            this.Disposed = true;
        }

        ~AdvancedSettingsPanel()
        {
            this.Dispose(false);
        }

        public void HideSettingsItem()
        {
            foreach (IPlanItem item in this.items)
            {
                item.HideSettingsItem();
            }
        }

        public void Refresh(PowerPlan activePlan)
        {
            AdvancedSettings myAdvancedSettings = activePlan.MyAdvancedSettings;
            this.hybridsleepPlanItem.SetCurrentAC(myAdvancedSettings.ValueOfHibridSleepAC);
            this.hybridsleepPlanItem.SetCurrentDC(myAdvancedSettings.ValueOfHibridSleepDC);
            this.allowWakeTimersPlanItem.SetCurrentAC(myAdvancedSettings.ValueOfAllowWakeTimersAC);
            this.allowWakeTimersPlanItem.SetCurrentDC(myAdvancedSettings.ValueOfAllowWakeTimersDC);
            this.wirelessPowerSavingModePlanItem.SetCurrentAC(myAdvancedSettings.ValueOfWirelessPowerSavingMondeAC);
            this.wirelessPowerSavingModePlanItem.SetCurrentDC(myAdvancedSettings.ValueOfWirelessPowerSavingMondeDC);
            this.linkStatePlanItem.SetCurrentAC(myAdvancedSettings.ValueOfLinkStateAC);
            this.linkStatePlanItem.SetCurrentDC(myAdvancedSettings.ValueOfLinkStateDC);
            this.sharingMediaPlanItem.SetCurrentAC(myAdvancedSettings.ValueOfSharingMediaAC);
            this.sharingMediaPlanItem.SetCurrentDC(myAdvancedSettings.ValueOfSharingMediaDC);
            this.playingVideoPlanItem.SetCurrentAC(myAdvancedSettings.ValueOfVideoPlayingAC);
            this.playingVideoPlanItem.SetCurrentDC(myAdvancedSettings.ValueOfVideoPlayingDC);
            this.adaptiveDisplayPlanItem.SetCurrentAC(myAdvancedSettings.ValueOfAdaptiveDisplayAC);
            this.adaptiveDisplayPlanItem.SetCurrentDC(myAdvancedSettings.ValueOfAdaptiveDisplayDC);
            this.searchPowerSavingModePlanItem.SetCurrentAC(myAdvancedSettings.ValueOfSearchAndSavingModeAC);
            this.searchPowerSavingModePlanItem.SetCurrentDC(myAdvancedSettings.ValueOfSearchAndSavingModeDC);
            this.usbSelectiveSuspendPlanItem.SetCurrentAC(myAdvancedSettings.ValueOfUsbSelectiveSuspendAC);
            this.usbSelectiveSuspendPlanItem.SetCurrentDC(myAdvancedSettings.ValueOfUsbSelectiveSuspendDC);
            this.slideShowPlanItem.SetCurrentAC(myAdvancedSettings.ValueOfSlideShowAC);
            this.slideShowPlanItem.SetCurrentDC(myAdvancedSettings.ValueOfSlideShowDC);
            this.systemCoolingPolicyPlanItem.SetCurrentAC(myAdvancedSettings.ValueOfSystemCoolingPolicyAC);
            this.systemCoolingPolicyPlanItem.SetCurrentDC(myAdvancedSettings.ValueOfSystemCoolingPolicyDC);
        }

        public void ShowSettingsItem()
        {
            foreach (IPlanItem item in this.items)
            {
                item.ShowSettingsItem();
            }
        }
    }
}

