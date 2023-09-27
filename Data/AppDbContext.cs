using Microsoft.EntityFrameworkCore;

namespace SendEmail_MailKit.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
            
        }


    }
}
