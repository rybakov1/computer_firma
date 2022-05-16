using System.Windows;
using app_computer.Pages;

namespace app_computer
{
    public partial class FrameWindow : Window
    {
        public FrameWindow()
        {
            InitializeComponent();
            _mainFrame.Navigate(new AdminWindow());
        }
    }
}
