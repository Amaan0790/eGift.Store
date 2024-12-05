using System.ComponentModel.DataAnnotations;

namespace eGift.Store.Razor.Models.ViewModels
{
    public class OrderViewModel : BaseModel
    {
        #region Data Models

        public int ID { get; set; }

        [Display(Name = "Customer")]
        [Required]
        public int CustomerId { get; set; }

        [Display(Name = "Total Amount")]
        [Required]
        public decimal TotalAmount { get; set; }

        [Display(Name = "Total Discount")]
        public decimal? TotalDiscount { get; set; }

        [Display(Name = "Total Tax")]
        public decimal? TotalTax { get; set; }

        [Display(Name = "Order Number")]
        [Required]
        public string OrderNumber { get; set; }

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
        [Required]
        public int StatusId { get; set; }

        #endregion Data Models

        #region View Models

        [Display(Name = "Status")]
        public string? StatusName { get; set; }

        [Display(Name = "Customer Name")]
        public string? CustomerName { get; set; }

        [Display(Name = "Contact Number")]
        public string? MobileNumber { get; set; }

        [Display(Name = "Customer Address")]
        public string? Address { get; set; }

        #endregion View Models
    }
}