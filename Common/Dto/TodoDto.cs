namespace Common.Dto;

public class TodoDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required bool IsCompleted { get; set; }
    public required int UserId { get; set; }
}