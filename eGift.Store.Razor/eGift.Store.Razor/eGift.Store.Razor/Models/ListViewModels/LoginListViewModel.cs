﻿using eGift.Store.Razor.Models.ViewModels;

namespace eGift.Store.Razor.Models.ListViewModels
{
    public class LoginListViewModel
    {
        #region Constructors

        public LoginListViewModel()
        {
            LoginList = new List<LoginViewModel>();
        }

        #endregion Constructors

        #region List View Models

        public List<LoginViewModel> LoginList { get; set; }

        #endregion List View Models
    }
}