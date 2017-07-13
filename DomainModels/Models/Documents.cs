using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModels.Models
{
    public class Documents
    {
        public virtual long Id { get; set; }
        public virtual Guid Uid { get; set; }
        public virtual string Name { get; set; }
        public virtual User Author { get; set; }
        public virtual DateTime? NameDate { get; set; }
        public virtual string Path { get; set; }
    }
}
