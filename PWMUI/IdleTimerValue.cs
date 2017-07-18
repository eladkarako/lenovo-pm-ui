namespace PWMUI
{
    using Lenovo.PowerManager.PWMUICtl;
    using System;

    public class IdleTimerValue : ValueWithRange
    {
        public const uint IMMEDIATELY = 0;
        private uint min;
        public const uint ONE_MINUTE = 1;
        private string unitImmediately = ((string) Application.Current.FindResource("IdleTimerImmediately"));
        private string unitMin = ((string) Application.Current.FindResource("UnitMinute"));
        private string unitMins = ((string) Application.Current.FindResource("UnitMinutes"));

        public override uint DecreaseCurrentValue(uint value)
        {
            if (value <= this.GetMin())
            {
                return this.GetMin();
            }
            return (value - 1);
        }

        public override uint GetMax() => 
            0x4444444;

        public override uint GetMin() => 
            this.min;

        public override uint IncreaseCurrentValue(uint value)
        {
            if (value >= this.GetMax())
            {
                return this.GetMax();
            }
            return (value + 1);
        }

        public override void SetMax(uint max)
        {
        }

        public override void SetMin(uint min)
        {
            this.min = min;
        }

        public override void SetUint(string unit)
        {
        }

        public override string ToString(uint value)
        {
            if (value == 0)
            {
                return this.unitImmediately;
            }
            string unitMins = this.unitMins;
            if (value == 1)
            {
                unitMins = this.unitMin;
            }
            return $"{value} {unitMins}";
        }
    }
}

