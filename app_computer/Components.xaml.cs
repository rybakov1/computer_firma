using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using app_computer.Models;

namespace app_computer
{
    public partial class Components : Window
    {
        mydbContext db;

        public Components()
        {
            InitializeComponent();
            db = new mydbContext();

            CategoriesButtonsCreate();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string category = textBox.Text;
            GetComponentByCategory(category);
        }

        private void GetComponentByCategory(string category)
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
                    Text = $"{i + 1}. {comp.Model} ({comp.Title} руб.) - {comp.Description} \n",
                    Name = String.Format("Textblock{0}", i)
                };
                box.Inlines.Add(tBlock[i]);
                Grid.SetRow(tBlock[i], i);
                i++;
            }
        }

        private void GetPresetComponents(string category)
        {
            var presets = db.Presets.Include(c => c.Comps).Where(c => c.Name == category)
                        .ToList();

            TextBlock[] tBlock = new TextBlock[4];
            int i = 0;
            box.Text = "";

            foreach (var preset in presets)
            {
                foreach (var components in preset.Comps)
                {
                    tBlock[i] = new TextBlock
                    {
                        Text = $"{i + 1}. {components.Model} ({components.Price} руб.) - {components.Description} \n",
                        Name = String.Format("Textblock{0}", i)
                    };
                    box.Inlines.Add(tBlock[i]);
                    Grid.SetRow(tBlock[i], i);
                    i++;
                }
            }
        }

        private void CategoriesButtonsCreate()
        {
            var components = from u in db.Typeofcomponents
                             select new { u.Title };

            var presets = db.Presets.Include(c => c.Comps)
                        .ToList();


            List<string> list_comps = new List<string>();
            List<string> list_presets = new List<string>();

            foreach (var preset in presets)
            {
                list_presets.Add(preset.Name);
            }

            foreach (var category in components)
            {
                list_comps.Add(category.Title);
            }

            ButtonGeneration(list_presets, stack_pc, handler: button_click2);
            ButtonGeneration(list_comps, stack_comp, handler: button_click1);
        }

        private void ButtonGeneration(List<string> categories, StackPanel stackPanel, RoutedEventHandler handler)
        {
            int i = 0;
            Button[] b = new Button[categories.Count];

            foreach (var category in categories)
            {
                b[i] = new Button
                {
                    Name = $"button{i}",
                    Content = $"{category}",
                    Height = 20,
                };
                Thickness thickness = b[i].Margin;
                thickness.Top = 5;
                b[i].Margin = thickness;

                b[i].Click += handler;

                stackPanel.Children.Add(b[i]);
                i++;
            }
        }

        void button_click1(object sender, RoutedEventArgs e)
        {
            var senderBtn = sender as Button;
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            GetComponentByCategory(senderBtn.Content.ToString());
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
        }

        void button_click2(object sender, RoutedEventArgs e)
        {
            var senderBtn = sender as Button;
#pragma warning disable CS8602 // Разыменование вероятной пустой ссылки.
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            GetPresetComponents(senderBtn.Content.ToString());
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
#pragma warning restore CS8602 // Разыменование вероятной пустой ссылки.
        }
    }
}