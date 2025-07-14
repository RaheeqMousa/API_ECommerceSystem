using System.ComponentModel.DataAnnotations;

namespace API_eCommerceProject.Model.Category
{
    public class Category: BaseModel
    {

        public List<CategoryTranslation> CategoryTranslations { get; set; }=new List<CategoryTranslation>(){ };
    }
}
