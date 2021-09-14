using UnityEngine;
using TMPro;

namespace QuantumTek.QuantumAttributes.Demo
{
    public class QA_AttributeDemo : MonoBehaviour
    {
        [SerializeField] private QA_AttributeHandler attributeHandler = null;
        [SerializeField] private TextMeshProUGUI strength = null;
        [SerializeField] private TextMeshProUGUI health = null;
        [SerializeField] private TextMeshProUGUI strengthButton = null;

        private void Awake()
        {
            // Add the attributes from the database to the handler, with starting values of 5 and 100
            attributeHandler.AddAttribute("Strength", 5);
            attributeHandler.AddAttribute("Health", 100);
        }

        private void Update()
        {
            // Change the health text
            health.text = attributeHandler.GetAttributeValue("Health").ToString();
        }

        public void AddBuff()
        {
            if (strengthButton.text == "Remove Buff")
            {
                RemoveBuff();
                return;
            }
            // Add the buff modifier
            attributeHandler.AddModifier("Strength", "Strength Buff");
            strengthButton.text = "Remove Buff";
            strength.text = attributeHandler.GetAttributeValue("Strength").ToString();
        }
        public void RemoveBuff()
        {
            if (strengthButton.text == "Add Buff")
            {
                AddBuff();
                return;
            }
            // Remove the buff modifier
            attributeHandler.RemoveModifier("Strength", "Strength Buff");
            strengthButton.text = "Add Buff";
            strength.text = attributeHandler.GetAttributeValue("Strength").ToString();
        }

        public void AddPoison()
        {
            // Add the poison modifier
            attributeHandler.AddModifier("Health", "Poison");
        }
    }
}