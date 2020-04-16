using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pitang.Sms.NetCore.Entities.Models;
using Pitang.Sms.NetCore.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
//using Pitang.Sms.NetCore.Services;
using Pitang.Sms.NetCore.Auth.Services;
using Pitang.Sms.NetCore.Services;

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
            [FromServices] DataContext contexto,
            [FromServices] IUserService userService
             )
        {
            try
            {
                var users = await userService.GetAllUsers(contexto);
                return Ok(users);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível buscar os usuários" });
            }

        }



        [HttpGet]
        [Route("{id:int}")]
        //[Authorize(Roles = "usuario")]
        public async Task<ActionResult<User>> GetById(
            int id,
            [FromServices] DataContext context,
            [FromServices] IUserService userService
            )
        {
            try
            {
                var user = await userService.GetUser(context, id);
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
        public async Task<ActionResult<User>> Post(
            [FromBody] User model,
            [FromServices] DataContext context,
            [FromServices] IUserService userService
            )
        {
           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var postUser = await userService.PostUser(context, model);

            if(postUser == null)
            {
                return BadRequest(new { message = "Não foi possível criar o usuário" });
            }
            return Ok(model);

          
        }

        [HttpPut]
        [Route("{id:int}")]
        //[Authorize(Roles = "usuario")]
        public async Task<ActionResult<User>> Put(
           int id,
           [FromServices] DataContext context,
           [FromBody] User model,
           [FromServices] IUserService userService)
        {

        
            var user = await userService.GetUser(context, id);
            if (user == null)
            {
                return NotFound(new { message = "Usuário não encontrado!" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            

            var putUser = await userService.PutUser(context, model);

            if(putUser != null)
            {
                return Ok(model);
            }

            return BadRequest(new { message = "Não foi possível alterar o usuário." });

        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromServices] DataContext context,
            [FromBody] User model,
            [FromServices] IUserService userService)
        {

            var user = await userService.PostLoginUser(context, model);

            if (user == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }

            var token = TokenService.GenerateToken(user);
            return new
            {
                user = user,
                token = token
            };

        }

        [HttpDelete]
        [Route("{id:int}")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<User>> Delete(
            int id,
            [FromServices] DataContext context,
            [FromServices] IUserService userService
            )
        {
            var user = await userService.GetUser(context, id);

            if (user == null)
            {
                return NotFound(new { message = "Usuário não encontrado" });
            }

            var deleteUser = await userService.DeleteUser(context, user);

            if (deleteUser.okMessage)
            {
                return Ok(deleteUser);
            }
            return BadRequest(deleteUser);

        }

    }
    
}
