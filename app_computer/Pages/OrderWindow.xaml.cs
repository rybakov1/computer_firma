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

                oplata_button.IsEnabled = true;
                var authorized_customer = db.Customers.Where(c => c.IdCustomer == Config.IdCustomer).ToList();

                foreach (var field in authorized_customer)
                {
                    login_label.Content = field.Firstname + ", выберите детали заказа ниже!";
                }
            }
            else
            {
                oplata_button.IsEnabled = false;
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
                        Price = field.Price,
                        Description = field.Description,
                        Specifications = field.Specifications,
                        Count = item.Value,
                        TotalPrice = (field.Price * item.Value).ToString()
                    });
                }
                CalculateSum();
                componentList.ItemsSource = ComponentList;
            }
            total_sum_label.Content = "Итого к оплате: " + CalculateSum().ToString() + " руб.";
            label_order.Text = "Итого к оплате: " + CalculateSum().ToString() + " руб.";
        }
        private decimal CalculateSum()
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
            return total_sum;
        }
        private void back_text_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new CartWindow());
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Config.IdCustomer != null)
            {
                Order current_order = new() { OrderDate = System.DateTime.Now, IdCustomer = Config.IdCustomer, TotalPrice = (int)CalculateSum() };
                db.Orders.Add(current_order);
                db.SaveChanges();
                foreach (var item in Config.Id)
                {
                    OrderComponent orderComponent = new() { IdOrder = current_order.IdOrder, IdComp = item.Key, CompCount = item.Value };

                    db.OrderComponents.Add(orderComponent);
                    db.SaveChanges();
                }
                NavigationService.Navigate(new OrderDetailsWindow(current_order.IdOrder));
            }
        }
        private void go_login_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new LoginWindow());
        }
    }
}
