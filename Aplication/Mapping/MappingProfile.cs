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
            CreateMap<Author, AuthorDTO>();
            CreateMap<Editorial, EditorialDTO>();
            CreateMap<Book, BookDTO>()
                .ForMember(x=> x.AuthorFullName, y=> y.MapFrom(z=>z.Author.FullName))
                .ForMember(p => p.EditorialName, w => w.MapFrom(a => a.Editorial.Name));
        }
    }
}
