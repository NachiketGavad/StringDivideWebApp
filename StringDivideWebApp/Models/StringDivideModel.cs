using System.ComponentModel.DataAnnotations;

namespace StringDivideWebApp.Models
{
    public class StringDivideModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string InputArray { get; set; }

        public string ProcessedResult { get; set; }
    }
}
