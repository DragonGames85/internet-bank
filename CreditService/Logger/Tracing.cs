using System.Net.Http;

namespace CreditService.Logger
{
    public class Tracing
    {
        public TimeSpan Time { get; set; }
        public DateTime Created_At { get; set; }
        public string Route { get; set; }
        public string Method { get; set; }
        public string? Description { get; set; }
        public int StatusCode { get; set; }
        public int Type { get; set; }
        public int Service { get; set; } = 0;

    }
    public interface IMonitoring
    {
        void MonitoringService(TimeSpan time, string route, string method, int status, int type, string? description);
    }
    public class Monitoring: IMonitoring
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public Monitoring(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public void MonitoringService(TimeSpan time, string route, string method, int status, int type, string? description)
        {
            var coreClient = _httpClientFactory.CreateClient();

            Tracing data = new Tracing
            {
                Time = time,
                Created_At = DateTime.Now,
                Route = route,
                Method = method,
                StatusCode = status,
                Type = type,
                Description = description
            };

            JsonContent content = JsonContent.Create(data);

            coreClient.PostAsync($"https://localhost:7129/monitoring/api/tracing", content);
        }
    }
}
