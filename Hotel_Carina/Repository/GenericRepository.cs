﻿using Hotel_Carina.Data;
using Hotel_Carina.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Hotel_Carina.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DataBaseContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(DataBaseContext context )
        {
            _context = context;
            _db = _context.Set<T>();
        }
        public async Task Delete(int id)
        {
           var entity =   await _db.FindAsync(id);
            _context.Remove(entity);
        }

        public void  DeleteRange(IEnumerable<T> entities)
        {
            _db.RemoveRange(entities);
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<T>> GetAll(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
            throw new NotImplementedException();
        }

        public async  Task Insert(T entity)
        {
           await _db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
            await _db.AddRangeAsync(entities);
        }

        public void Update(T entity)
        {//this is done as a 2 step process
            // attache means pay attention to this, it attches the object(entity ) to the Dbset of the enityFrameworkcore

            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;   // says that if the entitiy is modiefied 
                                                                    //then update whwta we have in the database
        }
    }

}