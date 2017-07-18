namespace PWMUI
{
    using System;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls.Primitives;

    public class PMResizeThumbPeer : ThumbAutomationPeer
    {
        public PMResizeThumbPeer(Thumb owner) : base(owner)
        {
        }

        protected override string GetClassNameCore() => 
            "ResizeThumb";
    }
}

