using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UTM.Domain.Models;

namespace UTM.Service
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookIn, Book>();
            CreateMap<Book, BookOut>();
        }
    }
}
