using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class MyEditor : EditorWindow {
	int _toolbar = 0;
	string[] toolbarSettings = { "1", "2" };
	static public string jsonFileName = "objectives.json";
	public List<MainObjectiveList> masterObjectiveList = new List<MainObjectiveList> ();

	[MenuItem ("Editor/Edit Window")]

	static void Init () {
		MyEditor window = (MyEditor) EditorWindow.GetWindow (typeof (MyEditor));
		window.Show ();
	}

	public static MainObjectiveList CreateFromJSON (string jsonString) {
		return JsonUtility.FromJson<MainObjectiveList> (jsonString);
	}

	static public string CreateJSON (MainObjectiveList objectiveData) {
		return JsonUtility.ToJson (objectiveData);
	}
	public void WriteFile () //Objective objective)
	{
		string path = Application.persistentDataPath + "/" + jsonFileName;
		//Write some text to the test.txt file
		StreamWriter writer = new StreamWriter (path, false);
		writer.Close ();
		//masterObjectiveList = new List<MainObjectiveList>();
		writer = new StreamWriter (path, true);
		// MainObjectiveList newObject = new MainObjectiveList();
		// newObject.sceneFile = "Space";
		// newObject._objectiveObjectName = "Sphere";
		// masterObjectiveList.Add(newObject);

		// newObject = new MainObjectiveList();
		// newObject.sceneFile = "Space";
		// newObject._objectiveObjectName = "Sphere (1)";
		// masterObjectiveList.Add(newObject);

		// newObject = new MainObjectiveList();
		// newObject.sceneFile = "Space";
		// newObject._objectiveObjectName = "Sphere (2)";
		// masterObjectiveList.Add(newObject);

		// newObject = new MainObjectiveList();
		// newObject.sceneFile = "Space";
		// newObject._objectiveObjectName = "Enemy";
		// masterObjectiveList.Add(newObject);

		foreach (MainObjectiveList obj in masterObjectiveList) {
			obj._objectiveGameObject = GameObject.Find (obj._objectiveObjectName).GetComponent<Objective> ();
		}

		foreach (MainObjectiveList obj in masterObjectiveList) {
			//WriteFile(obj);
			//Debug.Log(CreateJSON(obj));
			writer.WriteLine (CreateJSON (obj));
		}

		writer.Close ();

		// StreamReader reader = new StreamReader(path);
		// //Print the text from the file
		// Debug.Log(path);
		// Debug.Log(reader.ReadToEnd());
		// reader.Close();
	}
	public void ReadFile () {
		masterObjectiveList = new List<MainObjectiveList> ();

		string path = Application.persistentDataPath + "/" + jsonFileName;
		//Read the text from directly from the test.txt file
		StreamReader reader = new StreamReader (path);
		string line = "";

		while (reader.ReadLine () != null) {
			line = reader.ReadLine ();
			masterObjectiveList.Add (CreateFromJSON (line));
		}
		//.ReadToEnd());
		reader.Close ();
	}

	void OnGUI () {

		GUILayout.BeginHorizontal ();
		_toolbar = GUILayout.Toolbar (_toolbar, toolbarSettings);
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal();
        if (GUILayout.Button("Load Data")) {
            ReadFile();
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal ();
		GUILayout.Box ("Select");
		GUILayout.Box ("Obj Name");
		GUILayout.Box ("Description");
		GUILayout.Box ("Pending?");
		GUILayout.EndHorizontal ();

		GUILayout.BeginHorizontal ();
		foreach(MainObjectiveList obj in masterObjectiveList){
			GUILayout.BeginVertical ();
			GUILayout.Box(obj._objectiveObjectName);
			GUILayout.EndVertical ();
		}GUILayout.EndHorizontal ();
	}
	// Start is called before the first frame update
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}
}