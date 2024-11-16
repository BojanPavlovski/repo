namespace PackageChecker.HelperClass
{
    //a helper class to seperate code from the business logic
    public static class HelperClass
    {
        public static int CalculateVolume(int width, int height, int depth)
        {
            return width * height * depth;
        }

        public static double ComparePackagePrices(double res1, double res2)
        {
            if (res1 > res2)
            {
                return res1;
            }
            else
            {
                return res2;
            }
        }
    }
}
