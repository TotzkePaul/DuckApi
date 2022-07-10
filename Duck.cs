using System.ComponentModel.DataAnnotations;

namespace DuckApi
{
    public class Duck
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Genus { get; set; }
        public string? Species { get; set; }
        public decimal? WingSpanCm { get; set; }
        public decimal? WeightKg { get; set; }
        public string? Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime LastModified { get; set; }
    }
}