using System.Collections.Generic;
using System.Windows;
using YandexTranslateCSharpSdk;

namespace YandexTranslateCSharpDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string _apiKey;
        LanguagesManager langManager = new LanguagesManager();

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (_apiKey != null)
            {
                langManager.ApiKey = _apiKey;
                List<string> languages = await langManager.GetLanguages();
            }
        }

        private void detectButton_Click(object sender, RoutedEventArgs e)
        {
            if (_apiKey != null)
            {

            }
        }

        private void detectButton2_Click(object sender, RoutedEventArgs e)
        {
            if (_apiKey != null)
            {

            }
        }
    }
}
