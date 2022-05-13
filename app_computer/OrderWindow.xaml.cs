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
using System.Windows.Shapes;
using app_computer.Logic;
using app_computer.Models;

namespace app_computer
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        mydbContext db;
        ObservableCollection<ComponentMem> ComponentList;
        public OrderWindow()
        {
            InitializeComponent();
            db = new mydbContext();

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
                    }) ;
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
            CartWindow order = new();
            Application.Current.MainWindow = order;
            this.Close();
            order.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
