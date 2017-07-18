namespace PWMUI
{
    using System;
    using System.Collections.ObjectModel;

    public class PowerAgendaSettings
    {
        private eAction action;
        private uint brightnesslevel;
        private ObservableCollection<eDayOfWeek> dayofweek;
        private DateTime disableChargingTime;
        private DateTime endDate;
        private DateTime endTime;
        private eFrequency frequency;
        private uint iblockWindows;
        private uint idleTimerValue;
        private bool ischecked;
        private bool isCheckedMonitoringBatteryUsage;
        private bool isCheckedRemainingBatteryLevel;
        private bool isenabled;
        private string name;
        private string powerplanname;
        private uint prenotice;
        private uint remainingBatteryLevel;
        private DateTime runOnBatteryTime;
        private DateTime startDate;
        private DateTime startTime;
        private uint thinkPadBrightnessLevel;

        public PowerAgendaSettings()
        {
            this.IsEnabled = true;
            this.IsChecked = true;
            this.Name = "";
            this.Action = eAction.Sleep;
            this.BrightnessLevel = 0;
            this.ThinkPadBrightnessLevel = 0;
            this.PowerPlanName = "";
            this.IdleTimerValue = 1;
            this.Frequency = eFrequency.Daily;
            this.DayOfWeek = new ObservableCollection<eDayOfWeek>();
            this.StartTime = DateTime.Now;
            this.EndTime = DateTime.Now;
            this.Prenotice = 60;
            this.iBlockWindows = 1;
            this.startDate = DateTime.Now;
            this.endDate = DateTime.Now;
            this.runOnBatteryTime = DateTime.Now;
            this.disableChargingTime = DateTime.Now;
            this.isCheckedRemainingBatteryLevel = true;
            this.remainingBatteryLevel = 10;
            this.isCheckedMonitoringBatteryUsage = false;
        }

        public void UpdateWithListData(PowerAgendaListData listData)
        {
            if (listData != null)
            {
                this.IsEnabled = listData.IsEnabled;
                this.IsChecked = listData.IsChecked;
                this.Name = listData.Name;
                this.Action = listData.ActionCommandLine.Action;
                this.BrightnessLevel = listData.ActionCommandLine.BrightnessLevel;
                this.ThinkPadBrightnessLevel = listData.ActionCommandLine.ThinkPadBrightnessLevel;
                this.IdleTimerValue = listData.ActionCommandLine.IdleTimerValue;
                this.PowerPlanName = listData.ActionCommandLine.PowerPlanName;
                this.Frequency = listData.ActionCommandLine.Frequency;
                this.DayOfWeek = listData.ActionCommandLine.DayOfWeek;
                this.StartTime = listData.ActionCommandLine.StartTime;
                this.EndTime = listData.ActionCommandLine.EndTime;
                this.Prenotice = listData.ActionCommandLine.Prenotice;
                this.iBlockWindows = listData.ActionCommandLine.iBlockWindows;
                this.startDate = listData.ActionCommandLine.StartDate;
                this.endDate = listData.ActionCommandLine.EndDate;
                this.runOnBatteryTime = listData.ActionCommandLine.RunOnBatteryTime;
                this.disableChargingTime = listData.ActionCommandLine.DisableChargingTime;
                this.isCheckedRemainingBatteryLevel = listData.ActionCommandLine.IsCheckedRemainingBatteryLevel;
                this.remainingBatteryLevel = listData.ActionCommandLine.RemainingBatteryLevel;
                this.isCheckedMonitoringBatteryUsage = listData.ActionCommandLine.IsCheckedMonitoringBatteryUsage;
            }
        }

        public eAction Action
        {
            get => 
                this.action;
            set
            {
                this.action = value;
            }
        }

        public uint BrightnessLevel
        {
            get => 
                this.brightnesslevel;
            set
            {
                this.brightnesslevel = value;
            }
        }

        public ObservableCollection<eDayOfWeek> DayOfWeek
        {
            get => 
                this.dayofweek;
            set
            {
                this.dayofweek = value;
            }
        }

        public DateTime DisableChargingTime
        {
            get => 
                this.disableChargingTime;
            set
            {
                this.disableChargingTime = value;
            }
        }

        public DateTime EndDate
        {
            get => 
                this.endDate;
            set
            {
                this.endDate = value;
            }
        }

        public DateTime EndTime
        {
            get => 
                this.endTime;
            set
            {
                this.endTime = value;
            }
        }

        public eFrequency Frequency
        {
            get => 
                this.frequency;
            set
            {
                this.frequency = value;
            }
        }

        public uint iBlockWindows
        {
            get => 
                this.iblockWindows;
            set
            {
                this.iblockWindows = value;
            }
        }

        public uint IdleTimerValue
        {
            get => 
                this.idleTimerValue;
            set
            {
                this.idleTimerValue = value;
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

        public bool IsCheckedMonitoringBatteryUsage
        {
            get => 
                this.isCheckedMonitoringBatteryUsage;
            set
            {
                this.isCheckedMonitoringBatteryUsage = value;
            }
        }

        public bool IsCheckedRemainingBatteryLevel
        {
            get => 
                this.isCheckedRemainingBatteryLevel;
            set
            {
                this.isCheckedRemainingBatteryLevel = value;
            }
        }

        public bool IsDaily =>
            (this.frequency == eFrequency.Daily);

        public bool IsEnabled
        {
            get => 
                this.isenabled;
            set
            {
                this.isenabled = value;
            }
        }

        public bool IsWeekly =>
            (this.frequency == eFrequency.Weekly);

        public string Name
        {
            get => 
                this.name;
            set
            {
                this.name = value;
            }
        }

        public string PowerPlanName
        {
            get => 
                this.powerplanname;
            set
            {
                this.powerplanname = value;
            }
        }

        public uint Prenotice
        {
            get => 
                this.prenotice;
            set
            {
                this.prenotice = value;
            }
        }

        public uint RemainingBatteryLevel
        {
            get => 
                this.remainingBatteryLevel;
            set
            {
                this.remainingBatteryLevel = value;
            }
        }

        public DateTime RunOnBatteryTime
        {
            get => 
                this.runOnBatteryTime;
            set
            {
                this.runOnBatteryTime = value;
            }
        }

        public DateTime StartDate
        {
            get => 
                this.startDate;
            set
            {
                this.startDate = value;
            }
        }

        public DateTime StartTime
        {
            get => 
                this.startTime;
            set
            {
                this.startTime = value;
            }
        }

        public uint ThinkPadBrightnessLevel
        {
            get => 
                this.thinkPadBrightnessLevel;
            set
            {
                this.thinkPadBrightnessLevel = value;
            }
        }
    }
}

