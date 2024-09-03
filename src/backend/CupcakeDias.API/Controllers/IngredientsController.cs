using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CupcakeDias.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController(IIngredientService ingredientService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateIngredient([FromBody] Ingredient ingredient)
    {
        if (ingredient == null)
        {
            return BadRequest("Ingredient is null.");
        }

        var createdIngredient = await ingredientService.CreateIngredientAsync(ingredient);
        return CreatedAtAction(nameof(GetIngredientById), new { id = createdIngredient.IngredientId }, createdIngredient);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetIngredientById(int id)
    {
        var ingredient = await ingredientService.GetIngredientByIdAsync(id);
        if (ingredient == null)
        {
            return NotFound();
        }
        return Ok(ingredient);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllIngredients()
    {
        var ingredients = await ingredientService.GetAllIngredientsAsync();
        return Ok(ingredients);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateIngredient(int id, [FromBody] Ingredient ingredient)
    {
        if (ingredient == null || ingredient.IngredientId != id)
        {
            return BadRequest("Ingredient is null or ID mismatch.");
        }

        var existingIngredient = await ingredientService.GetIngredientByIdAsync(id);
        if (existingIngredient == null)
        {
            return NotFound();
        }

        await ingredientService.UpdateIngredientAsync(ingredient);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteIngredient(int id)
    {
        var ingredient = await ingredientService.GetIngredientByIdAsync(id);
        if (ingredient == null)
        {
            return NotFound();
        }

        await ingredientService.DeleteIngredientAsync(id);
        return NoContent();
    }
}
