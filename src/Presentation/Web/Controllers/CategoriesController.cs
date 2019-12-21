﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreSpa.Application.Categories.Commands.DeleteCategory;
using AspNetCoreSpa.Application.Categories.Commands.UpsertCategory;
using AspNetCoreSpa.Application.Categories.Queries.GetCategoriesList;
using System.Threading.Tasks;

namespace AspNetCoreSpa.Web.Controllers
{
    [Authorize]
    public class CategoriesController : BaseController
    {
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<CategoriesListVm>> GetAll()
        {
            return Ok(await Mediator.Send(new GetCategoriesListQuery()));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Upsert(UpsertCategoryCommand command)
        {
            var id = await Mediator.Send(command);

            return Ok(id);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCategoryCommand { Id = id });

            return NoContent();
        }
    }
}
