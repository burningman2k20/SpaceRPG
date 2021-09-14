using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace QuantumTek.QuantumInventory.Demo
{
    public class QI_VendorDemo : MonoBehaviour
    {
        public QI_Vendor player;
        public QI_Vendor shopkeeper;
        public QI_ItemDatabase itemDatabase;
        public QI_CurrencyDatabase currencyDatabase;
        private QI_ItemData sword = null;
        public TextMeshProUGUI playerSwordCount;
        public TextMeshProUGUI shopkeeperSwordCount;
        public TextMeshProUGUI playerCoinCount;
        public TextMeshProUGUI shopkeeperCoinCount;

        private void Awake()
        {
            // Get the coin from the database
            QI_Currency coin = currencyDatabase.GetCurrency("Coin");
            // Add 5 coins to each vendor. The player is technically a vendor, because they are able to buy and sell items
            player.AddCurrency(coin, 5);
            shopkeeper.AddCurrency(coin, 5);
            // Get the sword's data, and add it to the dictionary
            sword = itemDatabase.GetItem("Sword");
            // Add a sword to each vendor
            player.Inventory.AddItem(sword, 1);
            shopkeeper.Inventory.AddItem(sword, 1);
        }

        public void PlayerBuy()
        {
            // Make a transaction between the player and the shopkeeper. The player buys and the shopkeeper sells
            QI_Vendor.Transaction(player, shopkeeper, sword, "Coin", 1);
            // Update the text
            playerSwordCount.text = player.Inventory.GetStock("Sword") + " Swords";
            shopkeeperSwordCount.text = shopkeeper.Inventory.GetStock("Sword") + " Swords";
            playerCoinCount.text = player.GetCurrency("Coin") + " Coins";
            shopkeeperCoinCount.text = shopkeeper.GetCurrency("Coin") + " Coins";
        }

        public void PlayerSell()
        {
            // Make a transaction between the player and the shopkeeper. The player sells and the shopkeeper buys
            QI_Vendor.Transaction(shopkeeper, player, sword, "Coin", 1);
            // Update the text
            playerSwordCount.text = player.Inventory.GetStock("Sword") + " Swords";
            shopkeeperSwordCount.text = shopkeeper.Inventory.GetStock("Sword") + " Swords";
            playerCoinCount.text = player.GetCurrency("Coin") + " Coins";
            shopkeeperCoinCount.text = shopkeeper.GetCurrency("Coin") + " Coins";
        }
    }
}