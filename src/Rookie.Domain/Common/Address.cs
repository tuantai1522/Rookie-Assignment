using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Rookie.Domain.Common
{
    [Owned]
    public class Address
    {
        [Column("Address")]
        public string? Value { get; set; }
        [Column("City")]
        public string? City { get; set; }
        [Column("Country")]
        public string? Country { get; set; }
        [Column("ZipCode")]
        public string? ZipCode { get; set; }
    }
}