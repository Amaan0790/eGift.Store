using System.ComponentModel.DataAnnotations;

namespace eGift.Store.Razor.Models.ViewModels
{
    public class OrderDetailsViewModel : BaseModel
    {
        #region Constructors

        public OrderDetailsViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "Order")]
        public int OrderId { get; set; }

        [Display(Name = "Product")]
        public int ProductId { get; set; }

        [Display(Name = "UnitPrice")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        [Display(Name = "Discount")]
        public decimal? Discount { get; set; }

        [Display(Name = "Tax")]
        public decimal? Tax { get; set; }

        [Display(Name = "Price")]
        public decimal NetAmount { get; set; }

        #endregion Data Models

        #region View Models

        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Discount Amount")]
        public decimal DiscountAmount { get; set; }

        [Display(Name = "Tax Amount")]
        public decimal TaxAmount { get; set; }

        [Display(Name = "Product Image")]
        public byte[]? ProductImageData { get; set; }

        [Display(Name = "Order Number")]
        public string? OrderNumber { get; set; }

        [Display(Name = "Notes")]
        public string? Notes { get; set; }

        [Display(Name = "Dispatched Date")]
        public DateTime? DispatchedDate { get; set; }

        [Display(Name = "Shipped Date")]
        public DateTime? ShippedDate { get; set; }

        [Display(Name = "Delivery Date")]
        public DateTime? DeliveryDate { get; set; }

        [Display(Name = "Cancel Date")]
        public DateTime? CancelDate { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        #endregion
    }
}