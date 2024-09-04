using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CupcakeDias.Data.Entities;

public class Ingredient
{
    [Key]
    public Guid IngredientId { get; set; }
    [StringLength(100)]
    public required string Name { get; set; }
    [StringLength(50)]
    public required string Type { get; set; }
    public bool Availability { get; set; }
    [JsonIgnore]
    public ICollection<CupcakeIngredient>? CupcakeIngredients { get; set; }
}