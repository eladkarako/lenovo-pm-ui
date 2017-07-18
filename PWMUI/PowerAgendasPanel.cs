namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using Microsoft.Win32;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;
    using System.Windows.Markup;
    using System.Windows.Threading;

    public class PowerAgendasPanel : System.Windows.Controls.UserControl, IPMTabPanel, IComponentConnector, IStyleConnector
    {
        private bool _contentLoaded;
        private ObservableCollection<PowerAgendaListData> _listData = new ObservableCollection<PowerAgendaListData>();
        internal ListView agendasListView;
        private const int CSIDL_APPDATA = 0x1a;
        internal Button delActionBtn;
        internal Button editActionBtn;
        internal PowerAgendasInformer informer = new PowerAgendasInformer();
        private bool isChildWindowPopup;
        private bool isRegisteredEvent;
        internal Grid LayoutRoot;
        private const int MAX_PATH = 0xff;
        internal Button newActionBtn;
        private bool settingWasChanged;
        internal PowerAgendasPanel UserControl;

        public PowerAgendasPanel()
        {
            this.InitializeComponent();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void agendasListView_Loaded(object sender, RoutedEventArgs e)
        {
            this.EnableDisableListViewItems();
        }

        private void agendasListView_OnChecked(object sender, RoutedEventArgs e)
        {
            this.settingWasChanged = true;
            this.EnableApplyButton();
        }

        private void agendasListView_OnUnchecked(object sender, RoutedEventArgs e)
        {
            this.settingWasChanged = true;
            this.EnableApplyButton();
        }

        private void agendasListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.EnableDisableEditDelButtons();
        }

        public bool Apply()
        {
            if (!this.settingWasChanged)
            {
                return true;
            }
            this.CopyExceptTxt();
            if (this.IsPowerAgendaConflicting())
            {
                return false;
            }
            this.informer.Apply(this._listData);
            this.settingWasChanged = false;
            return true;
        }

        private bool CheckConflictCondition(ActionCommandLine checkedData, ActionCommandLine comparedData)
        {
            if ((checkedData.Action == comparedData.Action) && (this.IsTermsOfValidityOverlapped(checkedData, comparedData) && this.IsTimeOverlapped(checkedData, comparedData)))
            {
                return false;
            }
            return true;
        }

        public void CopyExceptTxt()
        {
            string path = this.GetPath();
            string sourceFileName = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + @"\Smartshutdexcept.txt";
            if (!File.Exists(path))
            {
                File.Copy(sourceFileName, path, true);
            }
        }

        public void Create()
        {
            this._listData = new PowerAgendaLoader().GetPowerAgendaListData();
            this.agendasListView.ItemsSource = this._listData;
            if (this.agendasListView.ItemContainerGenerator != null)
            {
                this.agendasListView.ItemContainerGenerator.StatusChanged += new EventHandler(this.OnListViewStatusChanged);
            }
            this.agendasListView.Loaded += new RoutedEventHandler(this.agendasListView_Loaded);
        }

        [DllImport("kernel32.dll")]
        public static extern int CreateDirectory(StringBuilder lpPathName, IntPtr lpSecurityAttributes);
        private void delActionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.agendasListView.SelectedItems.Count >= 1)
            {
                string text1 = (string) base.FindResource("CaptionPowerManager");
                string text = (string) base.FindResource("MessageConfirmAgendaDeletion");
                Resource resource = new Resource();
                if (resource != null)
                {
                    this.isChildWindowPopup = true;
                    if (!resource.ShowYesNoQuestionMessageBox(text))
                    {
                        this.isChildWindowPopup = false;
                        return;
                    }
                    this.isChildWindowPopup = false;
                }
                ObservableCollection<PowerAgendaListData> observables = new ObservableCollection<PowerAgendaListData>();
                foreach (PowerAgendaListData data in this.agendasListView.SelectedItems)
                {
                    observables.Add(data);
                }
                foreach (PowerAgendaListData data2 in observables)
                {
                    this._listData.Remove(data2);
                }
                this.settingWasChanged = true;
                this.EnableApplyButton();
            }
        }

        private void editActionBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.agendasListView.SelectedItems.Count == 1)
            {
                PowerAgendaListData selectedItem = (PowerAgendaListData) this.agendasListView.SelectedItem;
                PowerAgendaSettings powerAgendaSettings = new PowerAgendaSettings();
                PowerAgendaSettingsPanel control = new PowerAgendaSettingsPanel();
                powerAgendaSettings.UpdateWithListData(selectedItem);
                control.SetPowerAgendaSettings(powerAgendaSettings);
                control.SetActionMode(false);
                SubWindow window = new SubWindow {
                    Owner = MainWindow.Instance
                };
                window.SetCaption((string) base.FindResource("CaptionEditAgenda"));
                window.SetWindowSize(control.Width, control.Height);
                window.SetContentArea(control);
                window.SubWindowOkClickEvent2 += new SubWindowOkClickEventHandler2(control.OkClick);
                window.SubWindowCancelClickEvent += new SubWindowCancelClickEventHandler(control.CancelClick);
                this.isChildWindowPopup = true;
                bool? nullable = window.ShowDialog();
                if (nullable.HasValue ? nullable.GetValueOrDefault() : true)
                {
                    this._listData.Remove(selectedItem);
                    powerAgendaSettings = control.GetPowerAgendaSettings();
                    selectedItem.UpdateWithSettings(powerAgendaSettings);
                    this._listData.Add(selectedItem);
                    this.agendasListView.SelectedItem = selectedItem;
                    this.settingWasChanged = true;
                    this.EnableApplyButton();
                }
                this.isChildWindowPopup = false;
            }
        }

        public void EnableApplyButton()
        {
            MainWindow.Instance.EnableApplyButton();
        }

        private void EnableDisableEditDelButtons()
        {
            bool flag = false;
            foreach (PowerAgendaListData data in this.agendasListView.SelectedItems)
            {
                if (!data.IsEnabled)
                {
                    flag = true;
                    break;
                }
            }
            int count = this.agendasListView.SelectedItems.Count;
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Policies\Lenovo\PWRMGRV\PowerAgenda");
            if (key != null)
            {
                if (key.GetValue("Delete") != null)
                {
                    if (((int) key.GetValue("Delete")) == 0)
                    {
                        this.delActionBtn.IsEnabled = false;
                    }
                    else if ((count > 0) && !flag)
                    {
                        this.delActionBtn.IsEnabled = true;
                    }
                    else
                    {
                        this.delActionBtn.IsEnabled = false;
                    }
                }
                else if ((count > 0) && !flag)
                {
                    this.delActionBtn.IsEnabled = true;
                }
                else
                {
                    this.delActionBtn.IsEnabled = false;
                }
                key.Close();
            }
            else if ((count > 0) && !flag)
            {
                this.delActionBtn.IsEnabled = true;
            }
            else
            {
                this.delActionBtn.IsEnabled = false;
            }
            RegistryKey key2 = Registry.CurrentUser.OpenSubKey(@"Software\Policies\Lenovo\PWRMGRV\PowerAgenda");
            if (key2 != null)
            {
                if (key2.GetValue("Edit") != null)
                {
                    if (((int) key2.GetValue("Edit")) == 0)
                    {
                        this.editActionBtn.IsEnabled = false;
                    }
                    else if ((count == 1) && !flag)
                    {
                        this.editActionBtn.IsEnabled = true;
                    }
                    else
                    {
                        this.editActionBtn.IsEnabled = false;
                    }
                }
                else if ((count == 1) && !flag)
                {
                    this.editActionBtn.IsEnabled = true;
                }
                else
                {
                    this.editActionBtn.IsEnabled = false;
                }
                key2.Close();
            }
            else if ((count == 1) && !flag)
            {
                this.editActionBtn.IsEnabled = true;
            }
            else
            {
                this.editActionBtn.IsEnabled = false;
            }
        }

        private void EnableDisableListViewItems()
        {
            for (int i = 0; i < this.agendasListView.Items.Count; i++)
            {
                PowerAgendaListData itemAt = (PowerAgendaListData) this.agendasListView.Items.GetItemAt(i);
                if (!itemAt.IsEnabled)
                {
                    ListViewItem item = this.agendasListView.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
                    if (item != null)
                    {
                        item.IsEnabled = false;
                    }
                }
            }
        }

        public string GetPath()
        {
            StringBuilder pszPath = new StringBuilder(0x100);
            if (SHGetFolderPath(IntPtr.Zero, 0x1a, IntPtr.Zero, 0, pszPath) >= 0)
            {
                if (pszPath[pszPath.Length - 1] != '\\')
                {
                    pszPath.Append('\\');
                }
                pszPath.Append("PwrMgr");
                CreateDirectory(pszPath, IntPtr.Zero);
                pszPath.Append('\\');
            }
            return pszPath.Append("Smartshutdexcept.txt").ToString();
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/poweragendaspanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private bool IsPowerAgendaConflicting()
        {
            for (int i = 0; i < this._listData.Count; i++)
            {
                ActionCommandLine actionCommandLine = this._listData[i].ActionCommandLine;
                for (int j = 0; j < this._listData.Count; j++)
                {
                    if (i != j)
                    {
                        ActionCommandLine comparedData = this._listData[j].ActionCommandLine;
                        if (!this.CheckConflictCondition(actionCommandLine, comparedData))
                        {
                            string caption = (string) base.FindResource("CaptionPowerManager");
                            MessageBox.Show($"{(string) base.FindResource("MessageConflicting")}

{(string) base.FindResource("ConflictingAgenda")} {this._listData[i].Name}, {this._listData[j].Name}", caption, MessageBoxButton.OK, MessageBoxImage.Hand);
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private bool IsTermsOfValidityOverlapped(ActionCommandLine checkedData, ActionCommandLine comparedData)
        {
            if (checkedData.Action != comparedData.Action)
            {
                return false;
            }
            if (checkedData.Action != eAction.Peakshift)
            {
                return true;
            }
            DateTime today = DateTime.Today;
            DateTime time2 = new DateTime(today.Year, checkedData.StartDate.Month, checkedData.StartDate.Day, today.Hour, today.Minute, 0);
            DateTime time3 = new DateTime(today.Year, comparedData.StartDate.Month, comparedData.StartDate.Day, today.Hour, today.Minute, 0);
            DateTime time4 = new DateTime(today.Year, comparedData.EndDate.Month, comparedData.EndDate.Day, today.Hour, today.Minute, 0);
            if (comparedData.StartDate > comparedData.EndDate)
            {
                time4 = time4.AddYears(1);
            }
            return ((time3 <= time2) && (time4 >= time2));
        }

        private bool IsTimeOverlapped(ActionCommandLine checkedData, ActionCommandLine comparedData)
        {
            if (checkedData.Action != comparedData.Action)
            {
                return false;
            }
            DateTime today = DateTime.Today;
            DateTime time2 = new DateTime(today.Year, today.Month, today.Day, checkedData.StartTime.Hour, checkedData.StartTime.Minute, 0);
            DateTime time3 = new DateTime(today.Year, today.Month, today.Day, comparedData.StartTime.Hour, comparedData.StartTime.Minute, 0);
            DateTime time4 = new DateTime(today.Year, today.Month, today.Day, comparedData.EndTime.Hour, comparedData.EndTime.Minute, 0);
            if (comparedData.StartTime > comparedData.EndTime)
            {
                time4 = time4.AddDays(1.0);
            }
            return ((time3 <= time2) && (time4 >= time2));
        }

        private void newActionBtn_Click(object sender, RoutedEventArgs e)
        {
            PowerAgendaSettings powerAgendaSettings = new PowerAgendaSettings();
            PowerAgendaSettingsPanel control = new PowerAgendaSettingsPanel();
            control.SetPowerAgendaSettings(powerAgendaSettings);
            control.SetActionMode(true);
            SubWindow window = new SubWindow {
                Owner = MainWindow.Instance
            };
            window.SetCaption((string) base.FindResource("CaptionNewAgenda"));
            window.SetWindowSize(control.Width, control.Height);
            window.SetContentArea(control);
            window.okBtn.IsEnabled = false;
            window.SubWindowOkClickEvent2 += new SubWindowOkClickEventHandler2(control.OkClick);
            window.SubWindowCancelClickEvent += new SubWindowCancelClickEventHandler(control.CancelClick);
            this.isChildWindowPopup = true;
            bool? nullable = window.ShowDialog();
            if (nullable.HasValue ? nullable.GetValueOrDefault() : true)
            {
                powerAgendaSettings = control.GetPowerAgendaSettings();
                PowerAgendaListData item = new PowerAgendaListData();
                item.UpdateWithSettings(powerAgendaSettings);
                this._listData.Add(item);
                this.settingWasChanged = true;
                this.EnableApplyButton();
            }
            this.isChildWindowPopup = false;
        }

        protected override void OnInitialized(EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\Policies\Lenovo\PWRMGRV\PowerAgenda");
            if (key != null)
            {
                if ((key.GetValue("New") != null) && (((int) key.GetValue("New")) == 0))
                {
                    this.newActionBtn.IsEnabled = false;
                }
                key.Close();
            }
            base.Loaded += new RoutedEventHandler(this.OnLoaed);
            MainWindow.Instance.ShowEditAgendaWindowEvent += new ShowEditAgendaWindowEventHandler(this.ShowEditAgendaWindow);
            base.OnInitialized(e);
        }

        private void OnListViewStatusChanged(object sender, EventArgs e)
        {
            if (this.agendasListView.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                this.EnableDisableListViewItems();
                for (int i = 0; i < this.agendasListView.Items.Count; i++)
                {
                    PowerAgendaListData data = (PowerAgendaListData) this.agendasListView.Items[i];
                    AutomationProperties.SetName(this.agendasListView.ItemContainerGenerator.ContainerFromIndex(i), data.Name);
                }
            }
        }

        private void OnLoaed(object sender, RoutedEventArgs e)
        {
            if (!this.isRegisteredEvent)
            {
                string.Format("PWMUI: ShowEditAgendaWindowEvent registered", new object[0]);
                this.isRegisteredEvent = true;
            }
        }

        [DllImport("User32.dll", CallingConvention=CallingConvention.StdCall, SetLastError=true)]
        public static extern uint PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        public void Refresh()
        {
            this.RefreshListViewItems();
        }

        private void RefreshListViewItems()
        {
            this.SortListViewItemsByTime();
            this.EnableDisableEditDelButtons();
            this.EnableDisableListViewItems();
        }

        [DllImport("shell32.dll")]
        public static extern int SHGetFolderPath(IntPtr hwndOwner, int nFolder, IntPtr hToken, uint dwFlags, StringBuilder pszPath);
        private void ShowEditAgendaWindow(string agendaID)
        {
            $"PWMUI: ShowEditAgendaWindow({agendaID})";
            if ((MainWindow.Instance.OwnedWindows.Count <= 0) && !this.isChildWindowPopup)
            {
                string.Format("PWMUI: ShowEditAgendaWindow() - show up the Edit Agenda window", new object[0]);
                NormalShowMethod method = new NormalShowMethod(this.ShowEditAgendaWindowMethod);
                base.Dispatcher.BeginInvoke(DispatcherPriority.Normal, method, agendaID);
            }
        }

        private void ShowEditAgendaWindowMethod(string agendaID)
        {
            for (int i = 0; i < this.agendasListView.Items.Count; i++)
            {
                PowerAgendaListData itemAt = (PowerAgendaListData) this.agendasListView.Items.GetItemAt(i);
                if (itemAt.IsEnabled && (itemAt.ID == agendaID))
                {
                    this.agendasListView.SelectedIndex = i;
                    this.editActionBtn.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    return;
                }
            }
        }

        private void SortListViewItemsByTime()
        {
            ICollectionView defaultView = CollectionViewSource.GetDefaultView(this.agendasListView.ItemsSource);
            if (defaultView != null)
            {
                defaultView.SortDescriptions.Clear();
                SortDescription item = new SortDescription("OrderTime", ListSortDirection.Ascending);
                defaultView.SortDescriptions.Add(item);
                defaultView.Refresh();
            }
        }

        [EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (PowerAgendasPanel) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.agendasListView = (ListView) target;
                    this.agendasListView.SelectionChanged += new SelectionChangedEventHandler(this.agendasListView_SelectionChanged);
                    return;

                case 5:
                    this.newActionBtn = (Button) target;
                    this.newActionBtn.Click += new RoutedEventHandler(this.newActionBtn_Click);
                    return;

                case 6:
                    this.editActionBtn = (Button) target;
                    this.editActionBtn.Click += new RoutedEventHandler(this.editActionBtn_Click);
                    return;

                case 7:
                    this.delActionBtn = (Button) target;
                    this.delActionBtn.Click += new RoutedEventHandler(this.delActionBtn_Click);
                    return;
            }
            this._contentLoaded = true;
        }

        [DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IStyleConnector.Connect(int connectionId, object target)
        {
            if (connectionId == 4)
            {
                ((CheckBox) target).Checked += new RoutedEventHandler(this.agendasListView_OnChecked);
                ((CheckBox) target).Unchecked += new RoutedEventHandler(this.agendasListView_OnUnchecked);
            }
        }

        public enum eButtonState
        {
            DisableButton,
            EnableButton
        }

        public delegate void NormalShowMethod(string agendaID);
    }
}

