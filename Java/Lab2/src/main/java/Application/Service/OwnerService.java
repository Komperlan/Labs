package Application.Service;

import Application.Contracts.IOwnerService;
import Application.Objects.Cat;
import Application.Objects.Owner;
import DAO.Repositories.CatRepository;
import DAO.Repositories.OwnerRepository;

public class OwnerService implements IOwnerService {

    @Override
    public Owner GetOwner(long id) {
        OwnerRepository repo = new OwnerRepository();
        return repo.getById(id);
    }

    @Override
    public Owner AddOwner(Owner owner) {
        OwnerRepository repo = new OwnerRepository();
        return repo.save(owner);
    }
}
