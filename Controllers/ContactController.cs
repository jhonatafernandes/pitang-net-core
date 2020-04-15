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

    [Route("contacts")]
    public class ContactController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Contact>>> Get(
            [FromServices] DataContext context)
        {
            try
            {
                var contacts = await context.Contacts.AsNoTracking().ToListAsync();
                return Ok(contacts);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível buscar os contatos" });
            }

        }



        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Contact>> GetById(
            int id,
            [FromServices] DataContext context)
        {
            try
            {
                var contact = await context.Contacts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (contact == null)
                    return NotFound(new { message = "Contato não encontrado" });

                return Ok(contact);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível retornar o contato" });
            }

        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Contact>> Post(
            [FromBody] Contact model,
            [FromServices] DataContext context
            )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                context.Contacts.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar o contato" });

            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Contact>> Put(
           int id,
           [FromBody]Contact model,
           [FromServices] DataContext context)
        {


            if (id != model.Id)
            {
                return NotFound(new { message = "Contato não encontrado!" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                context.Entry<Contact>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);

            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Uma alteração já está sendo realizada" });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível alterar o contato." });
            }



        }





        /*[HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromServices] DataContext context,
            [FromBody] Contact model)
        {

            var Contact = await context.contacts
                    .AsNoTracking()
                    .Where(x => x.Contactname == model.Contactname && x.Password == model.Password)
                    .FirstOrDefaultAsync();

            if (Contact == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }

            var token = TokenService.GenerateToken(Contact);
            return new
            {
                Contact = Contact,
                token = token
            };

        }*/

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Contact>> Delete(
            int id,
            [FromServices] DataContext context
            )
        {
            var contact = await context.Contacts.FirstOrDefaultAsync(x => x.Id == id);
            if (contact == null)
                return NotFound(new { message = "Contato não encontrado" });

            try
            {
                context.Contacts.Remove(contact);
                await context.SaveChangesAsync();

                return Ok(new { message = "Contato deletado com sucesso!" });
            }
            catch
            {

                return BadRequest(new { message = "Não foi possível excluir o contato." });
            }

        }

    }

}
