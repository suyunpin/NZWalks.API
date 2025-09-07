namespace NZWalks.API.Models.DTO
{
    public class WalkDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double LengthInkm { get; set; }
        public string? WalkTmageUrl { get; set; }

        
        
        public RegionDto Region { get; set; }
        public DifficultyDto Difficulty { get; set; }


    }
}
