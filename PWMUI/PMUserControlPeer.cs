namespace PWMUI
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;

    public class PMUserControlPeer : UserControlAutomationPeer
    {
        public PMUserControlPeer(UserControl owner) : base(owner)
        {
        }

        protected override List<AutomationPeer> GetChildrenCore()
        {
            List<AutomationPeer> childrenCore = base.GetChildrenCore();
            if (childrenCore != null)
            {
                for (int i = 0; i < childrenCore.Count; i++)
                {
                    AutomationPeer peer = childrenCore[i];
                    if (peer.GetClassName() == "ResizeThumb")
                    {
                        childrenCore.RemoveAt(i);
                        i--;
                    }
                }
            }
            return childrenCore;
        }

        protected override string GetClassNameCore() => 
            "PMUserControl";
    }
}

