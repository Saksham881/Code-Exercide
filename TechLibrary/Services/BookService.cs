using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TechLibrary.BookCreationDTO;
using TechLibrary.Data;
using TechLibrary.Domain;
using TechLibrary.Helper;
using TechLibrary.Models;
using TechLibrary.Parameters;

namespace TechLibrary.Services
{
    public interface IBookService
    {
        Task<PagedList<Book>> GetBooksAsync(Resourseparameter resourseparameter);
        Task<Book> GetBookByIdAsync(int bookid);
        public bool Save();

        void UpdateBook(Book book);


        Task  CreateBook(Book bookCreationdto);
        //Task<List<Book>> GetBooksbytitle();
    }

    public class BookService : IBookService
    {
        private readonly DataContext _dataContext;

        public BookService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<PagedList<Book>> GetBooksAsync(Resourseparameter resourseparameter )
        {
            return Task.Run(() =>
            {
                var queryable = _dataContext.Books.AsQueryable();
                if (!string.IsNullOrWhiteSpace(resourseparameter.Title))
                {

                    var searchQuery = resourseparameter.Title.Trim();
                    queryable = queryable.Where(a => a.Title.Contains(searchQuery));

                }
                else if (!string.IsNullOrWhiteSpace(resourseparameter.Description))
                {

                    var searchQuery = resourseparameter.Description.Trim();
                    queryable = queryable.Where(a => a.LongDescr.Contains(searchQuery));

                }
                // return await queryable.ToListAsync();

                return   PagedList<Book>.Create(queryable, resourseparameter.PageNumber,
                    resourseparameter.PageSize);
            });
        }

        public async Task<Book> GetBookByIdAsync(int bookid)
        {
            return await _dataContext.Books.SingleOrDefaultAsync(x => x.BookId == bookid);
        }


        public Task CreateBook(Book bookCreationdto)
        {
            if (bookCreationdto == null)
            {
                throw new ArgumentNullException(nameof(bookCreationdto));
            }

            var books = _dataContext.Books.OrderByDescending(x => x.BookId).First();
            bookCreationdto.BookId = books.BookId + 1;
            
            _dataContext.AddAsync(bookCreationdto);
            return null;
        }

        public bool Save()
        {
            return (_dataContext.SaveChanges() >= 0);
        }

        public void UpdateBook(Book book)
        {
           // throw new NotImplementedException();
        }
    }
}
