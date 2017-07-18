namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Windows.Controls;

    internal class PlanNamePanel : StackPanel
    {
        private PlanNamePanelItem activeItem;
        private PowerPlan currentActivePlan;
        private bool isNotChangeCurrentActivePlan;

        public void AddNewPlan(PowerPlanInformer informer)
        {
            informer.Refresh();
            foreach (string str in informer.GetAllPlanName())
            {
                if (!this.PlanNameIsExist(str))
                {
                    PlanNamePanelItem element = new PlanNamePanelItem(informer.GetPlanFromName(str));
                    element.SelectedPowerPlanEvent += new SelectedPowerPlanEventHandler(this.planNameItem_SelectedPowerPlanEvent);
                    base.Children.Add(element);
                }
            }
        }

        public void Apply(ref PowerPlan activePlan)
        {
            this.activeItem.Apply(ref activePlan);
            this.currentActivePlan = activePlan;
        }

        public void Create(PowerPlanInformer informer)
        {
            MainWindow.Instance.SettingChangeWasCanceledEvent += new SettingChangeWasCanceledEventHandler(this.SettingChangeWasCanceledEvent);
            base.Children.Clear();
            foreach (string str in informer.GetAllPlanName())
            {
                PlanNamePanelItem element = new PlanNamePanelItem(informer.GetPlanFromName(str));
                element.SelectedPowerPlanEvent += new SelectedPowerPlanEventHandler(this.planNameItem_SelectedPowerPlanEvent);
                base.Children.Add(element);
            }
            this.currentActivePlan = informer.GetActivePlan();
        }

        public void DeletePlan(PowerPlan plan)
        {
            foreach (PlanNamePanelItem item in base.Children)
            {
                if (item.planNameBlock.Text == plan.Name)
                {
                    base.Children.Remove(item);
                    break;
                }
            }
        }

        private bool PlanNameIsExist(string name)
        {
            foreach (PlanNamePanelItem item in base.Children)
            {
                if (item.planNameBlock.Text == name)
                {
                    return true;
                }
            }
            return false;
        }

        private void planNameItem_SelectedPowerPlanEvent()
        {
            this.isNotChangeCurrentActivePlan = true;
        }

        public void SetActivePlan(PowerPlan activePlan)
        {
            foreach (PlanNamePanelItem item in base.Children)
            {
                if (item.planNameBlock.Text == activePlan.Name)
                {
                    item.SetActiveBackground();
                    this.activeItem = item;
                }
                else
                {
                    item.SetDefaultBackground();
                }
            }
            if (this.isNotChangeCurrentActivePlan)
            {
                this.isNotChangeCurrentActivePlan = false;
            }
            else
            {
                this.currentActivePlan = activePlan;
            }
        }

        private void SettingChangeWasCanceledEvent()
        {
            this.currentActivePlan.SetActive();
        }
    }
}

