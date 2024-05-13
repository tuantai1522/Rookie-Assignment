namespace Rookie.Domain.Common
{
    public class EfCoreValueConverterAttribute : Attribute
    {
        public EfCoreValueConverterAttribute(Type valueConverter)
        {
            ValueConverter = valueConverter;
        }

        public Type ValueConverter { get; }
    }
}