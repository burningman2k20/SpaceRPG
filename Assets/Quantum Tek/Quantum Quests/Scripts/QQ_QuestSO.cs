using System.Collections.Generic;
using UnityEngine;

namespace QuantumTek.QuantumQuest
{
    /// <summary>
    /// QQ_QuestSO is a quest in a scriptable object form.
    /// </summary>
    [CreateAssetMenu(menuName = "Quantum Tek/Quantum Quest/Quest")]
    public class QQ_QuestSO : ScriptableObject
    {
        public QQ_Quest Quest = new QQ_Quest(0);

        /// <summary>
        /// Returns a quest with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QQ_Quest GetQuest(int id)
        {
            return Quest;
        }

        /// <summary>
        /// Returns a task with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QQ_Task GetTask(int id)
        {
            foreach (var task in Quest.Tasks)
                if (task.ID == id)
                    return task;
            return null;
        }

        /// <summary>
        /// Returns the index of a quest in the list.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetQuestIndex(int id)
        {
            return 0;
        }

        /// <summary>
        /// Returns the index of a task in the list.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetTaskIndex(int id)
        {
            int questCount = Quest.Tasks.Count;
            for (int i = 0; i < questCount; ++i)
                if (Quest.Tasks[i].ID == id)
                    return i;
            return -1;
        }

        /// <summary>
        /// Sets a task with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public void SetQuest(int id, QQ_Quest quest)
        {
            Quest = quest;
        }

        /// <summary>
        /// Sets a task with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public void SetTask(int id, QQ_Task task)
        {
            Quest.Tasks[GetTaskIndex(id)] = task;
        }
    }
}