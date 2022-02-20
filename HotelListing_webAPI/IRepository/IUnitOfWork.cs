using System;
using System.Threading.Tasks;
using HotelListing_webAPI.Data;

namespace HotelListing_webAPI.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Country> Countries { get; }
        IGenericRepository<Hotel> Hotels{ get; }

        Task save();
    }
}
