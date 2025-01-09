using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Domain.Entities
{
    public class AutorEntity : BaseEntity
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
    }
}
