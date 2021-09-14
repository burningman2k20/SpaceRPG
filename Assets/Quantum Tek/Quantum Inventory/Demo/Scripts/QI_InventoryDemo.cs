using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace QuantumTek.QuantumInventory.Demo
{
    public class QI_InventoryDemo : MonoBehaviour
    {
        public QI_Inventory inventory;
        public QI_ItemDatabase itemDatabase;
        private Dictionary<string, QI_ItemData> items = new Dictionary<string, QI_ItemData>();
        public TextMeshProUGUI potionCount;

        private void Awake()
        {
            // Get the health potion's data, and add it to the dictionary
            items.Add("Health Potion", itemDatabase.GetItem("Health Potion"));
        }

        public void AddPotion()
        {
            // Add a health potion
            inventory.AddItem(items["Health Potion"], 1);
            // Update text after getting the number of health potions left
            potionCount.text = inventory.GetStock("Health Potion") + " Potions";
        }

        public void RemovePotion()
        {
            // Removes a health potion
            inventory.RemoveItem("Health Potion", 1);
            // Update text after getting the number of health potions left
            potionCount.text = inventory.GetStock("Health Potion") + " Potions";
        }
    }
}