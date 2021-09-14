using System.Collections.Generic;
using UnityEngine;

namespace QuantumTek.QuantumInventory
{
    /// <summary>
    /// QI_CurrencyDatabase stores a list of currencies.
    /// </summary>
    [CreateAssetMenu(menuName = "Quantum Tek/Quantum Inventory/Currency Database")]
    public class QI_CurrencyDatabase : ScriptableObject
    {
        public List<QI_Currency> Currencies = new List<QI_Currency>();

        /// <summary>
        /// Returns a currency by the given name.
        /// </summary>
        /// <param name="name">The name of the currency.</param>
        /// <returns></returns>
        public QI_Currency GetCurrency(string name)
        {
            foreach (var currency in Currencies)
                if (currency.Name == name)
                    return currency;
            return null;
        }
    }
}