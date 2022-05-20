using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class EditorialDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int MaximumRegisteredBooks { get; set; }
        public ICollection<BookDTO> BooksDTO { get; set; }
    }
}
