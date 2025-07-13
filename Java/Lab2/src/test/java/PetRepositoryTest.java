import Application.Objects.Cat;
import DAO.Repositories.CatRepository;
import DAO.utils.HibernateSessionFactoryUtil;
import jakarta.persistence.EntityManager;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.MockitoAnnotations;

import java.util.Arrays;
import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.mockito.Mockito.*;

public class PetRepositoryTest {
    @Mock
    private SessionFactory sessionFactory;

    @Mock
    private Session session;

    @Mock
    private Transaction transaction;

    @InjectMocks
    private CatRepository catRepository;

    @BeforeEach
    void setup() {
        MockitoAnnotations.openMocks(this);

        HibernateSessionFactoryUtil.setSessionFactory(sessionFactory);

        when(sessionFactory.openSession()).thenReturn(session);
        when(session.beginTransaction()).thenReturn(transaction);
    }

    @Test
    void testSave() {
        Cat cat = new Cat();
        cat.setName("Barsik");

        Cat result = catRepository.save(cat);

        verify(session).save(cat);
        verify(transaction).commit();
        verify(session).close();

        assertEquals("Barsik", result.getName());
    }

    @Test
    void testGetById() {
        Cat cat = new Cat();
        cat.setID(1L);
        when(sessionFactory.openSession()).thenReturn(session);
        when(session.get(Cat.class, 1L)).thenReturn(cat);

        Cat result = catRepository.getById(1L);

        assertEquals(1L, result.getID());
    }

    @Test
    void testUpdate() {
        Cat cat = new Cat();
        cat.setName("Old");

        Cat result = catRepository.update(cat);

        verify(session).update(cat);
        verify(transaction).commit();
        verify(session).close();

        assertEquals("Old", result.getName());
    }

    @Test
    void testDeleteByEntity() {
        Cat cat = new Cat();
        cat.setID(1);

        catRepository.deleteByEntity(cat);

        verify(session).delete(cat);
        verify(transaction).commit();
        verify(session).close();
    }

    @Test
    void testDeleteById() {
        Cat cat = new Cat();
        cat.setID(1L);
        when(session.get(Cat.class, 1L)).thenReturn(cat);

        catRepository.deleteById(1L);

        verify(session).delete(cat);
        verify(transaction).commit();
    }

    @Test
    void testGetAll() {
        List<Cat> mockCats = Arrays.asList(new Cat(), new Cat());
        when(session.createQuery("FROM pet")).thenReturn(mock(org.hibernate.query.Query.class));
        when(session.createQuery("FROM pet").list()).thenReturn(mockCats);

        List<Cat> cats = catRepository.getAll();

        assertEquals(2, cats.size());
    }
}