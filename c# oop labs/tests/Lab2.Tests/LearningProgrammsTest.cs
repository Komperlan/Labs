using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

using Xunit;

namespace Lab2.Tests;

public class LearningProgrammsTest
{
    [Fact]
    public void Labwork_ShouldReturnTrue_WhenCloningSucces()
    {
        var user1 = new User("Dryu");
        var user2 = new User("Looser");
        var user3 = new User("Asafev");

        Labwork lab = Labwork.Builder
            .AddDescription("description")
            .AddAutor(user1)
            .AddScore(20)
            .AddEvaluationCriteria("ggwp")
            .AddName("lab-2")
            .Build();

        Labwork lab2 = lab.Clone(user2);

        Assert.NotNull(lab2.BasiclyID);
    }

    [Fact]
    public void Labwork_ShouldReturnTrue_WhenCloningSuccesAndNewIDIsNew()
    {
        var user1 = new User("Dryu");
        var user2 = new User("Looser");

        Labwork lab = Labwork.Builder
            .AddDescription("description")
            .AddAutor(user1)
            .AddScore(20)
            .AddEvaluationCriteria("ggwp")
            .AddName("lab-2")
            .Build();

        Labwork lab2 = lab.Clone(user2);

        Assert.NotEqual(lab2.ID, lab.ID);
    }

    [Fact]
    public void Labwork_ShouldReturnTrue_WhenCloningSuccesAndBasiclyIDIsLab1ID()
    {
        var user1 = new User("Dryu");
        var user2 = new User("Looser");

        Labwork lab = Labwork.Builder
            .AddDescription("description")
            .AddAutor(user1)
            .AddScore(20)
            .AddEvaluationCriteria("ggwp")
            .AddName("lab-2")
            .Build();

        Labwork lab2 = lab.Clone(user2);

        Assert.Equal(lab2.BasiclyID, lab.ID);
    }

    [Fact]
    public void Labwork_ShouldReturnTrue_WhenAutorChangingLabwork()
    {
        var user1 = new User("Dryu");
        var user2 = new User("Looser");

        Labwork lab = Labwork.Builder
            .AddDescription("description")
            .AddAutor(user1)
            .AddScore(20)
            .AddEvaluationCriteria("ggwp")
            .AddName("lab-2")
            .Build();

        ChangeResultType result = lab.ChangeName(user1, "labka");

        Assert.True(result.IsSuccess);
        Assert.Equal(result.User, user1);
    }

    [Fact]
    public void Labwork_ShouldReturnFalse_WhenAnotherUserChangingLabwork()
    {
        var user1 = new User("Dryu");
        var user2 = new User("Looser");

        Labwork lab = Labwork.Builder
            .AddDescription("description")
            .AddAutor(user1)
            .AddScore(20)
            .AddEvaluationCriteria("ggwp")
            .AddName("lab-2")
            .Build();

        ChangeResultType result = lab.ChangeName(user2, "labka");

        Assert.False(result.IsSuccess);
        Assert.Equal(result.User, user2);
    }

    [Fact]
    public void Labwork_ShouldReturnTrue_WhenCloningClone()
    {
        var user1 = new User("Dryu");
        var user2 = new User("Looser");
        var user3 = new User("Asafev");

        Labwork lab = Labwork.Builder
            .AddDescription("description")
            .AddAutor(user1)
            .AddScore(20)
            .AddEvaluationCriteria("ggwp")
            .AddName("lab-2")
            .Build();

        Labwork lab2 = lab.Clone(user2).Clone(user3);

        Assert.Equal(lab2.Autor, user3);
    }
}
