using System.Windows;


namespace app_computer
{
    public partial class FrameWindow : Window
    {
        public FrameWindow()
        {
            InitializeComponent();
            _mainFrame.Navigate(new Catalog());
        }
    }
}
