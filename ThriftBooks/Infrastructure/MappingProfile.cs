using AutoMapper;
using ThriftBooks.Business.Models;
using ThriftBooks.Business.Models.Books;
using ThriftBooks.Business.Models.Users;
using ThriftBooks.Data.Entities;

namespace ThriftBooks.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserModel>().ReverseMap();
            CreateMap<User, CreateUserModel>().ReverseMap();
            CreateMap<Book, BookModel>().ReverseMap();
            CreateMap<Book, CreateBookModel>().ReverseMap();
        }

    }
}
