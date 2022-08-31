/*Thuy Giang Nguyen*/
using System.Windows;

namespace HouseCleaningThuyGiangNguyen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly VM vm;
        private readonly CustomerAppointment customerAppointment = new CustomerAppointment();

        public MainWindow(bool isEdit)
        {
            InitializeComponent();
            vm = VM.Instance;

            if (isEdit)
            {
                Title = "Edit";
                Add.Content = "Save";
                Clear.Content = "Cancel";
                ViewList.Visibility = Visibility.Hidden;
                IsNew.Visibility = Visibility.Hidden;
                SetDisabled();

                customerAppointment.IsNew = vm.SelectedCustomerAppointment.IsNew;
                customerAppointment.CustomerID = vm.SelectedCustomerAppointment?.CustomerID;
                customerAppointment.Name = vm.SelectedCustomerAppointment?.Name;
                customerAppointment.PostalCode = vm.SelectedCustomerAppointment?.PostalCode;
                customerAppointment.Phone = vm.SelectedCustomerAppointment?.Phone;
                customerAppointment.IsNewAppointment = vm.SelectedCustomerAppointment.IsNewAppointment;
                customerAppointment.AppointmentID = vm.SelectedCustomerAppointment?.AppointmentID;
                customerAppointment.ServiceDate = vm.SelectedCustomerAppointment?.ServiceDate;
                customerAppointment.HouseSize = vm.SelectedCustomerAppointment?.HouseSize;
                customerAppointment.ThisCleanerType = vm.SelectedCustomerAppointment?.ThisCleanerType;
            }
            customerAppointment.CleanerTypeList = vm.GetMasterDBCleanerTypes();
            DataContext = customerAppointment;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var success = vm.Save(customerAppointment);
            MessageBox.Show(success ? "Saved!" : "Something went wrong!");
        }

        private void ViewList_Click(object sender, RoutedEventArgs e)
        {
            var vl = new CustomerAppointmentList();
            vl.Show();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            CustomerID.Text = "";
            Name.Text = "";
            PostalCode.Text = "";
            Phone.Text = "";
            AppointmentID.Text = "";
            ServiceDate.Text = "";
            HouseSize.Text = "";
            CleanerType.SelectedItem = null;
        }

        private void SetDisabled()
        {
            Name.IsEnabled = false;
            PostalCode.IsEnabled = false;
            Phone.IsEnabled = false;
        }

        private void IsNew_Checked(object sender, RoutedEventArgs e)
        {
            if ((bool)IsNew.IsChecked)
            {
                ChooseCustomer.IsEnabled = true;
                SetDisabled();
            }
            else
            {
                ChooseCustomer.IsEnabled = false;
                Name.IsEnabled = true;
                PostalCode.IsEnabled = true;
                Phone.IsEnabled = true;
            }
        }

        private void ChooseCustomer_Click(object sender, RoutedEventArgs e)
        {
            var cl = new CustomerList();
            cl.ShowDialog();
            customerAppointment.CustomerID = vm.SelectedCustomer?.CustomerID;
            customerAppointment.Name = vm.SelectedCustomer?.Name;
            customerAppointment.PostalCode = vm.SelectedCustomer?.PostalCode;
            customerAppointment.Phone = vm.SelectedCustomer?.Phone;
            customerAppointment.IsNew = false;

            CustomerID.Text = customerAppointment.CustomerID.ToString();
            Name.Text = customerAppointment.Name;
            PostalCode.Text = customerAppointment.PostalCode;
            Phone.Text = customerAppointment.Phone;
        }
    }
}
