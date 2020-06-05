using Backstage.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Backstage.Repositories
{
    public class SoulfulRepository<T> where T : class
    {
        private readonly SoulfulBackStage _context;
        public SoulfulBackStage Context { get { return _context; } }

        public SoulfulRepository(SoulfulBackStage context)
        {
            if (context == null)
            {
                throw new ArgumentNullException();
            }

            _context = context;
        }
        public void Create(T value)
        {
            _context.Entry(value).State = EntityState.Added;
        }
        public void Update(T value)
        {
            _context.Entry(value).State = EntityState.Modified;
        }
        public void Delete(T value)
        {
            _context.Entry(value).State = EntityState.Deleted;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }
    }
}