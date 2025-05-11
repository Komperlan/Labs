using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.LectionMaterials;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.AcademicSubjects;

public interface IAcademicSubjectBuilder
{
    IAcademicSubjectBuilder AddAutor(User autor);

    IAcademicSubjectBuilder AddLection(LectionMaterial lection);

    IAcademicSubjectBuilder AddLabwork(Labwork lab);

    IAcademicSubjectBuilder AddName(string name);

    AcademicSubjectBase Build();
}
