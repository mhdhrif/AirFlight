using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AirFlight.Repository.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IList<T> GetAll();
        IList<T> FindBy(Func<T, bool> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Edit(T entity);
        T FindById(int id);
    }
}
