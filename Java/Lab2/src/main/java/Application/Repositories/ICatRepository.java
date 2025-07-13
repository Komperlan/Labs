package Application.Repositories;

import Application.Objects.Cat;
import DAO.Repository;

import java.util.List;

public interface ICatRepository extends Repository<Cat> {
    public Cat save(Cat entity);

    public void deleteById(long id);
    public void deleteByEntity(Cat entity);
    public void deleteAll();

    public Cat update(Cat entity);

    public Cat getById(long id);
    public List<Cat> getAll();
}
