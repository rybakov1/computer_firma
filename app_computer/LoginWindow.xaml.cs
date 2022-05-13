using System.Linq;
using System.Windows;
using app_computer.Logic;
using app_computer.Models;

namespace app_computer
{
    public partial class LoginWindow : Window
    {
        mydbContext db;

        public LoginWindow()
        {
            db = new mydbContext();
            InitializeComponent();
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            string mob = mobile_phone_tb.Text;
            string pas = password_tb.Text;

            var cus = from u in db.Customers
                      select new { u.MobileNumber, u.Password, u.IdCustomer };

            foreach (var c in cus)
            {
                if (mob == c.MobileNumber)
                {
                    if (pas == c.Password)
                    {
                        Config.IsAuthorized = true;
                        Config.IdCustomer = c.IdCustomer;

                        OrderWindow orderWindow = new OrderWindow();
                        Application.Current.MainWindow = orderWindow;
                        this.Close();
                        orderWindow.Show();
                    }
                }
            }
        }
    }
}
