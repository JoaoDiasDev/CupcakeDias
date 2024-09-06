using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CupcakeDias.Data.Entities;

public class CupcakeIngredient
{
    [Key]
    public Guid CupcakeIngredientId { get; set; }
    public Guid CupcakeId { get; set; }
    [JsonIgnore]
    public Cupcake? Cupcake { get; set; }
    public Guid IngredientId { get; set; }
    public Ingredient? Ingredient { get; set; }
}