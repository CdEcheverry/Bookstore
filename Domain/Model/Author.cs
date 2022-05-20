using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Author
    {
        [Key]
        public int IdAuthor { get; set; }
        public string FullName { get; set; }
        public string City { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
        public ICollection<Book> BooksList { get; set; }
    }
}
