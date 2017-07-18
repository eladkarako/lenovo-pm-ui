namespace PWMUI
{
    using System;
    using System.Collections;
    using System.Xml.Serialization;

    [XmlRoot("ScheduledTask")]
    public class ScheduledTaskList
    {
        private ArrayList _taskList = new ArrayList();

        public void AddTask(Task task)
        {
            this._taskList.Add(task);
        }

        public int Count() => 
            this._taskList.Count;

        [XmlArrayItem("Task", typeof(Task)), XmlArray("Tasks")]
        public ArrayList ScheduledTask
        {
            get => 
                this._taskList;
            set
            {
                this._taskList = value;
            }
        }
    }
}

