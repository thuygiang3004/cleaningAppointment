using System.Windows;

namespace HouseCleaningThuyGiangNguyen
{
    /// <summary>
    /// Interaction logic for CustomerAppointmentList.xaml
    /// </summary>
    public partial class CustomerAppointmentList : Window
    {
        VM vm;
        public CustomerAppointmentList()
        {
            InitializeComponent();
            vm = VM.Instance;
            DataContext = vm;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var ew = new MainWindow(false);
            ew.Show();
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var ew = new MainWindow(true);
            ew.Show();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var success = vm.Delete();
            MessageBox.Show(success ? "Saved!" : "Something went wrong!");
        }

        private void Customers_Sorting(object sender, System.Windows.Controls.DataGridSortingEventArgs e)
        {
            switch (e.Column.SortMemberPath.ToString())
            {
                case "Name": vm.Sort(VM.Columns.Name); break;
                case "PostalCode": vm.Sort(VM.Columns.PostalCode); break;
                case "Phone": vm.Sort(VM.Columns.Phone); break;
                case "ServiceDate": vm.Sort(VM.Columns.ServiceDate); break;
                case "HouseSize": vm.Sort(VM.Columns.HouseSize); break;
                case "CleanerType": vm.Sort(VM.Columns.CleanerType); break;
                case "Cost":
                    vm.Sort(VM.Columns.Cost); break;
                default:
                    break;
            }
        }
    }
}
