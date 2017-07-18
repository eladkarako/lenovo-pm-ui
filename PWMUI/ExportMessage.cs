namespace PWMUI
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interop;
    using System.Windows.Markup;

    public class ExportMessage : Window, IComponentConnector
    {
        private bool _contentLoaded;
        internal Button editBtn;
        internal ExportMessage Export;
        internal Grid LayoutRoot;
        internal Button okBtn;
        internal System.Windows.Controls.Image QIcon;

        public ExportMessage()
        {
            this.InitializeComponent();
            this.QIcon.Source = Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Question.Handle, Int32Rect.Empty, null);
        }

        private void editBtn_Click(object sender, RoutedEventArgs e)
        {
            base.DialogResult = false;
            base.Close();
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/exportmessage.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void okBtn_Click(object sender, RoutedEventArgs e)
        {
            base.DialogResult = true;
            base.Close();
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.Export = (ExportMessage) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.QIcon = (System.Windows.Controls.Image) target;
                    return;

                case 4:
                    this.okBtn = (Button) target;
                    this.okBtn.Click += new RoutedEventHandler(this.okBtn_Click);
                    return;

                case 5:
                    this.editBtn = (Button) target;
                    this.editBtn.Click += new RoutedEventHandler(this.editBtn_Click);
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

