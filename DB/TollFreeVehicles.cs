namespace Tax.DB
{
    public static class TollFreeVehicles
    {
        public static string[] GetList() => new[]
        {
            "Motorcycle",
            "Tractor",
            "Emergency",
            "Diplomat",
            "Foreign",
            "Military"
        };
    }
}