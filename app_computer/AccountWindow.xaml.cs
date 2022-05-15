using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using app_computer.Logic;
using app_computer.Models;

namespace app_computer
{

    public partial class AccountWindow : Page
    {
        mydbContext db;
        ObservableCollection<OrderMem> OrderList;
        ObservableCollection<OrderCompnentMem> OrderCompList;

        public AccountWindow()
        {
            InitializeComponent();
            db = new mydbContext();

            OrderList = new ObservableCollection<OrderMem>();
            OrderCompList = new ObservableCollection<OrderCompnentMem>();

            var my_orders = db.Orders.Where(c => c.IdCustomer == Config.IdCustomer).ToList();

            List<int> a = new List<int>();
            foreach (var field in my_orders) {
                a.Add(field.IdOrder);

                var my_order_comps = db.OrderComponents.Where(c => c.IdOrder == field.IdOrder).ToList();

                foreach (var field2 in my_order_comps) {
                    OrderList.Add(new OrderMem
                    {
                        IdComp = field2.IdComp,
                        CompCount = field2.CompCount
                    });
                }

                OrderList.Add(new OrderMem
                {
                    IdOrder = field.IdOrder,
                    OrderDate = field.OrderDate,
                    TotalPrice = field.TotalPrice.ToString()
                });
                //componentList.ItemsSource = OrderCompList;
            }
            componentList.ItemsSource = OrderList;
        }

        private void back_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new CatalogWindow());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Customer customer = db.Customers.Where(c => c.IdCustomer == Config.IdCustomer).FirstOrDefault();

            if (customer != null)
            {
                customer.IdCustomer = Config.IdCustomer.Value;
                customer.Firstname = firstname_text.Text;
                customer.Address = address_text.Text;
                customer.MobileNumber = mobile_text.Text;
                customer.Middlename = middle_text.Text;
                customer.Lastname = lastname_text.Text;
                customer.Password = password_text.Text;

                db.Update(customer);
                db.SaveChanges();
            }
        }
    }
}
