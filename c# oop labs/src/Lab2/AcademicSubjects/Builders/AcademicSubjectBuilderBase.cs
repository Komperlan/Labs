using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.LectionMaterials;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.AcademicSubjects.Builders;

public abstract class AcademicSubjectBuilderBase : IAcademicSubjectBuilder
{
    private protected Repository<Labwork> Labs { get; } = new Repository<Labwork>();

    private protected Repository<LectionMaterial> Lections { get; } = new Repository<LectionMaterial>();

    private protected string? Name { get; private set; }

    private protected User? Autor { get; private set; }

    public IAcademicSubjectBuilder AddAutor(User autor)
    {
        Autor = autor;
        return this;
    }

    public IAcademicSubjectBuilder AddLection(LectionMaterial lection)
    {
        Lections.Add(lection);
        return this;
    }

    public IAcademicSubjectBuilder AddLabwork(Labwork lab)
    {
        Labs.Add(lab);
        return this;
    }

    public IAcademicSubjectBuilder AddName(string name)
    {
        Name = name;
        return this;
    }

    public abstract AcademicSubjectBase Build();
}
