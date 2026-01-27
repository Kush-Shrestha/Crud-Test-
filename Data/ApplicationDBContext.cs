using Microsoft.EntityFrameworkCore;

namespace Crud.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options): base(options)
        {
            
        }
    }
}
