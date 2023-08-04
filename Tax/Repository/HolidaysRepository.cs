using Tax.Models;

namespace Tax.Repository
{
    public class HolidaysRepository : IRepository<Holiday>
    {
        public IList<Holiday> GetAll() => new List<Holiday> { new(2, 14, 3), new(6, 1, 3) };
    }
}