namespace PWMUI
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Windows.Shapes;

    internal class EffectImage : Control
    {
        public const int MAX_LEVEL = 7;

        public void AddToPanel(StackPanel target, uint level)
        {
            for (uint i = 0; i < level; i++)
            {
                target.Children.Add(this.CreateOnImage());
            }
            for (uint j = 0; j < (7 - level); j++)
            {
                target.Children.Add(this.CreateOffImage());
            }
        }

        private Rectangle CreateOffImage() => 
            new Rectangle { 
                Height = 6.0,
                Width = 6.0,
                Margin = new Thickness(6.0, 0.0, 6.0, 0.0),
                Fill = (Brush) base.FindResource("img_EffectOfSettings_Off_design")
            };

        private Rectangle CreateOnImage() => 
            new Rectangle { 
                Height = 14.0,
                Width = 14.0,
                Margin = new Thickness(0.0, 0.0, 4.0, 0.0),
                Fill = (Brush) base.FindResource("img_EffectOfSettings_On_design")
            };
    }
}

