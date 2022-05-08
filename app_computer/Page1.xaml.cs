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
using System.Windows.Navigation;
using System.Windows.Shapes;
using app_computer.Models;  

namespace app_computer
{
    /// <summary>
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            using (mydbContext db = new mydbContext()) {
                string item = textBox.Text;
                var components = db.Components
                    .Where(p => p.Model == item).ToList();   // асинхронное получение данных

                foreach (var comp in components)
                    Console.WriteLine($"{comp.Model} ({comp.Price}) - {comp.Description}");
            }
        }
    }
}
