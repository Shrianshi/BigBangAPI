using System.ComponentModel.DataAnnotations;

namespace BigBangAPI.Models
{
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Specialization { get; set; }
        public bool? Status { get; set; }

    }
}
