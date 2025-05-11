using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.BankSystem;
using Lab5.Application.Models.Users;
using Lab5.Application.Users;
using Moq;
using Xunit;

namespace Lab5.Tests;

public class BankSystemTest
{
    [Fact]
    public void Balance_ShouldReturnTrue_WhenNewBalanceIsCorrected()
    {
        var rep = new Mock<IUserRepository>();
        var man = new CurrentUserManager();

        man.User = new User(0, "100", UserRole.Employee, 0, 0);

        var service = new ChangeScoreService(rep.Object, man);

        service.AddScore(1000);

        rep.Verify(service => service.UpdateUserScore(1000, It.IsAny<long>()), Times.Once());
    }

    [Fact]
    public void Balance_ShouldReturnFalse_WhenUserBalanceIsNotEnough()
    {
        var rep = new Mock<IUserRepository>();
        var man = new CurrentUserManager();

        man.User = new User(0, "100", UserRole.Employee, 990, 0);

        var service = new ChangeScoreService(rep.Object, man);

        service.RemoveScore(1000);

        rep.Verify(service => service.UpdateUserScore(It.IsAny<long>(), It.IsAny<long>()), Times.Never());
    }

    [Fact]
    public void Balance_ShouldReturnTrue_WhenNewBalanceIsEnough()
    {
        var rep = new Mock<IUserRepository>();
        var man = new CurrentUserManager();

        man.User = new User(0, "100", UserRole.Employee, 1100, 0);

        var service = new ChangeScoreService(rep.Object, man);

        service.RemoveScore(1000);

        rep.Verify(service => service.UpdateUserScore(It.IsAny<long>(), It.IsAny<long>()), Times.Once());
    }
}
