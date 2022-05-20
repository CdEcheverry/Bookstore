using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Editorial
    {
        [Key]
        public int IdEditorial { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? MaximumRegisteredBooks { get; set; }
        public ICollection<Book> BooksList { get; set; }
    }
}
