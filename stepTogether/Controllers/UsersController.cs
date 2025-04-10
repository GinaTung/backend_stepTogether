using Microsoft.AspNetCore.Mvc;
using stepTogether.Data;
using stepTogether.Models;

namespace stepTogether.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly StepTogetherDbContext _context;

        public UsersController(StepTogetherDbContext context)
        {
            _context = context;
        }
    }

}
