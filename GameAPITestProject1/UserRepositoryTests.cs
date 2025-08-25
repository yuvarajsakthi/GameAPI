using Xunit;
using Moq;
using GameAPI.Models;
using GameAPI.Repositories.Interfaces;

namespace GameAPITestProject1
{
    public class UserRepositoryTests
    {
        [Fact]
        public async Task GetByEmailAsync_ReturnsUser_WhenEmailExists()
        {
            // Arrange
            var mockUserRepo = new Mock<IUser>();
            var expectedUser = new User { UserId = 1, UserName = "Yuvi", Email = "yuvi@example.com", Role = "Admin" };

            mockUserRepo.Setup(r => r.GetByEmailAsync("yuvi@example.com"))
                        .ReturnsAsync(expectedUser);

            // Act
            var result = await mockUserRepo.Object.GetByEmailAsync("yuvi@example.com");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Yuvi", result!.UserName);
            Assert.Equal("Admin", result.Role);
        }

        [Fact]
        public async Task GetByRoleAsync_ReturnsUsers_WhenRoleExists()
        {
            // Arrange
            var mockUserRepo = new Mock<IUser>();
            var users = new List<User>
            {
                new User { UserId = 1, UserName = "Yuvi", Role = "Admin" },
                new User { UserId = 2, UserName = "Raj", Role = "Admin" }
            };

            mockUserRepo.Setup(r => r.GetByRoleAsync("Admin"))
                        .ReturnsAsync(users);

            // Act
            var result = await mockUserRepo.Object.GetByRoleAsync("Admin");

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
