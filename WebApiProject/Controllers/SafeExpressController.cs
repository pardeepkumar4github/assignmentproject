using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Xml;
using WebApiProject.Helper;
using WebApiProject.Models;

namespace WebApiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SafeExpressController : ControllerBase
    {
        private IUserService _userService;
        private static readonly int applicationId = 3;
        public SafeExpressController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(
            [FromBody] XmlDocument xmlInput,
            [FromHeader] string username,
            [FromHeader] string password)
        {
            var user = await _userService.Authenticate(username, password, applicationId);
            if (user == null)
            {
                return new UnauthorizedObjectResult(new { ErrorMessage = "Invalid username or password." });
            }

            var source = xmlInput.SelectSingleNode("/SafeExpressInputDataModel/Source/AddLine1").InnerText;
            var destination = xmlInput.SelectSingleNode("/SafeExpressInputDataModel/Destination/AddLine1").InnerText;
            XmlNodeList dimensions = xmlInput.SelectNodes("/SafeExpressInputDataModel/Package/decimal");
            var length =Convert.ToDecimal(dimensions[0].InnerText);
            var width = Convert.ToDecimal(dimensions[1].InnerText);
            var height = Convert.ToDecimal(dimensions[2].InnerText);

            var distance = Utility.GetDistance(new Address { AddLine1 = source }, new Address { AddLine1 = destination });
            var pricePerCubeCentimeter = DataManager.GetTestData(Constants.SafeExpress); // Fetched price is per 100 kilometers.

            var packageVolume = length * width * height;
            var priceForPackagePerOneHundredKm = packageVolume * pricePerCubeCentimeter;  // This price is for per 100 kilometers.
            var totalPrice = (priceForPackagePerOneHundredKm / 100) * distance;

            return new OkObjectResult(new { Quote = totalPrice });
        }
    }
}
