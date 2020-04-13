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

    [Route("password")]
    public class HistoricPasswordController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<HistoricPassword>>> Get(
            [FromServices] DataContext context)
        {
            try
            {
                var historicPasswords = await context.Passwords.AsNoTracking().ToListAsync();
                return Ok(historicPasswords);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível buscar os históricos de senhas" });
            }

        }



        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<HistoricPassword>> GetById(
            int id,
            [FromServices] DataContext context)
        {
            try
            {
                var historicPassword = await context.Passwords.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (historicPassword == null)
                    return NotFound(new { message = "Histórico de senha não encontrado" });

                return Ok(historicPassword);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível retornar o histórico de senha" });
            }

        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<HistoricPassword>> Post(
            [FromBody] HistoricPassword model,
            [FromServices] DataContext context
            )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                context.Passwords.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar o histórico de senha" });

            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<HistoricPassword>> Put(
           int id,
           [FromBody]HistoricPassword model,
           [FromServices] DataContext context)
        {


            if (id != model.Id)
            {
                return NotFound(new { message = "Histórico de senha não encontrado!" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                context.Entry<HistoricPassword>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);

            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Uma alteração já está sendo realizada" });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível alterar o histórico de senha." });
            }



        }





        /*[HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromServices] DataContext context,
            [FromBody] HistoricPassword model)
        {

            var HistoricPassword = await context.historicPasswords
                    .AsNoTracking()
                    .Where(x => x.Contactname == model.Contactname && x.Password == model.Password)
                    .FirstOrDefaultAsync();

            if (HistoricPassword == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }

            var token = TokenService.GenerateToken(HistoricPassword);
            return new
            {
                HistoricPassword = HistoricPassword,
                token = token
            };

        }*/

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<HistoricPassword>> Delete(
            int id,
            [FromServices] DataContext context
            )
        {
            var historicPassword = await context.Passwords.FirstOrDefaultAsync(x => x.Id == id);
            if (historicPassword == null)
                return NotFound(new { message = "Histórico de senha não encontrado" });

            try
            {
                context.Passwords.Remove(historicPassword);
                await context.SaveChangesAsync();

                return Ok(new { message = "Histórico de senha deletado com sucesso!" });
            }
            catch
            {

                return BadRequest(new { message = "Não foi possível excluir o histórico de senha." });
            }

        }

    }

}
