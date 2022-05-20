using System;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Book
    {
        [Key]
        public int IdBook { get; set; }
        public string Tittle { get; set; }
        public string Gender { get; set; }
        public int NumberPages { get; set; }
        public int? IdEditorial { get; set; }
        public string YearPublication { get; set; }
        public int IdAuthor { get; set; }
        public Editorial Editorial { get; set; }
        public Author   Author { get; set; } 
    }
}
