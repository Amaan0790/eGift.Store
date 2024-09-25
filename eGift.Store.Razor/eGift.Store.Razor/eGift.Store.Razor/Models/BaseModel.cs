namespace eGift.Store.Razor.Models
{
    public class BaseModel
    {
        #region Constructors

        public BaseModel()
        {
        }

        #endregion Constructors

        #region Data Models

        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

        #endregion Data Models
    }
}