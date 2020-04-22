using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pitang.Sms.NetCore.Entities.Models;
using Pitang.Sms.NetCore.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Pitang.Sms.NetCore.Services;

namespace sms_pitang_netcore.Controllers
{

    [Route("contacts")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        //[Authorize(Roles ="admin")]
        public async Task<ActionResult<List<Contact>>> Get(
            [FromServices] DataContext contexto,
            [FromServices] IContactService contactService
             )
        {

            var contacts = await contactService.GetAllContacts(contexto);

            if (contacts == null)
            {
                return NotFound(new { message = "Não há contatos cadastrados" });
            }
            return Ok(contacts);



        }



        [HttpGet]
        [Route("{id:int}")]
        //[Authorize(Roles = "usuario")]
        public async Task<ActionResult<Contact>> GetById(
            int id,
            [FromServices] DataContext context,
            [FromServices] IContactService contactService
            )
        {
            
            var contact = await contactService.GetContact(context, id);
            if (contact == null)
                return NotFound(new { message = "Contato não encontrado" });

            return Ok(contact);

            

        }

        [HttpPost]
        [Route("")]
        //[AllowAnonymous]
        public async Task<ActionResult<Contact>> Post(
            [FromBody] Contact model,
            [FromServices] DataContext context,
            [FromServices] IContactService contactService
            )
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var postContact = await contactService.PostContact(context, model);

            if (postContact is Contact)
            {
                return Ok(model);
                
            }
            return BadRequest(postContact);


        }

        [HttpPut]
        [Route("{id:int}")]
        //[Authorize(Roles = "usuario")]
        public async Task<ActionResult<Contact>> Put(
           int id,
           [FromServices] DataContext context,
           [FromBody] Contact model,
           [FromServices] IContactService contactService)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var contact = await contactService.GetContact(context, id);
            if (contact == null)
            {
                return NotFound(new { message = "contato não encontrado!" });
            }
           
            var putContact = await contactService.PutContact(context, model);
            if (putContact is Contact)
            {
                return Ok(model);
            }

            return BadRequest(putContact);

        }

        [HttpDelete]
        [Route("{id:int}")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<Contact>> Delete(
            int id,
            [FromServices] DataContext context,
            [FromServices] IContactService contactService
            )
        {
            var contact = await contactService.GetContact(context, id);

            if (contact == null)
            {
                return NotFound(new { message = "Contato não encontrado" });
            }

            var deleteContact = await contactService.DeleteContact(context, contact);

            if (deleteContact is Contact)
            {
                return Ok(new { message = "O contato foi deletado com sucesso" });
            }
            return BadRequest(deleteContact);

        }

    }

}
