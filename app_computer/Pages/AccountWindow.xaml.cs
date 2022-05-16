using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using app_computer.Logic;
using app_computer.Models;

namespace app_computer
{

    public partial class AccountWindow : Page
    {
        mydbContext db;
        ObservableCollection<OrderMem> OrderList;
        ObservableCollection<ComponentMem> CompList;

        public AccountWindow()
        {
            InitializeComponent();
            db = new mydbContext();
            OrderList = new ObservableCollection<OrderMem>();
            CompList = new ObservableCollection<ComponentMem>();

            if (!Config.IsAdmin)
            {
                AdminTab.Visibility = Visibility.Hidden;

                var my_orders = db.Orders.Where(c => c.IdCustomer == Config.IdCustomer).ToList();

                foreach (var field in my_orders)
                {
                    var components = from u in db.Components
                                     join c in db.OrderComponents on u.IdComp equals c.IdComp
                                     where c.IdOrder == field.IdOrder
                                     select new { u.IdComp, u.Model, u.Description, u.Price, u.Specifications, c.CompCount };

                    foreach (var field2 in components)
                    {
                        CompList.Add(new ComponentMem
                        {
                            Id = field2.IdComp,
                            Model = field2.Model,
                            Price = (decimal)field2.Price,
                            Description = field2.Description,
                            Specifications = field2.Specifications,
                            Count = field2.CompCount,
                            TotalPrice = (field2.Price * field2.CompCount).ToString()
                        });
                    }

                    OrderList.Add(new OrderMem
                    {
                        IdOrder = field.IdOrder,
                        OrderDate = field.OrderDate,
                        TotalPrice = field.TotalPrice.ToString(),
                        Comps = new(CompList)
                    });
                    CompList.Clear();
                }
                componentList.ItemsSource = OrderList;
            }
            else
            {
                OrdersTab.Visibility = Visibility.Hidden;
            }
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
