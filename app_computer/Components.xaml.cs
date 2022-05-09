using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using app_computer.Models;
using Microsoft.EntityFrameworkCore;

namespace app_computer
{
    public partial class Components : Window
    {
        public Components()
        {
            InitializeComponent();
            categoriesButtonsCreate();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string category = textBox.Text;
            getComponentByCategory(category);
        }

        private void getComponentByCategory(string category)
        {
            using (mydbContext db = new mydbContext())
            {
                var components = from u in db.Components
                                 join c in db.Typeofcomponents on u.IdType equals c.IdType
                                 where c.Title == category
                                 select new { u.Model, u.Description, c.Title };

                TextBlock[] tBlock = new TextBlock[4];
                int i = 0;
                box.Text = "";
                foreach (var comp in components)
                {
                    tBlock[i] = new TextBlock
                    {
                        Text = $"{i}. {comp.Model} ({comp.Title} руб.) - {comp.Description} \n",
                        Name = String.Format("Textblock{0}", i)
                    };
                    box.Inlines.Add(tBlock[i]);
                    Grid.SetRow(tBlock[i], i);
                    i++;
                }
            }
        }
        private void categoriesButtonsCreate()
        {
            using (mydbContext db = new mydbContext())
            {
                var categories = from u in db.Typeofcomponents
                                 select new { u.Title };

                int i = 0;
                Button[] b = new Button[8];
                foreach (var category in categories)
                {
                    b[i] = new Button
                    {
                        Name = $"button{i}",
                        Content = $"{category.Title}",
                        Height = 20,
                    };
                    Thickness thickness = b[i].Margin;
                    thickness.Top = 40 * i;
                    b[i].Margin = thickness;

                    b[i].Click += button_click1;

                    Grid.SetRow(b[i], 1);
                    Grid.SetColumn(b[i], 0);
                    grid1.Children.Add(b[i]);
                    i++;
                }
            }
            void button_click1(object sender, RoutedEventArgs e)
            {
                var senderBtn = sender as Button;
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                getComponentByCategory(senderBtn.Content.ToString());
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
            }
        }
    }
}
