using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FastFood.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        protected readonly ILogger<T> Logger;

        protected BaseController(ILogger<T> logger)
        {
            Logger = logger;
        }
    }
}
