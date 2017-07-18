namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    internal class EventsPanel : StackPanel, IPlanSettingPlane, IDisposable
    {
        protected bool Disposed;
        private FnF4Item fnF4Item = new FnF4Item();
        internal ComboBoxPlanItem fnF4PlanItem = new ComboBoxPlanItem();
        public const int IDOK = 1;
        private bool ignoreSelectionChanged;
        private List<IPlanItem> items = new List<IPlanItem>();
        private LidCloseItem lidClosedItem = new LidCloseItem();
        internal ComboBoxPlanItem lidClosedPlanItem = new ComboBoxPlanItem();
        private PowerButtonItem powerButtonItem = new PowerButtonItem();
        internal ComboBoxPlanItem powerButtonPlanItem = new ComboBoxPlanItem();
        private ReqPassItem reqPassItem = new ReqPassItem();
        internal ComboBoxPlanItem reqPassPlanItem = new ComboBoxPlanItem();
        private StartMenuItem startMenuItem = new StartMenuItem();
        internal ComboBoxPlanItem startMenuPlanItem = new ComboBoxPlanItem();

        public EventsPanel()
        {
            this.items.Add(this.reqPassPlanItem);
            this.items.Add(this.fnF4PlanItem);
            this.items.Add(this.powerButtonPlanItem);
            this.items.Add(this.lidClosedPlanItem);
            this.items.Add(this.startMenuPlanItem);
            this.reqPassPlanItem.settingAC.SelectionChanged += new SelectionChangedEventHandler(this.RegPass_SelectionChanged);
            this.reqPassPlanItem.settingDC.SelectionChanged += new SelectionChangedEventHandler(this.RegPass_SelectionChanged);
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
            Events myEvents = activePlan.MyEvents;
            myEvents.ValueOfReqPassAC = this.reqPassPlanItem.GetCurrentAC();
            myEvents.ValueOfReqPassDC = this.reqPassPlanItem.GetCurrentDC();
            myEvents.ValueOfFnf4AC = this.fnF4PlanItem.GetCurrentAC();
            myEvents.ValueOfFnf4DC = this.fnF4PlanItem.GetCurrentDC();
            myEvents.ValueOfPowerButtonAC = this.powerButtonPlanItem.GetCurrentAC();
            myEvents.ValueOfPowerButtonDC = this.powerButtonPlanItem.GetCurrentDC();
            myEvents.ValueOfLidCloseAC = this.lidClosedPlanItem.GetCurrentAC();
            myEvents.ValueOfLidCloseDC = this.lidClosedPlanItem.GetCurrentDC();
            myEvents.ValueOfStartMenuAC = this.startMenuPlanItem.GetCurrentAC();
            myEvents.ValueOfStartMenuDC = this.startMenuPlanItem.GetCurrentDC();
            myEvents.ApplyEventsToAllPlansFromUI = MainWindow.Instance.IsApplyEventsToAllPlansChecked;
        }

        public void Create(WizardPowerPlan powerPlan)
        {
            this.reqPassPlanItem.SetRelatedItem(this.reqPassItem);
            this.reqPassPlanItem.SetTitle("TitleReqPass");
            this.fnF4PlanItem.SetRelatedItem(this.fnF4Item);
            this.fnF4PlanItem.SetTitle("TitleFnF4");
            this.powerButtonPlanItem.SetRelatedItem(this.powerButtonItem);
            this.powerButtonPlanItem.SetTitle("TitlePowerButton");
            this.lidClosedPlanItem.SetRelatedItem(this.lidClosedItem);
            this.lidClosedPlanItem.SetTitle("TitleLidClosed");
            this.startMenuPlanItem.SetRelatedItem(this.startMenuItem);
            this.startMenuPlanItem.SetTitle("TitleStartMenu");
            bool expflag = powerPlan.IsExport();
            if (this.fnF4Item.IsAssignedFnF1)
            {
                this.fnF4PlanItem.SetTitle("TitleFnF1OfSleepButton");
            }
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

        ~EventsPanel()
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
            Events myEvents = activePlan.MyEvents;
            if (this.reqPassPlanItem.GetCurrentAC() != myEvents.ValueOfReqPassAC)
            {
                this.ignoreSelectionChanged = true;
                this.reqPassPlanItem.SetCurrentAC(myEvents.ValueOfReqPassAC);
            }
            if (this.reqPassPlanItem.GetCurrentDC() != myEvents.ValueOfReqPassDC)
            {
                this.ignoreSelectionChanged = true;
                this.reqPassPlanItem.SetCurrentDC(myEvents.ValueOfReqPassDC);
            }
            this.fnF4PlanItem.SetCurrentAC(myEvents.ValueOfFnf4AC);
            this.fnF4PlanItem.SetCurrentDC(myEvents.ValueOfFnf4DC);
            this.powerButtonPlanItem.SetCurrentAC(myEvents.ValueOfPowerButtonAC);
            this.powerButtonPlanItem.SetCurrentDC(myEvents.ValueOfPowerButtonDC);
            this.lidClosedPlanItem.SetCurrentAC(myEvents.ValueOfLidCloseAC);
            this.lidClosedPlanItem.SetCurrentDC(myEvents.ValueOfLidCloseDC);
            this.startMenuPlanItem.SetCurrentAC(myEvents.ValueOfStartMenuAC);
            this.startMenuPlanItem.SetCurrentDC(myEvents.ValueOfStartMenuDC);
        }

        private void RegPass_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ignoreSelectionChanged)
            {
                this.ignoreSelectionChanged = false;
            }
            else if (base.IsVisible && !this.reqPassItem.CanAccess())
            {
                string mainProgram = "PWMUI.exe";
                Resource resource = new Resource();
                if (resource.ShowElevationDialog(mainProgram) == 1)
                {
                    MainWindow.Instance.Close();
                }
                else
                {
                    ComboBox originalSource = e.OriginalSource as ComboBox;
                    this.ignoreSelectionChanged = true;
                    originalSource.SelectedValue = e.RemovedItems[0];
                }
            }
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

