using Itmo.ObjectOrientedProgramming.Lab2.AcademicSubjects.Builders;
using Itmo.ObjectOrientedProgramming.Lab2.Labworks;
using Itmo.ObjectOrientedProgramming.Lab2.LectionMaterials;
using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.AcademicSubjects.Subjects;

public class ExamAcademicSubject : AcademicSubjectBase
{
    public static ExamAcademicSubjectBuilder Builder => new ExamAcademicSubjectBuilder();

    public class ExamAcademicSubjectBuilder : AcademicSubjectBuilderBase
    {
        private int _examScore;

        public override AcademicSubjectBase Build()
        {
            return Build(Name, Autor, Labs, Lections, _examScore);
        }

        public ExamAcademicSubjectBuilder AddExamScore(int examScore)
        {
            _examScore = examScore;
            return this;
        }

        protected AcademicSubjectBase Build(string? name, User? autor, Repository<Labwork> labs, Repository<LectionMaterial> lections, int examScore)
        {
            return new ExamAcademicSubject(name, autor, labs, lections, examScore);
        }
    }

    public int ExamScore { get; private set; }

    public Guid? BasiclyID { get; }

    public ExamAcademicSubject(string? name, User? autor, Repository<Labwork>? labs, Repository<LectionMaterial>? lections, int examScore, Guid? id = null)
    {
        ExamScore = examScore;
        ID = Guid.NewGuid();
        BasiclyID = id;
        Name = name;
        Autor = autor;
        Labs = labs;
        Lections = lections;
    }

    public override ExamAcademicSubject Clone(User autor)
    {
        var subject = new ExamAcademicSubject(Name, autor, Labs, Lections, ExamScore, ID);
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

        int scoreSum = ExamScore;

        foreach (Labwork lab in Labs)
        {
            scoreSum += lab.Score;
        }

        if (scoreSum != 100)
        {
            throw new Exception("Sum of score should be 100");
        }

        return new ChangeResultType(true, autor);
    }

    public override ChangeResultType RemoveLabwork(Guid id, User autor)
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

        int scoreSum = ExamScore;

        foreach (Labwork lab in Labs)
        {
            scoreSum += lab.Score;
        }

        if (scoreSum != 100)
        {
            throw new Exception("sum of score should be 100");
        }

        return new ChangeResultType(true, autor);
    }

    public override ChangeResultType AddLection(LectionMaterial lection, User autor)
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

    public override ChangeResultType RemoveLection(Guid id, User autor)
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