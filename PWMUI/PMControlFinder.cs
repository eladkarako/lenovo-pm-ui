namespace PWMUI
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class PMControlFinder
    {
        public static childItem FindVisualChild<childItem>(DependencyObject obj) where childItem: DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if ((child != null) && (child is childItem))
                {
                    return (childItem) child;
                }
                childItem local = FindVisualChild<childItem>(child);
                if (local != null)
                {
                    return local;
                }
            }
            return default(childItem);
        }
    }
}

