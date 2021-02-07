using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ThriftBooks.Business.Models.Users;
using ThriftBooks.Business.Services.Interfaces;
using ThriftBooks.Data.Entities;
using ThriftBooks.Data.Repositories.Interfaces;

namespace ThriftBooks.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public List<UserModel> GetAll(Expression<Func<UserModel, bool>> filter = null)
        {
            var repoFilter = _mapper.Map<Expression<Func<User, bool>>>(filter);

            var result = _userRepository.GetAll(repoFilter);

            return _mapper.Map<List<UserModel>>(result);
        }

        public UserModel GetById(Guid id)
        {
            var result = _userRepository.GetById(id);

            return _mapper.Map<UserModel>(result);
        }

        public async Task InsertAsync(CreateUserModel model)
        {
            var entity = _mapper.Map<User>(model);

            entity.Password = AuthService.HashPassword(entity.Password);

            await _userRepository.InsertAndSaveAsync(entity);
        }

        public async Task UpdateAsync(EditUserModel model)
        {
            var entity = _mapper.Map<User>(model);

            entity.Password = AuthService.HashPassword(entity.Password);

            await _userRepository.UpdateAndSaveAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAndSaveAsync(id);
        }

        public List<UserModel> GetAllUsersWithBooks(Expression<Func<UserModel, bool>> filter = null)
        {
            var repoFilter = _mapper.Map<Expression<Func<User, bool>>>(filter);

            var result = _userRepository.GetAllUsersWithBooks(repoFilter);

            return _mapper.Map<List<UserModel>>(result);
        }

        public UserModel GetUserWithBooks(Guid id)
        {
            var dbUser = _userRepository.GetUserWithBooks(id);

            return _mapper.Map<UserModel>(dbUser);
        }

        public async Task BuyBook(Guid userId, Guid bookId)
        {
            await _userRepository.BuyBook(userId, bookId);
        }

        public UserAuthModel GetUserByUsername(string username)
        {
            var result = _userRepository.GetUserByUsername(username);

            return _mapper.Map<UserAuthModel>(result);
        }

        public bool DoesUsernameExist(string username)
        {
            var result = _userRepository.GetUserByUsername(username);

            return result != null;
        }
    }
}
