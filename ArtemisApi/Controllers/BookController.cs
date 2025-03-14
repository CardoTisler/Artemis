using ArtemisApi.Models;
using ArtemisApi.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ArtemisApi.Controllers;

[Route("/books")]
[ApiController]
public class BookController(BookRepository repository) : Controller
{
    [HttpGet("{externalId}", Name = "GetBookByExternalId")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Book))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBookByExternalId([FromRoute] string externalId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var book = await repository.GetBookByExternalIdAsync(externalId);
        if (book == null) return NotFound();

        return Ok(book);
    }

    [HttpPost(Name = "CreateBook")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Book))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateBook([FromBody] Book book)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await repository.CreateBookAsync(book);

        return Ok();
    }

    // TODO: Should require special permissions
    [HttpDelete("{externalId}", Name = "DeleteBook")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteBook([FromRoute] string externalId)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        
        var book = await repository.GetBookByExternalIdAsync(externalId);
        if (book == null) return NotFound();
        
        await repository.DeleteBookAsync(book);
        return NoContent();
    }

    [HttpPut(Name = "UpdateBook")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateBook([FromBody] Book book)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        await repository.UpdateBookAsync(book);
        
        return NoContent();
    }
}