using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QuantumTek.QuantumQuest.Editor
{
    [System.Serializable]
    public class QQ_QuestNode : QQ_Node
    {
        public new string WindowTitle => Data.Name;
        public QQ_Quest Data;

        public QQ_QuestNode(int id, QQ_NodeType type, float x = 0, float y = 0) : base(id, type, x, y)
        {
            Type = QQ_NodeType.Quest;
            Window = new Rect(0, 0, 300, 125);
            Inputs = new List<QQ_Knob>
            { };
            Outputs = new List<QQ_Knob>
            {
                new QQ_Knob(0, "First Tasks", QQ_KnobType.Output, 7.5f, true)
            };
            AllowedInputs = new List<QQ_ConnectionRule>
            { };
            AllowedOutputs = new List<QQ_ConnectionRule>
            {
                new QQ_ConnectionRule(QQ_NodeType.Task, 0, QQ_NodeType.Quest, 0)
            };
        }

        public override void DrawWindow(int id)
        {
            EditorGUI.BeginChangeCheck();

            EditorGUI.LabelField(new Rect(5, 35, 100, 20), "Name", QQ_QuestEditor.skin.label);
            string fName = EditorGUI.TextField(new Rect(110, 35, 185, 20), Data.Name, QQ_QuestEditor.skin.textField);
            EditorGUI.LabelField(new Rect(5, 65, 100, 20), "NPC Name", QQ_QuestEditor.skin.label);
            string fNPCName = EditorGUI.TextField(new Rect(110, 65, 185, 20), Data.NPCName, QQ_QuestEditor.skin.textField);
            EditorGUI.LabelField(new Rect(5, 95, 100, 20), "Description", QQ_QuestEditor.skin.label);
            string fDescription = EditorGUI.TextField(new Rect(110, 95, 185, 20), Data.Description, QQ_QuestEditor.skin.textField);

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(QQ_QuestEditor.db.DataDB);
                Data.Name = fName;
                Data.NPCName = fNPCName;
                Data.Description = fDescription;
                QQ_QuestEditor.db.DataDB.SetQuest(Data.ID, Data);
            }

            GUI.DragWindow();
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
        public override void OnConnect(QQ_QuestSO quest, QQ_NodeType connectionType, int connectionID, int connectionKnobID, int knobID, QQ_KnobType knobType)
        {
            Data.OnConnect(quest, connectionType, connectionID, connectionKnobID, knobID, knobType);
            QQ_QuestEditor.db.DataDB.SetQuest(Data.ID, Data);
        }

        /// <summary>
        /// The function called to modify the node's data whenever a node disconnects.
        /// </summary>
        /// <param name="quest">The quest data.</param>
        /// <param name="connectionType">The type of the connecting node.</param>
        /// <param name="connectionID">The id of the connected node.</param>
        public override void OnDisconnect(QQ_QuestSO quest, QQ_NodeType connectionType, int connectionID)
        {
            Data.OnDisconnect(quest, connectionType, connectionID);
            QQ_QuestEditor.db.DataDB.SetQuest(Data.ID, Data);
        }

        /// <summary>
        /// The function called to modify the node's data whenever a node disconnects.
        /// </summary>
        /// <param name="quest">The quest data.</param>
        /// <param name="connectionType">The type of the connecting node.</param>
        /// <param name="connectionID">The id of the connected node.</param>
        /// <param name="knobID">The id of the knob.</param>
        /// <param name="knobType">The type of the knob.</param>
        public override void OnDisconnect(QQ_QuestSO quest, QQ_NodeType connectionType, int connectionID, int knobID, QQ_KnobType knobType)
        {
            Data.OnDisconnect(quest, connectionType, connectionID, knobID, knobType);
            QQ_QuestEditor.db.DataDB.SetQuest(Data.ID, Data);
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
        public override void OnDisconnect(QQ_QuestSO quest, QQ_NodeType connectionType, int connectionID, int connectionKnobID, int knobID, QQ_KnobType knobType)
        {
            Data.OnDisconnect(quest, connectionType, connectionID, connectionKnobID, knobID, knobType);
            QQ_QuestEditor.db.DataDB.SetQuest(Data.ID, Data);
        }
    }
}