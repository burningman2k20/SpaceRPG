namespace QuantumTek.QuantumInventory
{
    /// <summary>
    /// QI_CurrencyStash represents a certain amount of currency.
    /// </summary>
    [System.Serializable]
    public struct QI_CurrencyStash
    {
        public QI_Currency Currency;
        public float Amount;
    }
}