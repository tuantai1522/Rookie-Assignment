using Microsoft.Extensions.Options;
using Moq;
using FluentAssertions;
using Rookie.Infrastructure.Security.TokenGenerator;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Rookie.Infrastructure.Tests.Security.TokenGenerator
{
    public class JwtTokenGeneratorTests
    {
        [Fact]
        public void GenerateToken_ReturnsCorrectToken()
        {
            // Arrange
            var jwtSettings = new JwtSettings
            {
                Secret = "fggsdfgsdfgsdfgert rtdfgsdfgsdfbfbsdvscfvbadfgsegsdfg",
                Issuer = "your_issuer_here",
                Audience = "your_audience_here",
                TokenExpirationInMinutes = 60
            };

            var jwtOptionsMock = new Mock<IOptions<JwtSettings>>();
            jwtOptionsMock.Setup(x => x.Value).Returns(jwtSettings);

            var jwtTokenGenerator = new JwtTokenGenerator(jwtOptionsMock.Object);

            var id = "test_id";
            var firstName = "John";
            var lastName = "Doe";
            var userName = "johndoe";
            var email = "johndoe@example.com";
            var roles = new List<string> { "Admin", "User" };

            // Act
            var token = jwtTokenGenerator.GenerateToken(id, firstName, lastName, userName, email, roles);

            // Assert
            Assert.NotNull(token);

            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(token);

            var claims = decodedToken.Claims;

            var nameClaim = claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Name);

            Assert.NotNull(nameClaim);
            Assert.Equal(firstName, nameClaim.Value);

            nameClaim.Should().NotBeNull();
            firstName.Should().Be(nameClaim.Value);

        }
    }
}
