using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eGift.Store.Razor.Models.ViewModels
{
    public class ProductViewModel : BaseModel
    {
        #region Constructors

        public ProductViewModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public int ID { get; set; }

        [Display(Name = "Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Category")]
        [Required]
        public int CategoryId { get; set; }

        [Display(Name = "Sub Category")]
        [Required]
        public int SubCategoryId { get; set; }

        [Display(Name = "Quantity Per Unit")]
        public int? QuantityPerUnit { get; set; }

        [Display(Name = "Unit Price")]
        [Required]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Size")]
        public int? SizeId { get; set; }

        [Display(Name = "Discount")]
        public decimal? Discount { get; set; }

        [Display(Name = "Unit In Stock")]
        public int? UnitInStock { get; set; }

        [Display(Name = "Unit In Order")]
        public int? UnitInOrder { get; set; }

        [Display(Name = "Product Available")]
        public int? ProductAvailable { get; set; }

        [Display(Name = "Short Description")]
        public string? ShortDescription { get; set; }

        [Display(Name = "Long Description")]
        public string? LongDescription { get; set; }

        [Display(Name = "Picture Path 1")]
        public string? PicturePath1 { get; set; }

        [Display(Name = "Picture Path 2")]
        public string? PicturePath2 { get; set; }

        [Display(Name = "Picture Path 3")]
        public string? PicturePath3 { get; set; }

        [Display(Name = "Picture Path 4")]
        public string? PicturePath4 { get; set; }

        [Display(Name = "Picture Data 1")]
        public byte[]? PictureData1 { get; set; }

        [Display(Name = "Picture Data 2")]
        public byte[]? PictureData2 { get; set; }

        [Display(Name = "Picture Data 3")]
        public byte[]? PictureData3 { get; set; }

        [Display(Name = "Picture Data 4")]
        public byte[]? PictureData4 { get; set; }

        [Display(Name = "Product Image Path")]
        [Required]
        public string ProductImagePath { get; set; }

        [Display(Name = "Product Image Data")]
        [Required]
        public byte[] ProductImageData { get; set; }

        #endregion Data Models

        #region View Models

        [NotMapped]
        [Display(Name = "Image Url")]
        public IFormFile? ImageUrl { get; set; }

        [NotMapped]
        [Display(Name = "Picture 1")]
        public IFormFile? Picture1 { get; set; }

        [NotMapped]
        [Display(Name = "Picture 2")]
        public IFormFile? Picture2 { get; set; }

        [NotMapped]
        [Display(Name = "Picture 3")]
        public IFormFile? Picture3 { get; set; }

        [NotMapped]
        [Display(Name = "Picture 4")]
        public IFormFile? Picture4 { get; set; }

        [Display(Name = "Category")]
        public string? CategoryName { get; set; }

        [Display(Name = "Sub Category")]
        public string? SubCategoryName { get; set; }

        [Display(Name = "Size")]
        public string? SizeName { get; set; }

        #endregion View Models
    }
}