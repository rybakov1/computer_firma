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
using System.Windows.Shapes;
using app_computer.Models;
using app_computer.Logic;
using System.Collections.ObjectModel;

namespace app_computer
{
    public partial class OrderDetailsWindow : Page
    {
        mydbContext db;
        ObservableCollection<ComponentMem> ComponentList;
        public OrderDetailsWindow(int id)
        {
            InitializeComponent();

            db = new mydbContext();
            order_num.Content = "Ваш номер заказа " + id;

            var a = db.Orders.Where(c => c.IdOrder == id).Select(c => new
            {
                TotalPrice = c.TotalPrice
            }).First();

            order_sum.Content = "Сумма заказа: " + a.TotalPrice.ToString();
            GetListItems();
        }
        void GetListItems()
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
                componentList.ItemsSource = ComponentList;
            }
        }
        private void go_back_button_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Catalog());
        }
    }
}
