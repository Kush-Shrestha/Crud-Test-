using practicing.Data;                                          // Types of architecture 
                                                                // DDD = Domain Driven Design
namespace practicing.Services                                   // TDD= Test Driven Development
{                                                               // Hexagonal Architecture = Ports and Adapters
    public class StudentService
    {
              // Types of Services                                                               
              // Scopped = when http is removed object also get deleted and makes new when new http is made                                                          
     // Singelton = Make object one time and provide object to everyone when requested
     //Transient= Always make new object when requested
         private readonly AppDbContext _context;

        public StudentController(AppDbContext dbContext)
        {
            _context = dbContext;
        }
        public StudentController(AppDbContext dbContext)             
            _context = dbContext;                                // Domain = Model/Entity and DTO
                                                                 // Application = Service and Interface
        }                                                        // INfrastructure = Reposoritory and third party services
    }                                                            // API = Controllers and Middlewares
}
