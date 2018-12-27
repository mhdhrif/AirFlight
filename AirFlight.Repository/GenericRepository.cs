using AirFlight.Model.Context;
using AirFlight.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AirFlight.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public AirFlightJsonContext JsonContext { get; set; }

        public GenericRepository(AirFlightJsonContext jsonContext)
        {
            JsonContext = jsonContext;
        }

        public void Add(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Edit(T entity)
        {
            throw new NotImplementedException();
        }

        public IList<T> FindBy(Func<T, bool> predicate)
        {
            return JsonContext.FindBy<T>(predicate);
        }

        public T FindById(int id)
        {
            return JsonContext.Find<T>(id);
        }

        public IList<T> GetAll()
        {
            return JsonContext.GetListByType<T>();
        }
    }
}
