namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;
    using System.Collections.Generic;
    using System.Windows;
    using System.Windows.Controls;

    internal class BatteryDetailsPanel : StackPanel
    {
        private List<BatteryDetailsItem> advancedItems = new List<BatteryDetailsItem>();
        internal BatteryDetailsItem barCodingNumberItem = new BatteryDetailsItem();
        private List<BatteryDetailsItem> basicItems = new List<BatteryDetailsItem>();
        internal BatteryDetailsItem currentItem = new BatteryDetailsItem();
        internal BatteryDetailsItem cycleCountItem = new BatteryDetailsItem();
        internal BatteryDetailsItem designCapacityItem = new BatteryDetailsItem();
        internal BatteryDetailsItem designVoltageItem = new BatteryDetailsItem();
        internal BatteryDetailsItem deviceChemistryItem = new BatteryDetailsItem();
        internal BatteryDetailsItem firmwareVersion = new BatteryDetailsItem();
        internal BatteryDetailsItem firstUsedDateItem = new BatteryDetailsItem();
        internal BatteryDetailsItem fruPartNumberItem = new BatteryDetailsItem();
        internal BatteryDetailsItem fullChargeCapacityItem = new BatteryDetailsItem();
        internal BatteryDetailsItem manufacturerDateItem = new BatteryDetailsItem();
        internal BatteryDetailsItem manufacturerNameItem = new BatteryDetailsItem();
        internal BatteryDetailsItem remainingCapacityItem = new BatteryDetailsItem();
        internal BatteryDetailsItem remainingPercentageItem = new BatteryDetailsItem();
        internal BatteryDetailsItem remainingTimeItem = new BatteryDetailsItem();
        internal BatteryDetailsItem serialNumberItem = new BatteryDetailsItem();
        internal BatteryDetailsItem statusItem = new BatteryDetailsItem();
        internal BatteryDetailsItem tempratureItem = new BatteryDetailsItem();
        internal BatteryDetailsItem voltageItem = new BatteryDetailsItem();
        internal BatteryDetailsItem wattageItem = new BatteryDetailsItem();

        public BatteryDetailsPanel()
        {
            this.basicItems.Add(this.statusItem);
            this.basicItems.Add(this.remainingPercentageItem);
            this.basicItems.Add(this.remainingTimeItem);
            this.basicItems.Add(this.cycleCountItem);
            this.basicItems.Add(this.fruPartNumberItem);
            this.advancedItems.Add(this.statusItem);
            this.advancedItems.Add(this.remainingPercentageItem);
            this.advancedItems.Add(this.remainingTimeItem);
            this.advancedItems.Add(this.remainingCapacityItem);
            this.advancedItems.Add(this.fullChargeCapacityItem);
            this.advancedItems.Add(this.currentItem);
            this.advancedItems.Add(this.voltageItem);
            this.advancedItems.Add(this.wattageItem);
            this.advancedItems.Add(this.tempratureItem);
            this.advancedItems.Add(this.cycleCountItem);
            this.advancedItems.Add(this.manufacturerNameItem);
            this.advancedItems.Add(this.manufacturerDateItem);
            this.advancedItems.Add(this.firstUsedDateItem);
            this.advancedItems.Add(this.serialNumberItem);
            this.advancedItems.Add(this.barCodingNumberItem);
            this.advancedItems.Add(this.fruPartNumberItem);
            this.advancedItems.Add(this.deviceChemistryItem);
            this.advancedItems.Add(this.designCapacityItem);
            this.advancedItems.Add(this.designVoltageItem);
            this.advancedItems.Add(this.firmwareVersion);
        }

        private void AddDetailsItem(BatteryDetailsItem detailsItem)
        {
            if ((base.Children.Count % 2) == 1)
            {
                detailsItem.SetBackgroundToGray();
            }
            else
            {
                detailsItem.ResetBackground();
            }
            base.Children.Add(detailsItem);
        }

        private void AddItem()
        {
            UIElement element = base.Children[0];
            base.Children.Clear();
            base.Children.Add(element);
            if (MainWindow.Instance.IsBasic())
            {
                foreach (BatteryDetailsItem item in this.basicItems)
                {
                    this.AddDetailsItem(item);
                }
            }
            if (MainWindow.Instance.IsAdvanced())
            {
                foreach (BatteryDetailsItem item2 in this.advancedItems)
                {
                    this.AddDetailsItem(item2);
                }
            }
        }

        public void Create()
        {
            this.statusItem.SetTitle("TitleBatteryStatus");
            this.statusItem.Focusable = true;
            this.remainingPercentageItem.SetTitle("TitleRemainingPercentage");
            this.remainingPercentageItem.Focusable = true;
            this.remainingTimeItem.SetTitle("TitleRemainingTime");
            this.remainingTimeItem.Focusable = true;
            this.remainingCapacityItem.SetTitle("TitleRemainingCapacity");
            this.remainingCapacityItem.Focusable = true;
            this.fullChargeCapacityItem.SetTitle("TitleFullChargeCapacity");
            this.fullChargeCapacityItem.Focusable = true;
            this.currentItem.SetTitle("TitleCurrent");
            this.currentItem.Focusable = true;
            this.voltageItem.SetTitle("TitleVoltage");
            this.voltageItem.Focusable = true;
            this.wattageItem.SetTitle("TitleWattage");
            this.wattageItem.Focusable = true;
            this.tempratureItem.SetTitle("TitleTemperature");
            this.tempratureItem.Focusable = true;
            this.cycleCountItem.SetTitle("TitleCycleCount");
            this.cycleCountItem.Focusable = true;
            this.manufacturerNameItem.SetTitle("TitleManifacturerName");
            this.manufacturerNameItem.Focusable = true;
            this.manufacturerDateItem.SetTitle("TitleManifacturerDate");
            this.manufacturerDateItem.Focusable = true;
            this.firstUsedDateItem.SetTitle("TitleFirstUsedDate");
            this.firstUsedDateItem.Focusable = true;
            this.serialNumberItem.SetTitle("TitleSerialNumber");
            this.serialNumberItem.Focusable = true;
            this.barCodingNumberItem.SetTitle("TitleBarCodingNumber");
            this.barCodingNumberItem.Focusable = true;
            this.fruPartNumberItem.SetTitle("TitleFRUPartNumber");
            this.fruPartNumberItem.Focusable = true;
            this.deviceChemistryItem.SetTitle("TitleDeviceChemistry");
            this.deviceChemistryItem.Focusable = true;
            this.designCapacityItem.SetTitle("TitleDesignCapacity");
            this.designCapacityItem.Focusable = true;
            this.designVoltageItem.SetTitle("TitleDesignVoltage");
            this.designVoltageItem.Focusable = true;
            this.firmwareVersion.SetTitle("FirmwareVersion");
            this.firmwareVersion.Focusable = true;
        }

        public void Refresh(Battery battery, bool isBaseToTabletCharging)
        {
            this.AddItem();
            if (isBaseToTabletCharging)
            {
                this.statusItem.SetCurrentData((string) base.FindResource("TitleBaseToTablet"));
            }
            else
            {
                this.statusItem.SetCurrentData(battery.Status.ToString());
            }
            this.remainingPercentageItem.SetCurrentData(battery.RemainingPercent.ToString());
            if (battery.Status.MyStatus == BatteryStatus.Status.charge)
            {
                this.remainingTimeItem.SetTitle("TitleCompletionTime");
                this.remainingTimeItem.SetCurrentData(battery.CompletionTime.ToString());
            }
            else
            {
                this.remainingTimeItem.SetTitle("TitleRemainingTime");
                this.remainingTimeItem.SetCurrentData(battery.RemainingTime.ToString());
            }
            this.remainingCapacityItem.SetCurrentData(battery.RemainingCapacity);
            this.fullChargeCapacityItem.SetCurrentData(battery.FullChargeCapacity);
            this.currentItem.SetCurrentData(battery.Current);
            this.voltageItem.SetCurrentData(battery.Voltage);
            this.wattageItem.SetCurrentData(battery.Wattage);
            this.tempratureItem.SetCurrentData(battery.Temperature);
            this.cycleCountItem.SetCurrentData(battery.CycleCount);
            this.manufacturerNameItem.SetCurrentData(battery.ManufactureName);
            this.manufacturerDateItem.SetCurrentData(battery.ManufactureDate);
            this.firstUsedDateItem.SetCurrentData(battery.FirstUsedDate);
            this.serialNumberItem.SetCurrentData(battery.SerialNumber);
            this.barCodingNumberItem.SetCurrentData(battery.BarCodingNumber);
            this.fruPartNumberItem.SetCurrentData(battery.DeviceName);
            this.deviceChemistryItem.SetCurrentData(battery.DeviceChemistry);
            this.designCapacityItem.SetCurrentData(battery.DesignCapacity);
            this.designVoltageItem.SetCurrentData(battery.DesignVoltage);
            this.firmwareVersion.SetCurrentData(battery.FirmwareVersion);
        }
    }
}

