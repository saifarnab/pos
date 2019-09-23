namespace PosSustemUIU.ViewModels
{
    public class SalePostVM
    {
        public SelectedProducts[] SelectedProducts { get; set; }
        public string TotalQuantity { get; set; }
        public string TotalPrice { get; set; }
        public string PaidAmount { get; set; }
        public string Discount { get; set; }
        public string CustomerId { get; set; }
        public string SaleDate { get; set; }
        public string InternalMemo { get; set; }
        public string ExternalMemo{ get; set; }
        public string SaleNote { get; set; }
        public bool IsVatPaid { get; set; }
        public bool IsActive { get; set; }
    }
}