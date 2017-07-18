namespace PWMUI
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class WizardStateItem : System.Windows.Controls.UserControl, IComponentConnector
    {
        private bool _contentLoaded;
        internal Grid LayoutRoot;
        internal Rectangle stateIcon;
        internal TextBlock title;
        internal TextBlock title2;
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(WizardStateItem));
        internal WizardStateItem UserControl;

        public WizardStateItem()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/wizardstateitem.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public void SetStateToDefault()
        {
            this.stateIcon.Fill = null;
            this.stateIcon.Width = 20.0;
            this.stateIcon.Height = 20.0;
        }

        public void SetStateToDone()
        {
            this.stateIcon.Fill = (Brush) base.FindResource("img_wizard_done_design");
            this.stateIcon.Width = 15.0;
            this.stateIcon.Height = 10.0;
        }

        public void SetStateToNow()
        {
            this.stateIcon.Fill = (Brush) base.FindResource("img_wizard_now_design");
            this.stateIcon.Width = 10.0;
            this.stateIcon.Height = 10.0;
        }

        public void SetTitle2(bool expflag, bool capable)
        {
            if (expflag && !capable)
            {
                this.title2.Visibility = Visibility.Visible;
            }
            else
            {
                this.title2.Visibility = Visibility.Hidden;
            }
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (WizardStateItem) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.stateIcon = (Rectangle) target;
                    return;

                case 4:
                    this.title2 = (TextBlock) target;
                    return;

                case 5:
                    this.title = (TextBlock) target;
                    return;
            }
            this._contentLoaded = true;
        }

        public string Title
        {
            get => 
                ((string) base.GetValue(TitleProperty));
            set
            {
                base.SetValue(TitleProperty, value);
            }
        }
    }
}

