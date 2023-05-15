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

namespace Project_Balakin
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
        }
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            NavigationCommands.BrowseBack.InputGestures.Clear();
            NavigationCommands.BrowseForward.InputGestures.Clear();

            if (box_login.Text.Length > 0)
            {
                if (box_password.Password.Length > 0)
                {
                    using (TESTEntities DataBase = new TESTEntities())
                    {
                        string login = box_login.Text;
                        string password = box_password.Password;

                        bool isUserExistsLoginAdm = DataBase.admins.Any(u => u.login == login);
                        bool isUserExistsPassAdm = DataBase.admins.Any(u => u.password == password);
                        bool isUserExistsLogin = DataBase.users.Any(u => u.login == login);
                        bool isUserExistsPass = DataBase.users.Any(u => u.password == password);

                        if (isUserExistsLoginAdm && isUserExistsPassAdm)
                        {
                            MessageBox.Show("Админ авторизовался");
                            ClassFrame1.frame1.Navigate(new Page6());
                        }
                        else
                        {
                            if (isUserExistsLogin && isUserExistsPass)
                            {
                                MessageBox.Show("Пользователь авторизовался");
                                ClassFrame1.frame1.Navigate(new Page2());
                            }
                            else
                            {
                                MessageBox.Show("Неверный логин или пароль");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Введите пароль");
                }
            }
            else
            {
                MessageBox.Show("Введите логин");
            }
        }
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            ClassFrame1.frame1.Navigate(new Page3());
        }
    }
}

