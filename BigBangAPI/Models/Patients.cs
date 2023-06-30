using System.ComponentModel.DataAnnotations;

namespace BigBangAPI.Models
{
    public class Patients
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        

    }
}
