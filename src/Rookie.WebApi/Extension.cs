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
                options.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader()
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