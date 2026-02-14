using System.Linq.Expressions;

namespace TP4.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        // Méthodes de lecture
        IQueryable<T> GetAll();
        T GetById(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        
        // Méthodes d'écriture
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        
        // Méthodes avec Include
        IQueryable<T> GetAllWithIncludes(params Expression<Func<T, object>>[] includes);
    }
}