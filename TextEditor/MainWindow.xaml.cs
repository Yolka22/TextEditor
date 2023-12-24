using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            // Завантаження налаштувань шрифту з реєстру
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software\\TextEditor");

            if (key != null)
            {
                // Зчитування типу шрифту
                string fontFamily = key.GetValue("FontFamily", "Arial").ToString();
                FontFamily font = new FontFamily(fontFamily);
                txtEditor.FontFamily = font;

                // Зчитування розміру шрифту
                double fontSize = Convert.ToDouble(key.GetValue("FontSize", 20));
                txtEditor.FontSize = fontSize;

                // Зчитування кольору шрифту
                string fontColor = key.GetValue("FontColor", "Black").ToString();
                txtEditor.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(fontColor));

                key.Close();
            }
        }

        private void SaveSettings()
        {
            // Збереження налаштувань шрифту в реєстрі
            RegistryKey key = Registry.CurrentUser.CreateSubKey("Software\\TextEditor");

            if (key != null)
            {
                key.SetValue("FontFamily", txtEditor.FontFamily.ToString());
                key.SetValue("FontSize", txtEditor.FontSize.ToString());
                key.SetValue("FontColor", txtEditor.Foreground.ToString());

                key.Close();
            }
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            // Обробник події для елемента меню "Save Settings"
            SaveSettings();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Обробник події для збереження налаштувань перед закриттям вікна
            SaveSettings();
        }
    }
}