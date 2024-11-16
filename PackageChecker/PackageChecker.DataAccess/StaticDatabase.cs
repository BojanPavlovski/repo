using PackageChecker.Domain.Domain;

namespace PackageChecker.DataAccess
{
    public static class StaticDatabase
    {
        public static List<Package> Packages = new List<Package>()
        {
            new Package
            {
                Id = 1,
                Width = 20,
                Weight = 25,
                Height = 30,
                Depth = 12
            }
        };

    
    }
}
