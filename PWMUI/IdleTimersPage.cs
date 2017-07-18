namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;
    using System.Windows.Navigation;

    public class IdleTimersPage : PMPageFunction, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        internal Button backBtn;
        internal Button cancelBtn;
        protected bool Disposed;
        internal Grid footer;
        internal IdleTimersPanel idleTimersPanel;
        internal Grid LayoutRoot;
        private LowerBrightnessChecker lowerBrightnessChecker;
        internal Button nextBtn;
        internal Grid settingsGrid;
        private CreatePlanWizardStartParam startParam;
        internal Grid titleGrid;

        public IdleTimersPage(CreatePlanWizardStartParam startParam, SliderPlanItem brightnessPlanItem)
        {
            this.InitializeComponent();
            this.startParam = startParam;
            this.startParam.IdleTimersState.SetStateToDefault();
            this.lowerBrightnessChecker = new LowerBrightnessChecker(brightnessPlanItem, this.idleTimersPanel.lowerBrightnessTimePlanItem, this.idleTimersPanel.lowerBrightnessPlanItem);
            this.idleTimersPanel.Create(this.startParam.CreatePlan);
            this.idleTimersPanel.Refresh(this.startParam.CreatePlan);
            this.lowerBrightnessChecker.CheckManually();
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            this.startParam.IdleTimersState.SetStateToDefault();
            this.OnReturn(new ReturnEventArgs<CreatePlanWizardResult>(CreatePlanWizardResult.Backed));
            base.NavigationService.GoBack();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.OnReturn(new ReturnEventArgs<CreatePlanWizardResult>(CreatePlanWizardResult.Canceled));
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

        ~IdleTimersPage()
        {
            this.Dispose(false);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/idletimerspage.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            this.startParam.IdleTimersState.SetStateToDone();
            PowerPlan createPlan = this.startParam.CreatePlan;
            this.idleTimersPanel.Apply(ref createPlan);
            EventsPage root = new EventsPage(this.startParam);
            root.Return += new ReturnEventHandler<CreatePlanWizardResult>(this.page_Return);
            base.NavigationService.Navigate(root);
        }

        private void page_Return(object sender, ReturnEventArgs<CreatePlanWizardResult> e)
        {
            if (((CreatePlanWizardResult) e.Result) == CreatePlanWizardResult.Backed)
            {
                this.startParam.IdleTimersState.SetStateToNow();
                base.SetFirstFocus();
            }
            else
            {
                this.OnReturn(e);
            }
        }

        protected override void Start()
        {
            base.Start();
            this.startParam.IdleTimersState.SetStateToNow();
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), EditorBrowsable(EditorBrowsableState.Never), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 2:
                    this.titleGrid = (Grid) target;
                    return;

                case 3:
                    this.settingsGrid = (Grid) target;
                    return;

                case 4:
                    this.idleTimersPanel = (IdleTimersPanel) target;
                    return;

                case 5:
                    this.footer = (Grid) target;
                    return;

                case 6:
                    this.backBtn = (Button) target;
                    this.backBtn.Click += new RoutedEventHandler(this.backBtn_Click);
                    return;

                case 7:
                    this.nextBtn = (Button) target;
                    this.nextBtn.Click += new RoutedEventHandler(this.nextBtn_Click);
                    return;

                case 8:
                    this.cancelBtn = (Button) target;
                    this.cancelBtn.Click += new RoutedEventHandler(this.cancelBtn_Click);
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

