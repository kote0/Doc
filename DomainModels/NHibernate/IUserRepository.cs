using DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.NHibernate
{
    public interface IUserRepository
    {
        User Get(long Id);
        void Delete(User entity);
        void Update(User entity);
        ICollection<User> GetAll();
        User Create();
        bool IsValid(string Login, string Password);

    }
}
