using monitoring_service.Models.Enum;

namespace monitoring_service.Models.Entity
{
    public class TracingEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime Created_At { get; set; }
        public TimeSpan Time {  get; set; }
        public string Route {  get; set; }
        public string Method {  get; set; }
        public string? Description { get; set; }
        public int StatusCode {  get; set; }
        public TracingEnum Type {  get; set; }
        public ServiceEnum Service {  get; set; }

    }
}
