using Microsoft.Xaml.Behaviors;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace KaffeemaschineWPF.Behaviors
{
    public class TextBoxBehavior : Behavior<TextBox>
    {



        public bool AllowWhiteSpaces
        {
            get { return (bool)GetValue(AllowWhiteSpacesProperty); }
            set { SetValue(AllowWhiteSpacesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AllowWhiteSpaces.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AllowWhiteSpacesProperty =
            DependencyProperty.Register(nameof(AllowWhiteSpaces), typeof(bool), typeof(TextBox), new PropertyMetadata(true));


        public string RegExPattern
        {
            get { return (string)GetValue(RegExPatternProperty); }
            set { SetValue(RegExPatternProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RegExPattern.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RegExPatternProperty =
            DependencyProperty.Register(nameof(RegExPattern), typeof(string), typeof(TextBox), new PropertyMetadata(null));


        protected override void OnAttached()
        {
            AssociatedObject.PreviewTextInput += AssociatedObject_PreviewTextInput;
            if(!AllowWhiteSpaces)
                AssociatedObject.PreviewKeyDown += AssociatedObject_PreviewKeyDown;
            base.OnAttached();
        }

        private void AssociatedObject_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, RegExPattern);
        }
        private void AssociatedObject_PreviewKeyDown(object sender, KeyEventArgs e)
            => e.Handled = e.Key == Key.Space;

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewTextInput -= AssociatedObject_PreviewTextInput;
            if (!AllowWhiteSpaces)
                AssociatedObject.PreviewKeyDown -= AssociatedObject_PreviewKeyDown;
        }

    }
}
