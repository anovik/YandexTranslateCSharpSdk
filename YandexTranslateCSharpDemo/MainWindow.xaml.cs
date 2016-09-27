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
        string apiKey;
        LanguagesManager langManager = new LanguagesManager();
        DetectLanguageManager detectManager = new DetectLanguageManager();
        TranslateManager translateManager = new TranslateManager();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
             LoadLanguages();
        }

        private async void detectButton_Click(object sender, RoutedEventArgs e)
        {
            if (apiKey != null && !string.IsNullOrEmpty(text1.Text))
            {
                detectManager.ApiKey = apiKey;
                string lang = await detectManager.DetectLanguage(text1.Text);
                languagesCombo.SelectedValue = lang;
            }
        }

        private async void detectButton2_Click(object sender, RoutedEventArgs e)
        {
            if (apiKey != null && !string.IsNullOrEmpty(text2.Text))
            {
                detectManager.ApiKey = apiKey;
                string lang = await detectManager.DetectLanguage(text2.Text);
                languagesCombo2.SelectedValue = lang;
            }
        }

        private void setKeyItem_Click(object sender, RoutedEventArgs e)
        {
            SetKeyWindow setKeyWindow = new SetKeyWindow(apiKey);
            if (setKeyWindow.ShowDialog() == true)
            {
                apiKey = setKeyWindow.GetKey();
                LoadLanguages();
            }
        }

        private void closeItem_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private async void translateButton_Click(object sender, RoutedEventArgs e)
        {
            if (apiKey != null && !string.IsNullOrEmpty(text1.Text))
            {
                translateManager.ApiKey = apiKey;
                string direction = languagesCombo.SelectedValue + "-" + languagesCombo2.SelectedValue;
                string translatedText = await translateManager.TranslateText(text1.Text, direction);
                text2.Text = translatedText;
            }
        }

        private async void LoadLanguages()
        {
            if (apiKey != null)
            {
                langManager.ApiKey = apiKey;
                List<string> languages = await langManager.GetLanguages();
                languagesCombo.ItemsSource = languagesCombo2.ItemsSource = languages;
            }
        }
    }
}
