using Itmo.ObjectOrientedProgramming.Lab2.Users;

namespace Itmo.ObjectOrientedProgramming.Lab2.LectionMaterials;

public interface ILectionMaterialBuilder
{
    ILectionMaterialBuilder AddAutor(User autor);

    ILectionMaterialBuilder AddDescription(string description);

    ILectionMaterialBuilder AddName(string name);

    ILectionMaterialBuilder AddContent(string content);

    LectionMaterial Build();
}
