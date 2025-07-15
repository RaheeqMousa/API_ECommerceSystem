namespace API_eCommerceProject.DTO.Responses
{
    public class CategoriesResponseDTO
    {

        public int Id { get; set; }
        public List<CategoryTranslationResponse> CategoryTranslations { get; set; }
    }
}
