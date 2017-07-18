namespace PWMUI
{
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls;

    public class PMWindowPeer : WindowAutomationPeer
    {
        private List<PMUserControlPeer> userControlPeers;
        private List<UserControl> userControls;

        public PMWindowPeer(Window owner) : base(owner)
        {
            this.userControls = new List<UserControl>();
            this.userControlPeers = new List<PMUserControlPeer>();
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
                    if (peer.GetClassName() == "UserControl")
                    {
                        UserControl owner = (UserControl) (peer as UserControlAutomationPeer).Owner;
                        if (!this.userControls.Contains(owner))
                        {
                            this.userControls.Add(owner);
                            this.userControlPeers.Add(new PMUserControlPeer(owner));
                        }
                        int index = this.userControls.IndexOf(owner);
                        childrenCore[i] = this.userControlPeers[index];
                    }
                }
            }
            return childrenCore;
        }
    }
}

