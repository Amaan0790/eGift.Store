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

        #endregion
    }
}