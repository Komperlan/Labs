package DAO.Repositories;

import Application.Objects.Cat;

import Application.Objects.Owner;
import Application.Repositories.ICatRepository;
import DAO.utils.HibernateSessionFactoryUtil;
import org.hibernate.Session;
import org.hibernate.Transaction;


import java.util.List;

public class CatRepository implements ICatRepository {

    @Override
    public Cat save(Cat entity) {
        Session session = HibernateSessionFactoryUtil.getSessionFactory().openSession();
        Transaction tx1 = session.beginTransaction();
        session.save(entity);
        tx1.commit();
        session.close();
        return entity;
    }

    @Override
    public void deleteById(long id) {
        Session session = HibernateSessionFactoryUtil.getSessionFactory().openSession();
        Transaction tx1 = session.beginTransaction();
        Cat kitty = this.getById(id);
        session.delete(kitty);
        tx1.commit();
        session.close();
    }

    @Override
    public void deleteAll() {
        var cats = this.getAll();
        for (Cat cat : cats) {
            this.deleteByEntity(cat);
        }
    }

    @Override
    public Cat getById(long id) {
        return HibernateSessionFactoryUtil.getSessionFactory().openSession().get(Cat.class, id);
    }

    @Override
    public List<Cat> getAll() {
        List<Cat> cats = (List<Cat>)  HibernateSessionFactoryUtil.getSessionFactory().openSession().createQuery("FROM pet").list();
        return cats;
    }

    @Override
    public Cat update(Cat entity) {
        Session session = HibernateSessionFactoryUtil.getSessionFactory().openSession();
        Transaction tx1 = session.beginTransaction();
        session.update(entity);
        tx1.commit();
        session.close();
        return entity;
    }

    @Override
    public void deleteByEntity(Cat entity) {
        Session session = HibernateSessionFactoryUtil.getSessionFactory().openSession();
        Transaction tx1 = session.beginTransaction();
        session.delete(entity);
        tx1.commit();
        session.close();
    }
}
