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
using System.Runtime;
using System.Threading;
using System.Numerics;

namespace Scaffold
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string key;
        string word;

        public MainWindow()
        {
            InitializeComponent();

            RandomWord();
        }

        public void RandomWord()
        {
            Random r = new Random();
            int random = r.Next(1, 9);
            word = (string)Application.Current.FindResource(Convert.ToString(random));
            TextBox.Text = new string('*', word.Length);
        }

        public string ReplaceCharInString(string str, int index, char newSymb)
        {
            return str.Remove(index, 1).Insert(index, newSymb.ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            string symb = (string)btn.Content;
            char key = symb[0];
            if (word.Contains(key, StringComparison.InvariantCultureIgnoreCase))
            {
                int n = word.IndexOf(key, StringComparison.InvariantCultureIgnoreCase);
                string TextBoxText = TextBox.Text;
                TextBox.Text = ReplaceCharInString(TextBoxText, n, key);
            }
        }
    }
}
