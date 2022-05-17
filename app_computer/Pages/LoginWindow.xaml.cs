using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using app_computer.Logic;
using app_computer.Models;
using app_computer.Pages;

namespace app_computer
{
    public partial class LoginWindow : Page
    {
        mydbContext db;
        int what_a_page;
        public LoginWindow(int from_order)
        {
            db = new mydbContext();
            InitializeComponent();

            what_a_page = from_order;
        }
        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            string mob = mobile_phone_tb.Text;
            string pas = password_tb.Password.ToString();

            if (String.IsNullOrWhiteSpace(mob) || String.IsNullOrWhiteSpace(pas))
            {
                MessageBox.Show("Заполните поля!");
                return;
            }

            try
            {
                if (!(bool)check_admin.IsChecked)
                {
                    var cc = db.Customers
                        .Select(u => new { u.MobileNumber, u.Password, u.IdCustomer })?
                        .Where(c => c.MobileNumber == mob)?
                        .First();

                    if (cc is not null)
                    {
                        if (mob == cc.MobileNumber)
                        {
                            if (pas == cc.Password)
                            {
                                Config.IsAuthorized = true;
                                Config.IdCustomer = cc.IdCustomer;


                                if (what_a_page == 1)
                                {
                                    NavigationService.Navigate(new AccountWindow());
                                }
                                else
                                {
                                    NavigationService.Navigate(new OrderWindow());
                                }
                            }
                            else
                            {
                                MessageBox.Show("Неправильно введён пароль!");
                            }
                        }
                    }
                }
                else
                {
                    var cc = db.Employees
                        .Select(u => new { u.MobileNumber, u.Password, u.IdEmployee })?
                        .Where(c => c.MobileNumber == mob)?
                        .First();

                    if (cc is not null)
                    {
                        if (mob == cc.MobileNumber)
                        {
                            if (pas == cc.Password)
                            {
                                Config.IsAuthorized = true;
                                Config.IsAdmin = true;
                                //Config.IdCustomer = cc.IdEmployee;

                                NavigationService.Navigate(new AdminWindow());
                            }
                            else
                            {
                                MessageBox.Show("Неправильно введён пароль!");
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Такого пользователя нет! Вы можете зарегистрироваться!");
            }
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string mob = mobile_phone_tb.Text;
            string pas = password_tb.Password.ToString();

            if (String.IsNullOrWhiteSpace(mob) || String.IsNullOrWhiteSpace(pas))
            {
                MessageBox.Show("Заполните поля!");
                return;
            }
            else if (check_admin.IsChecked.HasValue && check_admin.IsChecked.Value) {
                MessageBox.Show("Админом зарегистрироваться нельзя!");
                return;
            }

            try
            {
                Customer new_customer = new Customer { MobileNumber = mob, Password = pas };
                db.Customers.Add(new_customer);
                db.SaveChanges();

                Config.IdCustomer = new_customer.IdCustomer;

                if (what_a_page == 1)
                {
                    NavigationService.Navigate(new AccountWindow());
                }
                else
                {
                    NavigationService.Navigate(new OrderWindow());
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Такого пользователя нет! Вы можете зарегистрироваться!");
            }

        }
    }
}
