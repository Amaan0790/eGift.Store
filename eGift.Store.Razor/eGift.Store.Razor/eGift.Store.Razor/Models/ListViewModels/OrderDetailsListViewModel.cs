using eGift.Store.Razor.Models.ViewModels;

namespace eGift.Store.Razor.Models.ListViewModels
{
    public class OrderDetailsListViewModel
    {
        #region Constructors

        public OrderDetailsListViewModel()
        {
            OrderDetailsList = new List<OrderDetailsViewModel>();
        }

        #endregion Constructors

        #region List View Models

        public List<OrderDetailsViewModel> OrderDetailsList { get; set; }

        #endregion List View Models
    }
}