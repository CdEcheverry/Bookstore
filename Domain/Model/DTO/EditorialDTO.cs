using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class EditorialDTO
    {
        public int idEditorial { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int MaximumRegisteredBooks { get; set; }
    }
}
