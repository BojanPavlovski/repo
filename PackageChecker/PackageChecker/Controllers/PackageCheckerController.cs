using Microsoft.AspNetCore.Mvc;
using PackageChecker.Dto.Packages;
using PackageChecker.Services.Interfaces;

namespace PackageChecker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageCheckerController : ControllerBase
    {
        public IPackageChecker _packageChecker;
        public PackageCheckerController(IPackageChecker packageChecker)
        {
            _packageChecker = packageChecker;
        }

        [HttpGet]
        public ActionResult<double> CalculateFinalPriceForCompany4You(int weight,int width, int depth, int height)
        {
            try
            {
                //making a call to the PackageCheckerService
                double finalPrice = _packageChecker.FinalPriceCalculations(weight, width, depth, height);

                return finalPrice;
            }
            catch
            {
                return BadRequest("Incorrect inputs,try again.");
            }
        }

        //making a call to the service to add new package dimensions to the in-memory db 
        [HttpPost("addPackage")]
        public IActionResult AddPackageToDb(PackageDto packageDto)
        {
            try
            {
                _packageChecker.AddPackage(packageDto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //a call to the service to retrieve all packages from in-memory list(for testing purposes)
        [HttpGet("getAllPackages")]
        public ActionResult<List<PackageDto>> GetAllPackages()
        {
            try
            {
                return Ok(_packageChecker.GetAllPackages());
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
