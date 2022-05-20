using AutoMapper;
using Domain;
using Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplication
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<Author, AuthorDTO>().ForMember(x=> x.BookDTO, y=> y.MapFrom(z => z.BooksList));
            CreateMap<Editorial, EditorialDTO>().ForMember(x => x.BooksDTO, y => y.MapFrom(z => z.BooksList));
            CreateMap<Book, BookDTO>()
                .ForMember(x=> x.AuthorFullName, y=> y.MapFrom(z=>z.Author.FullName))
                .ForMember(p => p.EditorialName, w => w.MapFrom(a => a.Editorial.Name));
        }
    }
}
