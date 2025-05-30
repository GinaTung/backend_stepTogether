using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using stepTogether.Data;
using stepTogether.Models;

namespace stepTogether.Controllers
{
    [Authorize]
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
