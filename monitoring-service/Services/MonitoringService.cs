using Microsoft.EntityFrameworkCore;
using monitoring_service.Models.DTO;
using monitoring_service.Models.Entity;
using monitoring_service.Models.Enum;
using System.Runtime.CompilerServices;

namespace monitoring_service.Services
{
    public interface IMonitoringService
    {
        Task CreateTracing(TracingDTO model);
        Task<List<GetTracingDto>> GetAllTracing(DateTime begin, DateTime end);
        Task<List<GetTracingDto>> GetCreditTracing(DateTime begin, DateTime end, TracingEnum? type);
        Task<List<GetTracingDto>> GetAuthTracing(DateTime begin, DateTime end, TracingEnum? type);
        Task<List<GetTracingDto>> GetCoreTracing(DateTime begin, DateTime end, TracingEnum? type);
        Task DeleteTracing(DateTime begin, DateTime end, ServiceEnum? service);
    }
    public class MonitoringService: IMonitoringService
    {
        private readonly ApplicationDbContext _context;
        public MonitoringService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateTracing(TracingDTO model)
        {
            var tracing = new TracingEntity
            {
                Created_At = model.Created_At,
                Time = model.Time,
                Type = model.Type,
                Service = model.Service,
                Route = model.Route,
                Description = model.Description,
                StatusCode = model.StatusCode,
                Method = model.Method,
            };
            await _context.Tracing.AddAsync(tracing);
            await _context.SaveChangesAsync();
        }
        public async Task<List<GetTracingDto>> GetAllTracing(DateTime begin, DateTime end)
        {
           var list =  await _context.Tracing
                .Where(x => (x.Created_At <= end) && (x.Created_At >= begin))
                .OrderBy(x => x.Created_At)
                .ToListAsync();
            var tracing_list = new List<GetTracingDto>();

            foreach (var tracing in list )
            {
                var model = new GetTracingDto
                {
                    Created_At = tracing.Created_At,
                    Time = tracing.Time,
                    Type = tracing.Type.ToString(),
                    Service = tracing.Service.ToString(),
                    Route = tracing.Route,
                    Description = tracing.Description,
                    StatusCode = tracing.StatusCode,
                    Method = tracing.Method,
                    Id = tracing.Id
                };
                tracing_list.Add(model);
            }
            return tracing_list;
        }
        public async Task<List<GetTracingDto>> GetCreditTracing(DateTime begin, DateTime end, TracingEnum? type)
        {

            var list = await _context.Tracing
                 .Where(x => (x.Created_At <= end) && (x.Created_At >= begin) && (x.Service == Models.Enum.ServiceEnum.Credit) && ( type != null ? x.Type == type : x.Type == TracingEnum.Exception || x.Type == TracingEnum.Logging))
                 .OrderBy(x => x.Created_At)
                 .Select(tracing => new GetTracingDto
                 {
                     Created_At = tracing.Created_At,
                     Time = tracing.Time,
                     Type = tracing.Type.ToString(),
                     Service = tracing.Service.ToString(),
                     Route = tracing.Route,
                     Description = tracing.Description,
                     StatusCode = tracing.StatusCode,
                     Method = tracing.Method,
                     Id = tracing.Id
                 })
                 .ToListAsync();
           
            return list;
        }
        public async Task<List<GetTracingDto>> GetCoreTracing(DateTime begin, DateTime end, TracingEnum? type)
        {

            var list = await _context.Tracing
                 .Where(x => (x.Created_At <= end) && (x.Created_At >= begin) && (x.Service == Models.Enum.ServiceEnum.Core) && (type != null ? x.Type == type : x.Type == TracingEnum.Exception || x.Type == TracingEnum.Logging))
                 .OrderBy(x => x.Created_At)
                 .Select(tracing => new GetTracingDto
                 {
                     Created_At = tracing.Created_At,
                     Time = tracing.Time,
                     Type = tracing.Type.ToString(),
                     Service = tracing.Service.ToString(),
                     Route = tracing.Route,
                     Description = tracing.Description,
                     StatusCode = tracing.StatusCode,
                     Method = tracing.Method,
                     Id = tracing.Id
                 })
                 .ToListAsync();

            return list;
        }
        public async Task<List<GetTracingDto>> GetAuthTracing(DateTime begin, DateTime end, TracingEnum? type)
        {

            var list = await _context.Tracing
                 .Where(x => (x.Created_At <= end) && (x.Created_At >= begin) && (x.Service == Models.Enum.ServiceEnum.Auth) && (type != null ? x.Type == type : x.Type == TracingEnum.Exception || x.Type == TracingEnum.Logging))
                 .OrderBy(x => x.Created_At)
                 .Select(tracing => new GetTracingDto
                 {
                     Created_At = tracing.Created_At,
                     Time = tracing.Time,
                     Type = tracing.Type.ToString(),
                     Service = tracing.Service.ToString(),
                     Route = tracing.Route,
                     Description = tracing.Description,
                     StatusCode = tracing.StatusCode,
                     Method = tracing.Method,
                     Id = tracing.Id
                 })
                 .ToListAsync();

            return list;
        }
        public async Task DeleteTracing(DateTime begin, DateTime end, ServiceEnum? service)
        {
            var tracing = await _context.Tracing
                .Where(x => (x.Created_At <= end) && (x.Created_At >= begin) && (service != null ? x.Service == service: x.Service == ServiceEnum.Credit || x.Service == ServiceEnum.Core || x.Service == ServiceEnum.Auth))
                .ToListAsync();
            if (tracing == null) throw new KeyNotFoundException("Tracing not found");
            _context.Tracing.RemoveRange(tracing);
            await _context.SaveChangesAsync();
        }
    }
}
