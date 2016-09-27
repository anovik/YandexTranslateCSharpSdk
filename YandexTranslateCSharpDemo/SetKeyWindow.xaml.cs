using System.Windows;

namespace YandexTranslateCSharpDemo
{
    /// <summary>
    /// Window for setting API key
    /// </summary>
    public partial class SetKeyWindow : Window
    {
        public SetKeyWindow(string apiKey)
        {
            InitializeComponent();
            keyTextBox.Text = apiKey;
            keyTextBox.Focus();
        }

        public string GetKey()
        {
            return keyTextBox.Text;
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(keyTextBox.Text))
            {
                return;
            }
            DialogResult = true;
            Close();
        }
    }
}
