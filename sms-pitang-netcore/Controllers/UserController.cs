using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pitang.Sms.NetCore.Entities.Models;
using Pitang.Sms.NetCore.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Pitang.Sms.NetCore.Auth.Services;
using Pitang.Sms.NetCore.Services;
using System.Security.Cryptography;
using AutoMapper;
using Pitang.Sms.NetCore.DTO.User;
using Pitang.Sms.NetCore.Mapper;

namespace sms_pitang_netcore.Controllers
{
   
    [Route("users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        //[Authorize(Roles ="admin")]
        public async Task<ActionResult<List<User>>> Get(
            [FromServices] IUserService userService
             )
        {
            var users = await userService.Get();

            if(users == null)
            {
                return NotFound(new { message = "Não há usuários cadastrados" });
            }
            return Ok(users);
        }



        [HttpGet]
        [Route("{id:int}")]
        //[Authorize(Roles = "usuario")]
        public async Task<ActionResult<User>> GetById(
            int id,
            [FromServices] IUserService userService
            )
        {
            try
            {
                var user = await userService.GetById(id);
                if (user == null)
                    return NotFound(new { message = "Usuário não encontrado" });
                return Ok(user);
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível retornar o usuário" });
            }
        }

        [HttpPost]
        [Route("")]
        //[AllowAnonymous]
        public ActionResult<dynamic> Post(
            [FromBody] User model,
            [FromServices] IUserService userService
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = userService.Post(model);

            if(user is GetUserDto)
                return Ok(user);
            return BadRequest(user);
        }

        [HttpPut]
        [Route("{id:int}")]
        //[Authorize(Roles = "usuario")]
        public async Task<ActionResult<User>> Put(
           int id,
           [FromBody] GetUserDto model,
           [FromServices] IUserService userService)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var putUser = await userService.Put(id, model);


            if (putUser is User)
                return Ok(putUser);

            return BadRequest(putUser);

        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromBody] LoginUserDto model,
            [FromServices] IUserService userService)
        {

            var user = await userService.Authenticate(model);

            return user switch
            {
                1 => NotFound(new { message = "Email Inválido" }),
                2 => NotFound(new { message = "Senha Inválida" }),
                _ => Ok(user),
            };
        }

        [HttpDelete]
        [Route("{id:int}")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<User>> Delete(
            int id,
            [FromServices] IUserService userService
            )
        {

            var deleteUser = await userService.Delete(id);

            if (deleteUser is string)
            {
                return Ok(new { message = "Usuário " + deleteUser + " deletado com sucesso" });
            }
            return BadRequest(new { message = "Usuário não encontrado "});

        }

    }
    
}
