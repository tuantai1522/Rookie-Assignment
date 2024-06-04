namespace Rookie.Domain.Common
{
    public class BaseEntity
    {
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }

    }
}