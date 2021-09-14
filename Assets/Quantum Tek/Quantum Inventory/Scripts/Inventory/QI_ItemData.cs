using UnityEngine;

namespace QuantumTek.QuantumInventory
{
    /// <summary>
    /// QI_ItemData stores data about an item.
    /// </summary>
    [CreateAssetMenu(menuName = "Quantum Tek/Quantum Inventory/Item", fileName = "New Item")]
    public class QI_ItemData : ScriptableObject
    {
        public string Name;
        public string Description;
        public float Weight;
        public float Price;
        public Sprite Icon;
        public Transform ItemPrefab;
        public int MaxStack;
    }
}