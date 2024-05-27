using InternetBank.Core.Application.DTOs.OperationDTOs;
using InternetBank.Core.Application.Interfaces.Services.OperationServices;
using MassTransit;
using System.Threading.Tasks;

namespace InternetBank.Core.Infrastructure.Consumers;


public class CreateOperationConsumer : IConsumer<CreateOperationDto>
{
    private IOperationHandleService _operationHandleService;

    public CreateOperationConsumer(IOperationHandleService operationHandleService)
    {
        _operationHandleService = operationHandleService;
    }

    public async Task Consume(ConsumeContext<CreateOperationDto> context)
    {
        var dto = context.Message;

        await Task.Run(() =>
        {
            Console.WriteLine($"Received operation: {dto.Name}");

            _operationHandleService.CreateOperation(dto, false);
        });
    }
}