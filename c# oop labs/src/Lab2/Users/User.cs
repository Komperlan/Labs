namespace Itmo.ObjectOrientedProgramming.Lab2.Users;

public class User
{
    public Guid ID { get; }

    public string Name { get; }

    public User(string name)
    {
        ID = Guid.NewGuid();
        Name = name;
    }
}
