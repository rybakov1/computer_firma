using System.Linq;
using System.Windows;
using System.Windows.Controls;
using app_computer.Logic;
using app_computer.Models;

namespace app_computer
{
    public partial class LoginWindow : Page
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

            var cc = db.Customers
                .Select(u => new { u.MobileNumber, u.Password, u.IdCustomer })?
                .Where(c => c.MobileNumber == mob)?
                .First();

            if (cc is not null)
            {
                if (mob == cc.MobileNumber)
                {
                    if (pas == cc.Password)
                    {
                        Config.IsAuthorized = true;
                        Config.IdCustomer = cc.IdCustomer;

                        NavigationService.Navigate(new OrderWindow());
                    }
                }
            }
        }
    }
}
