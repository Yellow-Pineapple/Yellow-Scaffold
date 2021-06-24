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
using System.IO;

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
        bool hint;
        int hint_counter;
        int[] randomarray;
        int coin = 0;

        public MainWindow()
        {
            InitializeComponent();
            MainGrid.Visibility = Visibility.Hidden;        
            //In_Coin();
            
        }

        private void RandomWord(object sender, RoutedEventArgs e)
        {
            MainGrid.Visibility = Visibility.Hidden;
            CategoryGrid.Visibility = Visibility.Hidden;
            int letters_count = 1;
            Button button = (Button)sender;
            string f = Convert.ToString(button.Name[1]);
            int k = Convert.ToInt32(f);
            Random rand = new Random();
            if (k == 0) k = rand.Next(1, 4);

            var myRD = new ResourceDictionary();
            switch (k)
            {
                case 1:
                    {
                        myRD.Source = new Uri("/Resourses/Dictionary1.xaml", UriKind.RelativeOrAbsolute);
                        letters_count = 74;
                        break;
                    }
                case 2:
                    {
                        myRD.Source = new Uri("/Resourses/Dictionary2.xaml", UriKind.RelativeOrAbsolute);
                        letters_count = 32;
                        break;
                    }
                case 3:
                    {
                        myRD.Source = new Uri("/Resourses/Dictionary3.xaml", UriKind.RelativeOrAbsolute);
                        letters_count = 36;
                        break;
                    }
                case 4:
                    {
                        myRD.Source = new Uri("/Resourses/Dictionary4.xaml", UriKind.RelativeOrAbsolute);
                        letters_count = 112;
                        break;
                    }
            }

            k = rand.Next(1, letters_count);
            word = myRD[k.ToString()].ToString();

            TextBox.Text = new string('*', word.Length);
            points = 0;
            hint = true;

            MainGrid.Visibility = Visibility.Visible;
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

        private void Button_Click_ShowPic(object sender, RoutedEventArgs e)
        {
            //int miss = 0;
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
            if (hit == word.Length)
            {
                coin++;
                Out_Coin();
                Win_Message();               
            }
        }

        private void Game_Over_Message()
        {
            MessageBoxResult messageDialog = MessageBox.Show("Вы проиграли!\nЗагаданное слово: " + word + "\nХотите сыграть еще?", ":(", MessageBoxButton.YesNo);
            if (messageDialog == MessageBoxResult.No) Environment.Exit(0);
            if (messageDialog == MessageBoxResult.Yes) Restart(null, null);
        }

        private void Win_Message()
        {
            MessageBoxResult messageDialog = MessageBox.Show("Вы выиграли!\nВаш капитал: " + coin + " coins\nХотите сыграть еще?", ":)", MessageBoxButton.YesNo);
            if (messageDialog == MessageBoxResult.No) Environment.Exit(0);
            if (messageDialog == MessageBoxResult.Yes) Restart(null, null);
        }

        private void Restart(object sender, RoutedEventArgs e)
        {
            vis = 0;
            hit = 0;
            Out_Coin();
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
            MainGrid.Visibility = Visibility.Hidden;
            CategoryGrid.Visibility = Visibility.Visible;
        }

        private void Settings(object sender, RoutedEventArgs e)
        {

        }

        private int[] Shuffle(int limit)
        {
            int[] array = new int[limit];
            for (int i = 0; i < limit; i++) array[i] = i;
            var r = new Random();
            for (int i = 0; i < limit; i++)
            {
                int j = r.Next(limit);
                int buffer = array[i];
                array[i] = array[j];
                array[j] = buffer;
            }
            return array;
        }

        private void Get_Hint_Click(object sender, RoutedEventArgs e)
        {
            if (coin > 4)
            {
                if (hint)
                {
                    randomarray = Shuffle(word.Length);
                    hint = false;
                    hint_counter = 0;
                }

                int Letter_Position = randomarray[hint_counter];
                hint_counter++;
                char Letter = word[Letter_Position];
                Loop(Letter, Letter_Position);

                coin -= 5;
                Out_Coin();
            } 
            else
            {
                MessageBox.Show("Недостаточно монет!");
            }
        }

        private void Loop(char Letter, int Letter_Position)
        {
            var buttons = MainGrid.Children.OfType<Button>().ToList();

            foreach (var button in buttons)
            {
                if (((string)button.Content).Contains(Letter, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (TextBox.Text[Letter_Position] == '*') Button_Click_ShowPic(button, null);
                    return;
                }
            }
        }
        public void In_Coin()
        {
            StreamReader f;
            try
            {
                f = new StreamReader("coin.txt");
            }
            catch (Exception p)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!");
                return;
            }
            coin = int.Parse(f.ReadLine());
            f.Close();
        }
        public void Out_Coin()
        {
            StreamWriter f;
            try
            {
                f = new StreamWriter("coin.txt");
            }
            catch (Exception p)
            {
                MessageBox.Show("Возникла непредвиденная ошибка!");
                return;
            }
            f.WriteLine(coin);
            f.Close();
        }

    }
}