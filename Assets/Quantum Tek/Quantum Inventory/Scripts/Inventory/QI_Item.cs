using UnityEngine;

namespace QuantumTek.QuantumInventory
{
    /// <summary>
    /// QI_Item represents a single item in the game world, such as a sword or healing potion.
    /// </summary>
    [AddComponentMenu("Quantum Tek/Quantum Inventory/Item")]
    [DisallowMultipleComponent]
    public class QI_Item : MonoBehaviour
    {
        public QI_ItemData Data;
    }
}