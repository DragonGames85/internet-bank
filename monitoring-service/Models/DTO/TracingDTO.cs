﻿using monitoring_service.Models.Enum;

namespace monitoring_service.Models.DTO
{
    public class TracingDTO
    {
        public TimeSpan Time { get; set; }
        public DateTime Created_At { get; set; }
        public string Route { get; set; }
        public string Method { get; set; }
        public string? Description { get; set; }
        public int StatusCode { get; set; }
        public TracingEnum Type { get; set; }
        public ServiceEnum Service { get; set; }
    }
}
