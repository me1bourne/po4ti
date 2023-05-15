using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для Page3.xaml
    /// </summary>
    public partial class Page3 : Page
    {
        public Page3()
        {
            InitializeComponent();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            if (box_login.Text.Length > 0)
            {
                if (box_password.Password.Length > 0)
                {
                    if (box_password.Password.Length >= 6)
                    {
                        bool en = true; // английская раскладка
                        bool number = false;

                        for (int i = 0; i < box_password.Password.Length; i++)
                        {
                            if (box_password.Password[i] >= 'А' && box_password.Password[i] <= 'Я')
                            {
                                en = false; // если русская раскладка
                            }

                            if (box_password.Password[i] >= '0' && box_password.Password[i] <= '9')
                            {
                                number = true; // если цифры
                            }
                        }

                        if (!en)
                        {
                            MessageBox.Show("Доступна только английская раскладка");
                        }
                        else if (!number)
                        {
                            MessageBox.Show("Добавьте хотя бы одну цифру");
                        }

                        if (en && number)
                        {
                            if (box_check_password.Password.Length > 0)
                            {
                                if (box_password.Password == box_check_password.Password)
                                {
                                    string login = box_login.Text;
                                    string password = box_password.Password;
                                    string root = "Пользователь";

                                    using (TESTEntities DataBase = new TESTEntities())
                                    {

                                        bool isUserExists = DataBase.users.Any(u => u.login == login);

                                        if (isUserExists)
                                        {
                                            MessageBox.Show("Такой пользователь уже существует");
                                        }
                                        else
                                        {
                                            InsertUser(login, password, root);
                                            MessageBox.Show("Пользователь зарегистрирован");
                                            ClassFrame1.frame1.Navigate(new Page1());
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Пароли не совпадают");
                                }
                            }
                            else
                            {
                                MessageBox.Show("Повторите пароль");
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароль слишком короткий, минимум 6 символов");
                    }
                }
                else
                {
                    MessageBox.Show("Укажите пароль");
                }
            }
            else
            {
                MessageBox.Show("Укажите логин");
            }
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            ClassFrame1.frame1.Navigate(new Page1());
        }

        private static bool InsertUser(string login, string password, string root)
        {
            bool result = false;
            using (SqlConnection connection = new SqlConnection("Server=192.168.147.50\\MSSQLSA;Database=TEST;user id=ssa;password=1;MultipleActiveResultSets=True;"))
            {
                string query = "INSERT INTO users (login, password, root) VALUES (@login, @password, @root)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.Parameters.AddWithValue("@login", login);
                        command.Parameters.AddWithValue("@password", password);
                        command.Parameters.AddWithValue("@root", root);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            result = true;
                        }
                        else
                        {
                            MessageBox.Show("Error: User not inserted");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            return result;

        }
    }
}
