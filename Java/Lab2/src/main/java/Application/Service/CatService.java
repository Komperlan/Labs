package Application.Service;

import Application.Contracts.ICatService;
import Application.Objects.Cat;
import DAO.Repositories.CatRepository;

public class CatService implements ICatService {

    @Override
    public Cat GetCat(long id) {
        CatRepository repo = new CatRepository();
        return repo.getById(id);
    }

    @Override
    public Cat AddCat(Cat cat) {
        CatRepository repo = new CatRepository();
        return repo.save(cat);
    }
}
