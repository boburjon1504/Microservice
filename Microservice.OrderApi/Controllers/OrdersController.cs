using AutoMapper;
using Microservice.OrderApi.Data;
using Microservice.OrderApi.Models;
using Microservice.OrderApi.Models.Dtos;
using Microservice.OrderApi.Services.Interfaces;
using Microservice.OrderApi.Utility;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.OrderApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController(IMapper mapper, AppDbContext dbContext, IProductService productService)
    : ControllerBase
{
    private ResponseDto responseDto = new ResponseDto();

    [HttpPost]
    public async Task<ResponseDto> Create(CartDto cartDto)
    {
        try
        {
            OrderHeader orderHeader = mapper.Map<OrderHeader>(cartDto.CartHeader);

            orderHeader.OrderTime = DateTime.Now;
            orderHeader.Status = SD.Status_Pending;

            orderHeader.OrderDetails = mapper.Map<IEnumerable<OrderDetails>>(cartDto.CartDetails);

            await dbContext.OrderHeaders.AddAsync(orderHeader);

            await dbContext.SaveChangesAsync();

            responseDto.Result = mapper.Map<OrderHeaderDto>(orderHeader);
        }
        catch (Exception ex)
        {
            responseDto.IsSuccess = false;
        }

        return responseDto;
    }
}
