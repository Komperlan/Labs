using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Labworks;

public interface ILabworkBuilder
{
    ILabworkBuilder AddAutor(User autor);

    ILabworkBuilder AddDescription(string description);

    ILabworkBuilder AddScore(int score);

    ILabworkBuilder AddName(string name);

    ILabworkBuilder AddEvaluationCriteria(string criteria);

    ILabworkBuilder AddBasiclyID(Guid id);

    Labwork Build();
}
