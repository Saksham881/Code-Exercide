using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using TechLibrary.Domain;
using TechLibrary.Models;
using TechLibrary.Services;
using TechLibrary.Parameters;
using TechLibrary.BookCreationDTO;
using TechLibrary.Helper;
using System.Text.Json;

namespace TechLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : Controller
    {
        private readonly ILogger<BooksController> _logger;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;

        public BooksController(ILogger<BooksController> logger, IBookService bookService, IMapper mapper)
        {
            _logger = logger;
            _bookService = bookService;
            _mapper = mapper;
        }
 
        [HttpGet(Name = "GetAll")]
        public async Task<ActionResult<BookResponse>> GetAll([FromQuery] Resourseparameter resourseparameter)
        {
            _logger.LogInformation("Get all books");

            var bookbytitle = await _bookService.GetBooksAsync(resourseparameter);

            var previousPageLink = bookbytitle.HasPrevious ?
                CreateAuthorsResourceUri(resourseparameter,
                ResourceUriType.PreviousPage) : null;

            var nextPageLink = bookbytitle.HasNext ?
                CreateAuthorsResourceUri(resourseparameter,
                ResourceUriType.NextPage) : null;

            var paginationMetadata = new
            {
                totalCount = bookbytitle.TotalCount,
                pageSize = bookbytitle.PageSize,
                currentPage = bookbytitle.CurrentPage,
                totalPages = bookbytitle.TotalPages,
                previousPageLink,
                nextPageLink
            };

            // Response.Body["xPagination"] = JsonSerializer.Serialize(paginationMetadata));

            var bookResponse = new
            {
                Data = _mapper.Map<List<BookResponse>>(bookbytitle),
                PaginationMetadata = paginationMetadata
            };

            return Ok(bookResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation($"Get book by id {id}");

            var book = await _bookService.GetBookByIdAsync(id);

            var bookResponse = _mapper.Map<BookResponse>(book);

            return Ok(bookResponse);
        }

        [HttpPost]
        [Produces("application/json")]
        public ActionResult<int> CreateAuthor(BookCreationdto bookCreationdto)
        {
            bookCreationdto.ShortDescr = bookCreationdto.Descr;
            bookCreationdto.LongDescr = bookCreationdto.Descr;
            var bookEntity = _mapper.Map<Book>(bookCreationdto);
            _bookService.CreateBook(bookEntity);
            _bookService.Save();

            return Ok(bookEntity.BookId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(
           BookCreationdto book)
        {
            book.ShortDescr = book.Descr;
            book.LongDescr = book.Descr;
            var bookforupdate =  await _bookService.GetBookByIdAsync(book.BookId);

            var respose=   _mapper.Map(book, bookforupdate);
            _bookService.UpdateBook(respose); 
            _bookService.Save();
            return Ok(respose);
        }
 

        private string CreateAuthorsResourceUri(
           Resourseparameter authorsResourceParameters,
           ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link("GetAll",
                      new
                      {
                          pageNumber = authorsResourceParameters.PageNumber - 1,
                          pageSize = authorsResourceParameters.PageSize,
                          Title = authorsResourceParameters.Title,
                          Description = authorsResourceParameters.Description
                      });
                case ResourceUriType.NextPage:
                    return Url.Link("GetAll",
                      new
                      {
                          pageNumber = authorsResourceParameters.PageNumber + 1,
                          pageSize = authorsResourceParameters.PageSize,
                          Title = authorsResourceParameters.Title,
                          Description = authorsResourceParameters.Description
                      });

                default:
                    return Url.Link("GetAll",
                    new
                    {
                        pageNumber = authorsResourceParameters.PageNumber,
                        pageSize = authorsResourceParameters.PageSize,
                        Title = authorsResourceParameters.Title,
                        Description = authorsResourceParameters.Description
                    });
            }

        }


    }
}
