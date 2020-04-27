using LMS.Model.Entities;
using LMS.Repository.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        IEnumerable<Book> GetBooks();
        IEnumerable<Book> GetBooksByCurrentYear();
    
    }
}
