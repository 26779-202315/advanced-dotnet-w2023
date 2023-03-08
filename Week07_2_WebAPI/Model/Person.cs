using System.ComponentModel.DataAnnotations;

namespace Week07_2_WebAPI.Model
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(128)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(128)]
        public string LastName { get; set; }
        [Required]
        public DateTimeOffset CreationTime { get; set; }
    }
}
