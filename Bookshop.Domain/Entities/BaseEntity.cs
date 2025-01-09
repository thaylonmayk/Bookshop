using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Cod { get; set; }
        private DateTimeOffset _createdDate;
        private DateTimeOffset _lastModifiedDate;

        public DateTimeOffset CreatedDate
        {
            get => _createdDate;
            set => _createdDate = value.ToUniversalTime();
        }

        public DateTimeOffset LastModifiedDate
        {
            get => _lastModifiedDate;
            set => _lastModifiedDate = value.ToUniversalTime();
        }
    }

}
