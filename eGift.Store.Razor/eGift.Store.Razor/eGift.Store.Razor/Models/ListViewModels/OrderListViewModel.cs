using eGift.Store.Razor.Models.ViewModels;

namespace eGift.Store.Razor.Models.ListViewModels
{
    public class OrderListViewModel
    {
        #region Constructors

        public OrderListViewModel()
        {
            OrderList = new List<OrderViewModel>();
        }

        #endregion Constructors

        #region List View Models

        public List<OrderViewModel> OrderList { get; set; }

        #endregion List View Models
    }
}