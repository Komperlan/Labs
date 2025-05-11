using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.LectionMaterials;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.AcademicSubjects;

public abstract class AcademicSubjectBase : BaseEntity, IPrototype<AcademicSubjectBase>
{
    public override Guid ID { get; protected set; }

    public string? Name { get; protected set; }

    public User? Autor { get; protected set; }

    public Repository<Labwork>? Labs { get; protected set; } = new Repository<Labwork>();

    public Repository<LectionMaterial>? Lections { get; protected set; }

    public ChangeResultType ChangeName(User user, string newName)
    {
        if (user != Autor)
        {
            return new ChangeResultType(false, user);
        }

        Name = newName;
        return new ChangeResultType(true, user);
    }

    public abstract AcademicSubjectBase Clone(User autor);

    public abstract IResultType AddLabwork(Labwork labwork, User autor);

    public abstract IResultType AddLection(LectionMaterial lection, User autor);

    public abstract IResultType RemoveLabwork(Guid id, User autor);

    public abstract IResultType RemoveLection(Guid id, User autor);
}