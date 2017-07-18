namespace PWMUI
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class PMFixedWidthColumn : GridViewColumn
    {
        public static readonly DependencyProperty FixedWidthProperty = DependencyProperty.Register("FixedWidth", typeof(double), typeof(PMFixedWidthColumn), new FrameworkPropertyMetadata((double) 1.0 / (double) 0.0, new PropertyChangedCallback(PMFixedWidthColumn.OnFixedWidthChanged)));

        static PMFixedWidthColumn()
        {
            GridViewColumn.WidthProperty.OverrideMetadata(typeof(PMFixedWidthColumn), new FrameworkPropertyMetadata(null, new CoerceValueCallback(PMFixedWidthColumn.OnCoerceWidth)));
        }

        private static object OnCoerceWidth(DependencyObject o, object baseValue)
        {
            PMFixedWidthColumn column = o as PMFixedWidthColumn;
            if (column != null)
            {
                return column.FixedWidth;
            }
            return baseValue;
        }

        private static void OnFixedWidthChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            PMFixedWidthColumn column = o as PMFixedWidthColumn;
            if (column != null)
            {
                column.CoerceValue(GridViewColumn.WidthProperty);
            }
        }

        public double FixedWidth
        {
            get => 
                ((double) base.GetValue(FixedWidthProperty));
            set
            {
                base.SetValue(FixedWidthProperty, value);
            }
        }
    }
}

