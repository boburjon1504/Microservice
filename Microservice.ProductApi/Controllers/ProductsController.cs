using AutoMapper;
using Microservice.ProductApi.Data;
using Microservice.ProductApi.Models;
using Microservice.ProductApi.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microservice.ProductApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController(AppDbContext dbContext, IMapper mapper) : ControllerBase
{
    private ResponseDto responseDto = new ResponseDto();
    [HttpGet]
    public async Task<ResponseDto> GetAll()
    {
        var products = dbContext.Products.ToList();

        responseDto.Result = products;

        return responseDto;
    }

    [HttpGet("{id:int}")]
    public async Task<ResponseDto> GetById(int id)
    {
        var product = await dbContext.Products.FindAsync(id);

        responseDto.Result = product;

        return responseDto;
    }

    [Authorize("ADMIN")]
    [HttpPost]
    public async Task<ResponseDto> Create([FromBody] ProductDto productDto, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(productDto);

        await dbContext.Products.AddAsync(product, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        responseDto.Result = mapper.Map<ProductDto>(product);
        
        return responseDto;
    }

    [HttpPut]
    [Authorize("ADMIN")]
    public async Task<ResponseDto> Update([FromBody] ProductDto model)
    {
        var product = mapper.Map<Product>(model);

        dbContext.Products.Update(product);

        await dbContext.SaveChangesAsync();

        responseDto.Result = model;

        return responseDto;
    }

    [Authorize("ADMIN")]
    [HttpDelete("{id:int}")]
    public async Task<ResponseDto> Delete(int id)
    {
        var product = await dbContext.Products.FindAsync(id);

        dbContext.Products.Remove(product);

        await dbContext.SaveChangesAsync();

        return responseDto;
    }
}
