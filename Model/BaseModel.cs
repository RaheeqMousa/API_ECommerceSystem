namespace API_eCommerceProject.Model
{

    public enum Status { 
        Active=1,
        Inactive=2
    }
    public class BaseModel
    {
        public int Id { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;
        public Status Status { get; set; } = Status.Active;
    }
}
