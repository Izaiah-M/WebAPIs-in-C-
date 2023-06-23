using WebApplication_Project1.Models;

namespace WebApplication_Project1.IRepository
{
    // This registers for us what will be using our generic repository class for the crud operations
    // As of now we have two tables, so we register them...ie. "Country" and "Hotel"
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Country> CountryRepository { get; }
        IGenericRepository<Hotel> HotelRepository { get; }

        Task Save();
    }
}
