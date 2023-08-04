namespace Tax.Repository;

public interface IRepository<T>
{
    IList<T> GetAll();
}