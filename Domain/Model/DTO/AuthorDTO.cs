using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class AuthorDTO
    {
        public string FullName { get; set; }
        public string City { get; set; }
        public ICollection<BookDTO> BookDTO {get; set;}
    }
}
