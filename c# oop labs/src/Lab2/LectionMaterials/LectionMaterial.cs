using Itmo.ObjectOrientedProgramming.Lab2.ResultTypes;
using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.LectionMaterials;

public class LectionMaterial : BaseEntity, IPrototype<LectionMaterial>
{
    public static LectionMaterialBuilder Builder => new LectionMaterialBuilder();

    public class LectionMaterialBuilder : ILectionMaterialBuilder
    {
        private string? _description;

        private User? _autor;

        private string? _name;

        private string? _content;

        private Guid? _basiclyID;

        public ILectionMaterialBuilder AddAutor(User autor)
        {
            _autor = autor;
            return this;
        }

        public ILectionMaterialBuilder AddDescription(string description)
        {
            _description = description;
            return this;
        }

        public ILectionMaterialBuilder AddContent(string content)
        {
            _content = content;
            return this;
        }

        public ILectionMaterialBuilder AddName(string name)
        {
            _name = name;
            return this;
        }

        public ILectionMaterialBuilder AddBasiclyID(Guid id)
        {
            _basiclyID = id;
            return this;
        }

        public LectionMaterial Build()
        {
            return new LectionMaterial(_name, _description, _content, _autor, _basiclyID);
        }
    }

    public override Guid ID { get; protected set; }

    public string Name { get; private set; }

    public string Description { get; private set; }

    public string Content { get; private set; }

    public User Autor { get; }

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

    public ChangeResultType ChangeContent(User user, string evaluationCriteria)
    {
        if (user != Autor)
        {
            return new ChangeResultType(false, user);
        }

        Content = evaluationCriteria;
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

    public void ChangeContent(string content, User user)
    {
        if (user != Autor)
            throw new ArgumentException("input user is not Autor");

        content = content ?? string.Empty;
    }

    public LectionMaterial Clone(User autor)
    {
        var lectionMaterial = new LectionMaterial(Name, Description, Content, autor, ID);
        return lectionMaterial;
    }

    private LectionMaterial(string? name, string? description, string? content, User? autor, Guid? basiclyID = null)
    {
        if (string.IsNullOrEmpty(name)) throw new ArgumentException($"\"{nameof(name)}\" не может быть неопределенным или пустым.", nameof(name));
        ID = Guid.NewGuid();
        Name = name;
        Description = description ?? throw new ArgumentNullException(nameof(description));
        Content = content ?? throw new ArgumentNullException(nameof(content));
        Autor = autor ?? throw new ArgumentNullException(nameof(autor));
        BasiclyID = basiclyID;
    }
}