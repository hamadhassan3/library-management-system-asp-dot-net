using FluentValidation.Results;
using HH.Lms.Service;
using HH.Lms.Service.Dto;
using HH.Lms.Service.Library;
using HH.Lms.Service.Library.Dto;
using HH.Lms.Service.Library.Validator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HH.Lms.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : BaseController
{
    private readonly BookService bookService;

    public BookController(ILogger<BookController> logger, BookService bookService) : base(logger)
    {
        this.bookService = bookService;
    }

    /// <summary>
    /// This method gets all the books stored in the system.
    /// </summary>
    /// <returns> The list of books. </returns>
    [Authorize(Roles = "Admin,User")]
    [HttpGet("")]
    public async Task<ResponseDto<IEnumerable<BookDto>>> GetAllBooks()
    {
        ServiceResult<IEnumerable<BookDto>> books = await bookService.GetAllAsync();
        return Result(books);
    }

    /// <summary>
    /// This method finds books by title.
    /// </summary>
    /// <returns> The list of books. </returns>
    [Authorize(Roles = "Admin,User")]
    [HttpGet("find")]
    public async Task<ResponseDto<IEnumerable<BookDto>>> Find([FromQuery(Name = "title")] string? title,
        [FromQuery(Name = "isbn")] string? isbn)
    {
        ServiceResult<IEnumerable<BookDto>> books = await bookService.findByTitleOrIsbnAsync(title, isbn);
        return Result(books);
    }

    /// <summary>
    /// Issues a book to a user.
    /// </summary>
    /// <returns>The updated book.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("{bookId}/issue/{userId}")]
    public async Task<ServiceResult<BookDto>> IssueBook(int bookId, int userId)
    {
        return await bookService.issueBookAsync(bookId, userId);
    }

    /// <summary>
    /// Returns a book.
    /// </summary>
    /// <returns>The updated book.</returns>
    [Authorize(Roles = "Admin")]
    [HttpPost("{bookId}/return/{userId}")]
    public async Task<ServiceResult<BookDto>> ReturnBook(int bookId, int userId)
    {
        return await bookService.returnBookAsync(bookId, userId);
    }

    /// <summary>
    /// This method gets a book using its id.
    /// </summary>
    /// <returns> The Book. </returns>
    [Authorize(Roles = "Admin,User")]
    [HttpGet("{id}")]
    public async Task<ResponseDto<BookDto>> GetBookById(int id)
    {
        ServiceResult<BookDto> book = await bookService.GetAsync(id);
        return Result(book);
    }

    /// <summary>
    /// Creates a book using the data in payload.
    /// </summary>
    /// <returns> The created book. </returns>
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ResponseDto<BookDto>> CreateBook([FromBody] BookDto bookDto)
    {
        var validator = new BookDtoValidator();

        ValidationResult result = validator.Validate(bookDto);

        if (result.IsValid)
        {
            ServiceResult<BookDto> book = await bookService.AddAsync(bookDto);
            return Result(book);
        }
        else
        {
            return new ResponseDto<BookDto> { Success = false, Message = "Invalid Dto!", Errors = result.Errors.Select(e => e.ErrorMessage).ToList() };
        }
    }

    /// <summary>
    /// Updates a book using the data in payload.
    /// </summary>
    /// <returns> The updated book. </returns>
    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ResponseDto<BookDto>> UpdateBook([FromBody] BookDto bookDto)
    {
        var validator = new BookDtoValidator();

        ValidationResult result = validator.Validate(bookDto);

        if (result.IsValid)
        {
            ServiceResult<BookDto> book = await bookService.UpdateAsync(bookDto);
            return Result(book);
        }
        else
        {
            return new ResponseDto<BookDto> { Success = false, Message = "Invalid Dto!", Errors = result.Errors.Select(e => e.ErrorMessage).ToList() };
        }
    }

    /// <summary>
    /// Deletes a book using its id.
    /// </summary>
    /// <returns> Success if the book is deleted. </returns>
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<ResponseDto<string>> DeleteBook(int id)
    {
        await bookService.DeleteAsync(new BookDto { Id = id });
        return new ResponseDto<string>("Successfully Deleted.", true, "Success!");
    }
}
