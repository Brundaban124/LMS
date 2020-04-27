using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LMS.API.Controllers.Base;
using LMS.Data;
using LMS.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : BaseController<Book, BookRepository>
    {
        public BooksController(BookRepository bookRepository) : base(bookRepository)
        {
        }

        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Book> ByCurrentYear()
        {
            return base.Repository.GetBooksByCurrentYear();
        }

        [HttpGet("{id}")]
        public Book Get(int id)
        {
            return base.Repository.Get(id);
        }

       
    }
}