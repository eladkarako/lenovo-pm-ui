namespace PWMUI
{
    using System;
    using System.Xml.Serialization;

    public class Task
    {
        [XmlElement("Action")]
        public string Action;
        [XmlAttribute("Enabled")]
        public string Enabled;
        [XmlAttribute("ID")]
        public string ID;
        [XmlElement("Name")]
        public string Name;
        [XmlAttribute("Run")]
        public string Run;

        public Task()
        {
        }

        public Task(string run, string enabled, string id, string name, string action)
        {
            this.Run = run;
            this.Enabled = enabled;
            this.ID = id;
            this.Name = name;
            this.Action = action;
        }
    }
}

