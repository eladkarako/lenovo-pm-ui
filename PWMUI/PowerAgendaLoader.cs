namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Collections.ObjectModel;
    using System.Xml;

    internal class PowerAgendaLoader
    {
        public ObservableCollection<PowerAgendaListData> GetPowerAgendaListData()
        {
            ObservableCollection<PowerAgendaListData> observables = new ObservableCollection<PowerAgendaListData>();
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(PowerAgendaXmlPath.GetPath());
                foreach (XmlNode node in document.SelectNodes("//Task"))
                {
                    string id = "";
                    bool ischecked = true;
                    bool isenabled = true;
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        string str5 = attribute.Name;
                        if (str5 != null)
                        {
                            if (str5 != "Run")
                            {
                                if (str5 == "Enabled")
                                {
                                    goto Label_00B7;
                                }
                                if (str5 == "ID")
                                {
                                    goto Label_00CF;
                                }
                            }
                            else if (attribute.InnerText != "true")
                            {
                                ischecked = false;
                            }
                        }
                        continue;
                    Label_00B7:
                        if (attribute.InnerText != "true")
                        {
                            isenabled = false;
                        }
                        continue;
                    Label_00CF:
                        id = attribute.InnerText;
                    }
                    string name = "";
                    string cmdLine = "";
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        string str6 = node2.Name;
                        if (str6 != null)
                        {
                            if (str6 == "Name")
                            {
                                name = node2.InnerText;
                            }
                            else if (str6 == "Action")
                            {
                                goto Label_015B;
                            }
                        }
                        continue;
                    Label_015B:
                        cmdLine = node2.InnerText;
                    }
                    string[] args = null;
                    Resource resource = new Resource();
                    if (resource != null)
                    {
                        args = resource.CmdLineToArgv(cmdLine);
                    }
                    ActionCommandLine actioncommandline = new CommandLineHelper().ParseActionCommandLine(args);
                    if (actioncommandline != null)
                    {
                        string ordertime = actioncommandline.StartTime.ToString("HH:mm");
                        if (actioncommandline.Action == eAction.FastHibernation)
                        {
                            FastHibernationItem item = new FastHibernationItem();
                            if (!item.IsChecked)
                            {
                                continue;
                            }
                        }
                        observables.Add(new PowerAgendaListData(ischecked, isenabled, id, name, ordertime, actioncommandline));
                    }
                }
            }
            catch (XmlException)
            {
            }
            catch (Exception)
            {
            }
            return observables;
        }

        public int GetPowerAgendaMaxID()
        {
            int num = 0;
            int num2 = 0;
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(PowerAgendaXmlPath.GetPath());
                foreach (XmlNode node in document.SelectNodes("//Task"))
                {
                    foreach (XmlAttribute attribute in node.Attributes)
                    {
                        string str2;
                        if (((str2 = attribute.Name) != null) && (str2 == "ID"))
                        {
                            num2 = Convert.ToInt32(attribute.InnerText);
                            if (num < num2)
                            {
                                num = num2;
                            }
                        }
                    }
                }
            }
            catch (XmlException)
            {
            }
            catch (Exception)
            {
            }
            return num;
        }
    }
}

