using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;

[Serializable]
public class MainObjectiveList {
	//Objective _objective;
	public string _internalName;
	public Objective _objectiveGameObject;
	public string _objectiveObjectName;
	[Multiline (5)]
	public string _objectiveDescription;
	public string sceneFile;
	public bool enabled = false;
	public bool completed = false;
	public bool active = false;
}

public class ObjectivesList : MonoBehaviour {
	public bool loadObjectives = false;
	public GameObject[] objectiveList;

	static public string jsonFileName = "objectives.json";

	public List<MainObjectiveList> masterObjectiveList = new List<MainObjectiveList> ();
	public List<MainObjectiveList> playerObjectiveList = new List<MainObjectiveList> ();

	public static MainObjectiveList CreateFromJSON (string jsonString) {
		return JsonUtility.FromJson<MainObjectiveList> (jsonString);
	}

	static public string CreateJSON (MainObjectiveList objectiveData) {
		return JsonUtility.ToJson (objectiveData);
	}

	public void WriteFile (string jsonFile = "", List<MainObjectiveList> objList = null) //Objective objective)
	{

		if (jsonFile == "") jsonFile = jsonFileName;
		//Debug.Log("Saving -> " + jsonFile);
		//if (objList == null) objList = masterObjectiveList;
		string path = Application.persistentDataPath + "/" + jsonFile;
		//Write some text to the test.txt file
		StreamWriter writer = new StreamWriter (path, false);
		writer.Close ();
		//masterObjectiveList = new List<MainObjectiveList>();
		writer = new StreamWriter (path, true);
		// MainObjectiveList newObject = new MainObjectiveList();
		// newObject.sceneFile = "Space";
		// newObject._objectiveObjectName = "Sphere";
		// masterObjectiveList.Add(newObject);
		//
		// newObject = new MainObjectiveList();
		// newObject.sceneFile = "Space";
		// newObject._objectiveObjectName = "Sphere (1)";
		// masterObjectiveList.Add(newObject);
		//
		// newObject = new MainObjectiveList();
		// newObject.sceneFile = "Space";
		// newObject._objectiveObjectName = "Sphere (2)";
		// masterObjectiveList.Add(newObject);
		//
		// newObject = new MainObjectiveList();
		// newObject.sceneFile = "Space";
		// newObject._objectiveObjectName = "Enemy";
		// masterObjectiveList.Add(newObject);

		foreach (MainObjectiveList obj in objList) {
			GameObject find = GameObject.Find (obj._objectiveObjectName);
			if (find != null) obj._objectiveGameObject = find.GetComponent<Objective> ();
		}

		foreach (MainObjectiveList obj in objList) {
			//WriteFile(obj);
			//Debug.Log("Write -> " + CreateJSON(obj));
			writer.WriteLine (CreateJSON (obj));
		}

		writer.Close ();

		// StreamReader reader = new StreamReader(path);
		// //Print the text from the file
		// Debug.Log(path);
		// Debug.Log(reader.ReadToEnd());
		// reader.Close();
	}

	public List<MainObjectiveList> ReadFile (string jsonFile = "") {
		List<MainObjectiveList> objList = new List<MainObjectiveList> ();
		if (jsonFile == "") jsonFile = jsonFileName;
		// Debug.Log("Loading -> " + jsonFile);
		string path = Application.persistentDataPath + "/" + jsonFile;
		//Read the text from directly from the test.txt file
		StreamReader reader = new StreamReader (path);
		string line = "";
		bool done = false;
		//reader
		while ((line = reader.ReadLine ()) != null) {
			//Debug.Log("Read -> " + line);
			objList.Add (CreateFromJSON (line));
		}
		//.ReadToEnd());
		reader.Close ();
		return objList;
	}

	public bool isObjectiveEnabled (string name) {
		for (int index = 0; index < playerObjectiveList.Count; index++) {
			Debug.Log (playerObjectiveList[index]._objectiveObjectName);
			if (playerObjectiveList[index]._objectiveObjectName == name) {

				if (playerObjectiveList[index].enabled) return true;
			}

		}
		return false;
	}
	public void enableObjective (string name) {
		for (int index = 0; index < playerObjectiveList.Count; index++) {
			//MainObjectiveList obj in objList) {
			//WriteFile(obj);
			//Debug.Log(CreateJSON(obj));
			//Debug.Log(playerObjectiveList[index]._objectiveObjectName);
			if (playerObjectiveList[index]._objectiveObjectName == name) {
				//Debug.Log(playerObjectiveList[index]._objectiveObjectName);
				playerObjectiveList[index].enabled = true;
			}
			//playerObjectiveList[index] = masterObjectiveList[index];
		}
	}
	public void disableObjective (string name) {
		for (int index = 0; index < playerObjectiveList.Count; index++) {
			//MainObjectiveList obj in objList) {
			//WriteFile(obj);
			//Debug.Log(CreateJSON(obj));
			//Debug.Log(playerObjectiveList[index]._objectiveObjectName);
			if (playerObjectiveList[index]._objectiveObjectName == name) {
				//Debug.Log(playerObjectiveList[index]._objectiveObjectName);
				playerObjectiveList[index].enabled = false;
			}
			//playerObjectiveList[index] = masterObjectiveList[index];
		}
	}
	public void toggleCompleteObjective (string name, bool toggle) {
		for (int index = 0; index < playerObjectiveList.Count; index++) {
			//MainObjectiveList obj in objList) {
			//WriteFile(obj);
			//Debug.Log(CreateJSON(obj));
			//Debug.Log(playerObjectiveList[index]._objectiveObjectName);
			if (playerObjectiveList[index]._objectiveObjectName == name) {
				//Debug.Log(playerObjectiveList[index]._objectiveObjectName);
				playerObjectiveList[index].completed = toggle;
			}
			//playerObjectiveList[index] = masterObjectiveList[index];
		}
	}
	public void updatePlayerObjective (MainObjectiveList update) {
		for (int index = 0; index < masterObjectiveList.Count; index++) {
			//MainObjectiveList obj in objList) {
			//WriteFile(obj);
			//Debug.Log(CreateJSON(obj));
			//if (playerObjectiveList[index]._objectiveObjectName == update._objectiveObjectName)
			playerObjectiveList[index] = masterObjectiveList[index];
		}

	}

	// Start is called before the first frame update
	void Start () {

		//WriteFile();

		//ReadFile();

	}

	// Update is called once per frame
	void Update () {
		if (loadObjectives) {
			loadObjectives = false;
			//ReadFile();
		}
		//GameObject[] templist =
		objectiveList = GameObject.FindGameObjectsWithTag ("Objective");
		//objectiveList.Clear();

		// foreach(GameObject obj in templist){
		// 	objectiveList.Add(obj);
		// }

		//if (objectiveList.Length<0) objectiveList = GameObject.FindGameObjectsWithTag("Enemy");

	}

	void Awake () {
		//DontDestroyOnLoad(this.gameObject);
	}
}