using CupcakeDias.Data.Entities;
using CupcakeDias.Shared.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CupcakeDias.API.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class IngredientsController(IIngredientService ingredientService) : ControllerBase
{
    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> CreateIngredient([FromBody] Ingredient ingredient)
    {
        if (ingredient == null)
        {
            return BadRequest("Ingredient is null.");
        }

        var createdIngredient = await ingredientService.CreateIngredientAsync(ingredient);
        return CreatedAtAction(nameof(GetIngredientById),
            new { ingredientId = createdIngredient.IngredientId }, createdIngredient);
    }

    [HttpGet("{ingredientId:guid}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetIngredientById(Guid ingredientId)
    {
        var ingredient = await ingredientService.GetIngredientByIdAsync(ingredientId);
        if (ingredient == null)
        {
            return NotFound();
        }
        return Ok(ingredient);
    }

    [HttpGet]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> GetAllIngredients()
    {
        var ingredients = await ingredientService.GetAllIngredientsAsync();
        return Ok(ingredients);
    }

    [HttpPut("{ingredientId:guid}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> UpdateIngredient(Guid ingredientId, [FromBody] Ingredient ingredient)
    {
        if (ingredient == null || ingredient.IngredientId != ingredientId)
        {
            return BadRequest("Ingredient is null or ID mismatch.");
        }

        var existingIngredient = await ingredientService.GetIngredientByIdAsync(ingredientId);
        if (existingIngredient == null)
        {
            return NotFound();
        }

        await ingredientService.UpdateIngredientAsync(ingredient);
        return NoContent();
    }

    [HttpDelete("{ingredientId:guid}")]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> DeleteIngredient(Guid ingredientId)
    {
        var ingredient = await ingredientService.GetIngredientByIdAsync(ingredientId);
        if (ingredient == null)
        {
            return NotFound();
        }

        await ingredientService.DeleteIngredientAsync(ingredientId);
        return NoContent();
    }
}
