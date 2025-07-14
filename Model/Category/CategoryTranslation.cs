namespace API_eCommerceProject.Model.Category
{
    public class CategoryTranslation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; } = "en";
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
