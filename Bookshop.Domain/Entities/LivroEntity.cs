using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Domain.Entities
{
    public class LivroEntity : BaseEntity
    {
        public string Titulo { get; set; }
        public string Editora { get; set; }
        public string AnoPublicacao { get; set; }
        public int Edicao { get; set; }
        public string Sinopse { get; set; }
        public decimal Valor { get; set; }
        public ICollection<LivroAutorEntity> LivroAutores { get; set; }
        public ICollection<LivroAssuntoEntity> LivroAssuntos { get; set; }
    }
}
