using Hotel_Carina.Data;
using Hotel_Carina.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Carina.Repository
{
    public class UnitofWork : IUnitofWork
    {
        private readonly DataBaseContext _context;

        private IGenericRepository<Country> _countries;
        private IGenericRepository<Hotel> _hotels;
        private IGenericRepository<Customer> _customers;
        public UnitofWork(DataBaseContext context)
        {
            _context = context;
        }
        public IGenericRepository<Country> Countries => _countries ??= new GenericRepository<Country>(_context);

        public IGenericRepository<Hotel> Hotels => _hotels ??= new GenericRepository<Hotel>(_context);

        public IGenericRepository<Customer> Customers => _customers ??= new GenericRepository<Customer>(_context);

        public void Dispose()
        {
            _context.Dispose();
        
            GC.SuppressFinalize(this);
        }

        public async Task Save()
        {
           await _context.SaveChangesAsync();
        }
    }
}
