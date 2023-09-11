using System.Windows;
using System.Windows.Controls;

namespace Tasks.CustomStyle
{
    public class TextBoxPlaceholder : TextBox
    {
        public string Placeholder
        {
            get { return (string)GetValue(PlaceholderProperty); }
            set { SetValue(PlaceholderProperty, value); }
        }
        public static readonly DependencyProperty PlaceholderProperty =
            DependencyProperty.Register("Placeholder", typeof(string), typeof(TextBoxPlaceholder), new PropertyMetadata(string.Empty));

        public bool IsEmpty
        {
            get { return (bool)GetValue(IsEmptyProperty); }
            set { SetValue(IsEmptyPropertyKey, value); }
        }

        // DependencyPropertyKey as the backing store for IsEmpty.
        private static readonly DependencyPropertyKey IsEmptyPropertyKey =
            DependencyProperty.RegisterReadOnly("IsEmpty", typeof(bool), typeof(TextBoxPlaceholder), new PropertyMetadata(true));
        // DependencyProperty as the backing store for IsEmpty.
        public static readonly DependencyProperty IsEmptyProperty = IsEmptyPropertyKey.DependencyProperty;

        static TextBoxPlaceholder()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxPlaceholder), new FrameworkPropertyMetadata(typeof(TextBoxPlaceholder)));
        }

        protected override void OnTextChanged(TextChangedEventArgs e)
        {
            IsEmpty = string.IsNullOrEmpty(Text);
        }
    }
}
