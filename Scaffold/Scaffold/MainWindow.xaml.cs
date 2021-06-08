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
        int vis = 0;
        string word;

        public MainWindow()
        {
            InitializeComponent();
            RandomWord();
            Show_vis();
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
        public void Show_vis()  // Показ или скрытие элементов виселицы
        {
            switch (vis)
            {
                case 1:
                    vis1.Visibility = Visibility.Visible; break;                    
                case 2:
                    vis2.Visibility = Visibility.Visible; break;
                case 3:
                    vis3.Visibility = Visibility.Visible; break;
                case 4:
                    vis4.Visibility = Visibility.Visible; break;
                case 5:
                    vis5.Visibility = Visibility.Visible; break;
                case 6:
                    vis6.Visibility = Visibility.Visible; break;
                case 7:
                    vis7.Visibility = Visibility.Visible; break;
                case 8:
                    vis8.Visibility = Visibility.Visible; break;
                case 9:
                    vis10.Visibility = Visibility.Visible;
                    vis11.Visibility = Visibility.Visible;

                    break;
                default: 
                    vis1.Visibility = Visibility.Hidden;
                    vis2.Visibility = Visibility.Hidden;
                    vis3.Visibility = Visibility.Hidden;
                    vis4.Visibility = Visibility.Hidden;
                    vis5.Visibility = Visibility.Hidden;
                    vis6.Visibility = Visibility.Hidden;
                    vis7.Visibility = Visibility.Hidden;
                    vis8.Visibility = Visibility.Hidden;
                    vis10.Visibility = Visibility.Hidden;
                    vis11.Visibility = Visibility.Hidden;
                    break; 
            }
            vis++;
        }
        
        public bool Check_letter(char key)  // Проверка буквы в слове
        {
            bool flag = false;
            if (word.Contains(key, StringComparison.InvariantCultureIgnoreCase))
            {
                int p = -1;
                do
                {
                    if (p != word.Length) p = word.IndexOf(key.ToString(), p + 1, StringComparison.InvariantCultureIgnoreCase);
                    string TextBoxText = TextBox.Text;
                    if (p != -1)    // Буква в слове присутствует
                    {
                        TextBox.Text = ReplaceCharInString(TextBoxText, p, key);
                        flag = true;
                    }
                } while (p != -1);
            }
            else
            {
                Show_vis();
            }
            return flag;
        }
        public bool Transform(object sender)
        {
            bool check;
            Button btn = (Button)sender;
            string symb = (string)btn.Content;
            char key = symb[0];
            check = Check_letter(key);
            return check;
        }

        private void Button_Click_ShowPic(object sender, RoutedEventArgs e)    // Буква А
        {
            Image img = new Image();
            img.Margin = ((Button)sender).Margin;
            img.Height = img.Width = 30;
            if (Transform(sender))
            {
                img.Source = new BitmapImage(new Uri("/1112.png", UriKind.Relative));              
            }    
            else
            {
                img.Source = new BitmapImage(new Uri("/1113.png", UriKind.Relative));
            }
            canvas.Children.Add(img);
        }
        
        private void Restart(object sender, RoutedEventArgs e)
        {
            vis = 0;
            RandomWord();
            Show_vis();
            var images = canvas.Children.OfType<Image>().ToList();
            foreach (var image in images)
            {
                canvas.Children.Remove(image);
            }
        }

        private void Settings(object sender, RoutedEventArgs e)
        {

        }
    }
}
