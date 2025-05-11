using Itmo.ObjectOrientedProgramming.Lab2.AcademicSubjects.Subjects;
using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.LectionMaterials;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.AcademicSubjects.Factories;

public class TestAcademicSubjectFactory : IAcademicSubjectFactory
{
    public AcademicSubjectBase CreateAcademicSubject(Repository<Labwork> labs, Repository<LectionMaterial> lecs, string name, User autor, int score)
    {
        foreach (Labwork labwork in labs)
        {
            ExamAcademicSubject.Builder.AddLabwork(labwork);
        }

        foreach (LectionMaterial lec in lecs)
        {
            ExamAcademicSubject.Builder.AddLection(lec);
        }

        return TestAcademicSubject.Builder.AddTestScore(score).AddName(name).AddAutor(autor).Build();
    }
}
