namespace PWMUI
{
    using System;
    using System.Collections.ObjectModel;

    public class PowerAgendaListData
    {
        private PWMUI.ActionCommandLine actioncommandline;
        private string id;
        private bool ischecked;
        private bool isenabled;
        private string name;
        private string ordertime;

        public PowerAgendaListData()
        {
            this.IsEnabled = true;
            this.IsChecked = true;
            this.ID = "";
            this.Name = "";
            this.ActionCommandLine = new PWMUI.ActionCommandLine();
            this.ActionCommandLine.Action = eAction.Sleep;
            this.ActionCommandLine.BrightnessLevel = 0;
            this.ActionCommandLine.ThinkPadBrightnessLevel = 0;
            this.ActionCommandLine.IdleTimerValue = 0;
            this.ActionCommandLine.PowerPlanName = "";
            this.ActionCommandLine.Frequency = eFrequency.Daily;
            this.ActionCommandLine.DayOfWeek = new ObservableCollection<eDayOfWeek>();
            this.ActionCommandLine.StartTime = DateTime.Now;
            this.ActionCommandLine.EndTime = DateTime.Now;
            this.ActionCommandLine.Prenotice = 60;
            this.ActionCommandLine.iBlockWindows = 1;
            this.ActionCommandLine.StartDate = DateTime.Now;
            this.ActionCommandLine.EndDate = DateTime.Now;
            this.ActionCommandLine.RunOnBatteryTime = DateTime.Now;
            this.ActionCommandLine.DisableChargingTime = DateTime.Now;
            this.ActionCommandLine.IsCheckedRemainingBatteryLevel = true;
            this.ActionCommandLine.RemainingBatteryLevel = 10;
            this.ActionCommandLine.IsCheckedMonitoringBatteryUsage = false;
        }

        public PowerAgendaListData(bool ischecked, bool isenabled, string id, string name, string ordertime, PWMUI.ActionCommandLine actioncommandline)
        {
            this.ischecked = ischecked;
            this.isenabled = isenabled;
            this.id = id;
            this.name = name;
            this.ordertime = ordertime;
            this.actioncommandline = actioncommandline;
        }

        public void UpdateWithSettings(PowerAgendaSettings powerAgendaSettings)
        {
            if (powerAgendaSettings != null)
            {
                this.IsChecked = powerAgendaSettings.IsChecked;
                this.IsEnabled = powerAgendaSettings.IsEnabled;
                this.Name = powerAgendaSettings.Name;
                this.OrderTime = powerAgendaSettings.StartTime.ToString("HH:mm");
                this.ActionCommandLine.Action = powerAgendaSettings.Action;
                this.ActionCommandLine.PowerPlanName = powerAgendaSettings.PowerPlanName;
                this.ActionCommandLine.BrightnessLevel = powerAgendaSettings.BrightnessLevel;
                this.ActionCommandLine.ThinkPadBrightnessLevel = powerAgendaSettings.ThinkPadBrightnessLevel;
                this.ActionCommandLine.IdleTimerValue = powerAgendaSettings.IdleTimerValue;
                this.ActionCommandLine.StartTime = powerAgendaSettings.StartTime;
                this.ActionCommandLine.EndTime = powerAgendaSettings.EndTime;
                this.ActionCommandLine.Prenotice = powerAgendaSettings.Prenotice;
                this.ActionCommandLine.Frequency = powerAgendaSettings.Frequency;
                this.ActionCommandLine.DayOfWeek = powerAgendaSettings.DayOfWeek;
                this.ActionCommandLine.iBlockWindows = powerAgendaSettings.iBlockWindows;
                this.ActionCommandLine.StartDate = powerAgendaSettings.StartDate;
                this.ActionCommandLine.EndDate = powerAgendaSettings.EndDate;
                this.ActionCommandLine.RunOnBatteryTime = powerAgendaSettings.RunOnBatteryTime;
                this.ActionCommandLine.DisableChargingTime = powerAgendaSettings.DisableChargingTime;
                this.ActionCommandLine.IsCheckedRemainingBatteryLevel = powerAgendaSettings.IsCheckedRemainingBatteryLevel;
                this.ActionCommandLine.RemainingBatteryLevel = powerAgendaSettings.RemainingBatteryLevel;
                this.ActionCommandLine.IsCheckedMonitoringBatteryUsage = powerAgendaSettings.IsCheckedMonitoringBatteryUsage;
            }
        }

        public PWMUI.ActionCommandLine ActionCommandLine
        {
            get => 
                this.actioncommandline;
            set
            {
                this.actioncommandline = value;
            }
        }

        public string ID
        {
            get => 
                this.id;
            set
            {
                this.id = value;
            }
        }

        public bool IsChecked
        {
            get => 
                this.ischecked;
            set
            {
                this.ischecked = value;
            }
        }

        public bool IsEnabled
        {
            get => 
                this.isenabled;
            set
            {
                this.isenabled = value;
            }
        }

        public string Name
        {
            get => 
                this.name;
            set
            {
                this.name = value;
            }
        }

        public string OrderTime
        {
            get => 
                this.ordertime;
            set
            {
                this.ordertime = value;
            }
        }
    }
}

