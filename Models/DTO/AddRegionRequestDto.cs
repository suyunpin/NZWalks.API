using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        // public Guid Id { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "最少三个字符")]
        [MaxLength(5, ErrorMessage = "最大三个字符")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "最大100个字符")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
        public DateTime Time { get; set; } 
    }
}
