using CupcakeDias.Shared.Dtos;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CupcakeDias.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CupcakesController(ICupcakeService cupcakeService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> CreateCupcake([FromBody] CupcakeDto cupcakeDto)
    {
        if (cupcakeDto == null)
        {
            return BadRequest("Cupcake is null.");
        }

        var createdCupcake = await cupcakeService.CreateCupcakeAsync(cupcakeDto.Cupcake, cupcakeDto.IngredientIds);
        return CreatedAtAction(nameof(GetCupcakeById), new { cupcakeId = createdCupcake.CupcakeId }, createdCupcake);
    }

    [HttpGet("{cupcakeId:guid}")]
    public async Task<IActionResult> GetCupcakeById(Guid cupcakeId)
    {
        var cupcake = await cupcakeService.GetCupcakeByIdAsync(cupcakeId);
        if (cupcake == null)
        {
            return NotFound();
        }
        return Ok(cupcake);
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllCupcakes()
    {
        var cupcakes = await cupcakeService.GetAllCupcakesAsync();
        return Ok(cupcakes);
    }

    [HttpPut("{cupcakeId:guid}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> UpdateCupcake(Guid cupcakeId, [FromBody] CupcakeDto cupcakeUpdateDto)
    {
        if (cupcakeUpdateDto is null || cupcakeUpdateDto.Cupcake.CupcakeId != cupcakeId)
        {
            return BadRequest("Cupcake is null or ID mismatch.");
        }

        var existingCupcake = await cupcakeService.GetCupcakeByIdAsync(cupcakeId);
        if (existingCupcake == null)
        {
            return NotFound();
        }

        // Call the service method to update the cupcake and ingredients
        await cupcakeService.UpdateCupcakeAsync(cupcakeUpdateDto.Cupcake, cupcakeUpdateDto.IngredientIds);
        return NoContent();
    }

    [HttpDelete("{cupcakeId:guid}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> DeleteCupcake(Guid cupcakeId)
    {
        var cupcake = await cupcakeService.GetCupcakeByIdAsync(cupcakeId);
        if (cupcake == null)
        {
            return NotFound();
        }

        await cupcakeService.DeleteCupcakeAsync(cupcakeId);
        return NoContent();
    }

    [HttpPost("{cupcakeId:guid}/ingredients")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> AddIngredientsToCupcake(Guid cupcakeId, [FromBody] List<Guid> ingredientIds)
    {
        try
        {
            await cupcakeService.AddIngredientsToCupcakeAsync(cupcakeId, ingredientIds);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Cupcake or one of the ingredients not found.");
        }
    }

    [HttpDelete("{cupcakeId:guid}/ingredients/{ingredientId:guid}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> RemoveIngredientFromCupcake(Guid cupcakeId, Guid ingredientId)
    {
        await cupcakeService.RemoveIngredientFromCupcakeAsync(cupcakeId, ingredientId);
        return NoContent();
    }

}
