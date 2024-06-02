namespace Rookie.WebApi.Controllers.Categories.Request
{
    public sealed record GetByIdRequest
    {
        public string? CategoryId { get; set; }

    }
}