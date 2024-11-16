using PackageChecker.DataAccess.Interfaces;
using PackageChecker.Domain.Domain;

namespace PackageChecker.DataAccess.Implementations
{
    public class PackageRepository : IPackageRepository
    {
        public List<Package> GetAllPackages()
        {
            return StaticDatabase.Packages.ToList();
        }

        public void Insert(Package package)
        {
            package.Id = StaticDatabase.Packages.Count + 1;
            StaticDatabase.Packages.Add(package);
        }
    }
}
