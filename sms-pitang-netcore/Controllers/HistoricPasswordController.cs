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

    [Route("password")]
    [ApiController]
    public class HistoricPasswordController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        //[Authorize(Roles ="admin")]
        public async Task<ActionResult<List<HistoricPassword>>> Get(
            [FromServices] DataContext contexto,
            [FromServices] IHistoricPasswordService historicPasswordService
             )
        {

            var historicPasswords = await historicPasswordService.GetAllHistoricPassword(contexto);

            if (historicPasswords == null)
            {
                return NotFound(new { historicPassword = "Não há históricos de senhas cadastradas" });
            }
            return Ok(historicPasswords);



        }



        [HttpGet]
        [Route("{id:int}")]
        //[Authorize(Roles = "usuario")]
        public async Task<ActionResult<HistoricPassword>> GetById(
            int id,
            [FromServices] DataContext context,
            [FromServices] IHistoricPasswordService historicPasswordService
            )
        {

            var historicPassword = await historicPasswordService.GetHistoricPassword(context, id);
            if (historicPassword == null)
                return NotFound(new { historicPassword = "Histórico de senha não encontrado" });

            return Ok(historicPassword);



        }

        [HttpPost]
        [Route("")]
        //[AllowAnonymous]
        public async Task<ActionResult<HistoricPassword>> Post(
            [FromBody] HistoricPassword model,
            [FromServices] DataContext context,
            [FromServices] IHistoricPasswordService historicPasswordService
            )
        {

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var postHistoricPassword = await historicPasswordService.PostHistoricPassword(context, model);

            if (postHistoricPassword == null)
            {
                return BadRequest(new { historicPassword = "Não foi possível enviar o histórico de senha" });
            }
            return Ok(model);


        }

        [HttpPut]
        [Route("{id:int}")]
        //[Authorize(Roles = "usuario")]
        public async Task<ActionResult<HistoricPassword>> Put(
           int id,
           [FromServices] DataContext context,
           [FromBody] HistoricPassword model,
           [FromServices] IHistoricPasswordService historicPasswordService)
        {


            var historicPassword = await historicPasswordService.GetHistoricPassword(context, id);
            if (historicPassword == null)
            {
                return NotFound(new { historicPassword = "histórico de senha não encontrada!" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var putHistoricPassword = await historicPasswordService.PutHistoricPassword(context, model);
            if (putHistoricPassword != null)
            {
                return Ok(model);
            }

            return BadRequest(new { historicPassword = "Não foi possível alterar a histórico de senha." });

        }

        [HttpDelete]
        [Route("{id:int}")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<HistoricPassword>> Delete(
            int id,
            [FromServices] DataContext context,
            [FromServices] IHistoricPasswordService historicPasswordService
            )
        {
            var historicPassword = await historicPasswordService.GetHistoricPassword(context, id);

            if (historicPassword == null)
            {
                return NotFound(new { historicPassword = "Mensagem não encontrada" });
            }

            var deleteHistoricPassword = await historicPasswordService.DeleteHistoricPassword(context, historicPassword);

            if (deleteHistoricPassword.okHistoricPassword)
            {
                return Ok(deleteHistoricPassword);
            }
            return BadRequest(deleteHistoricPassword);

        }

    }

}
