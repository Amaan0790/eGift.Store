using eGift.Store.Razor.Models.ViewModels;

namespace eGift.Store.Razor.Models.ListViewModels
{
    public class CustomerListViewModel
    {
        #region Constructors

        public CustomerListViewModel()
        {
            ProductList = new List<ProductViewModel>();
        }

        #endregion Constructors

        #region List View Models

        public List<ProductViewModel> ProductList { get; set; }

        #endregion List View Models
    }
}