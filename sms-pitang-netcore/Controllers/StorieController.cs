using System;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Pitang.Sms.NetCore.Entities.Models;
using Pitang.Sms.NetCore.Data.DataContext;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace sms_pitang_netcore.Controllers
{

    [Route("stories")]
    [ApiController]
    public class StorieController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Storie>>> Get(
            [FromServices] DataContext context)
        {
            try
            {
                var stories = await context.Stories.AsNoTracking().ToListAsync();
                return Ok(stories);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível buscar os stories" });
            }

        }



        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Storie>> GetById(
            int id,
            [FromServices] DataContext context)
        {
            try
            {
                var storie = await context.Stories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
                if (storie == null)
                    return NotFound(new { message = "Storie não encontrado" });

                return Ok(storie);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível retornar o storie" });
            }

        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Storie>> Post(
            [FromBody] Storie model,
            [FromServices] DataContext context
            )
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                context.Stories.Add(model);
                await context.SaveChangesAsync();
                return Ok(model);

            }
            catch
            {
                return BadRequest(new { message = "Não foi possível criar o storie" });

            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Storie>> Put(
           int id,
           [FromBody]Storie model,
           [FromServices] DataContext context)
        {


            if (id != model.Id)
            {
                return NotFound(new { message = "Storie não encontrado!" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                context.Entry<Storie>(model).State = EntityState.Modified;
                await context.SaveChangesAsync();
                return Ok(model);

            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Uma alteração já está sendo realizada" });
            }
            catch
            {
                return BadRequest(new { message = "Não foi possível alterar o storie." });
            }



        }





        /*[HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> Authenticate(
            [FromServices] DataContext context,
            [FromBody] Storie model)
        {

            var Storie = await context.stories
                    .AsNoTracking()
                    .Where(x => x.Contactname == model.Contactname && x.Password == model.Password)
                    .FirstOrDefaultAsync();

            if (Storie == null)
            {
                return NotFound(new { message = "Usuário ou senha inválidos" });
            }

            var token = TokenService.GenerateToken(Storie);
            return new
            {
                Storie = Storie,
                token = token
            };

        }*/

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<Storie>> Delete(
            int id,
            [FromServices] DataContext context
            )
        {
            var storie = await context.Stories.FirstOrDefaultAsync(x => x.Id == id);
            if (storie == null)
                return NotFound(new { message = "Storie não encontrado" });

            try
            {
                context.Stories.Remove(storie);
                await context.SaveChangesAsync();

                return Ok(new { message = "Storie deletado com sucesso!" });
            }
            catch
            {

                return BadRequest(new { message = "Não foi possível excluir o storie." });
            }

        }

    }

}
