using eGift.Store.Razor.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace eGift.Store.Razor.Models.Context
{
    public class ApplicationDbContext : DbContext
    {
        #region Constructor

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        #endregion Constructor

        #region DBSets

        //Customer
        public DbSet<CustomerViewModel> Customer { get; set; }

        //Login
        public DbSet<LoginViewModel> Login { get; set; }

        //Order
        public DbSet<OrderViewModel> Order { get; set; }

        //Order Details
        public DbSet<OrderDetailsViewModel> OrderDetails { get; set; }

        //Product
        public DbSet<ProductViewModel> Product { get; set; }

        //Address
        public DbSet<AddressViewModel> Address { get; set; }

        #endregion DBSets

        #region On Model Creating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SignInViewModel>().HasNoKey();
        }

        public DbSet<eGift.Store.Razor.Models.SignInViewModel> SignInViewModel { get; set; } = default!;

        #endregion
    }
}
