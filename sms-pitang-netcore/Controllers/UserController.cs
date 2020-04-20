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
using Pitang.Sms.NetCore.Entities.auxiliares;
using System.Security.Cryptography;

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
            
            var contacts = await userService.GetAllUsers(contexto);

            if(contacts == null)
            {
                return NotFound(new { message = "Não há contatos cadastrados" });
            }
            return Ok(contacts);
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
            [FromServices] IUserService userService,
            [FromServices] IHistoricPasswordService passwordService,
            [FromServices] ICriptographyService crypt
            )
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            using (SHA256 sha256Hash = SHA256.Create())
            {
                model.Password = crypt.GetHash(sha256Hash, model.Password);
            }
            var postUser = await userService.PostUser(context, model);
            await passwordService.PostHistoricPassword(context,
                new HistoricPassword { User = postUser, Password = postUser.Password });
            if (postUser is User)
            {
                model.Password = "";
                return Ok(model);
            }
            return BadRequest(postUser);
        }

        [HttpPut]
        [Route("{id:int}")]
        //[Authorize(Roles = "usuario")]
        public async Task<ActionResult<User>> Put(
           int id,
           [FromServices] DataContext context,
           [FromBody] User model,
           [FromServices] IUserService userService,
           [FromServices] ICriptographyService crypt,
           [FromServices] IHistoricPasswordService passwordService)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userService.GetUser(context, id);
            if (user == null)
            {
                return NotFound(new { message = "Usuário não encontrado!" });
            }
            using (SHA256 sha256Hash = SHA256.Create())
            {
                model.Password = crypt.GetHash(sha256Hash, model.Password);
            }
            model.Id = id;

            var putUser = await userService.PutUser(context, model);
            
            await passwordService.PostHistoricPassword(context,
                new HistoricPassword { User = putUser, Password = putUser.Password });
            if (putUser is User)
            {
                putUser.Password = "";
                return Ok(putUser);
            }
            return BadRequest(putUser);

        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromServices] DataContext context,
            [FromBody] UserLogin model,
            [FromServices] IUserService userService,
            [FromServices] ICriptographyService crypt)
        {

            var user = await userService.PostLoginUser(context, model);

            if (user == null)
            {
                return NotFound(new { message = "Usuário não existe" });
            }

            using (SHA256 sha256Hash = SHA256.Create())
            {
                if (crypt.VerifyHash(sha256Hash, model.Password, user.Password))
                {
                    var token = TokenService.GenerateToken(user);
                    user.Password = "";
                    return new
                    {
                        user = user,
                        token = token
                    };
                }
                else
                {
                    return NotFound(new { message = "Senha Incorreta" });
                }
            }
            


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

            if (deleteUser is User)
            {
                return Ok("Usuário "+deleteUser.Username + " deletado com sucesso");
            }
            return BadRequest(deleteUser);

        }

    }
    
}
