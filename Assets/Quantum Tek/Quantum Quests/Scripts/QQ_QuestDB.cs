using System.Collections.Generic;
using UnityEngine;

namespace QuantumTek.QuantumQuest
{
    /// <summary>
    /// QQ_QuestDB is a list of quests.
    /// </summary>
    [CreateAssetMenu(menuName = "Quantum Tek/Quantum Quest/Quest Database")]
    public class QQ_QuestDB : ScriptableObject
    {
        public List<QQ_QuestSO> Quests = new List<QQ_QuestSO>();

        /// <summary>
        /// Returns the quest with the given name.
        /// </summary>
        /// <param name="name">The name of the quest.</param>
        /// <returns></returns>
        public QQ_Quest GetQuest(string name)
        {
            foreach (var quest in Quests)
                if (quest.Quest.Name == name)
                    return quest.Quest;
            return null;
        }
    }
}