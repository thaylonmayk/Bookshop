using System.ComponentModel.DataAnnotations;

namespace Bookshop.Domain.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Cod { get; set; }
        private DateTime _createdDate;
        private DateTime _lastModifiedDate;

        public DateTime CreatedDate
        {
            get => _createdDate;
            set => _createdDate = value.ToUniversalTime();
        }

        public DateTime LastModifiedDate
        {
            get => _lastModifiedDate;
            set => _lastModifiedDate = value.ToUniversalTime();
        }
    }

}
