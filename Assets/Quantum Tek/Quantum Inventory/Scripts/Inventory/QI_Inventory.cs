using System.Collections.Generic;
using UnityEngine;

namespace QuantumTek.QuantumInventory
{
    /// <summary>
    /// QI_Inventory stores a list of item stacks and represents an inventory, such as a backpack.
    /// </summary>
    [AddComponentMenu("Quantum Tek/Quantum Inventory/Inventory")]
    public class QI_Inventory : MonoBehaviour
    {
        public string Name;
        public List<QI_ItemStack> Stacks { get; set; } = new List<QI_ItemStack>();
        public Dictionary<string, int> Stock { get; set; } = new Dictionary<string, int>();
        public int MaxStacks;

        /// <summary>
        /// Returns how much of an item there is.
        /// </summary>
        /// <param name="name">The name of the item.</param>
        /// <returns></returns>
        public int GetStock(string name)
        {
            if (Stock.ContainsKey(name))
                return Stock[name];
            return 0;
        }

        /// <summary>
        /// Attempts to add an item to an existing stack, but otherwise creates a new stack.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="amount">The amount to add.</param>
        /// <returns></returns>
        public void AddItem(QI_ItemData item, int amount)
        {
            if (amount < 1)
                return;
            int stackCount = Stacks.Count;
            for (int i = 0; i < stackCount; i++)
            {
                var stack = Stacks[i];
                if (stack.Item.Name != item.Name || stack.Amount == item.MaxStack && item.MaxStack > 0)
                    continue;
                int space = item.MaxStack - stack.Amount;
                int amountAdded = item.MaxStack == 0 ? amount : Mathf.Min(space, amount);
                
                stack.Amount += amountAdded;
                amount -= amountAdded;

                if (Stock.ContainsKey(item.Name))
                    Stock[item.Name] += amountAdded;

                Stacks[i] = stack;
            }
            
            while (amount > 0 && (Stacks.Count < MaxStacks && MaxStacks > 0 || MaxStacks == 0))
            {
                int amountAdded = item.MaxStack == 0 ? amount : Mathf.Min(item.MaxStack, amount);
                Stacks.Add(new QI_ItemStack { Item = item, Amount = amountAdded });
                if (Stock.ContainsKey(item.Name))
                    Stock[item.Name] += amountAdded;
                else
                    Stock.Add(item.Name, amountAdded);
                amount -= amountAdded;
            }
        }

        /// <summary>
        /// Attempts to remove an item from an existing stack, removing the stack if empty.
        /// </summary>
        /// <param name="name">The name of the item to remove.</param>
        /// <param name="amount">The amount to remove.</param>
        /// <returns></returns>
        public void RemoveItem(string name, int amount)
        {
            if (amount < 1)
                return;
            int stackCount = Stacks.Count;
            for (int i = stackCount - 1; i >= 0; i--)
            {
                var stack = Stacks[i];
                if (stack.Item.Name != name)
                    continue;
                int amountRemoved = Mathf.Min(stack.Amount, amount);

                stack.Amount -= amountRemoved;
                amount -= amountRemoved;
                if (Stock.ContainsKey(name))
                    Stock[name] -= amountRemoved;

                Stacks[i] = stack;

                if (stack.Amount <= 0)
                    Stacks.RemoveAt(i);
            }
        }
    }
}