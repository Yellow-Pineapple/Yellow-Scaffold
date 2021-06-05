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
            Hide_yesno();
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
        public void Hide_yesno()
        {
            yes11.Visibility = Visibility.Hidden;
            yes12.Visibility = Visibility.Hidden;
            yes13.Visibility = Visibility.Hidden;
            yes14.Visibility = Visibility.Hidden;
            yes15.Visibility = Visibility.Hidden;
            yes16.Visibility = Visibility.Hidden;
            yes17.Visibility = Visibility.Hidden;
            yes18.Visibility = Visibility.Hidden;
            yes19.Visibility = Visibility.Hidden;
            yes110.Visibility = Visibility.Hidden;
            yes111.Visibility = Visibility.Hidden;
            yes21.Visibility = Visibility.Hidden;
            yes22.Visibility = Visibility.Hidden;
            yes23.Visibility = Visibility.Hidden;
            yes24.Visibility = Visibility.Hidden;
            yes25.Visibility = Visibility.Hidden;
            yes26.Visibility = Visibility.Hidden;
            yes27.Visibility = Visibility.Hidden;
            yes28.Visibility = Visibility.Hidden;
            yes29.Visibility = Visibility.Hidden;
            yes210.Visibility = Visibility.Hidden;
            yes211.Visibility = Visibility.Hidden;
            yes31.Visibility = Visibility.Hidden;
            yes32.Visibility = Visibility.Hidden;
            yes33.Visibility = Visibility.Hidden;
            yes34.Visibility = Visibility.Hidden;
            yes35.Visibility = Visibility.Hidden;
            yes36.Visibility = Visibility.Hidden;
            yes37.Visibility = Visibility.Hidden;
            yes38.Visibility = Visibility.Hidden;
            yes39.Visibility = Visibility.Hidden;
            yes310.Visibility = Visibility.Hidden;
            yes311.Visibility = Visibility.Hidden;

            no11.Visibility = Visibility.Hidden;
            no12.Visibility = Visibility.Hidden;
            no13.Visibility = Visibility.Hidden;
            no14.Visibility = Visibility.Hidden;
            no15.Visibility = Visibility.Hidden;
            no16.Visibility = Visibility.Hidden;
            no17.Visibility = Visibility.Hidden;
            no18.Visibility = Visibility.Hidden;
            no19.Visibility = Visibility.Hidden;
            no110.Visibility = Visibility.Hidden;
            no111.Visibility = Visibility.Hidden;

            no21.Visibility = Visibility.Hidden;
            no22.Visibility = Visibility.Hidden;
            no23.Visibility = Visibility.Hidden;
            no24.Visibility = Visibility.Hidden;
            no25.Visibility = Visibility.Hidden;
            no26.Visibility = Visibility.Hidden;
            no27.Visibility = Visibility.Hidden;
            no28.Visibility = Visibility.Hidden;
            no29.Visibility = Visibility.Hidden;
            no210.Visibility = Visibility.Hidden;
            no211.Visibility = Visibility.Hidden;

            no31.Visibility = Visibility.Hidden;
            no32.Visibility = Visibility.Hidden;
            no33.Visibility = Visibility.Hidden;
            no34.Visibility = Visibility.Hidden;
            no35.Visibility = Visibility.Hidden;
            no36.Visibility = Visibility.Hidden;
            no37.Visibility = Visibility.Hidden;
            no38.Visibility = Visibility.Hidden;
            no39.Visibility = Visibility.Hidden;
            no310.Visibility = Visibility.Hidden;
            no311.Visibility = Visibility.Hidden;
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Transform(sender);
        }
        private void Button_Click1_1(object sender, RoutedEventArgs e)    // Буква А
        {
            if (Transform(sender))
                yes11.Visibility = Visibility.Visible;
            else
                no11.Visibility = Visibility.Visible;
        }
        private void Button_Click1_2(object sender, RoutedEventArgs e)    // Буква Б
        {
            if (Transform(sender))
                yes12.Visibility = Visibility.Visible;
            else
                no12.Visibility = Visibility.Visible;
        }
        private void Button_Click1_3(object sender, RoutedEventArgs e)    // Буква В
        {
            if (Transform(sender))
                yes13.Visibility = Visibility.Visible;
            else
                no13.Visibility = Visibility.Visible;
        }
        private void Button_Click1_4(object sender, RoutedEventArgs e)    // Буква Г
        {
            if (Transform(sender))
                yes14.Visibility = Visibility.Visible;
            else
                no14.Visibility = Visibility.Visible;
        }
        private void Button_Click1_5(object sender, RoutedEventArgs e)    // Буква Д
        {
            if (Transform(sender))
                yes15.Visibility = Visibility.Visible;
            else
                no15.Visibility = Visibility.Visible;
        }
        private void Button_Click1_6(object sender, RoutedEventArgs e)    // Буква Е
        {
            if (Transform(sender))
                yes16.Visibility = Visibility.Visible;
            else
                no16.Visibility = Visibility.Visible;
        }
        private void Button_Click1_7(object sender, RoutedEventArgs e)    // Буква Ё
        {
            if (Transform(sender))
                yes17.Visibility = Visibility.Visible;
            else
                no17.Visibility = Visibility.Visible;
        }
        private void Button_Click1_8(object sender, RoutedEventArgs e)    // Буква Ж
        {
            if (Transform(sender))
                yes18.Visibility = Visibility.Visible;
            else
                no18.Visibility = Visibility.Visible;
        }
        private void Button_Click1_9(object sender, RoutedEventArgs e)    // Буква З
        {
            if (Transform(sender))
                yes19.Visibility = Visibility.Visible;
            else
                no19.Visibility = Visibility.Visible;
        }
        private void Button_Click1_10(object sender, RoutedEventArgs e)    // Буква И
        {
            if (Transform(sender))
                yes110.Visibility = Visibility.Visible;
            else
                no110.Visibility = Visibility.Visible;
        }
        private void Button_Click1_11(object sender, RoutedEventArgs e)    // Буква Й
        {
            if (Transform(sender))
                yes111.Visibility = Visibility.Visible;
            else
                no111.Visibility = Visibility.Visible;
        }



        private void Button_Click2_1(object sender, RoutedEventArgs e)    // Буква К
        {
            if (Transform(sender))
                yes21.Visibility = Visibility.Visible;
            else
                no21.Visibility = Visibility.Visible;
        }
        private void Button_Click2_2(object sender, RoutedEventArgs e)    // Буква Л
        {
            if (Transform(sender))
                yes22.Visibility = Visibility.Visible;
            else
                no22.Visibility = Visibility.Visible;
        }
        private void Button_Click2_3(object sender, RoutedEventArgs e)    // Буква М
        {
            if (Transform(sender))
                yes23.Visibility = Visibility.Visible;
            else
                no23.Visibility = Visibility.Visible;
        }
        private void Button_Click2_4(object sender, RoutedEventArgs e)    // Буква Н
        {
            if (Transform(sender))
                yes24.Visibility = Visibility.Visible;
            else
                no24.Visibility = Visibility.Visible;
        }
        private void Button_Click2_5(object sender, RoutedEventArgs e)    // Буква О
        {
            if (Transform(sender))
                yes25.Visibility = Visibility.Visible;
            else
                no25.Visibility = Visibility.Visible;
        }
        private void Button_Click2_6(object sender, RoutedEventArgs e)    // Буква П
        {
            if (Transform(sender))
                yes26.Visibility = Visibility.Visible;
            else
                no26.Visibility = Visibility.Visible;
        }
        private void Button_Click2_7(object sender, RoutedEventArgs e)    // Буква Р
        {
            if (Transform(sender))
                yes27.Visibility = Visibility.Visible;
            else
                no27.Visibility = Visibility.Visible;
        }
        private void Button_Click2_8(object sender, RoutedEventArgs e)    // Буква С
        {
            if (Transform(sender))
                yes28.Visibility = Visibility.Visible;
            else
                no28.Visibility = Visibility.Visible;
        }
        private void Button_Click2_9(object sender, RoutedEventArgs e)    // Буква Т
        {
            if (Transform(sender))
                yes29.Visibility = Visibility.Visible;
            else
                no29.Visibility = Visibility.Visible;
        }
        private void Button_Click2_10(object sender, RoutedEventArgs e)    // Буква У
        {
            if (Transform(sender))
                yes210.Visibility = Visibility.Visible;
            else
                no210.Visibility = Visibility.Visible;
        }
        private void Button_Click2_11(object sender, RoutedEventArgs e)    // Буква Ф
        {
            if (Transform(sender))
                yes211.Visibility = Visibility.Visible;
            else
                no211.Visibility = Visibility.Visible;
        }



        private void Button_Click3_1(object sender, RoutedEventArgs e)    // Буква Х
        {
            if (Transform(sender))
                yes31.Visibility = Visibility.Visible;
            else
                no31.Visibility = Visibility.Visible;
        }
        private void Button_Click3_2(object sender, RoutedEventArgs e)    // Буква Ц
        {
            if (Transform(sender))
                yes32.Visibility = Visibility.Visible;
            else
                no32.Visibility = Visibility.Visible;
        }
        private void Button_Click3_3(object sender, RoutedEventArgs e)    // Буква Ч
        {
            if (Transform(sender))
                yes33.Visibility = Visibility.Visible;
            else
                no33.Visibility = Visibility.Visible;
        }
        private void Button_Click3_4(object sender, RoutedEventArgs e)    // Буква Ш
        {
            if (Transform(sender))
                yes34.Visibility = Visibility.Visible;
            else
                no34.Visibility = Visibility.Visible;
        }
        private void Button_Click3_5(object sender, RoutedEventArgs e)    // Буква Щ
        {
            if (Transform(sender))
                yes35.Visibility = Visibility.Visible;
            else
                no35.Visibility = Visibility.Visible;
        }
        private void Button_Click3_6(object sender, RoutedEventArgs e)    // Буква Ъ
        {
            if (Transform(sender))
                yes36.Visibility = Visibility.Visible;
            else
                no36.Visibility = Visibility.Visible;
        }
        private void Button_Click3_7(object sender, RoutedEventArgs e)    // Буква Ы
        {
            if (Transform(sender))
                yes37.Visibility = Visibility.Visible;
            else
                no37.Visibility = Visibility.Visible;
        }
        private void Button_Click3_8(object sender, RoutedEventArgs e)    // Буква Ь
        {
            if (Transform(sender))
                yes38.Visibility = Visibility.Visible;
            else
                no38.Visibility = Visibility.Visible;
        }
        private void Button_Click3_9(object sender, RoutedEventArgs e)    // Буква Э
        {
            if (Transform(sender))
                yes39.Visibility = Visibility.Visible;
            else
                no39.Visibility = Visibility.Visible;
        }
        private void Button_Click3_10(object sender, RoutedEventArgs e)    // Буква Ю
        {
            if (Transform(sender))
                yes310.Visibility = Visibility.Visible;
            else
                no310.Visibility = Visibility.Visible;
        }
        private void Button_Click3_11(object sender, RoutedEventArgs e)    // Буква Я
        {
            if (Transform(sender))
                yes311.Visibility = Visibility.Visible;
            else
                no311.Visibility = Visibility.Visible;
        }

        private void Restart(object sender, RoutedEventArgs e)
        {
            vis = 0;
            RandomWord();
            Hide_yesno();
            Show_vis();
        }

        private void Settings(object sender, RoutedEventArgs e)
        {

        }
    }
}
