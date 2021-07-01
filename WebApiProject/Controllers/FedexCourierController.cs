using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiProject.Helper;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FedexCourierController : ControllerBase
    {
        private IUserService _userService;
        private static readonly int applicationId = 2;
        public FedexCourierController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] FedexCourierInputData inputData,
            [FromHeader] string username,
            [FromHeader] string password)
        {
            var user = await _userService.Authenticate(username, password, applicationId);
            if (user == null)
            {
                return new UnauthorizedObjectResult(new { ErrorMessage = "Invalid username or password." });
            }

            var distance = Utility.GetDistance(inputData.Consignee, inputData.Consignor);
            var pricePerCubeCentimeter = DataManager.GetTestData(Constants.FedexCourier); // Fetched price is per 100 kilometers.

            var packageVolume = inputData.Carton[0] * inputData.Carton[1] * inputData.Carton[2];
            var priceForPackagePerOneHundredKm = packageVolume * pricePerCubeCentimeter;  // This price is for per 100 kilometers.
            var totalPrice = (priceForPackagePerOneHundredKm / 100) * distance;

            return new OkObjectResult(new { Total = totalPrice });
        }
    }
}
