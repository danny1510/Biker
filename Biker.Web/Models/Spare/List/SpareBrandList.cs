namespace Biker.Web.Models.Spare
{
    public class SpareBrandList
    {

        public int Id_Brand { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public bool Is_checked { get; set; }

        public int SpareCategoryId { get; set; }

        public string NameCategory { get; set; }

        public string ImgUrlCategory { get; set; }

        //public ICollection<BrandCategoryList> BrandCategories { get; set; }
    }
}
