using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class BookDTO
    {
        public string Tittle { get; set; }
        public string Gender { get; set; }
        public int NumberPages { get; set; }
        public int IdEditorial { get; set; }
        public string YearPublication { get; set; }
        public int IdAuthor { get; set; }
        public string AuthorFullName { get; set; }
        public string EditorialName { get; set; }
   
    }
}
