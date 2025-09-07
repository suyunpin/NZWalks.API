namespace NZWalks.API.Models.Domain
{
    public class Walk
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public double LengthInkm { get; set; }
        public string? WalkTmageUrl { get; set; }

        /// <summary>
        /// 外键属性
        /// </summary>
        public Guid DifficultyId { get; set; }
        public Guid RegionId { get; set; }



        /// <summary>
        /// 导航属性
        /// </summary>
        public Difficulty Difficulty {  get; set; }
        public Region Region { get; set; }
    }
}
