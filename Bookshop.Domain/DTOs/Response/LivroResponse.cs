using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Domain.DTOs.Response
{
    public class LivroResponse
    {
        public int Cod { get; set; }
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public int AnoPublicacao { get; set; }
        public int Edicao { get; set; }
        public string Sinopse { get; set; }
        public decimal Valor { get; set; }
    }
}
