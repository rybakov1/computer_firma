using System.Linq;
using app_computer.Models;
using System.Windows;
using app_computer.Logic;
using System.Collections.ObjectModel;
using Xceed.Wpf.Toolkit;
using System.Windows.Controls;

namespace app_computer
{
    public partial class CartWindow : Page
    {
        mydbContext db;
        ObservableCollection<ComponentMem> ComponentList;
        public CartWindow()
        {
            InitializeComponent();
            db = new mydbContext();

            MakeCart();
        }
        void MakeCart()
        {
            ComponentList = new ObservableCollection<ComponentMem>();

            if (Config.Id.Count == 0)
            {
                go_to_order.Visibility = Visibility.Hidden;
                total_price.Text = "Cart empty";
            }
            else
            {
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
                        });
                    }
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
            total_price.Text = "Сумма заказа: " + total_sum.ToString() + " руб.";
        }
        private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new Catalog());
        }
        private void myUpDownControl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var mem = sender as IntegerUpDown;
            Config.Id[(int)mem.Tag] = (int)mem.Value;
            CalculateSum();

            if (Config.Id[(int)mem.Tag] == 0)
            {
                Config.Id.Remove((int)mem.Tag);
            }
        }
        private void go_to_order_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrderWindow());
        }
    }
}
