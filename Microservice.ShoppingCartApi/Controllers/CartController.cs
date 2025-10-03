using AutoMapper;
using Microservice.ShoppingCartApi.Data;
using Microservice.ShoppingCartApi.Models;
using Microservice.ShoppingCartApi.Models.Dto;
using Microservice.ShoppingCartApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Microservice.ShoppingCartApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CartController(AppDbContext dbContext, IMapper mapper, IProductService productService) : ControllerBase
{
    private ResponseDto responseDto = new ResponseDto();

    [HttpPost("upsert")]
    public async Task<ResponseDto> Upsert([FromBody] CartDto model, CancellationToken cancellationToken)
    {
        try
        {
            var cartHeader = await dbContext.CartHeaders
                    .FirstOrDefaultAsync(x => x.UserId == model.CartHeader.UserId, cancellationToken);

            CartDetailsDto cartDetail = model.CartDetails.First();

            if (cartHeader is null)
            {
                cartHeader = mapper.Map<CartHeader>(model.CartHeader);

                await dbContext.CartHeaders.AddAsync(cartHeader);
                await dbContext.SaveChangesAsync();

                cartDetail.CartHeaderId = cartHeader.Id;

                await dbContext.CartDetails.AddAsync(mapper.Map<CartDetails>(cartDetail));
                await dbContext.SaveChangesAsync();
            }
            else
            {
                var cartDetails = await dbContext.CartDetails
                    .FirstOrDefaultAsync(c =>
                    c.ProductId == cartDetail.ProductId &&
                    c.CartHeaderId == cartHeader.Id, cancellationToken);

                if (cartDetails is null)
                {
                    cartDetail.CartHeaderId = cartHeader.Id;

                    dbContext.Add(mapper.Map<CartDetails>(cartDetail));
                    dbContext.SaveChanges();
                }
                else
                {
                    cartDetails.Count += cartDetail.Count;
                    dbContext.Update(cartDetails);
                    dbContext.SaveChanges();
                }
            }

            responseDto.Result = model;
        }
        catch (Exception ex)
        {
            responseDto.Message = ex.Message;
            responseDto.IsSuccess = false;
        }

        return responseDto;
    }

    [HttpPost("applycoupon")]
    public async Task<ResponseDto> ApplyCoupon([FromBody] CartDto model)
    {
        try
        {
            var cartFromDb = dbContext.CartHeaders.First(c => c.UserId == model.CartHeader.UserId);

            cartFromDb.CouponCode = model.CartHeader.CouponCode;

            dbContext.CartHeaders.Update(cartFromDb);
            dbContext.SaveChanges();

            responseDto.Result = true;
        }
        catch (Exception ex)
        {
            responseDto.Message = ex.Message;
            responseDto.IsSuccess = false;
        }

        return responseDto;
    }

    [HttpPost("removecoupon")]
    public async Task<ResponseDto> RemoveCoupon([FromBody] CartDto model)
    {
        try
        {
            var cartFromDb = dbContext.CartHeaders.First(c => c.UserId == model.CartHeader.UserId);

            cartFromDb.CouponCode = "";

            dbContext.CartHeaders.Update(cartFromDb);
            dbContext.SaveChanges();

            responseDto.Result = true;
        }
        catch (Exception ex)
        {
            responseDto.Message = ex.Message;
            responseDto.IsSuccess = false;
        }

        return responseDto;
    }

    [HttpPost("removecart")]
    public async Task<ResponseDto> RemoveCart([FromBody] int cartDetailId, CancellationToken cancellationToken)
    {
        try
        {
            var cardDetail = dbContext.CartDetails.First(x => x.Id == cartDetailId);

            int totalCountOfCartItem = dbContext.CartDetails.Where(c => c.CartHeaderId == cardDetail.CartHeaderId).Count();

            dbContext.CartDetails.Remove(cardDetail);

            if (totalCountOfCartItem == 1)
            {
                var cartHeader = dbContext.CartHeaders.First(c => c.Id == cardDetail.CartHeaderId);

                dbContext.CartHeaders.Remove(cartHeader);
            }

            dbContext.SaveChanges();

            responseDto.Result = true;
        }
        catch (Exception ex)
        {
            responseDto.Message = ex.Message;
            responseDto.IsSuccess = false;
        }

        return responseDto;
    }

    [HttpGet("{userId}")]
    public async Task<ResponseDto> GetUserCart(string userId)
    {

        try
        {
            var cartHeader = dbContext.CartHeaders.First(c => c.UserId == userId);
            var cartDetails = dbContext.CartDetails.Where(c => c.CartHeaderId == cartHeader.Id).ToList();

            var cart = new CartDto { CartHeader = mapper.Map<CartHeaderDto>(cartHeader), CartDetails = mapper.Map<IEnumerable<CartDetailsDto>>(cartDetails) };

            IEnumerable<ProductDto> products = await productService.GetProducts();

            foreach (var item in cart.CartDetails)
            {
                item.Product = products.FirstOrDefault(p => p.Id == item.ProductId);

                cart.CartHeader.CartTotal += (item.Count * item.Product.Price);
            }

            responseDto.Result = cart;

            return responseDto;
        }
        catch (Exception ex)
        {
            responseDto.Message = ex.Message;
            responseDto.IsSuccess = false;
        }
        return responseDto;
    }
}
