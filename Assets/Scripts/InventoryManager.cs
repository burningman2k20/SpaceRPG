using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using QuantumTek.QuantumInventory;
using UnityEngine;

public class InventoryData {
	public string name;
	public int amount;

	public InventoryData (string _name, int _amount) {
		name = _name;
		amount = _amount;
	}
}

public class InventoryManager : MonoBehaviour {
	public QI_Inventory inventory;
	public QI_ItemDatabase itemDatabase;
	public List<InventoryData> inventoryData = new List<InventoryData> ();
	public Dictionary<string, QI_ItemData> items = new Dictionary<string, QI_ItemData> ();

	static public string jsonFileName = "inventory.json";
	// Start is called before the first frame update
	void Start () {
		items.Add ("Health Potion", itemDatabase.GetItem ("Health Potion"));
		items.Add ("ShipEngine1", itemDatabase.GetItem ("ShipEngine1"));
		items.Add ("ShipWeapon1", itemDatabase.GetItem ("ShipWeapon1"));
		items.Add ("ShipGenerator1", itemDatabase.GetItem ("ShipGenerator1"));
		items.Add ("PlayerWeapon1", itemDatabase.GetItem ("PlayerWeapon1"));
		inventory.AddItem (items["Health Potion"], 1);
		inventory.AddItem (items["ShipEngine1"], 1);
		inventory.AddItem (items["ShipWeapon1"], 1);
		inventory.AddItem (items["ShipGenerator1"], 1);
		inventory.AddItem (items["PlayerWeapon1"], 1);
	}

	public static InventoryData CreateFromJSON (string jsonString) {
		return JsonUtility.FromJson<InventoryData> (jsonString);
	}

	static public string CreateJSON (InventoryData objectiveData) {
		return JsonUtility.ToJson (objectiveData);
	}

	public void WriteFile (string jsonFile = "") //Objective objective)
	{
		List<InventoryData> objList = new List<InventoryData> ();

		foreach (QI_ItemData entry in itemDatabase.Items) {
			if (inventory.GetStock (entry.Name) > 0) {
				InventoryData data = new InventoryData (entry.Name, inventory.GetStock (entry.Name));
				objList.Add (data);
			}
		}
		if (jsonFile == "") jsonFile = jsonFileName;
		//Debug.Log("Saving -> " + jsonFile);
		//if (objList == null) objList = masterObjectiveList;
		string path = Application.persistentDataPath + "/" + jsonFile;
		//Write some text to the test.txt file
		StreamWriter writer = new StreamWriter (path, false);
		writer.Close ();

		writer = new StreamWriter (path, true);

		foreach (InventoryData obj in objList) {
			//WriteFile(obj);
			//Debug.Log("Write -> " + CreateJSON(obj));
			writer.WriteLine (CreateJSON (obj));
		}

		writer.Close ();

	}

	public void ReadFile (string jsonFile = "") {
		List<InventoryData> objList = new List<InventoryData> ();
		//inventory = new QI_Inventory();
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
			InventoryData data = CreateFromJSON (line);
			inventory.AddItem (itemDatabase.GetItem (data.name), data.amount);
			//objList.Add(CreateFromJSON(line));
		}
		//.ReadToEnd());
		reader.Close ();
		//return objList;
	}

	public bool hasItem (string name) {
		if (inventory.GetStock (name) > 0) return true;
		return false;
	}

	public void Pickup (string _item = "", int amount = 1) {
		if (_item == "") return;
		// Add a health potion
		inventory.AddItem (items[_item], amount);
	}

	public void Drop (string _item = "", int amount = 1) {
		if (_item == "") return;
		// Removes a health potion
		inventory.RemoveItem (_item, amount);
	}

	public void PickupPotion (string _item = "") {
		if (_item == "") return;
		// Add a health potion
		inventory.AddItem (items["Health Potion"], 1);
		// Update text after getting the number of health potions left
		//potionCount.text = inventory.GetStock("Health Potion") + " Potions";
	}

	public void DropPotion (string _item = "") {
		if (_item == "") return;
		// Removes a health potion
		inventory.RemoveItem ("Health Potion", 1);
		// Update text after getting the number of health potions left
		//potionCount.text = inventory.GetStock("Health Potion") + " Potions";
	}

	// Update is called once per frame
	void Update () {

	}
}