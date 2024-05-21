namespace Rookie.Domain.Common
{
    public class Address : BaseEntity
    {
        public string? Value { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public string? ZipCode { get; set; }
    }
}