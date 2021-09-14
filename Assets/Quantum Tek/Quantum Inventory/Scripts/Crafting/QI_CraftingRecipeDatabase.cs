using System.Collections.Generic;
using UnityEngine;

namespace QuantumTek.QuantumInventory
{
    /// <summary>
    /// QI_CraftingRecipeDatabase stores a list of crafting recipes.
    /// </summary>
    [CreateAssetMenu(menuName = "Quantum Tek/Quantum Inventory/Crafting Recipe Database")]
    public class QI_CraftingRecipeDatabase : ScriptableObject
    {
        public List<QI_CraftingRecipe> CraftingRecipes = new List<QI_CraftingRecipe>();

        /// <summary>
        /// Returns a crafting recipe by the given name.
        /// </summary>
        /// <param name="name">The name of the recipe.</param>
        /// <returns></returns>
        public QI_CraftingRecipe GetCraftingRecipe(string name)
        {
            foreach (var recipe in CraftingRecipes)
                if (recipe.Name == name)
                    return recipe;
            return null;
        }
    }
}