namespace QuantumTek.QuantumInventory
{
    /// <summary>
    /// QI_CraftingQueue represent how long is left on crafting an item.
    /// </summary>
    [System.Serializable]
    public class QI_CraftingQueue
    {
        public QI_ItemData Item;
        public int Amount;
        public float Timer;
    }
}