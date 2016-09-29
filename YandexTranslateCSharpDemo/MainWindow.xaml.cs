using Microsoft.Win32;
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
        YandexTranslateSdk wrapper = new YandexTranslateSdk();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            object key = Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\YandexTranslateCSharpDemo",
                                    "ApiKey", null);
            if (key != null)
            {
                apiKey = key.ToString();
            }
            LoadLanguages();
        }

        private async void detectButton_Click(object sender, RoutedEventArgs e)
        {
            if (apiKey != null && !string.IsNullOrEmpty(text1.Text))
            {
                wrapper.ApiKey = apiKey;
                string lang = await wrapper.DetectLanguage(text1.Text);
                languagesCombo.SelectedValue = lang;
            }
        }

        private async void detectButton2_Click(object sender, RoutedEventArgs e)
        {
            if (apiKey != null && !string.IsNullOrEmpty(text2.Text))
            {
                wrapper.ApiKey = apiKey;
                string lang = await wrapper.DetectLanguage(text2.Text);
                languagesCombo2.SelectedValue = lang;
            }
        }

        private void setKeyItem_Click(object sender, RoutedEventArgs e)
        {
            SetKeyWindow setKeyWindow = new SetKeyWindow(apiKey);
            setKeyWindow.Owner = this;
            if (setKeyWindow.ShowDialog() == true)
            {
                apiKey = setKeyWindow.GetKey();
                LoadLanguages();
                Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\YandexTranslateCSharpDemo", "ApiKey", apiKey);
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
                wrapper.ApiKey = apiKey;
                string direction = languagesCombo.SelectedValue + "-" + languagesCombo2.SelectedValue;
                string translatedText = await wrapper.TranslateText(text1.Text, direction);
                text2.Text = translatedText;
            }
        }

        private async void LoadLanguages()
        {
            if (apiKey != null)
            {
                wrapper.ApiKey = apiKey;
                List<string> languages;
                try
                {
                    languages = await wrapper.GetLanguages();
                }
                catch(YandexTranslateException ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                languagesCombo.ItemsSource = languagesCombo2.ItemsSource = languages;
                if (languagesCombo.Items.Count > 0)
                {
                    languagesCombo.SelectedIndex = 0;
                    languagesCombo2.SelectedIndex = 0;
                }
            }
        }
    }
}
