using Microsoft.AspNetCore.Mvc;
using MVCTest.Service;
using System.Net.Http;
using System.Threading.Tasks;

namespace MVCTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DvdController : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly DvdService _dvdService;

        public DvdController(HttpClient httpClient, DvdService dvdService)
        {
            _httpClient = httpClient;
            _dvdService = dvdService;
        }

        //[HttpGet("get-all")]
        //public async Task<IActionResult> GetAllDvds()
        //{
        //    string apiUrl = "http://localhost:5092/api/Manager/GetAllDvds";
        //    var response = await _httpClient.GetAsync(apiUrl);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        var data = await response.Content.ReadAsStringAsync();
        //        return Content(data, "application/json"); // JSON format la return pannum
        //    }
        //    return BadRequest("Error fetching data");
        //}

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllDvds()
        {
            var dvds = await _dvdService.GetAllDvdsAsync();
            return Ok(dvds);
        }
    }
}
