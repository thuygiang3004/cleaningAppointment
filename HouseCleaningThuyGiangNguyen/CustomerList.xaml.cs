using System.Windows;

namespace HouseCleaningThuyGiangNguyen
{
    /// <summary>
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomerList : Window
    {
        VM vm;
        public CustomerList()
        {
            InitializeComponent();
            vm = VM.Instance;
            DataContext = vm;
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
