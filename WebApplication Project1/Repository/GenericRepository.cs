﻿/*using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApplication_Project1.IRepository;
using WebApplication_Project1.Models;

namespace WebApplication_Project1.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {

        private readonly DatabaseContext _context;
        private readonly DbSet<T> _db;

        public GenericRepository(DatabaseContext context) {
            _context = context;
            _db =_context.Set<T>();
        }
        
public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? expression = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<string>? includes = null)
        {
            IQueryable<T> query = _db;

            //expression refers to like the condition...i.e(get me hotels located in Uganda..thats an expression)

            if (expression != null) { 
            
                query = query.Where(expression);
            }

            // if someone adds the includes in the req...ie this is where we talk about the foreign key(entity) being returned, it brings the info on that foreing entity...ie. id, name, etc etc
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression, List<string>? includes = null)
        {
            IQueryable<T> query = _db;

            if (includes is not null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task Insert(T entity)
        {
            await _db.AddAsync(entity);
        }

        public async Task InsertRange(IEnumerable<T> entities)
        {
         

            await _db.AddRangeAsync(entities);

        }

        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);

            if(entity is not null)
            {
                _db.Remove(entity);
            }
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
          
                _db.RemoveRange(entities);
        
        }

        public void Update(T entity)
        {
            // Check if there is a difference between what has come from the user input and what is in the Database
            _db.Attach(entity);
            // If there are changes then modify/make the update
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
*/