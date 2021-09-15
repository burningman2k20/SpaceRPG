using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace QuantumTek.QuantumQuest.Editor
{
    public class QQ_QuestEditor : EditorWindow
    {
        public static QQ_QuestEditor editor;
        public static GUISkin skin;
        public static QQ_NodeDB db;
        public Vector2 mouse;
        public bool connectingNodes;
        public QQ_Node selectedNode;
        public int selectedKnob = -1;
        public QQ_KnobType knobType;
        public Vector2 scroll;

        [MenuItem("Window/Quantum Quest/Quest Editor")]
        public static void ShowWindow()
        { editor = GetWindow<QQ_QuestEditor>("Quest Editor"); }

        private void OnGUI()
        {
            if (!editor) editor = this;
            SelectDB();
            skin = Resources.Load<GUISkin>("Node Editor Skin");
            GUI.skin = skin;
            float eWidth = editor.position.width;
            float eHeight = editor.position.height;

            if (db && db.QuestNodes.Count == 0)
            {
                EditorUtility.SetDirty(db);
                EditorUtility.SetDirty(db.DataDB);
                db.CreateNode(QQ_NodeType.Quest, new Vector2(0, 0));
            }

            if (!db) EditorGUILayout.HelpBox("Select an object with a QQ_NodeDB script.", MessageType.Info);
            else
            {
                Event e = Event.current;
                mouse = e.mousePosition;
                int nodeCount = db.Nodes.Count;

                // Mouse click events, taken from https://forum.unity.com/threads/simple-node-editor.189230/
                if (e.type == EventType.MouseDown && e.button == 1 && !connectingNodes)
                {
                    bool clicked = false;
                    int selectedIndex = -1;
                    bool knobClicked = false;
                    int knobSelectedIndex = -1;

                    for (int i = 0; i < nodeCount; ++i)
                    {
                        QQ_Node node = db.Nodes[i];
                        if (node.Window.Contains(mouse + scroll))
                        {
                            selectedIndex = i;
                            clicked = true;
                            break;
                        }
                        else
                        {
                            int inputCount = node.Inputs.Count;
                            for (int j = 0; j < inputCount; ++j)
                            {
                                if (node.InputKnob(j).Contains(mouse + scroll))
                                {
                                    selectedIndex = i;
                                    clicked = true;
                                    knobSelectedIndex = j;
                                    knobClicked = true;
                                    knobType = QQ_KnobType.Input;
                                    selectedKnob = j;
                                    break;
                                }
                            }

                            if (knobClicked) break;

                            int outputCount = node.Outputs.Count;
                            for (int j = 0; j < outputCount; ++j)
                            {
                                if (node.OutputKnob(j).Contains(mouse + scroll))
                                {
                                    selectedIndex = i;
                                    clicked = true;
                                    knobSelectedIndex = j;
                                    knobClicked = true;
                                    knobType = QQ_KnobType.Output;
                                    selectedKnob = j;
                                    break;
                                }
                            }

                            if (knobClicked) break;
                        }
                    }

                    if (!clicked)
                    {
                        GenericMenu menu = new GenericMenu();
                        menu.AddItem(new GUIContent("Add Task"), false, ContextCallback, "Add Task");
                        menu.ShowAsContext();
                        e.Use();
                    }
                    else
                    {
                        GenericMenu menu = new GenericMenu();
                        selectedNode = db.Nodes[selectedIndex];
                        if (!knobClicked)
                        {
                            menu.AddItem(new GUIContent("Detach All Connected Nodes"), false, ContextCallback, "Detach All Connected Nodes");
                            if (selectedNode.Type != QQ_NodeType.Quest)
                                menu.AddItem(new GUIContent("Delete Node"), false, ContextCallback, "Delete Node");
                        }
                        else
                        {
                            menu.AddItem(new GUIContent("Attach to Node"), false, ContextCallback, "Attach to Node");
                            menu.AddItem(new GUIContent("Detach Connected Nodes"), false, ContextCallback, "Detach Connected Nodes");
                        }
                        menu.ShowAsContext();
                        e.Use();
                    }
                }
                else if (e.type == EventType.MouseDown && e.button == 0 && connectingNodes)
                {
                    bool clicked = false;
                    int selectedIndex = -1;
                    bool knobClicked = false;
                    int knobSelectedIndex = -1;
                    QQ_KnobType clickedKnobType = QQ_KnobType.Input;

                    for (int i = 0; i < nodeCount; ++i)
                    {
                        QQ_Node node = db.Nodes[i];
                        int inputCount = node.Inputs.Count;
                        for (int j = 0; j < inputCount; ++j)
                        {
                            if (node.InputKnob(j).Contains(mouse + scroll))
                            {
                                selectedIndex = i;
                                clicked = true;
                                knobSelectedIndex = j;
                                knobClicked = true;
                                clickedKnobType = QQ_KnobType.Input;
                                break;
                            }
                        }

                        if (knobClicked) break;

                        int outputCount = node.Outputs.Count;
                        for (int j = 0; j < outputCount; ++j)
                        {
                            if (node.OutputKnob(j).Contains(mouse + scroll))
                            {
                                selectedIndex = i;
                                clicked = true;
                                knobSelectedIndex = j;
                                knobClicked = true;
                                clickedKnobType = QQ_KnobType.Output;
                                break;
                            }
                        }

                        if (knobClicked) break;
                    }

                    if (clicked)
                    {
                        EditorUtility.SetDirty(db);
                        if (knobType == QQ_KnobType.Input && clickedKnobType == QQ_KnobType.Output)
                        {
                            QQ_Node outputNode = db.Nodes[selectedIndex];
                            QQ_Knob input = selectedNode.Inputs[selectedKnob];
                            QQ_Knob output = outputNode.Outputs[knobSelectedIndex];
                            if (selectedNode.ID != outputNode.ID &&
                                (!output.Connections.ContainsKey(selectedNode.ID) || !output.Connections.Get(selectedNode.ID).Contains(selectedKnob)) &&
                                (!input.Connections.ContainsKey(outputNode.ID) || !input.Connections.Get(outputNode.ID).Contains(knobSelectedIndex)) &&
                                (output.AllowMultipleConnections || output.Connections.Count == 0) &&
                                (input.AllowMultipleConnections || input.Connections.Count == 0) &&
                                selectedNode.CanConnect(knobType, selectedKnob, outputNode.Type, QQ_KnobType.Output, knobSelectedIndex)
                                )
                            {
                                selectedNode.ConnectNode(outputNode.ID, knobSelectedIndex, selectedKnob, QQ_KnobType.Input);
                                outputNode.ConnectNode(selectedNode.ID, selectedKnob, knobSelectedIndex, QQ_KnobType.Output);
                            }
                        }
                        else if (knobType == QQ_KnobType.Output && clickedKnobType == QQ_KnobType.Input)
                        {
                            QQ_Node inputNode = db.Nodes[selectedIndex];
                            QQ_Knob input = inputNode.Inputs[knobSelectedIndex];
                            QQ_Knob output = selectedNode.Outputs[selectedKnob];
                            if (selectedNode.ID != inputNode.ID &&
                                (!output.Connections.ContainsKey(inputNode.ID) || !output.Connections.Get(inputNode.ID).Contains(knobSelectedIndex)) &&
                                (!input.Connections.ContainsKey(selectedNode.ID) || !input.Connections.Get(selectedNode.ID).Contains(selectedKnob)) &&
                                (output.AllowMultipleConnections || output.Connections.Count == 0) &&
                                (input.AllowMultipleConnections || input.Connections.Count == 0) &&
                                selectedNode.CanConnect(knobType, selectedKnob, inputNode.Type, QQ_KnobType.Input, knobSelectedIndex)
                                )
                            {
                                selectedNode.ConnectNode(inputNode.ID, knobSelectedIndex, selectedKnob, QQ_KnobType.Output);
                                inputNode.ConnectNode(selectedNode.ID, selectedKnob, knobSelectedIndex, QQ_KnobType.Input);
                            }
                        }
                    }

                    connectingNodes = false;
                    selectedNode = null;
                    knobClicked = false;
                    selectedKnob = -1;
                    e.Use();
                }
                if (connectingNodes && selectedNode != null)
                {
                    Rect mouseRect = new Rect(e.mousePosition.x, e.mousePosition.y, 10, 10);
                    Rect knobRect = knobType == QQ_KnobType.Input ? selectedNode.InputKnob(selectedKnob) : selectedNode.OutputKnob(selectedKnob);
                    knobRect.position += new Vector2(knobRect.width / 2, knobRect.height / 2) - scroll;
                    DrawNodeCurve(knobType == QQ_KnobType.Output ? knobRect : mouseRect, knobType == QQ_KnobType.Output ? mouseRect : knobRect, false);
                    Repaint();
                }

                GUILayout.BeginArea(new Rect(-scroll, new Vector2(5000, 5000)));

                for (int i = 0; i < nodeCount; ++i)
                {
                    QQ_Node node = db.Nodes[i];
                    int outputCount = node.Outputs.Count;
                    for (int j = 0; j < outputCount; ++j)
                    {
                        QQ_Knob output = node.Outputs[j];
                        if (output.Connections.Count > 0)
                        {
                            foreach (KeyValuePair<int, List<int>> connection in output.Connections.dictionary)
                            {
                                int connectionCount = connection.Value.Count;
                                for (int k = 0; k < connectionCount; ++k)
                                    DrawNodeCurve(node.OutputKnob(j), db.GetNode(connection.Key).InputKnob(connection.Value[k]), true);
                            }
                        }
                    }
                }

                BeginWindows();
                for (int i = 0; i < nodeCount; ++i)
                {
                    QQ_Node node = db.Nodes[i];
                    if (node.Type == QQ_NodeType.Quest)
                    {
                        QQ_QuestNode realNode = db.GetQuestNode(node.ID);
                        node.Window = GUI.Window(node.ID, node.Window, realNode.DrawWindow, realNode.WindowTitle);
                    }
                    else if (node.Type == QQ_NodeType.Task)
                    {
                        QQ_TaskNode realNode = db.GetTaskNode(node.ID);
                        node.Window = GUI.Window(node.ID, node.Window, realNode.DrawWindow, realNode.WindowTitle);
                    }
                    node.DrawKnobs();
                }
                EndWindows();

                GUILayout.EndArea();

                scroll = new Vector2(
                    GUI.HorizontalScrollbar(new Rect(0, eHeight - 15, eWidth - 15, eHeight - 5), scroll.x, eHeight, 0, 5000),
                    GUI.VerticalScrollbar(new Rect(eWidth - 15, 0, eWidth - 5, eHeight - 15), scroll.y, eHeight, 0, 5000)
                );
            }
        }

        public void SelectDB()
        {
            if (Selection.activeObject && Selection.activeObject.GetType() == typeof(QQ_NodeDB))
                db = (QQ_NodeDB)Selection.activeObject;
            else db = null;
        }

        // Taken from https://forum.unity.com/threads/simple-node-editor.189230/
        public void DrawNodeCurve(Rect start, Rect end, bool node)
        {
            Vector3 startPos = new Vector3(start.x + (node ? start.width : 0), start.y + (node ? start.height / 2 : 0), 0);
            Vector3 endPos = new Vector3(end.x, end.y + (node ? end.height / 2 : 0), 0);
            Vector3 startTan = startPos + Vector3.right * 50;
            Vector3 endTan = endPos + Vector3.left * 50;
            Handles.DrawBezier(startPos, endPos, startTan, endTan, skin.customStyles[3].normal.textColor, null, 3);
        }

        public void ContextCallback(object obj)
        {
            string command = obj.ToString();
            EditorUtility.SetDirty(db);
            EditorUtility.SetDirty(db.DataDB);

            if (command == "Add Task")
                db.CreateNode(QQ_NodeType.Task, mouse + scroll);
            else if (command == "Attach to Node")
                connectingNodes = true;
            else if (command == "Detach All Connected Nodes")
                DetachNodes();
            else if (command == "Detach Connected Nodes")
                DetachNodes(selectedKnob, knobType);
            else if (command == "Delete Node")
            {
                DetachNodes();

                int i = db.GetNodeIndex(selectedNode.ID);
                QQ_Node node = db.Nodes[i];
                if (node.Type == QQ_NodeType.Task)
                    db.TaskNodes.RemoveAt(db.GetTaskNodeIndex(selectedNode.ID));
                db.Nodes.RemoveAt(i);
                if (node.Type == QQ_NodeType.Task)
                {
                    i = db.DataDB.GetTaskIndex(selectedNode.ID);
                    db.DataDB.Quest.Tasks.RemoveAt(i);
                }
                if (db.Nodes.Count == 0)
                    db.NextID = 0;
            }
        }

        public void DetachNodes()
        {
            int inputCount = selectedNode.Inputs.Count;
            int outputCount = selectedNode.Outputs.Count;

            for (int i = 0; i < inputCount; ++i)
                DetachNodes(i, QQ_KnobType.Input);
            for (int i = 0; i < outputCount; ++i)
                DetachNodes(i, QQ_KnobType.Output);
        }
        public void DetachNodes(int knobID, QQ_KnobType type)
        {
            QQ_Knob knob = selectedNode.GetKnob(type, knobID);
            
            List<int> keys = new List<int>();
            if (knob.Connections.Count > 0)
            {
                foreach (KeyValuePair<int, List<int>> connection in knob.Connections.dictionary)
                {
                    int connectionCount = connection.Value.Count;
                    QQ_Node conn = db.GetNode(connection.Key);
                    for (int j = 0; j < connectionCount; ++j)
                        conn.DisconnectNode(selectedNode.ID, connection.Value[j], type == QQ_KnobType.Input ? QQ_KnobType.Output : QQ_KnobType.Input);
                    keys.Add(connection.Key);
                }
            }
            int keyCount = keys.Count;
            for (int j = 0; j < keyCount; ++j)
                selectedNode.DisconnectNode(keys[j], knobID, type);
        }
    }
}