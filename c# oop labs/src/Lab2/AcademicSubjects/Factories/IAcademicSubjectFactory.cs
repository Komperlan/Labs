using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.LectionMaterials;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.AcademicSubjects.Factories;

public interface IAcademicSubjectFactory
{
    public AcademicSubjectBase CreateAcademicSubject(Repository<Labwork> labs, Repository<LectionMaterial> lecs, string name, User autor, int score);
}
