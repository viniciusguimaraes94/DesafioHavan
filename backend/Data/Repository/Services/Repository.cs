using System.Threading.Tasks;
using backend.Data.Repository.Interfaces;

namespace backend.Data.Repository.Services
{
    public class Repository : IRepository
    {
       private readonly DataContext _contexto;

        public Repository(DataContext contexto)
        {
            this._contexto = contexto;
        }


        public void Add<T>(T entity) where T : class
        {
            this._contexto.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            this._contexto.Remove(entity);
        }

        public async Task<bool> SaveChanges()
        {
            return (await this._contexto.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            this._contexto.Update(entity);
        } 
    }
}