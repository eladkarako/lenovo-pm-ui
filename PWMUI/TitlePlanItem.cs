namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Media;

    public class TitlePlanItem : System.Windows.Controls.UserControl, IPlanItem, IComponentConnector
    {
        private bool _contentLoaded;
        internal Grid LayoutRoot;
        private List<SettingItem> relatedItems = new List<SettingItem>();
        internal Grid settingPanel;
        internal TextBlock settingTitle;
        internal TitlePlanItem UserControl;

        public TitlePlanItem()
        {
            this.InitializeComponent();
        }

        public bool CanShowExport()
        {
            foreach (SettingItem item in this.relatedItems)
            {
                if (item.CanShowExport())
                {
                    return true;
                }
            }
            return false;
        }

        public void HideSettingsItem()
        {
        }

        public void HideTitle2()
        {
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/titleplanitem.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public bool IsCapable()
        {
            foreach (SettingItem item in this.relatedItems)
            {
                if (item.IsCapable())
                {
                    return true;
                }
            }
            return false;
        }

        public void SetBackgroundToGray()
        {
            this.settingPanel.Background = (Brush) base.FindResource("GrayBackgroundBrush");
        }

        public void SetRelatedItem(SettingItem relatedItem)
        {
            foreach (SettingItem item in this.relatedItems)
            {
                if (item == relatedItem)
                {
                    return;
                }
            }
            this.relatedItems.Add(relatedItem);
        }

        public void SetTitle(string titleKey)
        {
            this.settingTitle.Text = (string) base.FindResource(titleKey);
        }

        public void ShowSettingsItem()
        {
        }

        public void ShowTitle2()
        {
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (TitlePlanItem) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.settingPanel = (Grid) target;
                    return;

                case 4:
                    this.settingTitle = (TextBlock) target;
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

