using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.Labworks;

public class Labwork : BaseEntity, IPrototype<Labwork>
{
    public static LabworkBuilder Builder => new LabworkBuilder();

    public class LabworkBuilder : ILabworkBuilder
    {
        private string? _description;

        private string? _evaluationCriteria;

        private User? _autor;

        private int _score;

        private string? _name;

        private Guid? _id;

        public ILabworkBuilder AddAutor(User autor)
        {
            _autor = autor;
            return this;
        }

        public ILabworkBuilder AddBasiclyID(Guid id)
        {
            _id = id;
            return this;
        }

        public ILabworkBuilder AddDescription(string description)
        {
            _description = description;
            return this;
        }

        public ILabworkBuilder AddScore(int score)
        {
            _score = score;
            return this;
        }

        public ILabworkBuilder AddName(string name)
        {
            _name = name;
            return this;
        }

        public ILabworkBuilder AddEvaluationCriteria(string criteria)
        {
            _evaluationCriteria = criteria;
            return this;
        }

        public Labwork Build()
        {
            return new Labwork(_name, _autor, _description, _evaluationCriteria, _score, _id);
        }
    }

    public override Guid ID { get; protected set; }

    public string Description { get; private set; }

    public string EvaluationCriteria { get; private set; }

    public User Autor { get; }

    public int Score { get; }

    public string Name { get; private set; }

    public Guid? BasiclyID { get; }

    public ChangeResultType ChangeName(User user, string newName)
    {
        if (user != Autor)
        {
            return new ChangeResultType(false, user);
        }

        Name = newName;
        return new ChangeResultType(true, user);
    }

    public ChangeResultType ChangeEvaluationCriteria(User user, string evaluationCriteria)
    {
        if (user != Autor)
        {
            return new ChangeResultType(false, user);
        }

        EvaluationCriteria = evaluationCriteria;
        return new ChangeResultType(true, user);
    }

    public ChangeResultType ChangeDescription(User user, string newDescription)
    {
        if (user != Autor)
        {
            return new ChangeResultType(false, user);
        }

        Description = newDescription;
        return new ChangeResultType(true, user);
    }

    public Labwork Clone(User autor)
    {
        var builder = new LabworkBuilder();
        builder.AddAutor(autor).AddScore(Score).AddDescription(Description).AddName(Name).AddEvaluationCriteria(EvaluationCriteria).AddBasiclyID(ID);
        Labwork lab = builder.Build();
        return lab;
    }

    private Labwork(string? name, User? user, string? description, string? evaluationCriteria, int score, Guid? id = null)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException($"\"{nameof(name)}\" не может быть неопределенным или пустым.", nameof(name));
        ID = Guid.NewGuid();
        BasiclyID = id;
        Name = name;
        Autor = user ?? throw new ArgumentNullException(nameof(user));
        Score = score;
        Description = description ?? throw new ArgumentNullException(nameof(description));
        EvaluationCriteria = evaluationCriteria ?? throw new ArgumentNullException(nameof(evaluationCriteria));
    }
}
