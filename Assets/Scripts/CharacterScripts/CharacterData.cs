using System;
using System.Linq;
using System.Linq.Expressions;
using System.IO;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameDataTypes;
using static GameManager;

public class Data{
	public string shipObjectName;
	public string groundObjectName;
}

public class CharacterData
{
	static public string jsonFileName = "character.json";
    //GameManager gameManger;
    // SelectionUI selectionUI;

    // constructor
    public CharacterData()
    {
        // gameManger = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        // selectionUI = GameObject.Find("UIManager").GetComponent<SelectionUI>();

    }

	public static Data CreateFromJSON(string jsonString)  {
			return JsonUtility.FromJson<Data>(jsonString);
	}

	static public string CreateJSON(Data objectiveData) {
		return JsonUtility.ToJson(objectiveData);
	}

	public void WriteFile(string jsonFile = "")//Objective objective)
	{
		//List<InventoryData> objList = new List<InventoryData>();

		// foreach(QI_ItemData entry in itemDatabase.Items)
		// {
		// 	if (inventory.GetStock(entry.Name) > 0) {
		// 		InventoryData data = new InventoryData(entry.Name, inventory.GetStock(entry.Name));
		// 		objList.Add(data);
		// 	}
		// }
	if (jsonFile == "") jsonFile = jsonFileName;
	//Debug.Log("Saving -> " + jsonFile);
	//if (objList == null) objList = masterObjectiveList;
	string path = Application.persistentDataPath + "/" + jsonFile;
	//Write some text to the test.txt file
	StreamWriter writer = new StreamWriter(path, false);
	writer.Close();

	writer = new StreamWriter(path, true);

	// foreach (InventoryData obj in objList) {
	// 	//WriteFile(obj);
	// 	//Debug.Log("Write -> " + CreateJSON(obj));
	// 	writer.WriteLine(CreateJSON(obj));
	// }

	 writer.Close();

	}

	public void ReadFile(string jsonFile ="") {
	 //List<InventoryData> objList = new List<InventoryData>();
	 //inventory = new QI_Inventory();
	 if (jsonFile == "") jsonFile = jsonFileName;
	// Debug.Log("Loading -> " + jsonFile);
	string path = Application.persistentDataPath + "/" + jsonFile;
	//Read the text from directly from the test.txt file
	StreamReader reader = new StreamReader(path);
	string line = "";
	bool done = false;
	//reader
	while ((line = reader.ReadLine()) != null){
		//Debug.Log("Read -> " + line);
		//InventoryData data = CreateFromJSON(line);
		//inventory.AddItem(itemDatabase.GetItem(data.name), data.amount);

	}

	reader.Close();

	}
}
