using ArtemisApi.Data;
using ArtemisApi.Interfaces;
using ArtemisApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtemisApi.Repositories;

public class BookRepository(DataContext context) : IBookRepository
{
    public async Task<Book?> GetBookByExternalIdAsync(string externalId)
    {
        return await context.Books.FirstOrDefaultAsync(book => book.ExternalId == externalId);
    }

    public async Task<(bool isSuccess, string? ErrorMessage)> CreateBookAsync(Book book)
    {
        try
        {
            if (!string.IsNullOrEmpty(book.ExternalId))
            {
                var existingBook = await GetBookByExternalIdAsync(book.ExternalId);
                if (existingBook != null)
                {
                    return (false, "A book with the same external id already exists.");
                }
            }

            context.Books.Add(book);
            await context.SaveChangesAsync();
            
            return (true, null);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool isSuccess, string? ErrorMessage)> UpdateBookAsync(Book book)
    {
        try
        {
            context.Books.Update(book);
            await context.SaveChangesAsync();

            return (true, null);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }

    public async Task<(bool isSuccess, string? ErrorMessage)> DeleteBookAsync(Book book)
    {
        try
        {
            context.Books.Remove(book);
            await context.SaveChangesAsync();

            return (true, null);
        }
        catch (Exception e)
        {
            return (false, e.Message);
        }
    }
}