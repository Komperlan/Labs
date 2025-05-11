using Itmo.ObjectOrientedProgramming.Lab2.AcademicSubjects.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.LectionMaterials;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.AcademicSubjects.Subjects;

public class TestAcademicSubject : AcademicSubjectBase
{
    public static TestAcademicSubjectBuilder Builder => new TestAcademicSubjectBuilder();

    public class TestAcademicSubjectBuilder : AcademicSubjectBuilderBase
    {
        private int _testScore;

        public override AcademicSubjectBase Build()
        {
            return Build(Name, Autor, Labs, Lections, _testScore);
        }

        public IAcademicSubjectBuilder AddTestScore(int testScore)
        {
            _testScore = testScore;
            return this;
        }

        protected AcademicSubjectBase Build(string? name, User? autor, Repository<Labwork> labs, Repository<LectionMaterial> lections, int testScore)
        {
            return new TestAcademicSubject(name, autor, labs, lections, testScore);
        }
    }

    public int TestScore { get; private set; }

    public Guid? BasiclyID { get; }

    public TestAcademicSubject(string? name, User? autor, Repository<Labwork>? labs, Repository<LectionMaterial>? lections, int testScore, Guid? basiclyID = null)
    {
        TestScore = testScore;
        ID = Guid.NewGuid();
        Name = name;
        Autor = autor;
        Labs = labs;
        Lections = lections;
        BasiclyID = basiclyID;
    }

    public override TestAcademicSubject Clone(User autor)
    {
        var subject = new TestAcademicSubject(Name, autor, Labs, Lections, TestScore, ID);
        return subject;
    }

    public override IResultType AddLabwork(Labwork labwork, User autor)
    {
        if (autor != Autor)
        {
            return new ChangeResultType(false, autor);
        }

        if (Labs == null)
        {
            Labs = new Repository<Labwork>();
        }

        Labs.Add(labwork);

        int scoreSum = TestScore;

        foreach (Labwork lab in Labs)
        {
            scoreSum += lab.Score;
        }

        if (scoreSum != 100)
        {
            throw new InvalidOperationException("sum score should be 100");
        }

        return new ChangeResultType(true, autor);
    }

    public override IResultType RemoveLabwork(Guid id, User autor)
    {
        if (autor != Autor)
        {
            return new ChangeResultType(false, autor);
        }

        if (Labs == null)
        {
            Labs = new Repository<Labwork>();
        }

        Labs.Delete(id);

        int scoreSum = TestScore;

        foreach (Labwork lab in Labs)
        {
            scoreSum += lab.Score;
        }

        if (scoreSum != 100)
        {
            throw new InvalidOperationException("sum score should be 100");
        }

        return new ChangeResultType(true, autor);
    }

    public override IResultType AddLection(LectionMaterial lection, User autor)
    {
        if (autor != Autor)
        {
            return new ChangeResultType(false, autor);
        }

        if (Lections == null)
        {
            Lections = new Repository<LectionMaterial>();
        }

        Lections.Add(lection);

        return new ChangeResultType(true, autor);
    }

    public override IResultType RemoveLection(Guid id, User autor)
    {
        if (autor != Autor)
        {
            return new ChangeResultType(false, autor);
        }

        if (Lections == null)
        {
            Lections = new Repository<LectionMaterial>();
        }

        Lections.Delete(id);

        return new ChangeResultType(true, autor);
    }
}
