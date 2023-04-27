using Microsoft.EntityFrameworkCore;

namespace ShiftLogger.Models;

public class LoggerContext : DbContext
{
    public DbSet<ShiftItem> ShiftItems { get; set; }

    public string ConnectionString { get; }

    public LoggerContext(DbContextOptions<LoggerContext> options) : base(options)
    {
        ConnectionString =
            "Data Source=UHLAHLAH;Initial Catalog=ShiftLogger;Integrated Security=True;TrustServerCertificate = True";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(ConnectionString);

}
