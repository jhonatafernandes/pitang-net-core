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
            [FromServices] DataContext contexto
             )
        {
            try
            {
                var users = await UserService.GetAllUsers(contexto);
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
            [FromServices] DataContext context)
        {
            try
            {
                var user = await UserService.GetUser(context, id);
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
            [FromServices] DataContext context
            )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                context.Users.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar o usuário" });

            }
        }

        [HttpPut]
        [Route("{id:int}")]
        //[Authorize(Roles = "usuario")]
        public async Task<ActionResult<User>> Put(
           int id,
           [FromBody]User model,
           [FromServices] DataContext context)
        {


            if (id != model.Id)
            {
                return NotFound(new { message = "Usuário não encontrado!" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                context.Entry<User>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);

            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Uma alteração já está sendo realizada" });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível alterar o usuário." });
            }



        }





        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromServices] DataContext context,
            [FromBody] User model)
        {

            var user = await context.Users
                    .AsNoTracking()
                    .Where(x => x.Username == model.Username && x.Password == model.Password)
                    .FirstOrDefaultAsync();

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
            [FromServices] DataContext context
            )
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
                return NotFound(new { message = "Usuário não encontrado" });

            try
            {
                context.Users.Remove(user);
                await context.SaveChangesAsync();

                return Ok(new { message = "Usuário deletado com sucesso!" });
            }
            catch
            {

                return BadRequest(new { message = "Não foi possível excluir o usuário." });
            }

        }

    }
    
}
