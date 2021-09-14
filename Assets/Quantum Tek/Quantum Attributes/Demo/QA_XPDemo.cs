using UnityEngine;
using TMPro;

namespace QuantumTek.QuantumAttributes.Demo
{
    public class QA_XPDemo : MonoBehaviour
    {
        [SerializeField] private QA_ExperienceHandler xpHandler = null;
        [SerializeField] private TextMeshProUGUI level = null;
        [SerializeField] private TextMeshProUGUI xp = null;

        public void IncreaseXP()
        {
            // Checks if the handler leveled up, and then changes the text
            if (xpHandler.AddXP(Random.Range(10, 25)))
                level.text = "Level " + xpHandler.Level + " Knight";
            // Shows the XP
            xp.text = xpHandler.XP + " XP";
        }
    }
}