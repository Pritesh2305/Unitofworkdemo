using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unitofworkdemo.Model;

namespace Unitofworkdemo.Interfaces
{
    public interface ICityRepository
    {
        Task<IEnumerable<City>> GetCitiesAsync();
        void AddCity(City city);
        void DeleteCity(int cityid);
        Task<City> FindCity(Int64 id);
        
    }
}
