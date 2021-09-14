using System.Collections.Generic;
using UnityEngine;

namespace QuantumTek.QuantumInventory
{
    /// <summary>
    /// QI_CraftingRecipe represents a list of crafting ingredients for making an item.
    /// </summary>
    [CreateAssetMenu(menuName = "Quantum Tek/Quantum Inventory/Crafting Recipe", fileName = "New Crafting Recipe")]
    public class QI_CraftingRecipe : ScriptableObject
    {
        public string Name;
        public List<QI_CraftingIngredient> Ingredients = new List<QI_CraftingIngredient>();
        public QI_CraftingIngredient Product;
        public float CraftingTime;
    }
}