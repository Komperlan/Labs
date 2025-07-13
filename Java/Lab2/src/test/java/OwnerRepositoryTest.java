import Application.Objects.Owner;
import DAO.Repositories.OwnerRepository;
import DAO.utils.HibernateSessionFactoryUtil;
import org.hibernate.Session;
import org.hibernate.SessionFactory;
import org.hibernate.Transaction;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import java.util.Date;

import java.util.Arrays;
import java.util.List;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.mockito.Mockito.*;

public class OwnerRepositoryTest {

        @Mock
        private SessionFactory sessionFactory;

        @Mock
        private Session session;

        @Mock
        private Transaction transaction;

        @InjectMocks
        private OwnerRepository ownerRepository;

        @BeforeEach
        void setup() {
            MockitoAnnotations.openMocks(this);

            HibernateSessionFactoryUtil.setSessionFactory(sessionFactory);

            when(sessionFactory.openSession()).thenReturn(session);
            when(session.beginTransaction()).thenReturn(transaction);
        }

        @Test
        void testSave() {
            Owner owner = new Owner();
            owner.setName("Andrew");
            owner.setBirthDate(new Date(2005, 9, 8));

            Owner result = ownerRepository.save(owner);

            verify(session).save(owner);
            verify(transaction).commit();
            verify(session).close();

            assertEquals("Andrew", result.getName());
        }

        @Test
        void testGetById() {
            Owner owner = new Owner();
            owner.setId(1L);
            when(session.get(Owner.class, 1L)).thenReturn(owner);

            Owner result = ownerRepository.getById(1L);

            assertEquals(1L, result.getId());
        }

        @Test
        void testUpdate() {
            Owner owner = new Owner();
            owner.setName("Mers");

            Owner result = ownerRepository.update(owner);

            verify(session).update(owner);
            verify(transaction).commit();
            verify(session).close();

            assertEquals("Mers", result.getName());
        }

        @Test
        void testDeleteByEntity() {
            Owner owner = new Owner();
            owner.setId(2L);

            ownerRepository.deleteByEntity(owner);

            verify(session).delete(owner);
            verify(transaction).commit();
            verify(session).close();
        }

        @Test
        void testDeleteById() {
            Owner owner = new Owner();
            owner.setId(3L);
            when(session.get(Owner.class, 3L)).thenReturn(owner);

            ownerRepository.deleteById(3L);

            verify(session).delete(owner);
            verify(transaction).commit();
        }

        @Test
        void testGetAll() {
            List<Owner> mockOwners = Arrays.asList(new Owner(), new Owner());
            when(session.createQuery("FROM Owner")).thenReturn(mock(org.hibernate.query.Query.class));
            when(session.createQuery("FROM Owner").list()).thenReturn(mockOwners);

            List<Owner> owners = ownerRepository.getAll();

            assertEquals(2, owners.size());
        }
    }
