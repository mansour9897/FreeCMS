namespace FreeCMS.DomainModels.System
{
    public class ContactMessage
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Email { get; set; }
        public string? Content { get; set; }
    }
    
}