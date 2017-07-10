using DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.NHibernate
{
    public interface IDocumentsRepository 
    {
        Documents Create();
        Documents Get(long Id);
        void Delete(Documents entity);
        void Update(Documents entity);
        ICollection<Documents> GetAll(User user);
    }
}
