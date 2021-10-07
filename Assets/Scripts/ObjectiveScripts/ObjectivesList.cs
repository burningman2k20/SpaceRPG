using System;
using System.Linq;
using System.Linq.Expressions;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MainObjectiveList{
    //Objective _objective;
	public Objective _objectiveGameObject;
    public string _objectiveObjectName;
	[Multiline(5)]
	public string _objectiveDescription;
	public string sceneFile;
    public bool enabled = false;
    public bool active = false;
}

public class ObjectivesList : MonoBehaviour
{
	public bool loadObjectives = false;
    public GameObject[] objectiveList;

	static public string jsonFileName = "objectives.json";

	public List<MainObjectiveList> masterObjectiveList = new List<MainObjectiveList>();

	public static MainObjectiveList CreateFromJSON(string jsonString)  {
	        return JsonUtility.FromJson<MainObjectiveList>(jsonString);
	}

	static public string CreateJSON(MainObjectiveList objectiveData) {
        return JsonUtility.ToJson(objectiveData);
    }

	public void WriteFile()//Objective objective)
{
	string path = Application.persistentDataPath + "/" + jsonFileName;
	//Write some text to the test.txt file
	StreamWriter writer = new StreamWriter(path, false);
	writer.Close();
	masterObjectiveList = new List<MainObjectiveList>();
	writer = new StreamWriter(path, true);
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


	foreach(MainObjectiveList obj in masterObjectiveList){
		GameObject find = GameObject.Find(obj._objectiveObjectName);
		if (find != null) obj._objectiveGameObject = find.GetComponent<Objective>();
	}

	foreach (MainObjectiveList obj in masterObjectiveList) {
		//WriteFile(obj);
		//Debug.Log(CreateJSON(obj));
		writer.WriteLine(CreateJSON(obj));
	}

	 writer.Close();

	// StreamReader reader = new StreamReader(path);
	// //Print the text from the file
	// Debug.Log(path);
	// Debug.Log(reader.ReadToEnd());
	// reader.Close();
 }

 public void ReadFile(){
	string path = Application.persistentDataPath + "/" + jsonFileName;
	//Read the text from directly from the test.txt file
	StreamReader reader = new StreamReader(path);
	string line = "";
	bool done = false;
	while (!done){
		line = reader.ReadLine();
		Debug.Log(line);
		if (line == "") done = true;
		masterObjectiveList.Add(CreateFromJSON(line));
	}
	//.ReadToEnd());
	reader.Close();
}
    // Start is called before the first frame update
    void Start() {

		//WriteFile();


	ReadFile();

    }

    // Update is called once per frame
    void Update()
    {
		if (loadObjectives){
			loadObjectives = false;
			//ReadFile();
		}
		//GameObject[] templist =
		objectiveList = GameObject.FindGameObjectsWithTag("Objective");
		//objectiveList.Clear();

		// foreach(GameObject obj in templist){
		// 	objectiveList.Add(obj);
		// }

        //if (objectiveList.Length<0) objectiveList = GameObject.FindGameObjectsWithTag("Enemy");

    }
}
