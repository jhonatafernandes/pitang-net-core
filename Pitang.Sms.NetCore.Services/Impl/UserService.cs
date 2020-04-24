using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pitang.Sms.NetCore.Auth.Services;
using Pitang.Sms.NetCore.Data.DataContext;
using Pitang.Sms.NetCore.DTO.User;
using Pitang.Sms.NetCore.Entities.Models;
using Pitang.Sms.NetCore.Mapper;
using Pitang.Sms.NetCore.Repositories;
using Pitang.Sms.NetCore.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Pitang.Sms.NetCore.Services
{
    public class UserService :  IUserService
    {
        protected readonly MapperConfig _mapper;
        protected readonly IUserRepository _repository;
        protected readonly ICriptographyService _crypt;
        public UserService(MapperConfig mapper, IUserRepository repository, 
            ICriptographyService crypt)
        {
            _mapper = mapper;
            _repository = repository;
            _crypt = crypt;
        }

        public async Task<List<GetUserDto>> Get()
        {
            var currentUsers = new List<GetUserDto>();
            var users = await _repository.Get();
            foreach (var user in users)
            {
                currentUsers.Add(_mapper.iMapper.Map<User, GetUserDto>(user));
            }
            return currentUsers;

        }

        public async Task<GetUserDto> GetById(
            int id
            )
        {
            var user = await _repository.GetById(id);
            var userDto = _mapper.iMapper.Map<User, GetUserDto>(user);
            return userDto;
        }

        public async Task<dynamic> Authenticate(
            LoginUserDto model)
        {

            var user = await _repository.Authenticate(model);

            if (user == null)
                return "Email não cadastrado";

            using (SHA256 sha256Hash = SHA256.Create())
            {
                if (_crypt.VerifyHash(sha256Hash, model.Password, user.Password))
                {
                    var token = TokenService.GenerateToken(user);
                    return new
                    {
                        user = _mapper.iMapper.Map<User, GetUserDto>(user),
                        token = token
                    };
                }
                else
                {
                    return new { message = "Senha Incorreta" };
                }
            }
        }

        public dynamic Post(
            User model)
        {
            try
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    model.Password = _crypt.GetHash(sha256Hash, model.Password);
                }

                var user = _repository.Post(model);
                _repository.HistoricPassword(model);

                return user;
            }
            catch (DbUpdateException)
            {
                return "Username ou email já existem!";
            }
            catch
            {
                return "não foi possível criar um usuário, tente novamente!";
            }
        }

        public async Task<dynamic> Put(
            int id,
            GetUserDto model)
        {
            try
            {
                var userFromBd = await _repository.GetById(id);
                if (userFromBd == null)
                    return "O usuário não existe!";

                var user = _mapper.iMapper.Map<GetUserDto, User>(model);
                var putUser = _repository.Put(user);
                return putUser;

            }
            catch (DbUpdateException)
            {
                return "Username ou Email já existem!";
            }
            catch
            {
                return "Não foi possível alterar o usuário.";
            }
        }


        public async Task<dynamic> Delete(
            int id)
        {
            try
            {
                var userFromBd = await _repository.GetById(id);
                _repository.Delete(userFromBd);
                return userFromBd.Username;
            }
            catch
            {
                return null;
            }
        }


        
    }
}
