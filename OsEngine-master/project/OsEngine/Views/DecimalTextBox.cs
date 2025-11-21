using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace OsEngine.Views
{
    public class DecimalTextBox : TextBox
    {
        public DecimalTextBox()
        {
            PreviewTextInput += DecimalTextBox_PreviewTextInput;
            TextChanged += DecimalTextBox_TextChanged;
        }

        private void DecimalTextBox_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox textBox)
            {
                textBox.Select(textBox.Text.Length, 0);
            }
        }

        private void DecimalTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (char.IsDigit(e.Text, 0)
                || Text.Length == 0 && e.Text == "_"
                || e.Text == "." && Text.IndexOf(".") == -1)
            {
                e.Handled = false;
                return;
            }
            e.Handled = true;
        }
    }
}
