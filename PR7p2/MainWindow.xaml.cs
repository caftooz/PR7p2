using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PR7p2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Проверяет поля ввода и возвращает false с сообщением об ошибке при невалидных данных.
        /// </summary>
        private bool TryGetInputs(out string text, out int rows, out int cols)
        {
            text = string.Empty;
            rows = 0;
            cols = 0;

            text = InputTextBox.Text;
            if (string.IsNullOrEmpty(text))
            {
                ShowError("Введите текст для шифрования или дешифрования.");
                HighlightError(InputTextBox);
                return false;
            }

            if (!int.TryParse(RowsTextBox.Text, out rows))
            {
                ShowError("Введите целое положительное число для количества строк.");
                HighlightError(RowsTextBox);
                return false;
            }
            if (rows <= 0)
            {
                ShowError("Количество строк должно быть больше нуля.");
                HighlightError(RowsTextBox);
                return false;
            }

            if (!int.TryParse(ColsTextBox.Text, out cols))
            {
                ShowError("Введите целое положительное число для количества столбцов.");
                HighlightError(ColsTextBox);
                return false;
            }
            if (cols <= 0)
            {
                ShowError("Количество столбцов должно быть больше нуля.");
                HighlightError(ColsTextBox);
                return false;
            }

            if (rows * cols < text.Length)
            {
                ShowError($"Размер матрицы ({rows * cols}) меньше длины текста ({text.Length}). Увеличьте rows или cols.");
                HighlightError(RowsTextBox);
                HighlightError(ColsTextBox);
                return false;
            }

            ClearError();
            return true;
        }

        private void ShowError(string message)
        {
            ErrorTextBlock.Text = message;
            ErrorTextBlock.Visibility = Visibility.Visible;
        }

        private void ClearError()
        {
            ErrorTextBlock.Text = string.Empty;
            ErrorTextBlock.Visibility = Visibility.Collapsed;
            ResetHighlight(InputTextBox);
            ResetHighlight(RowsTextBox);
            ResetHighlight(ColsTextBox);
        }

        private void HighlightError(TextBox textBox)
        {
            textBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0xC0, 0x39, 0x2B));
            textBox.BorderThickness = new Thickness(1.5);
        }

        private void ResetHighlight(TextBox textBox)
        {
            textBox.BorderBrush = new SolidColorBrush(Color.FromRgb(0xAA, 0xAA, 0xAA));
            textBox.BorderThickness = new Thickness(1);
        }

        private void EncryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!TryGetInputs(out string text, out int rows, out int cols))
                return;

            try
            {
                string result = MatrixCipher.Encrypt(text, rows, cols);
                ResultTextBox.Text = result;
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка шифрования: {ex.Message}");
            }
        }

        private void DecryptButton_Click(object sender, RoutedEventArgs e)
        {
            if (!TryGetInputs(out string text, out int rows, out int cols))
                return;

            if (text.Length != rows * cols)
            {
                ShowError($"Для дешифрования длина текста ({text.Length}) должна точно совпадать с размером матрицы ({rows * cols}).");
                return;
            }

            try
            {
                string result = MatrixCipher.Decrypt(text, rows, cols);
                ResultTextBox.Text = result;
            }
            catch (Exception ex)
            {
                ShowError($"Ошибка дешифрования: {ex.Message}");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            InputTextBox.Clear();
            RowsTextBox.Clear();
            ColsTextBox.Clear();
            ResultTextBox.Clear();
            ClearError();
        }
    }
}