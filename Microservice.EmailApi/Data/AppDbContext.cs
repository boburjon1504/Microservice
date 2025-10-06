using Microservice.EmailApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Microservice.EmailApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<EmailLogger> EmailLoggers { get; set; }
}
