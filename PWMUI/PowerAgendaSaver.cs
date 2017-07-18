namespace PWMUI
{
    using Microsoft.Win32;
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    internal class PowerAgendaSaver
    {
        public void Save(ObservableCollection<PowerAgendaListData> list)
        {
            int powerAgendaMaxID = new PowerAgendaLoader().GetPowerAgendaMaxID();
            ScheduledTaskList o = new ScheduledTaskList();
            foreach (PowerAgendaListData data in list)
            {
                Task task = new Task();
                task = this.UpdateWithListData(data);
                if (task.ID == "")
                {
                    task.ID = Convert.ToString(++powerAgendaMaxID);
                }
                o.AddTask(task);
            }
            if (o != null)
            {
                try
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(ScheduledTaskList));
                    using (Stream stream = new FileStream(PowerAgendaXmlPath.GetPath(), FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        XmlWriter xmlWriter = new XmlTextWriter(stream, Encoding.UTF8);
                        XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                        namespaces.Add(string.Empty, string.Empty);
                        serializer.Serialize(xmlWriter, o, namespaces);
                        xmlWriter.Close();
                        XmlDocument document = new XmlDocument();
                        document.Load(PowerAgendaXmlPath.GetPath());
                        XmlElement element = (XmlElement) document.SelectSingleNode("//ScheduledTask");
                        element.SetAttribute("Version", "1.0");
                        RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Policies\Lenovo\PWRMGRV\PowerAgenda");
                        if (key != null)
                        {
                            if (key.GetValue("PolicyStamp") != null)
                            {
                                string str = (string) key.GetValue("PolicyStamp");
                                element.SetAttribute("PolicyStamp", str);
                            }
                            else
                            {
                                element.SetAttribute("PolicyStamp", "");
                            }
                            key.Close();
                        }
                        document.Save(PowerAgendaXmlPath.GetPath());
                    }
                }
                catch (XmlException)
                {
                }
                catch (Exception)
                {
                }
            }
        }

        private Task UpdateWithListData(PowerAgendaListData data)
        {
            CommandLineHelper helper = new CommandLineHelper();
            return new Task { 
                Run = data.IsChecked ? "true" : "false",
                Enabled = data.IsEnabled ? "true" : "false",
                ID = data.ID,
                Name = data.Name,
                Action = helper.ConvertActionCommandLine(data.ActionCommandLine)
            };
        }
    }
}

