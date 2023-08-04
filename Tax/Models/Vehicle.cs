using Tax.Interfaces;

namespace Tax.Models
{
    public class Vehicle : IVehicle
    {
        protected Vehicle(string category)
        {
            Category = category;
        }

        public string Category { get; set; }

        public bool IsTollFree(IEnumerable<string> tollFreeVehicles) =>
            tollFreeVehicles.Any(v => string.Equals(v, Category, StringComparison.CurrentCultureIgnoreCase));
    }
}