using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2;

public interface IPrototype<T> where T : IPrototype<T>
{
    T Clone(User autor);
}
