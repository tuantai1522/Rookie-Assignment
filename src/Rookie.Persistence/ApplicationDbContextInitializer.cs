using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Rookie.Domain.ApplicationUserEntity;
using Rookie.Persistence;

namespace Rookie.Persistence
{
    public sealed class ApplicationDbContextInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ApplicationDbContextInitializer> _logger;

        public ApplicationDbContextInitializer(ApplicationDbContext context,
                                              RoleManager<ApplicationRole> roleManager,
                                              UserManager<ApplicationUser> userManager,
                                              ILogger<ApplicationDbContextInitializer> logger)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{Prefix}] Seeding error with message: {Message}",
                    nameof(ApplicationDbContextInitializer), ex.Message);
            }
        }

        private async Task TrySeedAsync()
        {
            //Seeding role
            var admin = new ApplicationRole("Admin");

            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(admin);
            else
                _logger.LogDebug("[{Prefix}] admin already exists", nameof(ApplicationDbContextInitializer));


            var customer = new ApplicationRole("Customer");

            if (!await _roleManager.RoleExistsAsync("Customer"))
                await _roleManager.CreateAsync(customer);
            else
                _logger.LogDebug("[{Prefix}] customer already exists", nameof(ApplicationDbContextInitializer));


            const string password = "P@ssw0rd";

            var user1 = new ApplicationUser
            {
                Email = "user1@gmail.com",
                FirstName = "John",
                LastName = "Doe",
                UserName = "J.Doe"
            };
            if (_userManager.Users.All(u => u.UserName != user1.UserName))
            {
                var result = _userManager.CreateAsync(user1, password).Result;

                if (!result.Succeeded)
                    throw new ValidationException(result.Errors.First().Description);

                await _userManager.AddToRolesAsync(user1, [admin.Name!, customer.Name!]);

                _logger.LogDebug("[{Prefix}] administrator created", nameof(ApplicationDbContextInitializer));
            }
            else
            {
                _logger.LogDebug("[{Prefix}] administrator already exists", nameof(ApplicationDbContextInitializer));
            }

            var user2 = new ApplicationUser
            {
                Email = "user2@gmail.com",
                FirstName = "Michael",
                LastName = "Cole ",
                UserName = "M.Cole"
            };

            if (_userManager.Users.All(u => u.UserName != user2.UserName))
            {
                var result = _userManager.CreateAsync(user2, password).Result;

                if (!result.Succeeded)
                    throw new ValidationException(result.Errors.First().Description);

                await _userManager.AddToRoleAsync(user2, customer.Name!);

                _logger.LogDebug("[{Prefix}] customer created", nameof(ApplicationDbContextInitializer));
            }
            else
            {
                _logger.LogDebug("[{Prefix}] customer already exists", nameof(ApplicationDbContextInitializer));
            }
        }
    }
}

public static class InitializerExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();
        await initializer.SeedAsync();
    }
}