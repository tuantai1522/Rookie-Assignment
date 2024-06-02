namespace Rookie.WebApi.Controllers.Orders.Request
{
    public sealed record GetByIdRequest
    {
        public string? OrderId { get; set; }
    }
}