namespace Rookie.Application.Ratings.ViewModels
{
    public class RatingVm
    {
        public string ProductName { get; set; }
        public List<string> UserNames { get; set; } = new List<string>();
        public double Rating { get; set; }
        public List<string> Comments { get; set; } = new List<string>();
    }
}