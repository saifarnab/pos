namespace PosSustemUIU.ViewModels
{
    public class PurchasePostVM
    {
        public string SupplierId { get; set; }
        public string PaidAmount { get; set; }
        public string PurchaseDate { get; set; }
        public SelectedProducts[] SelectedProducts { get; set; }
        public string InternalMemo { get; set; }
        public string ExternalMemo{ get; set; }
        public string PurchaseNote { get; set; }
        public string DeliveryNote { get; set; }
        public string Attachment { get; set; }
        public string TotalPrice { get; set; }
        public string TotalVat { get; set; }
        public string TotalQuantity { get; set; }
        public bool IsVatPaid { get; set; }
        public bool IsActive { get; set; }
        public string OtherNote { get; set; }
    }
}