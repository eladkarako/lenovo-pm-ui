﻿namespace PWMUI
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

    public class EventsPage : PMPageFunction, IComponentConnector
    {
        private bool _contentLoaded;
        internal Button backBtn;
        internal Button cancelBtn;
        internal EventsPanel eventsPanel;
        internal Grid footer;
        internal Grid LayoutRoot;
        internal Button nextBtn;
        internal Grid settingsGrid;
        private CreatePlanWizardStartParam startParam;
        internal Grid titleGrid;

        public EventsPage(CreatePlanWizardStartParam startParam)
        {
            this.InitializeComponent();
            this.startParam = startParam;
            this.startParam.EventsState.SetStateToDefault();
            this.eventsPanel.Create(this.startParam.CreatePlan);
            this.eventsPanel.Refresh(this.startParam.CreatePlan);
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            this.startParam.EventsState.SetStateToDefault();
            this.OnReturn(new ReturnEventArgs<CreatePlanWizardResult>(CreatePlanWizardResult.Backed));
            base.NavigationService.GoBack();
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.OnReturn(new ReturnEventArgs<CreatePlanWizardResult>(CreatePlanWizardResult.Canceled));
        }

        [DebuggerNonUserCode, GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/eventspage.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            this.startParam.EventsState.SetStateToDone();
            PowerPlan createPlan = this.startParam.CreatePlan;
            this.eventsPanel.Apply(ref createPlan);
            AlarmsPage root = new AlarmsPage(this.startParam);
            root.Return += new ReturnEventHandler<CreatePlanWizardResult>(this.page_Return);
            base.NavigationService.Navigate(root);
        }

        private void page_Return(object sender, ReturnEventArgs<CreatePlanWizardResult> e)
        {
            if (((CreatePlanWizardResult) e.Result) == CreatePlanWizardResult.Backed)
            {
                this.startParam.EventsState.SetStateToNow();
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
            this.startParam.EventsState.SetStateToNow();
        }

        [DebuggerNonUserCode, EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
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
                    this.eventsPanel = (EventsPanel) target;
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

