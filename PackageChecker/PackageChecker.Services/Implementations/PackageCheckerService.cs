using PackageChecker.DataAccess.Interfaces;
using PackageChecker.Domain.Domain;
using PackageChecker.Dto.Packages;
using PackageChecker.Mappers;
using PackageChecker.Services.Interfaces;

namespace PackageChecker.Services.Implementations
{
    public class PackageCheckerService : IPackageChecker
    {
        private IPackageRepository _packageRepository;

        public PackageCheckerService(IPackageRepository packageRepository)
        {
            _packageRepository = packageRepository;
        }
        //a method used for adding the package information to the in-memory database
        public void AddPackage(PackageDto packageDto)
        {
            //calling a mapper to map from PackageDto to domain Package
            _packageRepository.Insert(PackageMapper.ToPackage(packageDto));
        }

        //a method for calculating the price of the package based on dimensions for Cargo4You company
        public int CalculatePriceBasedOnDimensions(int width, int height, int depth)
        {
            int finalPrice = 0;
            if (width <= 0 || height <= 0 || depth <= 0)
            {
                throw new Exception("Invalid inputs, please try again!");
            }

            int volume = HelperClass.HelperClass.CalculateVolume(width,height, depth);

            if(volume <= 1000)
            {
                finalPrice = 10;
            }

            if(volume > 1000 && volume <= 2000){

                finalPrice = 20;
            }
            return finalPrice;
        }

        //a method for calculating the price of the package based on dimensions for MaltaShip company
        public double CalculatePriceBasedOnDimensionsForMaltaShip(int width, int height, int depth)
        {
            double finalPrice = 0;
            int volume = HelperClass.HelperClass.CalculateVolume(width, height, depth);
            if(volume < 500)
            {
                finalPrice = 0;
            }

            if(volume <= 1000)
            {
                finalPrice = 9.50;
            }

            if(volume > 1000 && volume <= 2000)
            {
                finalPrice = 19.50;
            }

            if(volume > 2000 && volume <= 5000)
            {
                finalPrice = 48.50;
            }

            if(volume > 5000)
            {
                finalPrice = 147.50;
            }
            return finalPrice;
        }

        //a method for calculating the price of the package based on dimensions for ShipFaster company
        public double CalculatePriceBasedOnDimensionsForShipFaster(int width, int height, int depth)
        {
            double finalPrice = 0;
            int volume = HelperClass.HelperClass.CalculateVolume(width, height, depth);

            if (volume > 1700)
            {
                finalPrice = 0;
            }

            if(volume <= 1000)
            {
                finalPrice = 11.99;
            }

            if(volume > 1000 && volume <= 1700)
            {
                finalPrice = 21.99;
            }
            return finalPrice;
        }

        //a method for calculating the price of the package based on weight for Cargo4You company
        public int CalculatePriceBasedOnWeight(int weight)
        {
            int finalPrice = 0;
            if (weight <= 0 || weight > 20)
            {
                finalPrice = 0;
            }

            if (weight <= 2)
            {
                finalPrice = 15;
            }

            if (weight > 2 && weight <= 15)
            {
                finalPrice = 18;
            }

            if (weight > 15 && weight <= 20)
            {
                finalPrice = 35;
            }
            return finalPrice;
        }

        //a method for calculating the price of the package based on weight for MaltaShip company
        public double CalculatePriceBasedOnWeightForMaltaShip(int weight)
        {
            double finalPrice = 0;
            if(weight < 10)
            {
                finalPrice = 0;
            }

            if(weight >= 10 && weight <= 20)
            {
                finalPrice = 16.99;
            }

            if(weight > 20 && weight <= 30)
            {
                finalPrice = 33.99;
            }

            if(weight > 30)
            {
                finalPrice = 43.99 + (weight - 25) * 0.41;
            }
            return finalPrice;
        }

        //a method for calculating the price of the package based on weight for ShipFaster company
        public double CalculatePriceBasedOnWeightForShipFaster(int weight)
        {
            double finalPrice = 0;
            if(weight < 10 && weight > 30)
            {
                finalPrice = 0;
            }

            if(weight > 10 && weight <= 15)
            {
                finalPrice = 16.50;
            }

            if (weight > 15 && weight <= 25)
            {
                finalPrice = 36.50;
            }

            if(weight > 25)
            {
                finalPrice = 40 + (weight - 25) * 0.417;
            }
            return finalPrice;
        }

        public double FinalPriceCalculations(int weight, int width, int height, int depth)
        {
            //calculating and comparing prices based on weight and dimensions for Cargo4You
            int finalPriceWithWeightForCargo4You = CalculatePriceBasedOnWeight(weight);
            int finalPriceWithDimensionsForCargo4You = CalculatePriceBasedOnDimensions(width, height, depth);
            //best price from Cargo4You
            int firstResult = (int)HelperClass.HelperClass.ComparePackagePrices(finalPriceWithWeightForCargo4You, finalPriceWithDimensionsForCargo4You);

            //calculating and comparing prices based on weight and dimensions for ShipFaster
            double finalPriceWithWeightForShipFaster =  CalculatePriceBasedOnWeightForShipFaster(weight);
            double finalPriceWithDimensionsForShipFaster = CalculatePriceBasedOnDimensionsForShipFaster(width,height,depth);
            //best price from ShipFaster
            double secondResult = HelperClass.HelperClass.ComparePackagePrices(finalPriceWithWeightForShipFaster, finalPriceWithDimensionsForShipFaster);

            //calculating and comparing prices based on weight and dimensions for MaltaShip
            double finalPriceWithWeightForMaltaShip = CalculatePriceBasedOnWeightForMaltaShip( weight);
            double finalPriceWithDimensionsForMaltaShip =  CalculatePriceBasedOnDimensionsForMaltaShip(width, height, depth);
            //best price from MaltaShip
            double thirdResult = HelperClass.HelperClass.ComparePackagePrices(finalPriceWithWeightForMaltaShip, finalPriceWithDimensionsForMaltaShip);

            //final comparison for best price for user(the most expensive one)
            double fourthResult = HelperClass.HelperClass.ComparePackagePrices(firstResult, secondResult);
            double fifthResult = HelperClass.HelperClass.ComparePackagePrices(fourthResult, thirdResult);
            //returning highest price
            return fifthResult;
        }
        //a method that returns all packages, just to see whether the packages details are correctly saved in the local database
        public List<PackageDto> GetAllPackages()
        {
            List<Package> packages = _packageRepository.GetAllPackages();
            List<PackageDto> packageDto = packages.Select(x => PackageMapper.ToPackageDto(x)).ToList();
            return packageDto;
        }
    }
}
