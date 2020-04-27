using LMS.Data.Abstract;
using LMS.Data.Context;
using LMS.Model.Entities;
using LMS.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LMS.Data
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public ApplicationDatabaseContext ApplicationDatabaseContext
        {
            get { return DatabaseContext as ApplicationDatabaseContext; }
        }

        public BookRepository(ApplicationDatabaseContext context) : base(context) { }

        public IEnumerable<Book> GetBooks()
        {
            return ApplicationDatabaseContext.Books.ToList();
        }

        public IEnumerable<Book> GetBooksByCurrentYear()
        {
            return ApplicationDatabaseContext.Books.Where(b => b.Date.Year >= DateTime.Now.Year).ToList();
        }
        
    }
}
