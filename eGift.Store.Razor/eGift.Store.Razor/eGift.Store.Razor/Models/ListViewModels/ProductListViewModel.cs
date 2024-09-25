using eGift.Store.Razor.Models.ViewModels;

namespace eGift.Store.Razor.Models.ListViewModels
{
    public class ProductListViewModel
    {
        #region Constructors

        public ProductListViewModel()
        {
            ProductList = new List<ProductViewModel>();
        }

        #endregion Constructors

        #region List View Models

        public List<ProductViewModel> ProductList { get; set; }

        #endregion List View Models
    }
}