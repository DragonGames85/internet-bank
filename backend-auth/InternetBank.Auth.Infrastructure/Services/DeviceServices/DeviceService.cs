using InternetBank.Auth.Application.DTOs.DeviceDTOs;
using InternetBank.Auth.Domain.Entities;
using InternetBank.Auth.Persistence.Contexts.EfCore;
using Microsoft.EntityFrameworkCore;

namespace InternetBank.Auth.Infrastructure.Services.DeviceServices;

public interface IDeviceService
{
    Task CreateDevice(Guid userId, string token);
    Task<List<DeviceDto>> GetUserDevices(Guid userId);
    Task<List<DeviceDto>> GetEmployeesDevices();
}

public class DeviceService : IDeviceService
{
    private readonly ApplicationDbContext _context;

    public DeviceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task CreateDevice(Guid userId, string token)
    {
        if (_context.Devices.FirstOrDefault(x => x.Token == token) != null)
            throw new Exception("Token has already created.");

        var device = new Device()
        {
            Token = token,
        };
        var user = _context.Users.FirstOrDefault(x => x.Id == userId) 
            ?? throw new Exception("User is not found.");

        device.User = user;

        await _context.Devices.AddAsync(device);
        await _context.SaveChangesAsync();
    }

    public async Task<List<DeviceDto>> GetUserDevices(Guid userId)
    {
        var devicesDto = new List<DeviceDto>();

        var devices = await _context.Devices.Include(x => x.User).Where(x => x.User.Id == userId).ToListAsync();

        foreach (var device in devices) 
        {
            devicesDto.Add(new DeviceDto(device.Token));
        }

        return devicesDto;
    }

    public async Task<List<DeviceDto>> GetEmployeesDevices()
    {
        var devicesDto = new List<DeviceDto>();

        var employees = await _context.Users
            .Include(x => x.UserRoles)
                .ThenInclude(x => x.RoleUsers)
            .Where(x => x.UserRoles.Any(x => x.Name == "Employee"))
            .ToListAsync();
        
        var employeeIds = employees.Select(e => e.Id).ToList();

        var devices = await _context.Devices.Include(x => x.User).Where(x => employeeIds.Contains(x.Id)).ToListAsync();

        foreach (var device in devices)
        {
            devicesDto.Add(new DeviceDto(device.Token));
        }

        return devicesDto;
    }
}
