using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using sms_pitang_netcore.Models;
using sms_pitang_netcore.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace sms_pitang_netcore.Controllers
{

    [Route("messages")]
    public class MessageController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Messages>>> Get(
            [FromServices] DataContext context)
        {
            try
            {
                var messages = await context.Messages.AsNoTracking().ToListAsync();
                return Ok(messages);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível buscar os mensagens" });
            }

        }



        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Messages>> GetById(
            int id,
            [FromServices] DataContext context)
        {
            try
            {
                var mensagem = await context.Messages.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (mensagem == null)
                    return NotFound(new { message = "Mensagem não encontrada" });

                return Ok(mensagem);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível retornar a mensagem" });
            }

        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Messages>> Post(
            [FromBody] Messages model,
            [FromServices] DataContext context
            )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                context.Messages.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar a mensagem" });

            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Messages>> Put(
           int id,
           [FromBody]Messages model,
           [FromServices] DataContext context)
        {


            if (id != model.Id)
            {
                return NotFound(new { message = "Mensagem não encontrada!" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                context.Entry<Messages>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);

            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Uma alteração já está sendo realizada" });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível alterar a mensagem." });
            }



        }





        /*[HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromServices] DataContext context,
            [FromBody] Messages model)
        {

            var Messages = await context.messages
                    .AsNoTracking()
                    .Where(x => x.Contactname == model.Contactname && x.Password == model.Password)
                    .FirstOrDefaultAsync();

            if (Messages == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }

            var token = TokenService.GenerateToken(Messages);
            return new
            {
                Messages = Messages,
                token = token
            };

        }*/

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Messages>> Delete(
            int id,
            [FromServices] DataContext context
            )
        {
            var mensagem = await context.Messages.FirstOrDefaultAsync(x => x.Id == id);
            if (mensagem == null)
                return NotFound(new { message = "Mensagem não encontrado" });

            try
            {
                context.Messages.Remove(mensagem);
                await context.SaveChangesAsync();

                return Ok(new { message = "Mensagem deletado com sucesso!" });
            }
            catch
            {

                return BadRequest(new { message = "Não foi possível excluir a mensagem." });
            }

        }

    }

}
