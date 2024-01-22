using System.ComponentModel.DataAnnotations;

namespace FreeCMS.DomainModels.System
{
    public class Setting
    {
        [Key]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
        
    }
    
}