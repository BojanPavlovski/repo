using PackageChecker.Dto.Packages;

namespace PackageChecker.Services.Interfaces
{
    public interface IPackageChecker
    {
        public int CalculatePriceBasedOnWeight(int weight);
        public int CalculatePriceBasedOnDimensions(int width,int height,int depth);
        public double CalculatePriceBasedOnWeightForShipFaster(int weight);
        public double CalculatePriceBasedOnDimensionsForShipFaster(int width, int height, int depth);
        public double CalculatePriceBasedOnWeightForMaltaShip(int weight);
        public double CalculatePriceBasedOnDimensionsForMaltaShip(int width, int height, int depth);
        public double FinalPriceCalculations(int weight, int width, int height, int depth);
        public List<PackageDto> GetAllPackages();
        public void AddPackage(PackageDto packageDto);
    }
}
