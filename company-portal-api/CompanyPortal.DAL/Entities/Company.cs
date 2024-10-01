using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.DAL.Entities
{
    public class Company
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(10)]
        public string Ticker { get; set; }

        [Required]
        [StringLength(100)]
        public string Exchange { get; set; }

        [Required]
        [StringLength(12)]
        public string Isin { get; set; }

        [StringLength(255)]
        public string? Website { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;

    }
}
