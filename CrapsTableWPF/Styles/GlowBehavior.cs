using System.Windows;

namespace CrapsTableWPF.Styles
{
    public static class GlowBehavior
    {
        public static readonly DependencyProperty IsGlowingProperty = 
            DependencyProperty.RegisterAttached(
                "IsGlowing",
                typeof(bool),
                typeof(GlowBehavior),
                new PropertyMetadata(false) 
                );

        public static void SetIsGlowing(DependencyObject element, bool value)
        {
            element.SetValue(IsGlowingProperty, value);
        }

        public static bool GetIsGlowing(DependencyObject element)
        {
            return (bool)element.GetValue(IsGlowingProperty);
        }
    }
}
