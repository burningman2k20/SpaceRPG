namespace QuantumTek.QuantumInventory
{
    /// <summary>
    /// QI_ItemStack represents one stack of items in an inventory.
    /// </summary>
    [System.Serializable]
    public struct QI_ItemStack
    {
        public QI_ItemData Item;
        public int Amount;
    }
}