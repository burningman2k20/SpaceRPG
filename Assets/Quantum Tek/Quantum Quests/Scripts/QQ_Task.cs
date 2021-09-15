using System.Collections.Generic;
using UnityEngine;

namespace QuantumTek.QuantumQuest
{
    /// <summary>
    /// QQ_Task represents a part of a quest, such as collect 3 apples.
    /// </summary>
    [System.Serializable]
    public class QQ_Task
    {
        public int ID;
        public string Name;
        public string Description;
        public List<int> NextTasks = new List<int>();
        public float Progress;
        public float MaxProgress = 1;
        public bool Optional;
        public bool Completed;

        public QQ_Task(int id)
        { ID = id; }

        public QQ_Task(QQ_Task template)
        {
            ID = template.ID;
            Name = template.Name;
            Description = template.Description;
            NextTasks = new List<int>(template.NextTasks);
            MaxProgress = template.MaxProgress;
            Optional = template.Optional;
        }

        /// <summary>
        /// Marks the task as Complete.
        /// </summary>
        public void Complete()
        { Completed = true; }

        /// <summary>
        /// Increases the progress of the task, and completes it if there is enough progress.
        /// </summary>
        /// <param name="amount">The amount to increase the progress by.</param>
        public void IncreaseProgress(float amount)
        {
            Progress = Mathf.Clamp(Progress + amount, 0, MaxProgress);
            if (Mathf.Approximately(Progress, MaxProgress))
                Complete();
        }

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
                if (!NextTasks.Contains(connectionID))
                    NextTasks.Add(connectionID);
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
            if (NextTasks.Contains(connectionID))
                NextTasks.Remove(connectionID);
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
                if (NextTasks.Contains(connectionID))
                    NextTasks.Remove(connectionID);
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