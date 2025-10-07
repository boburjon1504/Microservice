using Microsoft.EntityFrameworkCore;

namespace Microservice.ProductApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
}
