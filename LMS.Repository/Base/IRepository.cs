using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Repository.Base
{
    public interface IRepository<TModel> where TModel : class
    {
        TModel Get(int id);
        IEnumerable<TModel> GetAll();
        void Add(TModel entity);
        void Remove(TModel entity);
    }
}
