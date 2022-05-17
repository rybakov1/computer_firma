using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using app_computer.Models;
using Microsoft.EntityFrameworkCore;

namespace app_computer.Pages
{
    public partial class AdminWindow : Page
    {
        mydbContext db;
        public AdminWindow()
        {
            db = new mydbContext();
            InitializeComponent();

            db.Components.Load();

            var componentMems = db.Components.Local.ToBindingList();
            tableDataGrid.ItemsSource = componentMems;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
        }
        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (tableDataGrid.SelectedItems.Count > 0)
            {
                for (int i = 0; i < tableDataGrid.SelectedItems.Count; i++)
                {
                    Component comp = tableDataGrid.SelectedItems[i] as Component;
                    if (comp != null)
                    {
                        db.Components.Remove(comp);
                    }
                }
            }
            db.SaveChanges();
        }
    }
}
