package DAO.Repositories;

import Application.Objects.Cat;
import Application.Objects.Owner;
import DAO.Repository;
import DAO.utils.HibernateSessionFactoryUtil;
import org.hibernate.Session;
import org.hibernate.Transaction;

import java.util.List;

public class OwnerRepository implements Repository<Owner> {

    @Override
    public Owner save(Owner entity) {
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
        Owner entity = session.get(Owner.class, id);
        session.delete(entity);
        tx1.commit();
        session.close();
    }

    @Override
    public void deleteByEntity(Owner entity) {
        Session session = HibernateSessionFactoryUtil.getSessionFactory().openSession();
        Transaction tx1 = session.beginTransaction();
        session.delete(entity);
        tx1.commit();
        session.close();
    }

    @Override
    public void deleteAll() {
        var owners = this.getAll();
        for (Owner owner : owners) {
            this.deleteByEntity(owner);
        }
    }

    @Override
    public Owner update(Owner entity) {
        Session session = HibernateSessionFactoryUtil.getSessionFactory().openSession();
        Transaction tx1 = session.beginTransaction();
        session.update(entity);
        tx1.commit();
        session.close();
        return entity;
    }

    @Override
    public Owner getById(long id) {
        return HibernateSessionFactoryUtil.getSessionFactory().openSession().get(Owner.class, id);
    }

    @Override
    public List<Owner> getAll() {
        List<Owner> owners = (List<Owner>)  HibernateSessionFactoryUtil.getSessionFactory().openSession().createQuery("FROM Owner").list();
        return owners;
    }
}
