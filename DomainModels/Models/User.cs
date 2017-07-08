using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Models
{
    public class User
    {
        public virtual long Id { get; set; }
        public virtual Guid Uid { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Login { get; set; }
        public virtual string Password { get; set; }


    }
}
