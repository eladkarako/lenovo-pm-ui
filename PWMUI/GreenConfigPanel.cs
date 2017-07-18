namespace PWMUI
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    public class GreenConfigPanel : System.Windows.Controls.UserControl, IComponentConnector
    {
        private bool _contentLoaded;
        internal Grid LayoutRoot;
        internal GreenConfigPanel UserControl;

        public GreenConfigPanel()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/greenconfigpanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (GreenConfigPanel) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

