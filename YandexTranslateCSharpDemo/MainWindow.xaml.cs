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

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //LanguagesManager langManager = new LanguagesManager();
            //langManager.ApiKey = _apiKey;            
            //List<string> languages = await langManager.GetLanguages();
        }
    }
}
