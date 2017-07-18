namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Threading;

    internal class MainTabControl : TabControl, IDisposable
    {
        private List<IPMTabPanel> advancedPanels = new List<IPMTabPanel>();
        private List<TabItem> advancedTabItems = new List<TabItem>();
        private DispatcherTimer afterSelectionChangedTimer = new DispatcherTimer();
        private List<IPMTabPanel> basicPanels = new List<IPMTabPanel>();
        private List<TabItem> basicTabItems = new List<TabItem>();
        internal BatteryPanel batteryPanel;
        internal TabItem batteryTabItem = new TabItem();
        internal UICustomizationsPanel customizationsPanel;
        protected bool Disposed;
        internal GlobalPowerSettingsPanel globalPanel;
        internal TabItem globalTabItem = new TabItem();
        private AfterSelectionChangedHandler handerForShowInstantResume;
        internal PlanPanel planPanel;
        internal TabItem planTabItem = new TabItem();
        internal PowerAgendasPanel poweragendasPanel;
        internal TabItem powerAgendasTabItem = new TabItem();
        internal TabItem powerUseTabItem = new TabItem();
        private bool showInstantResumeIsProcessing;
        internal TabItem uiCustomizationsTabItem = new TabItem();
        internal UsePanel usePanel;

        public event AfterSelectionChangedHandler AfterSelectionChangedEvent;

        public MainTabControl()
        {
            base.SelectionChanged += new SelectionChangedEventHandler(this.MainTabControl_SelectionChanged);
            this.powerAgendasTabItem.Loaded += new RoutedEventHandler(this.powerAgendasTabItem_Loaded);
            this.afterSelectionChangedTimer.Interval = TimeSpan.FromMilliseconds(500.0);
            this.afterSelectionChangedTimer.Tick += new EventHandler(this.AfterSelectionChanged);
            this.handerForShowInstantResume = new AfterSelectionChangedHandler(this.ShowInstantResumeAfterSelectionChanged);
            this.basicTabItems.Add(this.powerUseTabItem);
            this.basicTabItems.Add(this.batteryTabItem);
            this.advancedTabItems.Add(this.planTabItem);
            this.advancedTabItems.Add(this.globalTabItem);
            this.advancedTabItems.Add(this.powerAgendasTabItem);
            this.advancedTabItems.Add(this.batteryTabItem);
            this.advancedTabItems.Add(this.uiCustomizationsTabItem);
        }

        private void AfterSelectionChanged(object sender, EventArgs e)
        {
            this.afterSelectionChangedTimer.Stop();
            if (this.AfterSelectionChangedEvent != null)
            {
                this.AfterSelectionChangedEvent();
            }
        }

        public bool Apply()
        {
            bool flag = true;
            if (MainWindow.Instance.IsBasic())
            {
                foreach (IPMTabPanel panel in this.basicPanels)
                {
                    if (!panel.Apply())
                    {
                        flag = false;
                    }
                }
            }
            if (MainWindow.Instance.IsAdvanced())
            {
                foreach (IPMTabPanel panel2 in this.advancedPanels)
                {
                    if (!panel2.Apply())
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        public void Create()
        {
            this.planTabItem.Style = (Style) base.FindResource("Power Plan Tab");
            this.globalTabItem.Style = (Style) base.FindResource("Global Power Settings Tab");
            this.batteryTabItem.Style = (Style) base.FindResource("Battery Tab");
            this.powerUseTabItem.Style = (Style) base.FindResource("Power Use Tab");
            this.powerAgendasTabItem.Style = (Style) base.FindResource("Power Agendas Tab");
            this.uiCustomizationsTabItem.Style = (Style) base.FindResource("UI Customizations Tab");
            base.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new NoArgDelegate(this.CreateUsePanel));
            base.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new NoArgDelegate(this.CreateBatteryPanel));
            base.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new NoArgDelegate(this.CreatePlanPanel));
            base.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new NoArgDelegate(this.CreateGlobalPanel));
            base.Dispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new NoArgDelegate(this.CreateCustomizationsPanel));
        }

        public void CreateAdvancedItem()
        {
            if (base.SelectedItem == this.planTabItem)
            {
                this.CreatePlanPanel();
            }
            if (base.SelectedItem == this.globalTabItem)
            {
                this.CreateGlobalPanel();
            }
            if (base.SelectedItem == this.batteryTabItem)
            {
                this.CreateBatteryPanel();
            }
            if (base.SelectedItem == this.powerAgendasTabItem)
            {
                this.CreatePowerAgendasPanel();
            }
            if (base.SelectedItem == this.uiCustomizationsTabItem)
            {
                this.CreateCustomizationsPanel();
            }
        }

        public void CreateBasicItem()
        {
            if (base.SelectedItem == this.powerUseTabItem)
            {
                this.CreateUsePanel();
            }
            if (base.SelectedItem == this.batteryTabItem)
            {
                this.CreateBatteryPanel();
            }
        }

        private void CreateBatteryPanel()
        {
            if (this.batteryPanel == null)
            {
                this.batteryPanel = new BatteryPanel();
                this.batteryPanel.Create();
                this.batteryPanel.Refresh();
                this.basicPanels.Add(this.batteryPanel);
                this.advancedPanels.Add(this.batteryPanel);
                this.batteryTabItem.Content = this.batteryPanel;
            }
        }

        private void CreateCustomizationsPanel()
        {
            if (this.customizationsPanel == null)
            {
                this.customizationsPanel = new UICustomizationsPanel();
                this.customizationsPanel.Create();
                this.customizationsPanel.Refresh();
                this.advancedPanels.Add(this.customizationsPanel);
                this.uiCustomizationsTabItem.Content = this.customizationsPanel;
            }
        }

        private void CreateGlobalPanel()
        {
            if (this.globalPanel == null)
            {
                this.globalPanel = new GlobalPowerSettingsPanel();
                this.globalPanel.Create();
                this.globalPanel.Refresh();
                this.advancedPanels.Add(this.globalPanel);
                this.globalTabItem.Content = this.globalPanel;
            }
        }

        private void CreatePlanPanel()
        {
            if (this.planPanel == null)
            {
                this.planPanel = new PlanPanel();
                this.planPanel.Create();
                this.advancedPanels.Add(this.planPanel);
                this.planTabItem.Content = this.planPanel;
            }
        }

        private void CreatePowerAgendasPanel()
        {
            if (this.poweragendasPanel == null)
            {
                this.poweragendasPanel = new PowerAgendasPanel();
                this.poweragendasPanel.Create();
                this.poweragendasPanel.Refresh();
                this.advancedPanels.Add(this.poweragendasPanel);
                this.powerAgendasTabItem.Content = this.poweragendasPanel;
            }
        }

        private void CreateUsePanel()
        {
            if (this.usePanel == null)
            {
                this.usePanel = new UsePanel();
                this.usePanel.Create();
                this.usePanel.Refresh();
                this.basicPanels.Add(this.usePanel);
                this.powerUseTabItem.Content = this.usePanel;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.Disposed)
            {
            }
            this.Disposed = true;
        }

        ~MainTabControl()
        {
            this.Dispose(false);
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender == e.OriginalSource)
            {
                TabItem selectedItem = (TabItem) base.SelectedItem;
                if (selectedItem != null)
                {
                    this.CreateBasicItem();
                    this.CreateAdvancedItem();
                    if (selectedItem == this.powerUseTabItem)
                    {
                        this.usePanel.OnFocusControl.Focus();
                    }
                    if (selectedItem == this.planTabItem)
                    {
                        this.planPanel.OnFocusControl.Focus();
                    }
                    if (selectedItem == this.globalTabItem)
                    {
                        this.globalPanel.OnFocusControl.Focus();
                    }
                    if (selectedItem == this.batteryTabItem)
                    {
                        this.batteryPanel.OnFocusControl.Focus();
                    }
                    if (selectedItem == this.uiCustomizationsTabItem)
                    {
                        this.customizationsPanel.OnFocusControl.Focus();
                    }
                    if (this.AfterSelectionChangedEvent != null)
                    {
                        this.afterSelectionChangedTimer.Start();
                    }
                }
            }
        }

        private void powerAgendasTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            ContentPresenter presenter = PMControlFinder.FindVisualChild<ContentPresenter>(this.powerAgendasTabItem);
            if (presenter != null)
            {
                this.powerAgendasTabItem.Header = presenter.Content;
            }
        }

        public void Refresh()
        {
            base.Items.Clear();
            if (MainWindow.Instance.IsBasic())
            {
                this.CreateBasicItem();
                foreach (TabItem item in this.basicTabItems)
                {
                    base.Items.Add(item);
                }
                foreach (IPMTabPanel panel in this.basicPanels)
                {
                    panel.Refresh();
                }
                this.powerUseTabItem.Focus();
            }
            if (MainWindow.Instance.IsAdvanced())
            {
                this.CreateAdvancedItem();
                foreach (TabItem item2 in this.advancedTabItems)
                {
                    base.Items.Add(item2);
                }
                foreach (IPMTabPanel panel2 in this.advancedPanels)
                {
                    panel2.Refresh();
                }
                this.planTabItem.Focus();
            }
        }

        public void ShowInstantResume()
        {
            RapidResume resume = new RapidResume();
            if (resume.IsCapable())
            {
                TabItem powerUseTabItem = null;
                if (MainWindow.Instance.IsBasic() && (base.SelectedItem != this.powerUseTabItem))
                {
                    powerUseTabItem = this.powerUseTabItem;
                }
                if (MainWindow.Instance.IsAdvanced() && (base.SelectedItem != this.globalTabItem))
                {
                    powerUseTabItem = this.globalTabItem;
                }
                this.showInstantResumeIsProcessing = true;
                if (powerUseTabItem == null)
                {
                    this.ShowInstantResumeAfterSelectionChanged();
                }
                else
                {
                    this.AfterSelectionChangedEvent += this.handerForShowInstantResume;
                    base.SelectedItem = powerUseTabItem;
                }
            }
        }

        private void ShowInstantResumeAfterSelectionChanged()
        {
            if (this.showInstantResumeIsProcessing)
            {
                if (MainWindow.Instance.IsBasic())
                {
                    this.usePanel.ShowInstantResume();
                }
                if (MainWindow.Instance.IsAdvanced())
                {
                    this.globalPanel.ShowInstantResume();
                }
                this.AfterSelectionChangedEvent -= this.handerForShowInstantResume;
                this.showInstantResumeIsProcessing = false;
            }
        }

        private delegate void NoArgDelegate();
    }
}

