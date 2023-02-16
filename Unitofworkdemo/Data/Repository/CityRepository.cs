using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unitofworkdemo.Interfaces;
using Unitofworkdemo.Model;
using Microsoft.EntityFrameworkCore;
using Unitofworkdemo.Data;

namespace Unitofworkdemo.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly DataContext dc;

        public CityRepository(DataContext _dc)
        {
            this.dc = _dc;
        }
        public void AddCity(City city)
        {
            dc.Add(city);
        }

        public void DeleteCity(int cityid)
        {
            var city = dc.Cities.Find(cityid);
            dc.Cities.Remove(city);
        }

        public async Task<City> FindCity(Int64 id)
        {
            return await dc.Cities.FindAsync(id);
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await dc.Cities.ToListAsync();
        }
        //public async Task<bool> SaveAsync()
        //{
        //    return await dc.SaveChangesAsync() > 0;
        //}
    }
}
