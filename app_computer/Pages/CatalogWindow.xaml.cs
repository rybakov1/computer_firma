using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using app_computer.Models;
using app_computer.Logic;
using System.Collections.ObjectModel;
using System.Windows.Navigation;

namespace app_computer
{
    public partial class CatalogWindow : Page
    {
        mydbContext db;
        ObservableCollection<ComponentMem> ComponentList;

        public CatalogWindow()
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
                             select new { u.IdComp, u.Model, u.Description, u.Price, u.Specifications };

            ComponentList = new ObservableCollection<ComponentMem>();
            foreach (var component in components)
            {
                ComponentList.Add(new ComponentMem { Id = component.IdComp, Model = component.Model, Price = (decimal)component.Price, Description = component.Description, Specifications = component.Specifications });
            }
            componentList.ItemsSource = ComponentList;
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
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
                list_presets.Add(preset.Name);
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
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
                    FontSize = 14
                };
                Thickness thickness = b[i].Margin;
                thickness.Top = 5;
                b[i].Margin = thickness;

                b[i].Click += handler;

                stackPanel.Children.Add(b[i]);
                i++;
            }
        }
        private void button_click1(object sender, RoutedEventArgs e)
        {
            var senderBtn = sender as Button;
#pragma warning disable CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
            GetComponentByCategory(senderBtn.Content.ToString());
        }

        private void button_click2(object sender, RoutedEventArgs e)
        {
            var senderBtn = sender as Button;
            GetPresetComponents(senderBtn.Content.ToString());
#pragma warning restore CS8604 // Возможно, аргумент-ссылка, допускающий значение NULL.
        }
        private void dataTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            var senderBtn = sender as Button;
            if (Config.Id.ContainsKey((int)senderBtn.Tag))
            {
                Config.Id[(int)senderBtn.Tag] += 1;
            }
            else
            {
                Config.Id.Add((int)senderBtn.Tag, 1);
            }
        }
        private void cart_button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CartWindow());
        }

        private void account_button_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

            if (Config.IsAuthorized)
            {
                NavigationService.Navigate(new AccountWindow());
            }
            else
            {
                NavigationService.Navigate(new LoginWindow());
            }
        }
    }
}
