using monitoring_service.Models.Enum;

namespace monitoring_service.Models.DTO
{
    public class GetTracingDto
    {
        public Guid Id { get; set; }
        public DateTime Created_At { get; set; }
        public TimeSpan Time { get; set; }
        public string Route { get; set; }
        public string Method { get; set; }
        public string? Description { get; set; }
        public int StatusCode { get; set; }
        public string Type { get; set; }
        public string Service { get; set; }
    }
}
