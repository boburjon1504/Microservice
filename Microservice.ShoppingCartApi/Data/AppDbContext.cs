using Microservice.ShoppingCartApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservice.ShoppingCartApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<CartHeader> CartHeaders { get; set; }

    public DbSet<CartDetails> CartDetails { get; set; }
}
