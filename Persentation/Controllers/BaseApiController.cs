using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persentation.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public abstract class BaseApiController:ControllerBase
    {
        public string GetUserEmail()
                  =>User.FindFirstValue(ClaimTypes.Email)!;
        
    }
}
