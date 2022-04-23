using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FieldAgent.DAL
{
    public class DBFactory
    {
        public AppDbContext GetDbContext()
        {
            var builder = new ConfigurationBuilder();
            builder.AddUserSecrets<AppDbContext>();
            var config = builder.Build();

            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(config["ConnectionStrings:FieldAgent"])
                .Options;

            return new AppDbContext(options);    
        }
    }
}
