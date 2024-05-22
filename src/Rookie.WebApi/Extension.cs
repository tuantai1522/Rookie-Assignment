namespace Rookie.WebApi
{
    public static class Extension
    {
        public static void AddApi(this WebApplication app)
        {
            //to fix CORS policy
            app.UseCors(options =>
            {
                options.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .WithExposedHeaders("pagination");
            });
        }
    }
}