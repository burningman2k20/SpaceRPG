using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace QuantumTek.QuantumInventory
{
    /// <summary>
    /// QI_CraftingType determines how crafting will take place, either one queue after another or all at the same time.
    /// </summary>
    public enum QI_CraftingType
    {
        Consecutive,
        Simultaneous
    }

    /// <summary>
    /// QI_CraftingHandler manages crafting queues of items waiting to be crafted.
    /// </summary>
    [AddComponentMenu("Quantum Tek/Quantum Inventory/Crafting Handler")]
    [DisallowMultipleComponent]
    public class QI_CraftingHandler : MonoBehaviour
    {
        public QI_Inventory Inventory;
        public QI_CraftingType Type;
        public List<QI_CraftingQueue> Queues { get; set; } = new List<QI_CraftingQueue>();
        public UnityEvent OnCrafted;

        private void Update()
        {
            int queueCount = Queues.Count;

            if (Type == QI_CraftingType.Consecutive && queueCount > 0)
                Work(0);
            else if (Type == QI_CraftingType.Simultaneous && queueCount > 0)
                for (int i = queueCount - 1; i >= 0; i--)
                    Work(i);
        }

        private void Work(int index)
        {
            var queue = Queues[index];
            queue.Timer = Mathf.Clamp(queue.Timer - Time.deltaTime, 0, queue.Timer + Time.deltaTime);
            Queues[index] = queue;
            
            if (Mathf.Approximately(queue.Timer, 0))
            {
                Inventory.AddItem(queue.Item, queue.Amount);
                Queues.RemoveAt(index);
                OnCrafted.Invoke();
            }
        }

        /// <summary>
        /// Adds a queue to the list of items to craft, from the given recipe. Returns if it was able to craft, based on available materials. Also removes those materials.
        /// </summary>
        /// <param name="recipe">The recipe to craft.</param>
        /// <param name="amount">How many times to craft this recipe.</param>
        public bool Craft(QI_CraftingRecipe recipe, int amount)
        {
            foreach (var ingredient in recipe.Ingredients)
                if (Inventory.GetStock(ingredient.Item.Name) < ingredient.Amount * amount)
                    return false;

            foreach (var ingredient in recipe.Ingredients)
                Inventory.RemoveItem(ingredient.Item.Name, ingredient.Amount * amount);

            Queues.Add(new QI_CraftingQueue { Item = recipe.Product.Item, Amount = recipe.Product.Amount * amount, Timer = recipe.CraftingTime });
            return true;
        }
    }
}