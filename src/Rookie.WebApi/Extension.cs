namespace Rookie.WebApi
{
    public static class Extension
    {
        public static void AddApi(this WebApplication app)
        {
            app.AddCors();
            app.AddIdentity();
        }

        private static void AddCors(this WebApplication app)
        {
            //to fix CORS policy
            app.UseCors(options =>
            {
                options.AllowAnyHeader()
                       .AllowAnyMethod()
                       .AllowCredentials()
                       .WithOrigins("http://localhost:3000")
                       .WithOrigins("http://localhost:5227")
                       .WithExposedHeaders("pagination");
            });
        }

        private static void AddIdentity(this WebApplication app)
        {
            app.UseAuthentication();
            app.UseAuthorization();
        }


    }
}