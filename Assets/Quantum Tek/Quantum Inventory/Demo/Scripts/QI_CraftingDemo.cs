using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace QuantumTek.QuantumInventory.Demo
{
    public class QI_CraftingDemo : MonoBehaviour
    {
        public QI_Inventory inventory;
        public QI_CraftingHandler craftingHandler;
        public QI_ItemDatabase itemDatabase;
        public QI_CraftingRecipeDatabase recipeDatabase;
        private Dictionary<string, QI_ItemData> items = new Dictionary<string, QI_ItemData>();
        private Dictionary<string, QI_CraftingRecipe> recipes = new Dictionary<string, QI_CraftingRecipe>();
        public TextMeshProUGUI ironCount;
        public TextMeshProUGUI swordCount;

        private void Awake()
        {
            // Get the iron ingot's data, and add it to the dictionary
            items.Add("Iron", itemDatabase.GetItem("Iron"));
            // Get the sword's data, and add it to the dictionary
            items.Add("Sword", itemDatabase.GetItem("Sword"));
            // Get the sword's recipe, and add it to the dictionary
            recipes.Add("Sword", recipeDatabase.GetCraftingRecipe("Sword"));
            // Add iron to the inventory
            inventory.AddItem(items["Iron"], 6);
        }

        public void Craft()
        {
            // Get the recipe from the dictionary and craft one of it
            craftingHandler.Craft(recipes["Sword"], 1);
            // Update the text
            ironCount.text = inventory.GetStock("Iron") + " Iron";
        }

        // This is called when an item is finished crafting
        public void OnCrafted()
        {
            // Update the text
            swordCount.text = inventory.GetStock("Sword") + " Swords";
        }
    }
}