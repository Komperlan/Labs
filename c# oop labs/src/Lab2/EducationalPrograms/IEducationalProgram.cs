using Itmo.ObjectOrientedProgramming.Lab2.AcademicSubjects;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms;

public interface IEducationalProgram
{
    public Guid ID { get; }

    public string? Name { get; }

    public Repository<AcademicSubjectBase>? AcademicSubjects { get; }

    public int NumOfSemestr { get; }

    public User? Director { get; }
}
