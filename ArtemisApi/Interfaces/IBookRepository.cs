using ArtemisApi.Models;

namespace ArtemisApi.Interfaces;

public interface IBookRepository
{
    Task<Book?> GetBookByExternalIdAsync(string externalId);
    Task<(bool isSuccess, string? ErrorMessage)> CreateBookAsync(Book book);
    Task<(bool isSuccess, string? ErrorMessage)> UpdateBookAsync(Book book);
    Task<(bool isSuccess, string? ErrorMessage)> DeleteBookAsync(Book book);
}