using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using app_computer.Logic;
using app_computer.Models;
using Microsoft.EntityFrameworkCore;

namespace app_computer.Pages
{
    public partial class AdminWindow : Page
    {
        mydbContext db;
        public ObservableCollection<Component> Components2 { get; private set; }
        public AdminWindow()
        {
            db = new mydbContext();
            InitializeComponent();

            db.Components.Load();
            tableDataGrid.ItemsSource = db.Components.Local.ToBindingList();
            lol.ItemsSource = db.Typeofcomponents.Local.ToBindingList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }
    }
}
