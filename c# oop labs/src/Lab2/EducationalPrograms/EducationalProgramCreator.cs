using Itmo.ObjectOrientedProgramming.Lab2.AcademicSubjects;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.EducationalPrograms;

public abstract class EducationalProgramCreator
{
    public abstract IEducationalProgram CreateEducationalProgram(string name, Repository<AcademicSubjectBase> academicSubjects, int numOfSemestr, User director);
}
