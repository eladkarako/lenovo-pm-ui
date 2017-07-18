namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Collections;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public class CommandLineHelper
    {
        public string ConvertActionCommandLine(ActionCommandLine actionCommandLine)
        {
            bool flag = true;
            string str = "";
            string str2 = "";
            if (actionCommandLine.Prenotice > 0)
            {
                str2 = $"/Prenotice:{actionCommandLine.Prenotice} ";
            }
            str = str + str2;
            string str3 = actionCommandLine.StartTime.ToString("HH:mm").Replace('.', ':');
            str2 = $"/StartTime:{str3} ";
            str = str + str2;
            str3 = actionCommandLine.EndTime.ToString("HH:mm").Replace('.', ':');
            str2 = $"/EndTime:{str3} ";
            str = str + str2;
            if (actionCommandLine.Frequency == eFrequency.Weekly)
            {
                bool flag2 = true;
                str2 = "";
                foreach (eDayOfWeek week in actionCommandLine.DayOfWeek)
                {
                    if (flag2)
                    {
                        flag2 = false;
                        str2 = str2 + $"{((int) week)}";
                    }
                    else
                    {
                        str2 = str2 + $",{((int) week)}";
                    }
                }
                if (str2.Length > 0)
                {
                    str2 = str2 + " ";
                    str2 = "/Weekly:" + str2;
                    str = str + str2;
                }
            }
            str2 = "/Action:";
            switch (actionCommandLine.Action)
            {
                case eAction.Sleep:
                case eAction.SleepImmediately:
                    str2 = str2 + $"Sleep /ActionParam:{actionCommandLine.IdleTimerValue}";
                    break;

                case eAction.Hibernate:
                case eAction.HibernateImmediately:
                    str2 = str2 + $"Hibernate /ActionParam:{actionCommandLine.IdleTimerValue}";
                    break;

                case eAction.MonitorOff:
                    str2 = str2 + $"MonitorOff /ActionParam:{actionCommandLine.IdleTimerValue}";
                    break;

                case eAction.Shutdown:
                    str2 = str2 + $"Shutdown /ActionParam:{actionCommandLine.iBlockWindows}";
                    break;

                case eAction.SwitchPowerPlan:
                    str2 = str2 + $"SwitchPowerPlan "/ActionParam:{actionCommandLine.PowerPlanName.ToString()}"";
                    break;

                case eAction.SetBrightness:
                    str2 = str2 + $"SetBrightness /ActionParam:{actionCommandLine.BrightnessLevel},{actionCommandLine.ThinkPadBrightnessLevel}";
                    break;

                case eAction.Peakshift:
                    str2 = str2 + $"Peakshift /ActionParam:{actionCommandLine.StartDate.ToString("MM/dd", DateTimeFormatInfo.InvariantInfo)},{actionCommandLine.EndDate.ToString("MM/dd", DateTimeFormatInfo.InvariantInfo)},{actionCommandLine.StartTime.ToString("HH:mm").Replace('.', ':')},{actionCommandLine.RunOnBatteryTime.ToString("HH:mm").Replace('.', ':')},{actionCommandLine.DisableChargingTime.ToString("HH:mm").Replace('.', ':')},{Convert.ToUInt32(actionCommandLine.IsCheckedRemainingBatteryLevel)},{actionCommandLine.RemainingBatteryLevel},{Convert.ToUInt32(actionCommandLine.IsCheckedMonitoringBatteryUsage)}";
                    break;

                case eAction.FastHibernation:
                    str2 = str2 + "FastHibernation";
                    break;

                default:
                    flag = false;
                    break;
            }
            str = str + str2;
            if (flag)
            {
                return str;
            }
            return "";
        }

        private bool GetActionFromStringValue(string value, out eAction action)
        {
            bool flag = true;
            switch (value)
            {
                case "Shutdown":
                    action = eAction.Shutdown;
                    return flag;

                case "Sleep":
                    action = eAction.Sleep;
                    return flag;

                case "Hibernate":
                    action = eAction.Hibernate;
                    return flag;

                case "SwitchPowerPlan":
                    action = eAction.SwitchPowerPlan;
                    return flag;

                case "SetBrightness":
                    action = eAction.SetBrightness;
                    return flag;

                case "MonitorOff":
                    action = eAction.MonitorOff;
                    return flag;

                case "Peakshift":
                    action = eAction.Peakshift;
                    return flag;

                case "FastHibernation":
                    action = eAction.FastHibernation;
                    return flag;
            }
            action = eAction.Unknown;
            return false;
        }

        private ObservableCollection<eDayOfWeek> GetDayOfWeekFromStringValue(string value)
        {
            ObservableCollection<eDayOfWeek> observables = new ObservableCollection<eDayOfWeek>();
            eDayOfWeek sunday = eDayOfWeek.Sunday;
            char[] separator = new char[] { ',' };
            string[] strArray2 = value.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < strArray2.Length; i++)
            {
                switch (strArray2[i])
                {
                    case "0":
                        sunday = eDayOfWeek.Sunday;
                        break;

                    case "1":
                        sunday = eDayOfWeek.Monday;
                        break;

                    case "2":
                        sunday = eDayOfWeek.Tuesday;
                        break;

                    case "3":
                        sunday = eDayOfWeek.Wednesday;
                        break;

                    case "4":
                        sunday = eDayOfWeek.Thursday;
                        break;

                    case "5":
                        sunday = eDayOfWeek.Friday;
                        break;

                    case "6":
                        sunday = eDayOfWeek.Saturday;
                        break;
                }
                observables.Add(sunday);
            }
            return observables;
        }

        private bool GetPowerPlanGuidFromStringValue(string value, out Guid guid) => 
            this.IsGuid(value, out guid);

        internal bool IsGuid(string candidate, out Guid output)
        {
            Regex regex = new Regex(@"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$", RegexOptions.Compiled);
            bool flag = false;
            output = Guid.Empty;
            if ((candidate != null) && regex.IsMatch(candidate))
            {
                output = new Guid(candidate);
                flag = true;
            }
            return flag;
        }

        public ActionCommandLine ParseActionCommandLine(string[] args)
        {
            if (args.Length == 0)
            {
                return null;
            }
            Hashtable hashtable = new Hashtable();
            string pattern = @"(?<argname>/\w+):(?<argvalue>(\w|\W)*)";
            foreach (string str2 in args)
            {
                Match match = Regex.Match(str2, pattern);
                if (!match.Success)
                {
                    throw new ArgumentException("The command line arguments are improperly formed. Use /argname:argvalue.");
                }
                hashtable[match.Groups["argname"].Value] = match.Groups["argvalue"].Value;
            }
            ActionCommandLine line = new ActionCommandLine();
            foreach (DictionaryEntry entry in hashtable)
            {
                eAction action;
                string str3 = entry.Key.ToString().Trim();
                string str4 = entry.Value.ToString().Trim();
                string str5 = str3;
                if (str5 != null)
                {
                    if (str5 != "/Prenotice")
                    {
                        if (str5 == "/StartTime")
                        {
                            goto Label_0138;
                        }
                        if (str5 == "/EndTime")
                        {
                            goto Label_014B;
                        }
                        if (str5 == "/Weekly")
                        {
                            goto Label_015E;
                        }
                        if (str5 == "/Action")
                        {
                            goto Label_017A;
                        }
                        if (str5 == "/ActionParam")
                        {
                            goto Label_0197;
                        }
                    }
                    else
                    {
                        line.Prenotice = Convert.ToUInt32(str4);
                    }
                }
                continue;
            Label_0138:
                line.StartTime = Convert.ToDateTime(str4);
                continue;
            Label_014B:
                line.EndTime = Convert.ToDateTime(str4);
                continue;
            Label_015E:
                line.Frequency = eFrequency.Weekly;
                line.DayOfWeek = this.GetDayOfWeekFromStringValue(str4);
                continue;
            Label_017A:
                if (this.GetActionFromStringValue(str4, out action))
                {
                    line.Action = action;
                }
                continue;
            Label_0197:
                if (line.Action == eAction.SwitchPowerPlan)
                {
                    Guid empty = Guid.Empty;
                    if (this.GetPowerPlanGuidFromStringValue(str4, out empty))
                    {
                        PowerPlan planFromGuid = new PowerPlanInformer().GetPlanFromGuid(empty);
                        line.PowerPlanName = planFromGuid.Name;
                    }
                    else
                    {
                        line.PowerPlanName = str4;
                    }
                }
                else if (line.Action == eAction.SetBrightness)
                {
                    string[] strArray = str4.Split(new char[] { ',' });
                    if (strArray.Length >= 1)
                    {
                        line.BrightnessLevel = Convert.ToUInt32(strArray[0]);
                    }
                    if (strArray.Length >= 2)
                    {
                        line.ThinkPadBrightnessLevel = Convert.ToUInt32(strArray[1]);
                    }
                }
                else if (line.Action == eAction.Sleep)
                {
                    line.IdleTimerValue = Convert.ToUInt32(str4);
                    if (line.IdleTimerValue == 0)
                    {
                        line.Action = eAction.SleepImmediately;
                    }
                }
                else if (line.Action == eAction.Hibernate)
                {
                    line.IdleTimerValue = Convert.ToUInt32(str4);
                    if (line.IdleTimerValue == 0)
                    {
                        line.Action = eAction.HibernateImmediately;
                    }
                }
                else if (line.Action == eAction.MonitorOff)
                {
                    line.IdleTimerValue = Convert.ToUInt32(str4);
                }
                else if (line.Action == eAction.Peakshift)
                {
                    string[] strArray2 = str4.Split(new char[] { ',' });
                    if (strArray2.Length >= 1)
                    {
                        line.StartDate = Convert.ToDateTime(strArray2[0]);
                    }
                    if (strArray2.Length >= 2)
                    {
                        line.EndDate = Convert.ToDateTime(strArray2[1]);
                    }
                    if (strArray2.Length >= 4)
                    {
                        line.RunOnBatteryTime = Convert.ToDateTime(strArray2[3]);
                    }
                    if (strArray2.Length >= 5)
                    {
                        line.DisableChargingTime = Convert.ToDateTime(strArray2[4]);
                    }
                    if (strArray2.Length >= 6)
                    {
                        line.IsCheckedRemainingBatteryLevel = Convert.ToUInt32(strArray2[5]) != 0;
                    }
                    if (strArray2.Length >= 7)
                    {
                        line.RemainingBatteryLevel = Convert.ToUInt32(strArray2[6]);
                    }
                    if (strArray2.Length >= 8)
                    {
                        line.IsCheckedMonitoringBatteryUsage = Convert.ToUInt32(strArray2[7]) != 0;
                    }
                }
                if (line.Action == eAction.Shutdown)
                {
                    line.iBlockWindows = Convert.ToUInt32(str4);
                }
            }
            return line;
        }
    }
}

