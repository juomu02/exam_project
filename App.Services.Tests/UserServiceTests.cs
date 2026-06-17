using Microsoft.AspNetCore.Identity;
using Moq;
using App.Entities;
using App.Repositories;

namespace App.Services.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> userRepositoryMock =
            new Mock<IUserRepository>();
        private readonly Mock<IPasswordHasher<User>> passwordHasherMock =
            new Mock<IPasswordHasher<User>>();
        private readonly UserService userService;

        public UserServiceTests()
        {
            userService = new UserService(userRepositoryMock.Object,
                passwordHasherMock.Object);
        }

        [Fact]
        public async Task AddAsync_ReturnsUserId_WhenUserIsAddedSuccessfully()
        {
            var userName = "testuser";
            var email = "testemail@test.com";
            var password = "testpassword";
            var expectedUserId = 50;

            userRepositoryMock.Setup(dbContext => dbContext.GetByUserNameAsync(userName))
                .ReturnsAsync((User)null);

            userRepositoryMock.Setup(dbContext => dbContext.GetByEmailAsync(email))
                .ReturnsAsync((User)null);

            passwordHasherMock.Setup(hasher => hasher.HashPassword(It.IsAny<User>(), password))
                .Returns("hashedpassword");

            userRepositoryMock.Setup(dbContext => dbContext.AddAsync(It.IsAny<User>()))
                .ReturnsAsync(expectedUserId);

            var result = await userService.AddAsync(userName, email, password);

            Assert.Equal(expectedUserId, result);
        }

        [Fact]
        public async Task AddAsync_ThrowsArgumentException_WhenUserNameAlreadyExists()
        {
            var userName = "existinguser";
            var email = "testemail@test.com";
            var password = "testpassword";
            var existingUser = new User { Id = 50, Email = "existing@example.com", UserName = userName };

            userRepositoryMock.Setup(dbContext => dbContext.GetByUserNameAsync(userName))
                .ReturnsAsync(existingUser);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await userService.AddAsync(userName, email, password)
            );

            Assert.Equal("User name is already in use.", exception.Message);
        }

        [Fact]
        public async Task AddAsync_ThrowsArgumentException_WhenEmailAlreadyExists()
        {
            var userName = "testuser";
            var email = "duplicate@email.com";
            var password = "testpassword";
            var existingUser = new User { Id = 50, Email = email, UserName = "existinguser" };

            userRepositoryMock.Setup(dbContext => dbContext.GetByUserNameAsync(userName))
                .ReturnsAsync((User)null);

            userRepositoryMock.Setup(dbContext => dbContext.GetByEmailAsync(email))
                .ReturnsAsync(existingUser);

            var exception = await Assert.ThrowsAsync<ArgumentException>(async () =>
                await userService.AddAsync(userName, email, password)
            );
            Assert.Equal("Email is already in use.", exception.Message);
        }

        [Fact]
        public async Task LoginAsync_ReturnsUser_WhenCredentialsAreValid()
        {
            var email = "testemail@test.com";
            var password = "testpassword";
            var user = new User { Id = 50, Email = email, UserName = "testuser" };

            userRepositoryMock.Setup(dbContext => dbContext.GetByEmailAsync(email))
                .ReturnsAsync(user);

            passwordHasherMock.Setup(hasher => hasher.VerifyHashedPassword(user,
                user.PasswordHash, password)).Returns(PasswordVerificationResult.Success);

            var result = await userService.LoginAsync(email, password);

            Assert.Equal(user, result);
        }

        [Fact]
        public async Task LoginAsync_ReturnsNull_WhenCredentialsAreInvalid()
        {
            var email = "testemail@test.com";
            var password = "testpassword";

            userRepositoryMock.Setup(dbContext => dbContext.GetByEmailAsync(email))
                .ReturnsAsync((User)null);

            var result = await userService.LoginAsync(email, password);

            Assert.Null(result);
        }
    }
}