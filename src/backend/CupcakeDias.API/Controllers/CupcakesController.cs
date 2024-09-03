using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CupcakeDias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CupcakesController(ICupcakeService cupcakeService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateCupcake([FromBody] Cupcake cupcake)
    {
        if (cupcake == null)
        {
            return BadRequest("Cupcake is null.");
        }

        var createdCupcake = await cupcakeService.CreateCupcakeAsync(cupcake);
        return CreatedAtAction(nameof(GetCupcakeById), new { id = createdCupcake.CupcakeId }, createdCupcake);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCupcakeById(int id)
    {
        var cupcake = await cupcakeService.GetCupcakeByIdAsync(id);
        if (cupcake == null)
        {
            return NotFound();
        }
        return Ok(cupcake);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCupcakes()
    {
        var cupcakes = await cupcakeService.GetAllCupcakesAsync();
        return Ok(cupcakes);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCupcake(int id, [FromBody] Cupcake cupcake)
    {
        if (cupcake == null || cupcake.CupcakeId != id)
        {
            return BadRequest("Cupcake is null or ID mismatch.");
        }

        var existingCupcake = await cupcakeService.GetCupcakeByIdAsync(id);
        if (existingCupcake == null)
        {
            return NotFound();
        }

        await cupcakeService.UpdateCupcakeAsync(cupcake);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCupcake(int id)
    {
        var cupcake = await cupcakeService.GetCupcakeByIdAsync(id);
        if (cupcake == null)
        {
            return NotFound();
        }

        await cupcakeService.DeleteCupcakeAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/ingredients")]
    public async Task<IActionResult> AddIngredientsToCupcake(int id, [FromBody] List<int> ingredientIds)
    {
        try
        {
            await cupcakeService.AddIngredientsToCupcakeAsync(id, ingredientIds);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound("Cupcake or one of the ingredients not found.");
        }
    }

    [HttpDelete("{id}/ingredients/{ingredientId}")]
    public async Task<IActionResult> RemoveIngredientFromCupcake(int id, int ingredientId)
    {
        await cupcakeService.RemoveIngredientFromCupcakeAsync(id, ingredientId);
        return NoContent();
    }

}
