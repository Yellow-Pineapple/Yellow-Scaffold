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
        int points;
        int hit = 0;
        bool[] guess = new bool[20];
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
            points = 0;
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
                        guess[p] = true;
                        TextBox.Text = ReplaceCharInString(TextBoxText, p, key);
                        flag = true;
                        hit++;
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
                points++;
            }
            else
            {
                img.Source = new BitmapImage(new Uri("/1113.png", UriKind.Relative));
                points--;
            }
            canvas.Children.Add(img);
            if (vis == 10) Game_Over_Message();
            if (hit == word.Length) Win_Message();
        }

        private void Game_Over_Message()
        {
            MessageBoxResult messageDialog = MessageBox.Show("Game Over! The word was: " + word + "\nWant to play some more?", ":(", MessageBoxButton.YesNo);
            if (messageDialog == MessageBoxResult.No) Environment.Exit(0);
            if (messageDialog == MessageBoxResult.Yes) Restart(null, null);
        }

        private void Win_Message()
        {
            MessageBoxResult messageDialog = MessageBox.Show("You win! The amount of your points is: " + points + "\nWant to play some more?", ":)", MessageBoxButton.YesNo);
            if (messageDialog == MessageBoxResult.No) Environment.Exit(0);
            if (messageDialog == MessageBoxResult.Yes) Restart(null, null);
        }

        private void Restart(object sender, RoutedEventArgs e)
        {
            vis = 0;
            hit = 0;
            fer1.Visibility = Visibility.Hidden;
            RandomWord();
            Scaff_Imgs_Disappear();
            var images = canvas.Children.OfType<Image>().ToList();
            foreach (var image in images)
            {
                canvas.Children.Remove(image);
            }
            var buttons = MainGrid.Children.OfType<Button>().ToList();
            foreach (var button in buttons)
            {
                button.IsEnabled = true;
            }
            for (int i=0; i<guess.Length; i++)
            {
                guess[i] = false;
            }
        }

        private void Settings(object sender, RoutedEventArgs e)
        {

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int l = word.Length;
            Random letter = new Random();
            int random = letter.Next(0, word.Length);
            while (guess[random] == true)
            {
                random = letter.Next(0, word.Length);
            }
            // Проверка, что весь массив трушный, чтобы не зависала прога
            Check_letter(word[random]);
        }
    }
}
