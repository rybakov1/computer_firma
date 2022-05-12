using System.Collections.Generic;
using System.Linq;
using System.Windows;
using app_computer.Models;

namespace app_computer
{
    /// <summary>
    /// Логика взаимодействия для OrderDetails.xaml
    /// </summary>
    public partial class OrderDetails : Window
    {
        mydbContext db;

        public OrderDetails(List<int> id)
        {
            InitializeComponent();
            db = new mydbContext();

            for (int i = 0; i < id.Count; i++)
            {
                var component = from u in db.Components
                                where u.IdComp == id[i]
                                select new { u.IdComp, u.Model, u.Description, u.Price, u.Specifications };

                foreach (var comp in component)
                {
                    mem.Text += "\n" + comp.Model;
                }
            }
        }

        private void back_button_Click(object sender, RoutedEventArgs e)
        {
            Catalog catalog = new Catalog();
            App.Current.MainWindow = catalog;

            this.Close();
            catalog.Show();
        }
    }
}
