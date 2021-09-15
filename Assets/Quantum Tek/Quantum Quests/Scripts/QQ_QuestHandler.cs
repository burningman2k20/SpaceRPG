using System.Collections.Generic;
using UnityEngine;

namespace QuantumTek.QuantumQuest
{
    /// <summary>
    /// QQ_QuestHandler keeps track of a character's current quests.
    /// </summary>
    [AddComponentMenu("Quantum Tek/Quantum Quests/Quest Handler")]
    [DisallowMultipleComponent]
    public class QQ_QuestHandler : MonoBehaviour
    {
        [Header("Object References")]
        public QQ_QuestDB questDB;
        public Dictionary<string, QQ_Quest> Quests = new Dictionary<string, QQ_Quest>();

        /// <summary>
        /// Assigns a quest from the database to this quest handler.
        /// </summary>
        /// <param name="name">The name of the quest.</param>
        public void AssignQuest(string name)
        {
            if (!Quests.ContainsKey(name))
            {
                QQ_Quest quest = new QQ_Quest(questDB.GetQuest(name));
                quest.Status = QQ_QuestStatus.Inactive;
                Quests.Add(name, quest);
            }
        }

        /// <summary>
        /// Returns the quest with the given name.
        /// </summary>
        /// <param name="name">The name of the quest.</param>
        public QQ_Quest GetQuest(string name)
        {
            if (Quests.ContainsKey(name))
                return Quests[name];
            return null;
        }

        /// <summary>
        /// Returns the task with the given name.
        /// </summary>
        /// <param name="questName">The name of the quest.</param>
        /// <param name="taskName">The name of the task.</param>
        /// <returns></returns>
        public QQ_Task GetTask(string questName, string taskName) => Quests[questName].GetTask(taskName);

        /// <summary>
        /// Returns the task with the given id.
        /// </summary>
        /// <param name="questName">The name of the quest.</param>
        /// <param name="id">The id of the task.</param>
        /// <returns></returns>
        public QQ_Task GetTask(string questName, int id) => Quests[questName].GetTask(id);

        /// <summary>
        /// Increases the progress of the task with the given name.
        /// </summary>
        /// <param name="questName">The name of the quest.</param>
        /// <param name="taskName">The name of the task.</param>
        /// <param name="amount">The amount to progress by.</param>
        public void ProgressTask(string questName, string taskName, float amount) => Quests[questName].ProgressTask(taskName, amount);

        /// <summary>
        /// Completes the task with the given name.
        /// </summary>
        /// <param name="questName">The name of the quest.</param>
        /// <param name="taskName">The name of the task.</param>
        public void CompleteTask(string questName, string taskName) => Quests[questName].CompleteTask(taskName);

        /// <summary>
        /// Sets the state to completed, and marks the quest as complete.
        /// </summary>
        /// <param name="questName">The name of the quest.</param>
        public void CompleteQuest(string questName) => Quests[questName].CompleteQuest();

        /// <summary>
        /// Sets the state to active.
        /// </summary>
        /// <param name="questName">The name of the quest.</param>
        public void ActivateQuest(string questName) => Quests[questName].ActivateQuest();

        /// <summary>
        /// Sets the state to inactive.
        /// </summary>
        /// <param name="questName">The name of the quest.</param>
        public void DectivateQuest(string questName) => Quests[questName].DectivateQuest();

        /// <summary>
        /// Sets the state to failed.
        /// </summary>
        /// <param name="questName">The name of the quest.</param>
        public void FailQuest(string questName) => Quests[questName].FailQuest();
    }
}