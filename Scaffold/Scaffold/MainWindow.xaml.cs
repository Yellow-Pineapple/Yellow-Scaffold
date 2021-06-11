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
        string word;
        int vis;
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

        public void Scaff_Imgs_Appear(int number)
        {
            foreach (Image img in canvas_vis.Children)
            {
                if (img.Name.Contains(Convert.ToString(number)))
                {
                    img.Visibility = Visibility.Visible;
                }
            }
        }

        public void Scaff_Imgs_Disappear()
        {
            foreach (Image img in canvas_vis.Children)
            {
                img.Visibility = Visibility.Hidden;
            }
        }

        public string ReplaceCharInString(string str, int index, char newSymb)
        {
            return str.Remove(index, 1).Insert(index, newSymb.ToString());
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
                Scaff_Imgs_Appear(vis);
                vis++;
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
            ((Button)sender).IsEnabled = false;
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
            if (vis == 10)
            {
                MessageBoxResult messageDialog = MessageBox.Show("Game Over!\nWant to play some more?", ":(", MessageBoxButton.YesNo);
                if (messageDialog == MessageBoxResult.No)
                    Environment.Exit(0);
                if (messageDialog == MessageBoxResult.Yes)
                    Restart(null, null);
            }
        }
        private void Restart(object sender, RoutedEventArgs e)
        {
            vis = 0;
            RandomWord();
            Scaff_Imgs_Disappear();
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
