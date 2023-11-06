using ePizzaHub.Repositories.Interfaces;
using ePizzaHub.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Services.Implementation
{
    public class Service<TEntity> : IServices<TEntity> where TEntity : class
    {
       protected IRepository<TEntity> _repo;
        public Service(IRepository<TEntity> repo) 
        { 
            _repo = repo;
        }    
        public void Add(TEntity entity)
        {
            _repo.Add(entity);
            _repo.SaveChanges();
        }

        public void Delete(object id)
        {
           _repo.Delete(id);
            _repo.SaveChanges();

        }

        public TEntity Find(object Id)
        {
          return _repo.Find(Id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            return _repo.GetAll();
        }

        public void Remove(TEntity entity)
        {
           _repo.Remove(entity);
            _repo.SaveChanges();
        }


        public void Update(TEntity entity)
        {
            _repo.Update(entity);
            _repo.SaveChanges();
        }
    }
}
