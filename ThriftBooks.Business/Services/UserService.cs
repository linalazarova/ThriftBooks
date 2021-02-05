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

            await _userRepository.InsertAndSaveAsync(entity);
        }

        public async Task UpdateAsync(UserModel model)
        {
            var entity = _mapper.Map<User>(model);

            await _userRepository.UpdateAndSaveAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _userRepository.DeleteAndSaveAsync(id);
        }


    }
}
