using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.Repository.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers.Base
{

    public abstract class BaseController<TModel, TRepository> : ControllerBase where TModel : class where TRepository : IRepository<TModel>
    {
        protected readonly TRepository Repository;

        public BaseController(TRepository repository)
        {
            this.Repository = repository;
        }

        [HttpGet]
        public IEnumerable<TModel> Get()
        {
            return Repository.GetAll();
        }

        [HttpPost]
        public void Add([FromBody] TModel item)
        {
            Repository.Add(item);
        }
    }
}
