namespace Tax.Repository
{
    public class TollFreeVehiclesRepository : IRepository<string>
    {
        public IList<string> GetAll() => new List<string>
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