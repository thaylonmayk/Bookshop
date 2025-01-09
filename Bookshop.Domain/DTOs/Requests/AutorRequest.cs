using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Domain.DTOs.Requests
{
    public class AutorRequest
    {
        public int? Cod { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
    }
}
