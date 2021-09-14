namespace QuantumTek.QuantumInventory
{
    /// <summary>
    /// QI_CraftingIngredient represents a certain amount of an item in a crafting recipe.
    /// </summary>
    [System.Serializable]
    public struct QI_CraftingIngredient
    {
        public QI_ItemData Item;
        public int Amount;
    }
}