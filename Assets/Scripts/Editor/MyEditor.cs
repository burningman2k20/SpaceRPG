using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class MyEditor : EditorWindow
{
	int _toolbar = 0;
	string[] toolbarSettings = {"1","2"};

	[MenuItem("Editor/Edit Window")]



	static void Init(){
		MyEditor window = (MyEditor)EditorWindow.GetWindow(typeof(MyEditor));
		window.Show();
	}

	void OnGUI(){


		GUILayout.BeginHorizontal();
		_toolbar = GUILayout.Toolbar(_toolbar, toolbarSettings);
		GUILayout.EndHorizontal();
	}
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
