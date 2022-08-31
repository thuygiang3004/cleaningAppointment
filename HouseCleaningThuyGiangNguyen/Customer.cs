namespace HouseCleaningThuyGiangNguyen
{
    internal class Customer
    {
        public bool IsNew { get; set; } = true;
        public int? CustomerID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
    }
}
