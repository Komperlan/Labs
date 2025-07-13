package Application.Repositories;

import Application.Objects.Cat;
import Application.Objects.Owner;
import DAO.Repository;

import java.util.List;

public interface IOwnerRepository extends Repository<Owner> {
    public Owner save(Owner entity);

    public void deleteById(long id);
    public void deleteByEntity(Owner entity);
    public void deleteAll();

    public Owner update(Owner entity);

    public Owner getById(long id);
    public List<Owner> getAll();
}
