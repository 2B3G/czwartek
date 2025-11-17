using AlghoritmsShowcase;
using System;
using System.Windows;
using System.Windows.Controls;

namespace AlghoritmsShowcase
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MethodBox.ItemsSource = new string[]
            {
                "Caesar",
                "Columnar",
                "ROT13",
                "XOR"
            };

            MethodBox.SelectedIndex = 0;
            MethodBox.SelectionChanged += MethodBox_SelectionChanged;

            UpdateKeyBoxState();
        }

        private void MethodBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateKeyBoxState();
        }

        private void UpdateKeyBoxState()
        {
            string method = MethodBox.SelectedItem?.ToString() ?? "";
            bool keyNeeded = method != "ROT13";

            KeyBox.IsEnabled = keyNeeded;

            if (!keyNeeded)
            {
                KeyBox.Text = "";
                KeyBox.Background = System.Windows.Media.Brushes.LightGray;
            }
            else
            {
                KeyBox.Background = System.Windows.Media.Brushes.White;
            }
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            Process(true);
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            Process(false);
        }

        private void Process(bool encrypt)
        {
            string input = InputBox.Text ?? "";
            string method = MethodBox.SelectedItem?.ToString() ?? "";
            string result = "";

            int key = 0;

            bool keyNeeded = method != "ROT13";

            if (keyNeeded && !int.TryParse(KeyBox.Text, out key))
            {
                MessageBox.Show("Podaj poprawny klucz liczbowy.");
                return;
            }

            try
            {
                switch (method)
                {
                    case "Caesar":
                        result = encrypt
                            ? CipherUtils.CaesarEncrypt(input, key)
                            : CipherUtils.CaesarDecrypt(input, key);
                        break;

                    case "Columnar":
                        result = encrypt
                            ? CipherUtils.ColumnarEncrypt(input, key)
                            : CipherUtils.ColumnarDecrypt(input, key);
                        break;

                    case "ROT13":
                        result = CipherUtils.ROT13(input);
                        break;

                    case "XOR":
                        result = encrypt
                            ? CipherUtils.XOREncrypt(input, key)
                            : CipherUtils.XORDecrypt(input, key);
                        break;
                }

                OutputBox.Text = result;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd: " + ex.Message);
            }
        }
    }
}
