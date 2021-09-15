using System.Collections.Generic;
using UnityEngine;

namespace QuantumTek.QuantumQuest.Editor
{
    [CreateAssetMenu(menuName = "Quantum Tek/Quantum Quest/Node Database")]
    public class QQ_NodeDB : ScriptableObject
    {
        public QQ_QuestSO DataDB;
        public int NextID = 0;
        public List<QQ_Node> Nodes = new List<QQ_Node>();
        public List<QQ_QuestNode> QuestNodes = new List<QQ_QuestNode>();
        public List<QQ_TaskNode> TaskNodes = new List<QQ_TaskNode>();

        /// <summary>
        /// Returns a node with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QQ_Node GetNode(int id)
        {
            foreach (var node in Nodes)
                if (node.ID == id)
                    return node;
            return null;
        }
        /// <summary>
        /// Returns a node with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QQ_QuestNode GetQuestNode(int id)
        {
            foreach (var node in QuestNodes)
                if (node.ID == id)
                    return node;
            return null;
        }
        /// <summary>
        /// Returns a node with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public QQ_TaskNode GetTaskNode(int id)
        {
            foreach (var node in TaskNodes)
                if (node.ID == id)
                    return node;
            return null;
        }

        /// <summary>
        /// Returns the index of a node in the list.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetNodeIndex(int id)
        {
            int nodeCount = Nodes.Count;
            for (int i = 0; i < nodeCount; ++i)
                if (Nodes[i].ID == id)
                    return i;
            return -1;
        }
        /// <summary>
        /// Returns the index of a node in the list.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetQuestNodeIndex(int id)
        {
            int nodeCount = QuestNodes.Count;
            for (int i = 0; i < nodeCount; ++i)
                if (QuestNodes[i].ID == id)
                    return i;
            return -1;
        }
        /// <summary>
        /// Returns the index of a node in the list.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetTaskNodeIndex(int id)
        {
            int nodeCount = TaskNodes.Count;
            for (int i = 0; i < nodeCount; ++i)
                if (TaskNodes[i].ID == id)
                    return i;
            return -1;
        }

        /// <summary>
        /// Creates a new node of the given type at the given position.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="position"></param>
        public void CreateNode(QQ_NodeType type, Vector2 position)
        {
            int id = NextID++;
            var node = new QQ_Node(id, type, position.x, position.y);

            if (type == QQ_NodeType.Quest)
            {
                var questNode = new QQ_QuestNode(id, type, position.x, position.y);

                node.Window.width = questNode.Window.width;
                node.Window.height = questNode.Window.height;
                node.Inputs = questNode.CloneKnobs(QQ_KnobType.Input);
                node.Outputs = questNode.CloneKnobs(QQ_KnobType.Output);
                node.AllowedInputs = questNode.AllowedInputs;
                node.AllowedOutputs = questNode.AllowedOutputs;

                var questData = DataDB.Quest;
                DataDB.Quest = questData;
                questNode.Data = questData;

                QuestNodes.Add(questNode);
            }
            else if (type == QQ_NodeType.Task)
            {
                var taskNode = new QQ_TaskNode(id, type, position.x, position.y);

                node.Window.width = taskNode.Window.width;
                node.Window.height = taskNode.Window.height;
                node.Inputs = taskNode.CloneKnobs(QQ_KnobType.Input);
                node.Outputs = taskNode.CloneKnobs(QQ_KnobType.Output);
                node.AllowedInputs = taskNode.AllowedInputs;
                node.AllowedOutputs = taskNode.AllowedOutputs;

                var taskData = new QQ_Task(id);
                QQ_QuestNode quest = GetQuestNode(0);
                quest.Data.Tasks.Add(taskData);
                DataDB.SetQuest(0, quest.Data);
                taskNode.Data = taskData;

                TaskNodes.Add(taskNode);
            }

            Nodes.Add(node);
        }
    }
}