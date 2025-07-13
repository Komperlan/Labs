package Application.Contracts;

import Application.Objects.Cat;
import Application.Objects.Owner;
import DAO.Repositories.CatRepository;

public interface IOwnerService {
    public Owner GetOwner(long id);
    public Owner AddOwner(Owner owner);
}
