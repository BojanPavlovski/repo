using PackageChecker.Domain.Domain;
using PackageChecker.Dto.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackageChecker.DataAccess.Interfaces
{
    public interface IPackageRepository
    {
        List<Package> GetAllPackages();
        void Insert(Package package);
    }
}
