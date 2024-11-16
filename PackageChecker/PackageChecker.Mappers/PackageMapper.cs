using PackageChecker.Domain.Domain;
using PackageChecker.Dto.Packages;

namespace PackageChecker.Mappers
{
    public class PackageMapper
    {
        public static PackageDto ToPackageDto(Package package)
        {
            return new PackageDto
            {
                Weight = package.Weight,
                Width = package.Width,
                Depth = package.Depth,
                Height = package.Height,
            };
        }

        public static Package ToPackage(PackageDto packageDto)
        {
            return new Package
            {
                Width = packageDto.Width,
                Height = packageDto.Height,
                Depth = packageDto.Depth,
                Weight = packageDto.Weight
            };
        }
    }
}
