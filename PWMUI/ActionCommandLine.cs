namespace PWMUI
{
    using System;
    using System.Collections.ObjectModel;

    public class ActionCommandLine
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
        private bool isCheckedMonitoringBatteryUsage;
        private bool isCheckedRemainingBatteryLevel;
        private string powerplanname;
        private uint prenotice;
        private uint remainingBatteryLevel;
        private DateTime runOnBatteryTime;
        private DateTime startDate;
        private DateTime startTime;
        private uint thinkPadBrightnessLevel;

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

