namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Threading;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Markup;
    using System.Windows.Media;
    using System.Windows.Shapes;

    public class PlanNamePanelItem : System.Windows.Controls.UserControl, IPMControl, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        internal Button Button_1;
        protected bool Disposed;
        private PowerPlanInformer informer = new PowerPlanInformer();
        private bool isActive;
        internal Grid LayoutRoot;
        internal PowerPlan myPlan;
        private string originalName;
        internal Rectangle planImage;
        internal TextBlock planNameBlock;
        internal TextBox planNameBox;
        internal PlanNamePanelItem UserControl;

        public event SelectedPowerPlanEventHandler SelectedPowerPlanEvent;

        public PlanNamePanelItem(PowerPlan plan)
        {
            this.InitializeComponent();
            this.myPlan = plan;
            this.originalName = plan.Name;
            this.planNameBox.Text = plan.Name;
            this.planNameBox.Visibility = Visibility.Hidden;
            this.planNameBlock.Text = plan.Name;
            this.planNameBlock.Visibility = Visibility.Visible;
            AutomationProperties.SetName(this.Button_1, plan.Name);
            this.SetPlanImage(plan.SectionNumberOfPredefined);
            MainWindow.Instance.ChangeActivePowerSchemeEvent += new ChangeActivePowerSchemeEventHandler(this.OnChangeActivePowerScheme);
        }

        public void Apply(ref PowerPlan activePlan)
        {
            activePlan.Name = this.planNameBox.Text;
            this.originalName = this.planNameBox.Text;
            this.planNameBlock.Text = this.planNameBox.Text;
        }

        private void Button_1_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedMyPlan();
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

        public void EnableApplyButton()
        {
            MainWindow.Instance.EnableApplyButton();
        }

        ~PlanNamePanelItem()
        {
            this.Dispose(false);
        }

        private Brush GetBrushOfActivePlanImage(uint sectionNo)
        {
            string resourceKey = "img_plan_Default3_design";
            if (sectionNo == 0)
            {
                resourceKey = "img_plan_MaxPower3_design";
            }
            if (sectionNo == 1)
            {
                resourceKey = "img_plan_MaxBattery3_design";
            }
            if (sectionNo == 2)
            {
                resourceKey = "img_plan_TimersOff3_design";
            }
            if (sectionNo == 3)
            {
                resourceKey = "img_plan_PowerSorceOptimized3_design";
            }
            if (sectionNo == 6)
            {
                resourceKey = "img_plan_videoplayback3_design";
            }
            if (sectionNo == 7)
            {
                resourceKey = "img_plan_Airplane3_design";
            }
            return (Brush) base.FindResource(resourceKey);
        }

        private Brush GetBrushOfPlanImage(uint sectionNo)
        {
            string resourceKey = "img_plan_Default2_design";
            if (sectionNo == 0)
            {
                resourceKey = "img_plan_MaxPower2_design";
            }
            if (sectionNo == 1)
            {
                resourceKey = "img_plan_MaxBattery2_design";
            }
            if (sectionNo == 2)
            {
                resourceKey = "img_plan_TimersOff2_design";
            }
            if (sectionNo == 3)
            {
                resourceKey = "img_plan_PowerSorceOptimized2_design";
            }
            if (sectionNo == 6)
            {
                resourceKey = "img_plan_videoplayback2_design";
            }
            if (sectionNo == 7)
            {
                resourceKey = "img_plan_Airplane2_design";
            }
            return (Brush) base.FindResource(resourceKey);
        }

        private Size GetSizeOfPlanImage(uint sectionNo)
        {
            if (sectionNo == 0)
            {
                return new Size(18.97, 24.0);
            }
            if (sectionNo == 1)
            {
                return new Size(24.0, 24.0);
            }
            if (sectionNo == 2)
            {
                return new Size(28.0, 18.82);
            }
            if (sectionNo == 3)
            {
                return new Size(23.0, 27.0);
            }
            if (sectionNo == 6)
            {
                return new Size(17.99, 26.0);
            }
            return new Size(18.0, 16.41);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/plannameitem.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void OnChangeActivePowerScheme()
        {
            this.planNameBox.Text = this.originalName;
        }

        private void planNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (base.IsVisible)
            {
                this.EnableApplyButton();
            }
        }

        private void SelectedMyPlan()
        {
            if (this.SelectedPowerPlanEvent != null)
            {
                this.SelectedPowerPlanEvent();
            }
            if (this.informer.CanSwitch() && this.informer.CanSelect(this.myPlan.SchemeId))
            {
                this.EnableApplyButton();
                this.myPlan.SetActive();
            }
        }

        public void SetActiveBackground()
        {
            this.isActive = true;
            this.planNameBlock.Foreground = Brushes.White;
            this.planNameBox.Foreground = Brushes.White;
            if (this.myPlan.CanRename())
            {
                this.planNameBlock.Visibility = Visibility.Hidden;
                this.planNameBox.Visibility = Visibility.Visible;
            }
            this.SetActivePlanImage(this.myPlan.SectionNumberOfPredefined);
            base.Background = (Brush) base.FindResource("ActiveBackgroundBrush");
            base.BorderBrush = (Brush) base.FindResource("ActiveBackgroundBrush");
        }

        private void SetActivePlanImage(uint sectionNo)
        {
            this.planImage.Fill = this.GetBrushOfActivePlanImage(sectionNo);
            Size sizeOfPlanImage = this.GetSizeOfPlanImage(sectionNo);
            this.planImage.Width = sizeOfPlanImage.Width;
            this.planImage.Height = sizeOfPlanImage.Height;
        }

        public void SetDefaultBackground()
        {
            this.isActive = false;
            if (!base.IsMouseOver)
            {
                this.planNameBlock.Foreground = Brushes.Black;
                this.planNameBox.Foreground = Brushes.Black;
                if (this.myPlan.CanRename())
                {
                    this.planNameBlock.Visibility = Visibility.Visible;
                    this.planNameBox.Visibility = Visibility.Hidden;
                }
                this.SetPlanImage(this.myPlan.SectionNumberOfPredefined);
                base.Background = (Brush) base.FindResource("GrayBackgroundBrush");
                base.BorderBrush = (Brush) base.FindResource("GrayBackgroundBrush");
            }
        }

        public void SetMouseOverBackground()
        {
            if (!this.isActive)
            {
                this.planNameBlock.Foreground = Brushes.Black;
                this.planNameBox.Foreground = Brushes.Black;
                if (this.myPlan.CanRename())
                {
                    this.planNameBlock.Visibility = Visibility.Visible;
                    this.planNameBox.Visibility = Visibility.Hidden;
                }
                this.SetPlanImage(this.myPlan.SectionNumberOfPredefined);
                base.Background = (Brush) base.FindResource("MouseOverBackgroundBrush");
                base.BorderBrush = (Brush) base.FindResource("LightBlueBrush");
            }
        }

        private void SetPlanImage(uint sectionNo)
        {
            this.planImage.Fill = this.GetBrushOfPlanImage(sectionNo);
            Size sizeOfPlanImage = this.GetSizeOfPlanImage(sectionNo);
            this.planImage.Width = sizeOfPlanImage.Width;
            this.planImage.Height = sizeOfPlanImage.Height;
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never)]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (PlanNamePanelItem) target;
                    this.UserControl.MouseEnter += new MouseEventHandler(this.UserControl_MouseEnter);
                    this.UserControl.MouseLeave += new MouseEventHandler(this.UserControl_MouseLeave);
                    this.UserControl.MouseLeftButtonUp += new MouseButtonEventHandler(this.UserControl_MouseLeftButtonUp);
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.Button_1 = (Button) target;
                    this.Button_1.Click += new RoutedEventHandler(this.Button_1_Click);
                    return;

                case 4:
                    this.planImage = (Rectangle) target;
                    return;

                case 5:
                    this.planNameBlock = (TextBlock) target;
                    return;

                case 6:
                    this.planNameBox = (TextBox) target;
                    this.planNameBox.TextChanged += new TextChangedEventHandler(this.planNameBox_TextChanged);
                    return;
            }
            this._contentLoaded = true;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.SetMouseOverBackground();
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.isActive)
            {
                this.SetActiveBackground();
            }
            else
            {
                this.SetDefaultBackground();
            }
        }

        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Button_1.Focus();
            this.SelectedMyPlan();
        }
    }
}

