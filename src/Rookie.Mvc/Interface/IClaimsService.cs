namespace Rookie.Mvc.Interface
{
    public interface IClaimsService
    {
        public string GetCurrentUserRole { get; }
        public string GetCurrentUserName { get; }
    }
}