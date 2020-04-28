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

    [Route("stories")]
    [ApiController]
    public class StorieController : ControllerBase
    {

        [HttpGet]
        [Route("")]
        //[Authorize(Roles ="admin")]
        public async Task<ActionResult<List<Storie>>> Get(
            [FromServices] DataContext contexto,
            [FromServices] IStorieService storieService
             )
        {

            var stories = await storieService.GetAllStories(contexto);

            if (stories == null)
            {
                return NotFound(new { message = "Não há stories cadastrados" });
            }
            return Ok(stories);



        }



        [HttpGet]
        [Route("{id:int}")]
        //[Authorize(Roles = "usuario")]
        public async Task<ActionResult<Storie>> GetById(
            int id,
            [FromServices] DataContext context,
            [FromServices] IStorieService storieService
            )
        {

            var storie = await storieService.GetStorie(context, id);
            if (storie == null)
                return NotFound(new { message = "Storie não encontrado" });

            return Ok(storie);



        }

        [HttpPost]
        [Route("")]
        //[AllowAnonymous]
        public async Task<ActionResult<Storie>> Post(
            [FromBody] Storie model,
            [FromServices] DataContext context,
            [FromServices] IStorieService storieService,
            [FromServices] IUserService userService
            )
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var validateUser = await userService.GetById(model.UserId);
            if(validateUser == null)
            {
                return BadRequest(new { message = "O usuário informado não existe" });
            }

            var postStorie = await storieService.PostStorie(context, model);
            if (postStorie == null)
            {
                return BadRequest(new { message = "Não foi possível criar o storie" });
            }
            return Ok(model);


        }

        [HttpPut]
        [Route("{id:int}")]
        //[Authorize(Roles = "usuario")]
        public async Task<ActionResult<Storie>> Put(
           int id,
           [FromServices] DataContext context,
           [FromBody] Storie model,
           [FromServices] IStorieService storieService)
        {


            var storie = await storieService.GetStorie(context, id);
            if (storie == null)
            {
                return NotFound(new { message = "storie não encontrado!" });
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var putStorie = await storieService.PutStorie(context, model);
            if (putStorie == null)
            {
                return BadRequest(new { message = "Não foi possível alterar o storie." });
            }
            return Ok(model);

        }

        [HttpDelete]
        [Route("{id:int}")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<Storie>> Delete(
            int id,
            [FromServices] DataContext context,
            [FromServices] IStorieService storieService
            )
        {
            var storie = await storieService.GetStorie(context, id);

            if (storie == null)
            {
                return NotFound(new { message = "Storie não encontrado" });
            }

            var deleteStorie = await storieService.DeleteStorie(context, storie);

            if (deleteStorie.okMessage)
            {
                return Ok(deleteStorie);
            }
            return BadRequest(deleteStorie);

        }

    }

}
