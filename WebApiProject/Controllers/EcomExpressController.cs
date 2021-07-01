using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApiProject.Helper;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EcomExpressController : ControllerBase
    {
        private IUserService _userService;
        private static readonly int applicationId = 1;
        public EcomExpressController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] EcomExpressInputData inputData,
            [FromHeader] string username,
            [FromHeader] string password)
        {
            var user = await _userService.Authenticate(username, password, applicationId);
            if (user == null)
            {
                return new UnauthorizedObjectResult(new { ErrorMessage = "Invalid username or password." });
            }

            var distance = Utility.GetDistance(inputData.ContactAddress, inputData.WarehouseAddress);
            var pricePerCubeCentimeter = DataManager.GetTestData(Constants.EcomExpress); // Fetched price is per 100 kilometers.

            var packageVolume =
                inputData.PackageDimensions[0] * inputData.PackageDimensions[1] * inputData.PackageDimensions[2];
            var priceForPackagePerOneHundredKm = packageVolume * pricePerCubeCentimeter;  // This price is for per 100 kilometers.
            var totalPrice = (priceForPackagePerOneHundredKm / 100) * distance;

            return new OkObjectResult(new EcomExpressApiResponse { Amount = totalPrice });
        }
    }

    public class EcomExpressApiResponse
    {
        public decimal Amount { get; set; }
    }
}
