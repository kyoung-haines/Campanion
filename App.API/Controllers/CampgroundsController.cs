using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CampgroundsController : ControllerBase
    {
    }
}

/*
 * For future me:
 * When I'm implementing the Controllers, and I come to this one, 
 * I want to create a base controller class that will include a routing prefix template.
 * That new class will inherit from ControllerBase, while this class will inherit from 
 * new custom base controller.
 */