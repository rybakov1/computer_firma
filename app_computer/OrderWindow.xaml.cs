using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using app_computer.Logic;
using app_computer.Models;

namespace app_computer
{
    public partial class OrderWindow : Page
    {
        mydbContext db;
        ObservableCollection<ComponentMem> ComponentList;
        public OrderWindow()
        {
            InitializeComponent();
            db = new mydbContext();

            if (Config.IsAuthorized == true)
            {
                button_go_login.Visibility = Visibility.Hidden;

                var mem1 = db.Customers.Where(c => c.IdCustomer == Config.IdCustomer).ToList();

                foreach (var a in mem1)
                {
                    login_label.Content = a.Firstname + ", ваш заказ:";
                }
            }
            MakeCart();
        }
        void MakeCart()
        {
            ComponentList = new ObservableCollection<ComponentMem>();

            foreach (var item in Config.Id)
            {
                var component = from u in db.Components
                                where u.IdComp == item.Key
                                select new { u.IdComp, u.Model, u.Description, u.Price, u.Specifications };

                foreach (var field in component)
                {
                    ComponentList.Add(new ComponentMem
                    {
                        Id = field.IdComp,
                        Model = field.Model,
                        Price = (decimal)field.Price,
                        Description = field.Description,
                        Specifications = field.Specifications,
                        Count = item.Value,
                        TotalPrice = (field.Price * item.Value).ToString()
                    });
                }
                CalculateSum();
                componentList.ItemsSource = ComponentList;
            }
        }
        private void CalculateSum()
        {
            decimal item_sum = 0;
            decimal total_sum = 0;
            foreach (var item in Config.Id)
            {
                var component = from u in db.Components
                                where u.IdComp == item.Key
                                select new { u.Price };

                foreach (var field in component)
                {
                    item_sum = (decimal)field.Price * item.Value;
                }
                total_sum += item_sum;
            }
            total_sum_label.Content = "Итого к оплате: " + total_sum.ToString();
            label_order.Text = "Итого к оплате: " + total_sum.ToString();
        }

        private void back_text_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new CartWindow());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginWindow());
        }

        private void go_login_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginWindow());
        }
    }
}
