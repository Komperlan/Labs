using Itmo.ObjectOrientedProgramming.Lab2.AcademicSubjects;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms.Creators;

public class DefaultEducationalProgramCreator : EducationalProgramCreator
{
    public override IEducationalProgram CreateEducationalProgram(string name, Repository<AcademicSubjectBase> academicSubjects, int numOfSemestr, User director)
    {
        return new DefaultEducationalProgram(name, academicSubjects, numOfSemestr, director);
    }
}
