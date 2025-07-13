package Application.Contracts;

import Application.Objects.Cat;

public interface ICatService {
    public Cat GetCat(long id);
    public Cat AddCat(Cat cat);
}
