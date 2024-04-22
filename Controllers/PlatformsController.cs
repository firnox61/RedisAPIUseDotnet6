using Microsoft.AspNetCore.Mvc;
using RedisAPI.Data;
using RedisAPI.Models;

namespace RedisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController:ControllerBase
    {
        private readonly IPlatformRepo _platformRepo;
        public PlatformsController(IPlatformRepo platformRepo)
        {
            _platformRepo=platformRepo;
        }

//https://localhost:7015/api/cars/getcardetailsid?carId=1


        [HttpGet("id")]//("id",Name ="GetPlatformById"
        public ActionResult<Platform> GetPlatformById(string id)
        {
            var result=_platformRepo.GetPlatformById(id);
            if(result!=null)
            {
                return Ok(result);
            }
            return BadRequest();

    
        }
        [HttpGet("getall")]
        public ActionResult<IEnumerable<Platform>> GetAllPlatform()
        {
            var result=_platformRepo.GetAllPlatforms();
            if(result!=null)
            {
                return Ok(result);
            }
            return BadRequest();

        }

        [HttpPost("add")]
        public ActionResult <Platform> CreatePlatform(Platform platform)
        {
            _platformRepo.CreatePlatform(platform);

            return CreatedAtRoute(nameof(GetPlatformById),new{Id=platform.Id}, platform);
        }

    }
}