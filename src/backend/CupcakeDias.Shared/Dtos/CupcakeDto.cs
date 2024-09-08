using CupcakeDias.Data.Entities;

namespace CupcakeDias.Shared.Dtos;

public class CupcakeDto
{
    public required Cupcake Cupcake { get; set; }
    public List<Guid> IngredientIds { get; set; } = [];
}