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

    public class AdvancedOptionsPanel : System.Windows.Controls.UserControl, IDisposable, IComponentConnector
    {
        private bool _contentLoaded;
        internal AccessText AccessText_1;
        internal AccessText AccessText_2;
        internal AccessText AccessText_3;
        internal AccessText AccessText_4;
        internal AccessText AccessText_5;
        internal PMComboBox cdromSpeed;
        private CDROMSpeedItem cdromSpeedItem = new CDROMSpeedItem();
        internal Label cdromSpeedTitle;
        internal PMComboBox cpu;
        private CPUItem cpuItem = new CPUItem();
        internal Label cpuTitle;
        protected bool Disposed;
        internal Grid LayoutRoot;
        internal PMComboBox pciBus;
        private PCIBusItem pciBusItem = new PCIBusItem();
        internal Label pciBusTitle;
        internal Label pciExpressBusTitle;
        internal TextBlock powerManagementTitle;
        private int rowPosition;
        internal PMComboBox undockAction;
        private UndockActionItem undockActionItem = new UndockActionItem();
        internal Label undockActionTitle;
        internal AdvancedOptionsPanel UserControl;

        public AdvancedOptionsPanel()
        {
            this.InitializeComponent();
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        internal Delegate _CreateDelegate(Type delegateType, string handler) => 
            Delegate.CreateDelegate(delegateType, this, handler);

        public void Create()
        {
            this.undockAction.SetSettableValue(this.undockActionItem.GetSettableValueDC());
            this.cdromSpeed.SetSettableValue(this.cdromSpeedItem.GetSettableValueDC());
            this.cpu.SetSettableValue(this.cpuItem.GetSettableValueDC());
            this.pciBus.SetSettableValue(this.pciBusItem.GetSettableValueDC());
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

        ~AdvancedOptionsPanel()
        {
            this.Dispose(false);
        }

        [GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        public void InitializeComponent()
        {
            if (!this._contentLoaded)
            {
                this._contentLoaded = true;
                Uri resourceLocator = new Uri("/PWMUI;component/advancedoptionspanel.xaml", UriKind.Relative);
                Application.LoadComponent(this, resourceLocator);
            }
        }

        public void Refresh(AdvancedOptions grp)
        {
            this.rowPosition = 2;
            this.RefreshUndockAction(grp);
            this.RefreshCDROMSpeed(grp);
            Grid.SetRow(this.powerManagementTitle, this.rowPosition);
            this.rowPosition++;
            this.RefreshCPU(grp);
            this.RefreshPCIBus(grp);
            if (!this.cpuItem.IsCapable() && !this.pciBusItem.IsCapable())
            {
                this.powerManagementTitle.Visibility = Visibility.Hidden;
            }
            else
            {
                this.powerManagementTitle.Visibility = Visibility.Visible;
            }
        }

        private void RefreshCDROMSpeed(AdvancedOptions grp)
        {
            if (this.cdromSpeedItem.IsCapable())
            {
                this.cdromSpeedTitle.Visibility = Visibility.Visible;
                this.cdromSpeed.Visibility = Visibility.Visible;
                Grid.SetRow(this.cdromSpeedTitle, this.rowPosition);
                Grid.SetRow(this.cdromSpeed, this.rowPosition);
                this.rowPosition++;
            }
            else
            {
                this.cdromSpeedTitle.Visibility = Visibility.Hidden;
                this.cdromSpeed.Visibility = Visibility.Hidden;
            }
            this.cdromSpeed.SetCurrentValue(grp.ValueOfCDROMSpeed);
        }

        private void RefreshCPU(AdvancedOptions grp)
        {
            if (this.cpuItem.IsCapable())
            {
                this.cpuTitle.Visibility = Visibility.Visible;
                this.cpu.Visibility = Visibility.Visible;
                Grid.SetRow(this.cpuTitle, this.rowPosition);
                Grid.SetRow(this.cpu, this.rowPosition);
                this.rowPosition++;
            }
            else
            {
                this.cpuTitle.Visibility = Visibility.Hidden;
                this.cpu.Visibility = Visibility.Hidden;
            }
            this.cpu.SetCurrentValue(grp.ValueOfCPU);
        }

        private void RefreshPCIBus(AdvancedOptions grp)
        {
            if (this.pciBusItem.IsCapable())
            {
                this.pciBus.Visibility = Visibility.Visible;
                if (this.pciBusItem.IsPCIExpress())
                {
                    this.pciExpressBusTitle.Visibility = Visibility.Visible;
                    this.pciBusTitle.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.pciExpressBusTitle.Visibility = Visibility.Hidden;
                    this.pciBusTitle.Visibility = Visibility.Visible;
                }
                Grid.SetRow(this.pciBusTitle, this.rowPosition);
                Grid.SetRow(this.pciExpressBusTitle, this.rowPosition);
                Grid.SetRow(this.pciBus, this.rowPosition);
                this.rowPosition++;
            }
            else
            {
                this.pciBusTitle.Visibility = Visibility.Hidden;
                this.pciExpressBusTitle.Visibility = Visibility.Hidden;
                this.pciBus.Visibility = Visibility.Hidden;
            }
            this.pciBus.SetCurrentValue(grp.ValueOfPCIBus);
        }

        private void RefreshUndockAction(AdvancedOptions grp)
        {
            if (this.undockActionItem.IsCapable())
            {
                this.undockActionTitle.Visibility = Visibility.Visible;
                this.undockAction.Visibility = Visibility.Visible;
                Grid.SetRow(this.undockActionTitle, this.rowPosition);
                Grid.SetRow(this.undockAction, this.rowPosition);
                this.rowPosition++;
            }
            else
            {
                this.undockActionTitle.Visibility = Visibility.Hidden;
                this.undockAction.Visibility = Visibility.Hidden;
            }
            this.undockAction.SetCurrentValue(grp.ValueOfUndockAction);
        }

        public void Save(ref AdvancedOptions grp)
        {
            grp.ValueOfUndockAction = this.undockAction.GetCurrentValue();
            grp.ValueOfCDROMSpeed = this.cdromSpeed.GetCurrentValue();
            grp.ValueOfCPU = this.cpu.GetCurrentValue();
            grp.ValueOfPCIBus = this.pciBus.GetCurrentValue();
        }

        [EditorBrowsable(EditorBrowsableState.Never), GeneratedCode("PresentationBuildTasks", "4.0.0.0"), DebuggerNonUserCode]
        void IComponentConnector.Connect(int connectionId, object target)
        {
            switch (connectionId)
            {
                case 1:
                    this.UserControl = (AdvancedOptionsPanel) target;
                    return;

                case 2:
                    this.LayoutRoot = (Grid) target;
                    return;

                case 3:
                    this.undockActionTitle = (Label) target;
                    return;

                case 4:
                    this.AccessText_1 = (AccessText) target;
                    return;

                case 5:
                    this.undockAction = (PMComboBox) target;
                    return;

                case 6:
                    this.cdromSpeedTitle = (Label) target;
                    return;

                case 7:
                    this.AccessText_2 = (AccessText) target;
                    return;

                case 8:
                    this.cdromSpeed = (PMComboBox) target;
                    return;

                case 9:
                    this.powerManagementTitle = (TextBlock) target;
                    return;

                case 10:
                    this.cpuTitle = (Label) target;
                    return;

                case 11:
                    this.AccessText_3 = (AccessText) target;
                    return;

                case 12:
                    this.cpu = (PMComboBox) target;
                    return;

                case 13:
                    this.pciExpressBusTitle = (Label) target;
                    return;

                case 14:
                    this.AccessText_4 = (AccessText) target;
                    return;

                case 15:
                    this.pciBusTitle = (Label) target;
                    return;

                case 0x10:
                    this.AccessText_5 = (AccessText) target;
                    return;

                case 0x11:
                    this.pciBus = (PMComboBox) target;
                    return;
            }
            this._contentLoaded = true;
        }
    }
}

