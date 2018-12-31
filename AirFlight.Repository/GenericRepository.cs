using AirFlight.Model.Context;
using AirFlight.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var id = JsonContext.GetListByType<T>().Count() > 0
                        ? (int) JsonContext.GetListByType<T>().Max(x => x.GetType().GetProperty("Id").GetValue(x)) + 1
                        : 1;
            entity.GetType().GetProperty("Id").SetValue(entity, id);
            JsonContext.GetListByType<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            JsonContext.GetListByType<T>().Remove(entity);
        }

        public void Edit(T entity)
        {
            var itemId = (int)entity.GetType().GetProperty("Id").GetValue(entity);
            var itemToEdit = FindById(itemId);
            Delete(itemToEdit);
            Add(entity);
        }

        public IList<T> FindBy(Func<T, bool> predicate)
        {
            return JsonContext.GetListByType<T>().Where(predicate).ToList();
        }

        public T FindById(int id)
        {
            return JsonContext.GetListByType<T>().FirstOrDefault(x => (int)x.GetType().GetProperty("Id").GetValue(x) == id);
        }

        public IList<T> GetAll()
        {
            return JsonContext.GetListByType<T>();
        }
    }
}
