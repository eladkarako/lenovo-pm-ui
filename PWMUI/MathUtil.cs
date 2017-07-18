namespace PWMUI
{
    using System;

    public static class MathUtil
    {
        public static int IncrementDecrementNumber(string num, int minValue, int maxVal, bool increment)
        {
            int num2 = ValidateNumber(num, minValue, maxVal);
            if (increment)
            {
                if (num2 < maxVal)
                {
                    return Math.Min(num2 + 1, maxVal);
                }
                return minValue;
            }
            if (num2 > 0)
            {
                return Math.Max(num2 - 1, 0);
            }
            return maxVal;
        }

        public static int ValidateNumber(int newNum, int minValue, int maxValue)
        {
            newNum = Math.Max(newNum, minValue);
            newNum = Math.Min(newNum, maxValue);
            return newNum;
        }

        public static int ValidateNumber(string newNum, int minValue, int maxValue)
        {
            int num;
            if (!int.TryParse(newNum, out num))
            {
                return 0;
            }
            return ValidateNumber(num, minValue, maxValue);
        }
    }
}

