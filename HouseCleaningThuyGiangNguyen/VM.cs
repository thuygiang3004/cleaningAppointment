/*Thuy Giang Nguyen*/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace HouseCleaningThuyGiangNguyen
{
    internal class VM : INotifyPropertyChanged
    {
        public enum Columns
        {
            Name,
            PostalCode,
            Phone,
            ServiceDate,
            HouseSize,
            CleanerType,
            Cost
        }
        private readonly DB db = DB.Instance;
        private List<CustomerAppointment> customerAppointmentList;

        private Columns sortColumn;
        private bool[] sortOrders = new bool[] { true, true, true, true, true, true, true };

        #region singleton
        private static VM vm;
        public static VM Instance { get { vm ??= new VM(); return vm; } }
        #endregion

        private VM()
        {
            customerAppointmentList = db.Get();
            sortColumn = Columns.Name;
            GetCleanerTypeList();
            selectedCleanerType = CleanerTypeListWithAll[0];
            UpdateList();
            GetCustomerList();
        }

        #region properties
        public BindingList<CustomerAppointment> CustomerAppointmentList { get; set; } = new BindingList<CustomerAppointment>();

        private List<CleanerType> cleanerTypeList { get; set; } = new List<CleanerType>();
        public BindingList<CleanerType> CleanerTypeListWithAll { get; set; } = new BindingList<CleanerType>();
        public BindingList<CleanerType> CleanerTypeList { get; set; } = new BindingList<CleanerType>();

        public CustomerAppointment CustomerAppointment { get; set; } = new CustomerAppointment();

        private CleanerType selectedCleanerType;
        public CleanerType SelectedCleanerType
        {
            get { return selectedCleanerType; }
            set { selectedCleanerType = value; propertyChanged(); UpdateList(); }
        }

        private CustomerAppointment selectedCustomerAppointment;
        public CustomerAppointment SelectedCustomerAppointment
        {
            get { return selectedCustomerAppointment; }
            set { selectedCustomerAppointment = value; propertyChanged(); }
        }

        private List<Customer> customerList { get; set; } = new List<Customer>();
        public BindingList<Customer> CustomerList { get; set; } = new BindingList<Customer>();

        private Customer selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return selectedCustomer; }
            set { selectedCustomer = value; propertyChanged(); }
        }

        private string searchKey;
        public string SearchKey
        {
            get { return searchKey; }
            set { searchKey = value; propertyChanged(); GetCustomerList(); }
        }
        #endregion

        #region methods
        public bool Save(CustomerAppointment customerAppointment)
        {
            bool success;
            if (customerAppointment.IsNew) success = db.Insert(customerAppointment);
            else success = db.Update(customerAppointment);
            customerAppointmentList = db.Get();
            UpdateList();
            return success;
        }

        public bool Delete()
        {
            bool success = db.Delete(SelectedCustomerAppointment);
            customerAppointmentList = db.Get();
            UpdateList();
            return success;
        }

        private void GetCleanerTypeList()
        {
            cleanerTypeList.Add(new CleanerType { ID = 100, Description = "ALL", RatePerMeter = null });
            cleanerTypeList.AddRange(db.GetCleanerTypeList());

            foreach (CleanerType ct in cleanerTypeList)
            {
                CleanerTypeListWithAll.Add(ct);
            }
        }

        public List<CleanerType> GetMasterDBCleanerTypes()
        {
            cleanerTypeList.Clear();
            cleanerTypeList.AddRange(db.GetCleanerTypeList());
            return cleanerTypeList;
        }

        public void Sort(Columns column)
        {
            sortColumn = column;
            UpdateList();

            sortOrders[(int)column] = !sortOrders[(int)column];
        }

        private object orderByName(CustomerAppointment ca) => ca.Name;
        private object orderByPostalCode(CustomerAppointment ca) => ca.PostalCode;
        private object orderByPhone(CustomerAppointment ca) => ca.Phone;
        private object orderByServiceDate(CustomerAppointment ca) => ca.ServiceDate;
        private object orderByHouseSize(CustomerAppointment ca) => ca.HouseSize;
        private object orderByCleanerType(CustomerAppointment ca) => ca.ThisCleanerType.Description;
        private object orderByCost(CustomerAppointment ca) => ca.Cost;

        private void UpdateList()
        {
            foreach (CustomerAppointment ca in customerAppointmentList)
            {
                if (ca.AppointmentID != null)
                {
                    ca.ThisCleanerType = new CleanerType();
                    ca.ThisCleanerType.ID = ca.CleanerTypeID;
                    ca.ThisCleanerType.Description = cleanerTypeList.Single(x => x.ID == ca.ThisCleanerType.ID).Description;
                    ca.ThisCleanerType.RatePerMeter = cleanerTypeList.Single(x => x.ID == ca.ThisCleanerType.ID).RatePerMeter;
                }
            }

            var filteredCustomerAppointmentList = (selectedCleanerType.ID == 100)
                ? customerAppointmentList
                : customerAppointmentList.Where(x => x.CleanerTypeID == selectedCleanerType.ID);

            Func<CustomerAppointment, object> sortFn = sortColumn switch
            {
                Columns.Name => orderByName,
                Columns.PostalCode => orderByPostalCode,
                Columns.Phone => orderByPhone,
                Columns.ServiceDate => orderByServiceDate,
                Columns.HouseSize => orderByHouseSize,
                Columns.CleanerType => orderByCleanerType,
                Columns.Cost => orderByCost,
                _ => orderByName,
            };

            filteredCustomerAppointmentList = (sortOrders[(int)sortColumn]
                ? filteredCustomerAppointmentList.OrderBy(sortFn)
                : filteredCustomerAppointmentList.OrderByDescending(sortFn));

            CustomerAppointmentList.Clear();
            foreach (CustomerAppointment ca in filteredCustomerAppointmentList)
            {
                CustomerAppointmentList.Add(ca);
            }
        }

        private void GetCustomerList()
        {
            customerList = db.GetCustomer();
            IEnumerable<Customer> filteredCustomerList;
            filteredCustomerList = string.IsNullOrWhiteSpace(searchKey)
                ? customerList
                : customerList.Where(x => x.Name.ToLower().Contains(searchKey.ToLower()) ||
                                            x.Phone.Contains(searchKey));
            CustomerList.Clear();
            foreach (Customer c in filteredCustomerList)
            {
                CustomerList.Add(c);
            }
        }
        #endregion

        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged;

        private void propertyChanged([CallerMemberName] string propertyName = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
