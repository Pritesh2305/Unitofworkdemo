using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unitofworkdemo.Interfaces;
using Unitofworkdemo.Repository;

namespace Unitofworkdemo.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext dc;

        public ICityRepository CityRepository => new CityRepository(dc);

        public UnitOfWork(DataContext _dc)
        {
            dc = _dc;
        }

        public async Task<bool> SaveAsync()
        {
            return await dc.SaveChangesAsync() >0;
        }
    }
}
