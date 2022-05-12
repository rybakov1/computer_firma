using System.Linq;
using System.Windows;
using app_computer.Models;
using app_computer.Logic;
using System.Collections.ObjectModel;
using Xceed.Wpf.Toolkit;

namespace app_computer
{
    public partial class OrderDetails : Window
    {
        mydbContext db;
        ObservableCollection<ComponentMem> ComponentList;
        public OrderDetails()
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
                    });
                }
            }
            CalculateSum();
            componentList.ItemsSource = ComponentList;
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
            total_price.Text = total_sum.ToString();
        }
        private void TextBlock_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Catalog catalog = new();
            Application.Current.MainWindow = catalog;
            this.Close();
            catalog.Show();
        }

        private void myUpDownControl_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var mem = sender as IntegerUpDown;
#pragma warning disable CS8629 // Тип значения, допускающего NULL, может быть NULL.
            Config.Id[(int)mem.Tag] = (int)mem.Value;
            CalculateSum();
#pragma warning restore CS8629 // Тип значения, допускающего NULL, может быть NULL.
        }
    }
}
