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

        sbyte pre_payment_mark = 0;
        int pre_payment_sum = 0;

        int? total_sum = 0;

        public OrderWindow()
        {
            InitializeComponent();
            db = new mydbContext();

            pickup_Check.IsChecked = true;
            prepayment_Check.IsChecked = true;

            if (Config.IsAuthorized == true)
            {
                button_go_login.Visibility = Visibility.Hidden;

                pickup_Check.IsEnabled = true;
                delivery_Check.IsEnabled = true;

                oplata_button.IsEnabled = true;
                var authorized_customer = db.Customers.Where(c => c.IdCustomer == Config.IdCustomer).First();

                if (!string.IsNullOrEmpty(authorized_customer.Firstname))
                    login_label.Content = authorized_customer.Firstname + ", выберите детали заказа ниже!";
                else login_label.Content = "Пожалуйста, выберите детали заказа ниже!";
            }
            else
            {
                pickup_Check.IsEnabled = false;
                delivery_Check.IsEnabled = false;
                prepayment_Check.IsEnabled = false;
                Nal_Check.IsEnabled = false;
                Bank_Check.IsEnabled = false;
                oplata_button.IsEnabled = false;
            }

            label_order.Text = "Итого к оплате: " + CalculateSum().ToString() + " руб.";
            total_sum_label.Content = "Итого к оплате: " + CalculateSum().ToString() + " руб.";

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
                componentList.ItemsSource = ComponentList;
            }
        }
        private int? CalculateSum()
        {
            total_sum = 0;
            int item_sum = 0;

            if (delivery_Check.IsChecked.HasValue && delivery_Check.IsChecked.Value)
            {
                Config.DeliveryDiscount = 0;
                address_delivery.Visibility = Visibility.Visible;

                address_text.Text = db.Customers.Where(c => c.IdCustomer == Config.IdCustomer).Select(c => c.Address).First();
                mobile_text.Text = db.Customers.Where(c => c.IdCustomer == Config.IdCustomer).Select(c => c.MobileNumber).First();
            }
            else
            {
                Config.DeliveryDiscount = 5;
                address_delivery.Visibility = Visibility.Collapsed;
            }

            if (prepayment_Check.IsChecked.HasValue && prepayment_Check.IsChecked.Value)
            {
                Config.PaymentDiscount = 5;
            }
            else
            {
                Config.PaymentDiscount = 0;
            }

            foreach (var item in Config.Id)
            {
                var component = from u in db.Components
                                where u.IdComp == item.Key
                                select new { u.Price };

                foreach (var field in component)
                {
                    item_sum = (int)(field.Price * item.Value);
                }
                total_sum += item_sum;
            }
            int a = (int)(Config.PaymentDiscount + Config.DeliveryDiscount);
            if (a > 1)
                total_sum -= total_sum / 100 * a;

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
                var a = db.Customers.Where(c => c.IdCustomer == Config.IdCustomer).First();

                if (a.Address == null) { 
                    Customer customer = db.Customers.FirstOrDefault(c => c.IdCustomer == Config.IdCustomer);
                    customer.Address = address_text.Text;

                    db.Update(customer);
                    db.SaveChanges();
                }

                Order current_order = new() { OrderDate = System.DateTime.Now, IdCustomer = Config.IdCustomer, TotalPrice = (int)CalculateSum(), PaymentMark = pre_payment_mark, Prepayment = pre_payment_sum };
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
            NavigationService.Navigate(new LoginWindow(0));
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            int sum = CalculateSum().Value;
            label_order.Text = "Итого к оплате: " + sum + " руб.";
            total_sum_label.Content = "Итого к оплате: " + sum + " руб.";
        }
    }
}
