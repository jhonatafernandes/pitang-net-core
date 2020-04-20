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

    [Route("messages")]
    [ApiController]
    public class MessageController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        //[Authorize(Roles ="admin")]
        public async Task<ActionResult<List<Messages>>> Get(
            [FromServices] DataContext contexto,
            [FromServices] IMessageService messageService
             )
        {

            var messages = await messageService.GetAllMessages(contexto);

            if (messages == null)
            {
                return NotFound(new { message = "Não há mensagens cadastradas" });
            }
            return Ok(messages);



        }



        [HttpGet]
        [Route("{id:int}")]
        //[Authorize(Roles = "usuario")]
        public async Task<ActionResult<Messages>> GetById(
            int id,
            [FromServices] DataContext context,
            [FromServices] IMessageService messageService
            )
        {

            var message = await messageService.GetMessage(context, id);
            if (message == null)
                return NotFound(new { message = "Mensagem não encontrada" });

            return Ok(message);



        }

        [HttpPost]
        [Route("")]
        //[AllowAnonymous]
        public async Task<ActionResult<Messages>> Post(
            [FromBody] Messages model,
            [FromServices] DataContext context,
            [FromServices] IMessageService messageService
            )
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var postMessage = await messageService.PostMessage(context, model);

            if (postMessage == null)
            {
                return BadRequest(new { message = "Não foi possível enviar a mensagem" });
            }
            return Ok(model);


        }

        [HttpPut]
        [Route("{id:int}")]
        //[Authorize(Roles = "usuario")]
        public async Task<ActionResult<Messages>> Put(
           int id,
           [FromServices] DataContext context,
           [FromBody] Messages model,
           [FromServices] IMessageService messageService)
        {


            var message = await messageService.GetMessage(context, id);
            if (message == null)
            {
                return NotFound(new { message = "mensagem não encontrada!" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var putMessage = await messageService.PutMessage(context, model);
            if (putMessage != null)
            {
                return Ok(model);
            }

            return BadRequest(new { message = "Não foi possível alterar a mensagem." });

        }

        [HttpDelete]
        [Route("{id:int}")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<Messages>> Delete(
            int id,
            [FromServices] DataContext context,
            [FromServices] IMessageService messageService
            )
        {
            var message = await messageService.GetMessage(context, id);

            if (message == null)
            {
                return NotFound(new { message = "Mensagem não encontrada" });
            }

            var deleteMessage = await messageService.DeleteMessage(context, message);

            if (deleteMessage.okMessage)
            {
                return Ok(deleteMessage);
            }
            return BadRequest(deleteMessage);

        }

    }

}
