using DomainModels.NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModels.Models;
using NHibernate;

namespace DomainModels.Repository
{
    public class UserRepository :  IUserRepository
    {
        public User Create()
        {
            return new User()
            {
                Uid = Guid.NewGuid()
            };
        }
        

        public bool IsValid(string Login, string Password)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                var result = session.QueryOver<User>().And(u => u.Login == Login &&
                u.Password == Password);

                return result.SingleOrDefault() != null ? true : false;
            }
        }
        public void Delete(User entity)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Delete(entity);
                    transaction.Commit();
                }
            }
        }

        public User Get(long Id)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.Get<User>(Id);
            }
        }

        public ICollection<User> GetAll()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                return session.QueryOver<User>()
                    .List();
            }
        }

        public void Update(User entity)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.SaveOrUpdate(entity);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }

    }
}
