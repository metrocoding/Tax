namespace Tax.Interfaces
{
    public interface IVehicle
    {
        string Category { get; set; }
        bool IsTollFree(IEnumerable<string> tollFreeVehicles);
    }
}