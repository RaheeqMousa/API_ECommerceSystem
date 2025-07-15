using System.ComponentModel.DataAnnotations;

namespace API_eCommerceProject.DTO.Requests
{
    public class CategoryTranslationRequest
    {
        [Required]
        public string Name { get; set; }
        public string Language { get; set; } = "en";
    }
}
