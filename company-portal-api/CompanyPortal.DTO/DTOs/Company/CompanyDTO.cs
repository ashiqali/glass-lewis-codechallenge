using System.ComponentModel.DataAnnotations;

namespace CompanyPortal.DTO.DTOs.Company
{
    public class CompanyDTO
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
    }
}
