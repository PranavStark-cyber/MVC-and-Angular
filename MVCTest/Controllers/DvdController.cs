using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCTest.Service;

namespace MVCTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DvdController : ControllerBase
    {
        private readonly DvdService _dvdService;

        public DvdController(DvdService dvdService)
        {
            _dvdService = dvdService;
        }

        // GET api/dvd/GetAllDvds
        [HttpGet("GetAllDvds")]
        public async Task<IActionResult> GetAllDvds()
        {
            try
            {
                var dvds = await _dvdService.GetAllDvdsAsync();
                //return Ok(dvds); // Respond with 200 OK and data
                return Ok(new { message = "API is working" });
            }
            catch (Exception ex)
            {
                // Log the error (optional)
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
