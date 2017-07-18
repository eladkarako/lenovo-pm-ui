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
    using System.Windows.Forms;
    using System.Windows.Markup;

    public class PlanPanel : System.Windows.Controls.UserControl, IPMTabPanel, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        internal AdvancedSettingsPanel advancedSettingsPanel;
        internal AlarmsPanel alarmsPanel;
        internal BatteryStretchPanel batteryStretchPanel;
        public CheckGlobalSettingsEventHandler CheckGlobalSettingsEvent;
        internal System.Windows.Controls.Button delPlanBtn;
        protected bool Disposed;
        internal EventsPanel eventsPanel;
        internal System.Windows.Controls.Button expPlanBtn;
        internal StackPanel fanSoundLevelAC;
        internal StackPanel fanSoundLevelDC;
        internal IdleTimersPanel idleTimersPanel;
        public const int IDOK = 1;
        internal System.Windows.Controls.Button impPlanBtn;
        internal PowerPlanInformer informer = new PowerPlanInformer();
        private IntelGfxPowerSettingChecker intelGfxPowerSettingChecker;
        private bool isRegisteredEvent;
        internal Grid LayoutRoot;
        private LowerBrightnessChecker lowerBrightnessChecker;
        internal System.Windows.Controls.Button newPlanBtn;
        internal TabItem OnFocusControl;
        private List<IPlanSettingPlane> panels = new List<IPlanSettingPlane>();
        internal StackPanel performancePanelAC;
        internal StackPanel performancePanelDC;
        internal PlanNamePanel planNamePanel;
        internal StackPanel powerUsageAC;
        internal StackPanel powerUsageDC;
        internal System.Windows.Controls.Button restoreBtn;
        internal SystemSettingsPanel systemSettingsPanel;
        internal StackPanel systemTemperatureAC;
        internal StackPanel systemTemperatureDC;
        internal PlanPanel UserControl;

        public PlanPanel()
        {
            this.InitializeComponent();
            this.panels.Add(this.systemSettingsPanel);
            this.panels.Add(this.idleTimersPanel);
            this.panels.Add(this.advancedSettingsPanel);
            this.panels.Add(this.eventsPanel);
            this.panels.Add(this.alarmsPanel);
            this.systemSettingsPanel.EffectOfSettingChangeEvent = (EffectOfSettingChangeEventHandler) Delegate.Combine(this.systemSettingsPanel.EffectOfSettingChangeEvent, new EffectOfSettingChangeEventHandler(this.EffectOfSettingChangeEvent));
            this.lowerBrightnessChecker = new LowerBrightnessChecker(this.systemSettingsPanel.brightnessPlanItem, this.idleTimersPanel.lowerBrightnessTimePlanItem, this.idleTimersPanel.lowerBrightnessPlanItem);
            this.intelGfxPowerSettingChecker = new IntelGfxPowerSettingChecker(this.systemSettingsPanel.nVidiaHybridPlanItem, this.idleTimersPanel.discreteGpuTimeoutPlanItem, this.systemSettingsPanel.gfxPowerSettingsPlanItem);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(System.Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        public bool Apply()
        {
            PowerPlan activePlan = this.informer.GetActivePlan();
            if (activePlan.IsValid)
            {
                this.planNamePanel.Apply(ref activePlan);
                foreach (IPlanSettingPlane plane in this.panels)
                {
                    plane.Apply(ref activePlan);
                }
                activePlan.Apply();
                if (this.CheckGlobalSettingsEvent != null)
                {
                    this.CheckGlobalSettingsEvent(activePlan.SchemeId);
                }
            }
            return true;
        }

        public void Create()
        {
            this.planNamePanel.Create(this.informer);
            NewPowerPlan activePlan = new NewPowerPlan();
            foreach (IPlanSettingPlane plane in this.panels)
            {
                plane.Create(activePlan);
            }
            this.newPlanBtn.IsEnabled = this.informer.CanCreate();
            this.impPlanBtn.IsEnabled = this.informer.CanCreate();
            if (!this.informer.CanSwitch())
            {
                this.delPlanBtn.IsEnabled = false;
            }
            PowerPlan plan2 = this.informer.GetActivePlan();
            this.expPlanBtn.IsEnabled = plan2.SchemeId != uint.MaxValue;
        }

        private void delPlanBtn_Click(object sender, RoutedEventArgs e)
        {
            PowerPlan activePlan = this.informer.GetActivePlan();
            if (activePlan.Delete())
            {
                this.informer.Refresh();
                this.planNamePanel.DeletePlan(activePlan);
                this.Refresh();
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

        private void EffectOfSettingChangeEvent()
        {
            PowerPlan activePlan = this.informer.GetActivePlan();
            this.systemSettingsPanel.Apply(ref activePlan);
            activePlan.Refresh();
            this.RefreshEffectPanel(activePlan);
        }

        private void ExecuteElevation()
        {
            string mainProgram = "PWMUI.exe";
            Resource resource = new Resource();
            if (resource.ShowElevationDialog(mainProgram) == 1)
            {
                MainWindow.Instance.Close();
            }
        }

        private bool ExportMessage()
        {
            PWMUI.ExportMessage message = new PWMUI.ExportMessage {
                Title = (string) base.FindResource("ExportCaption")
            };
            double num = (MainWindow.Instance.Height > message.Height) ? ((MainWindow.Instance.Height / 2.0) - (message.Height / 2.0)) : 0.0;
            double num2 = (MainWindow.Instance.Width > message.Width) ? ((MainWindow.Instance.Width / 2.0) - (message.Width / 2.0)) : 0.0;
            message.Top = MainWindow.Instance.Top + num;
            message.Left = MainWindow.Instance.Left + num2;
            return (message.ShowDialog() == true);
        }

        private void expPlanBtn_Click(object sender, RoutedEventArgs e)
        {
            PowerPlan activePlan = this.informer.GetActivePlan();
            ExportPowerPlan newPlan = new ExportPowerPlan {
                Name = activePlan.Name
            };
            string caption = (string) base.FindResource("ExportCaption");
            if (!this.informer.IsAllItemCapable() && !this.ExportMessage())
            {
                CreatePlanWizard wizard = new CreatePlanWizard(newPlan) {
                    Owner = MainWindow.Instance,
                    Top = MainWindow.Instance.Top + 50.0,
                    Left = MainWindow.Instance.Left + 50.0
                };
                if (wizard.ShowDialog() != true)
                {
                    return;
                }
            }
            SaveFileDialog dialog = new SaveFileDialog {
                Filter = (string) base.FindResource("ImportFileDlgFilter"),
                RestoreDirectory = true,
                FileName = (string) base.FindResource("ExportFilenameUntitled")
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                newPlan.Export(fileName);
                System.Windows.Forms.MessageBox.Show($"{(string) base.FindResource("ExportMessageSuccess")}

{(string) base.FindResource("ImportMessagePowerPlan")} {newPlan.Name}", caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                this.Refresh();
            }
        }

        ~PlanPanel()
        {
            this.Dispose(false);
        }

        private void HideSettingItem()
        {
            this.systemSettingsPanel.Visibility = Visibility.Hidden;
            this.idleTimersPanel.Visibility = Visibility.Hidden;
            this.advancedSettingsPanel.Visibility = Visibility.Hidden;
            this.eventsPanel.Visibility = Visibility.Hidden;
            this.alarmsPanel.Visibility = Visibility.Hidden;
        }

        private void HideSettingsItem()
        {
            foreach (IPlanSettingPlane plane in this.panels)
            {
                plane.HideSettingsItem();
            }
        }

        private void impPlanBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog {
                Filter = (string) base.FindResource("ImportFileDlgFilter"),
                RestoreDirectory = true
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = dialog.FileName;
                eImportStatus status = (eImportStatus) this.informer.CanImport(fileName);
                string text = "";
                string caption = (string) base.FindResource("ImportCaption");
                switch (status)
                {
                    case eImportStatus.InvalidIniFile:
                        text = (string) base.FindResource("ImportMessageFailInvalid");
                        System.Windows.Forms.MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;

                    case eImportStatus.HavingMaxPowerPlans:
                        text = (string) base.FindResource("ImportMessageFailMax");
                        System.Windows.Forms.MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        return;
                }
                string importPlanName = this.informer.GetImportPlanName();
                if ((status == eImportStatus.HavingSameName) && (System.Windows.Forms.MessageBox.Show($"{(string) base.FindResource("ImportMessageConfirm")}

{(string) base.FindResource("ImportMessagePowerPlan")} {importPlanName}", caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No))
                {
                    importPlanName = this.informer.GetNextPowerPlanName(importPlanName);
                }
                if (this.informer.NeedElevationToImport(importPlanName))
                {
                    this.ExecuteElevation();
                }
                else if (this.informer.ImportPowerPlan(fileName))
                {
                    System.Windows.Forms.MessageBox.Show($"{(string) base.FindResource("ImportMessageSuccess")}

{(string) base.FindResource("ImportMessagePowerPlan")} {importPlanName}", caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    this.planNamePanel.AddNewPlan(this.informer);
                    this.Refresh();
                }
            }
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/planpanel.xaml", UriKind.Relative);
                System.Windows.Application.LoadComponent(this, resourceLocator);
            }
        }

        private void MakeEditable()
        {
            this.systemSettingsPanel.IsEnabled = true;
            this.idleTimersPanel.IsEnabled = true;
            this.advancedSettingsPanel.IsEnabled = true;
            this.eventsPanel.IsEnabled = true;
            this.alarmsPanel.IsEnabled = true;
        }

        private void newPlanBtn_Click(object sender, RoutedEventArgs e)
        {
            NewPowerPlan newPlan = new NewPowerPlan();
            CreatePlanWizard wizard = new CreatePlanWizard(newPlan) {
                Owner = MainWindow.Instance,
                Top = MainWindow.Instance.Top + 50.0,
                Left = MainWindow.Instance.Left + 50.0
            };
            if (wizard.ShowDialog() == true)
            {
                this.planNamePanel.AddNewPlan(this.informer);
                this.Refresh();
            }
        }

        private void OnChangeActivePowerScheme()
        {
            this.Refresh();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaed);
            base.OnInitialized(e);
        }

        private void OnLoaed(object sender, RoutedEventArgs e)
        {
            if (!this.isRegisteredEvent)
            {
                MainWindow.Instance.ChangeActivePowerSchemeEvent += new ChangeActivePowerSchemeEventHandler(this.OnChangeActivePowerScheme);
                this.isRegisteredEvent = true;
            }
        }

        private void ProhibitEdit()
        {
            this.systemSettingsPanel.IsEnabled = false;
            this.idleTimersPanel.IsEnabled = false;
            this.advancedSettingsPanel.IsEnabled = false;
            this.eventsPanel.IsEnabled = false;
            this.alarmsPanel.IsEnabled = false;
        }

        public void Refresh()
        {
            this.informer.RefreshActiveScheme();
            PowerPlan activePlan = this.informer.GetActivePlan();
            this.planNamePanel.SetActivePlan(activePlan);
            this.RefreshEffectPanel(activePlan);
            this.delPlanBtn.IsEnabled = activePlan.CanDelete();
            if (!this.informer.CanSwitch())
            {
                this.delPlanBtn.IsEnabled = false;
            }
            this.restoreBtn.IsEnabled = activePlan.CanRestore();
            this.expPlanBtn.IsEnabled = activePlan.SchemeId != uint.MaxValue;
            this.batteryStretchPanel.Refresh();
            if (activePlan.IsValid)
            {
                this.ShowSettingsItem();
            }
            else
            {
                this.HideSettingsItem();
            }
            foreach (IPlanSettingPlane plane in this.panels)
            {
                plane.Refresh(activePlan);
            }
            if (!activePlan.CanEdit())
            {
                this.ProhibitEdit();
            }
            else
            {
                this.MakeEditable();
            }
        }

        private void RefreshEffectPanel(PowerPlan activePlan)
        {
            EffectImage image = new EffectImage();
            this.performancePanelDC.Children.Clear();
            image.AddToPanel(this.performancePanelDC, activePlan.MyEffectSettings.PerformanceDC);
            this.performancePanelAC.Children.Clear();
            image.AddToPanel(this.performancePanelAC, activePlan.MyEffectSettings.PerformanceAC);
            this.systemTemperatureDC.Children.Clear();
            image.AddToPanel(this.systemTemperatureDC, activePlan.MyEffectSettings.SystemTempratureDC);
            this.systemTemperatureAC.Children.Clear();
            image.AddToPanel(this.systemTemperatureAC, activePlan.MyEffectSettings.SystemTempratureAC);
            this.fanSoundLevelDC.Children.Clear();
            image.AddToPanel(this.fanSoundLevelDC, activePlan.MyEffectSettings.FanSoundLevelDC);
            this.fanSoundLevelAC.Children.Clear();
            image.AddToPanel(this.fanSoundLevelAC, activePlan.MyEffectSettings.FanSoundLevelAC);
            this.powerUsageDC.Children.Clear();
            image.AddToPanel(this.powerUsageDC, activePlan.MyEffectSettings.PowerUsageDC);
            this.powerUsageAC.Children.Clear();
            image.AddToPanel(this.powerUsageAC, activePlan.MyEffectSettings.PowerUsageAC);
        }

        private void restoreBtn_Click(object sender, RoutedEventArgs e)
        {
            PowerPlan activePlan = this.informer.GetActivePlan();
            bool isEnabled = MainWindow.Instance.applyBtn.IsEnabled;
            if (activePlan.NeedElevationToRestore())
            {
                this.ExecuteElevation();
            }
            else
            {
                RestorePanel control = new RestorePanel();
                SubWindow window = new SubWindow {
                    Owner = MainWindow.Instance
                };
                window.SetCaption((string) base.FindResource("CaptionRestore"));
                window.SetTitle((string) base.FindResource("CaptionRestore"));
                window.SetContentArea(control);
                window.Height = 200.0;
                window.okBtn.Content = (string) base.FindResource("ButtonRestore");
                if (window.ShowDialog() == true)
                {
                    activePlan.Restore();
                    this.Refresh();
                    if (!isEnabled)
                    {
                        MainWindow.Instance.applyBtn.IsEnabled = false;
                    }
                }
            }
        }

        private void ShowSettingItem()
        {
            this.systemSettingsPanel.Visibility = Visibility.Visible;
            this.idleTimersPanel.Visibility = Visibility.Visible;
            this.advancedSettingsPanel.Visibility = Visibility.Visible;
            this.eventsPanel.Visibility = Visibility.Visible;
            this.alarmsPanel.Visibility = Visibility.Visible;
        }

        private void ShowSettingsItem()
        {
            foreach (IPlanSettingPlane plane in this.panels)
            {
                plane.ShowSettingsItem();
            }
        }

        [DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (PlanPanel) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.OnFocusControl = (TabItem) target;
                    return;

                case 4:
                    this.newPlanBtn = (System.Windows.Controls.Button) target;
                    this.newPlanBtn.Click += new RoutedEventHandler(this.newPlanBtn_Click);
                    return;

                case 5:
                    this.delPlanBtn = (System.Windows.Controls.Button) target;
                    this.delPlanBtn.Click += new RoutedEventHandler(this.delPlanBtn_Click);
                    return;

                case 6:
                    this.impPlanBtn = (System.Windows.Controls.Button) target;
                    this.impPlanBtn.Click += new RoutedEventHandler(this.impPlanBtn_Click);
                    return;

                case 7:
                    this.expPlanBtn = (System.Windows.Controls.Button) target;
                    this.expPlanBtn.Click += new RoutedEventHandler(this.expPlanBtn_Click);
                    return;

                case 8:
                    this.planNamePanel = (PlanNamePanel) target;
                    return;

                case 9:
                    this.batteryStretchPanel = (BatteryStretchPanel) target;
                    return;

                case 10:
                    this.performancePanelDC = (StackPanel) target;
                    return;

                case 11:
                    this.performancePanelAC = (StackPanel) target;
                    return;

                case 12:
                    this.systemTemperatureDC = (StackPanel) target;
                    return;

                case 13:
                    this.systemTemperatureAC = (StackPanel) target;
                    return;

                case 14:
                    this.fanSoundLevelDC = (StackPanel) target;
                    return;

                case 15:
                    this.fanSoundLevelAC = (StackPanel) target;
                    return;

                case 0x10:
                    this.powerUsageDC = (StackPanel) target;
                    return;

                case 0x11:
                    this.powerUsageAC = (StackPanel) target;
                    return;

                case 0x12:
                    this.restoreBtn = (System.Windows.Controls.Button) target;
                    this.restoreBtn.Click += new RoutedEventHandler(this.restoreBtn_Click);
                    return;

                case 0x13:
                    this.systemSettingsPanel = (SystemSettingsPanel) target;
                    return;

                case 20:
                    this.idleTimersPanel = (IdleTimersPanel) target;
                    return;

                case 0x15:
                    this.advancedSettingsPanel = (AdvancedSettingsPanel) target;
                    return;

                case 0x16:
                    this.eventsPanel = (EventsPanel) target;
                    return;

                case 0x17:
                    this.alarmsPanel = (AlarmsPanel) target;
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

