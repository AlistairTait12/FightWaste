using System.Diagnostics.CodeAnalysis;

namespace FightWasteConsole.Models
{
    [ExcludeFromCodeCoverage]
    public class IngredientQuantityModel
    {
        public string? Name { get; set; }
        public int Quantity { get; set; }
        public Unit Unit { get; set; }
    }
}
