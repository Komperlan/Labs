using Itmo.ObjectOrientedProgramming.Lab2.AcademicSubjects;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms;

public class DefaultEducationalProgram : IEducationalProgram
{
    public Guid ID { get; }

    public string? Name { get; private set; }

    public Repository<AcademicSubjectBase>? AcademicSubjects { get; private set; }

    public int NumOfSemestr { get; private set; }

    public User? Director { get; private set; }

    public DefaultEducationalProgram(string? name, Repository<AcademicSubjectBase>? academicSubjects, int numOfSemestr, User? director)
    {
        ID = Guid.NewGuid();
        Name = name ?? throw new ArgumentNullException(nameof(name));
        AcademicSubjects = academicSubjects ?? throw new ArgumentNullException(nameof(academicSubjects));
        Director = director ?? throw new ArgumentNullException(nameof(director));
        NumOfSemestr = numOfSemestr;
    }
}
