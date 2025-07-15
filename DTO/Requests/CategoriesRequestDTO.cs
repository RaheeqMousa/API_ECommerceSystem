using API_eCommerceProject.Model;

namespace API_eCommerceProject.DTO.Requests
{
    public class CategoriesRequestDTO
    {
        public Status Status { get; set; } = Status.Active;
        public List<CategoryTranslationRequest> CategoryTranslations { get; set; }
    }
}
