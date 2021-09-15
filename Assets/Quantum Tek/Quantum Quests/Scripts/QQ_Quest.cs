using System.Collections.Generic;
using UnityEngine;

namespace QuantumTek.QuantumQuest
{
    /// <summary>
    /// QQ_QuestStatus tells if a quest hasn't been given, is inactive, is active, is completed, or is failed.
    /// </summary>
    [System.Serializable]
    public enum QQ_QuestStatus
    {
        NotGiven,
        Inactive,
        Active,
        Completed,
        Failed
    }

    /// <summary>
    /// QQ_Quest represents a quest given by an NPC in the world.
    /// </summary>
    [System.Serializable]
    public class QQ_Quest
    {
        public int ID;
        public string Name;
        public string NPCName;
        public string Description;
        public QQ_QuestStatus Status = QQ_QuestStatus.NotGiven;
        public int RequiredCompleted;
        public int MaxRequired
        {
            get
            {
                int maxRequired = 0;

                foreach (var task in Tasks)
                    if (!task.Optional)
                        maxRequired++;

                return maxRequired;
            }
        }
        public bool Completed;
        public List<QQ_Task> Tasks = new List<QQ_Task>();
        public List<int> FirstTasks = new List<int>();

        public QQ_Quest(int id)
        { ID = id; }

        public QQ_Quest(QQ_Quest template)
        {
            ID = template.ID;
            Name = template.Name;
            NPCName = template.NPCName;
            Description = template.Description;
            Name = template.Name;
            Tasks = new List<QQ_Task>();

            foreach (var task in template.Tasks)
                Tasks.Add(new QQ_Task(task));

            FirstTasks = new List<int>(template.FirstTasks);
        }

        /// <summary>
        /// Returns the task with the given name.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <returns></returns>
        public QQ_Task GetTask(string name)
        {
            foreach (var task in Tasks)
                if (task.Name == name)
                    return task;
            return null;
        }

        /// <summary>
        /// Returns the task with the given id.
        /// </summary>
        /// <param name="id">The id of the task.</param>
        /// <returns></returns>
        public QQ_Task GetTask(int id)
        {
            foreach (var task in Tasks)
                if (task.ID == id)
                    return task;
            return null;
        }

        /// <summary>
        /// Increases the progress of the task with the given name.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        /// <param name="amount">The amount to progress by.</param>
        public void ProgressTask(string name, float amount)
        {
            foreach (var task in Tasks)
            {
                if (task.Name == name)
                {
                    task.IncreaseProgress(amount);
                    if (task.Completed)
                        CompleteTask(name);
                }
            }
        }

        /// <summary>
        /// Completes the task with the given id.
        /// </summary>
        /// <param name="name">The name of the task.</param>
        public void CompleteTask(string name)
        {
            if (Status == QQ_QuestStatus.NotGiven || Status == QQ_QuestStatus.Failed || Status == QQ_QuestStatus.Completed)
                return;

            foreach (var task in Tasks)
            {
                if (task.Name == name)
                {
                    task.Complete();
                    if (!task.Optional)
                    {
                        RequiredCompleted++;
                        if (RequiredCompleted == MaxRequired)
                            CompleteQuest();
                    }
                }
            }
        }

        /// <summary>
        /// Sets the state to completed, and marks the quest as complete.
        /// </summary>
        public void CompleteQuest()
        {
            Completed = true;
            Status = QQ_QuestStatus.Completed;
        }

        /// <summary>
        /// Sets the state to active.
        /// </summary>
        public void ActivateQuest()
        { Status = QQ_QuestStatus.Active; }
        
        /// <summary>
        /// Sets the state to inactive.
        /// </summary>
        public void DectivateQuest()
        { Status = QQ_QuestStatus.Inactive; }

        /// <summary>
        /// Sets the state to failed.
        /// </summary>
        public void FailQuest()
        { Status = QQ_QuestStatus.Failed; }

        /// <summary>
        /// The function called to modify the node's data whenever a node connects.
        /// </summary>
        /// <param name="quest">The quest data.</param>
        /// <param name="connectionType">The type of the connecting node.</param>
        /// <param name="connectionID">The id of the connecting node.</param>
        /// <param name="connectionKnobID">The id of the connecting knob.</param>
        /// <param name="knobID">The id of this node's knob.</param>
        /// <param name="knobType">The type of this node's knob.</param>
        public void OnConnect(QQ_QuestSO quest, QQ_NodeType connectionType, int connectionID, int connectionKnobID, int knobID, QQ_KnobType knobType)
        {
            if (knobType == QQ_KnobType.Input)
            {
                
            }
            else if (knobType == QQ_KnobType.Output)
            {
                FirstTasks.Add(connectionID);
            }
        }

        /// <summary>
        /// The function called to modify the node's data whenever a node disconnects.
        /// </summary>
        /// <param name="quest">The quest data.</param>
        /// <param name="connectionType">The type of the connecting node.</param>
        /// <param name="connectionID">The id of the connected node.</param>
        public void OnDisconnect(QQ_QuestSO quest, QQ_NodeType connectionType, int connectionID)
        {
            FirstTasks.Remove(connectionID);
        }

        /// <summary>
        /// The function called to modify the node's data whenever a node disconnects.
        /// </summary>
        /// <param name="quest">The quest data.</param>
        /// <param name="connectionType">The type of the connecting node.</param>
        /// <param name="connectionID">The id of the connected node.</param>
        /// <param name="knobID">The id of the knob.</param>
        /// <param name="knobType">The type of the knob.</param>
        public void OnDisconnect(QQ_QuestSO quest, QQ_NodeType connectionType, int connectionID, int knobID, QQ_KnobType knobType)
        {
            if (knobType == QQ_KnobType.Input)
            {
                
            }
            else if (knobType == QQ_KnobType.Output)
            {
                FirstTasks.Remove(connectionID);
            }
        }

        /// <summary>
        /// The function called to modify the node's data whenever a node disconnects.
        /// </summary>
        /// <param name="quest">The quest data.</param>
        /// <param name="connectionType">The type of the connecting node.</param>
        /// <param name="connectionID">The id of the connected node.</param>
        /// <param name="connectionKnobID">The id of the connected knob.</param>
        /// <param name="knobID">The id of the knob.</param>
        /// <param name="knobType">The type of the knob.</param>
        public void OnDisconnect(QQ_QuestSO quest, QQ_NodeType connectionType, int connectionID, int connectionKnobID, int knobID, QQ_KnobType knobType)
        {
            OnDisconnect(quest, connectionType, connectionID, knobID, knobType);
        }
    }
}