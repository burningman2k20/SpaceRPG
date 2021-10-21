using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuantumTek.QuantumInventory;
using static GameDataTypes;

public class NewCharacterData {

    public ShipControls shipControls = new ShipControls();
    public SimpleTankController groundControls = new SimpleTankController();
    public string shipEngineName;
    public string shipWeaponName;
    public string groundWeaponName;
    public Engines shipEngine;
    public Weapons shipWeapon;
    public Weapons groundWeapon;
}
public class CharacterManager : MonoBehaviour
{
    public PrefabManager prefabManager;
    public InventoryManager inventoryManager;
    public GameManager gameManager;
    public string jsonFileName = "character_data.json";

    public NewCharacterData characterData = new NewCharacterData();

    public static NewCharacterData CreateFromJSON(string jsonString)  {
			return JsonUtility.FromJson<NewCharacterData>(jsonString);
	}

	static public string CreateJSON(NewCharacterData objectiveData) {
		return JsonUtility.ToJson(objectiveData);
	}

    void OnGUI(){

        GUILayout.BeginArea(new Rect(0,250,100,100));
        if (GUILayout.Button("Save")){
            WriteFile();
        }
        if (GUILayout.Button("Load")){
            ReadFile();
        }
        GUILayout.EndArea();
    }
	public void WriteFile(string jsonFile = "")//Objective objective)
	{
		// List<InventoryData> objList = new List<InventoryData>();

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
        Engines engine = characterData.shipEngine;
        //prefabManager.currentPrefab.GetComponent<ShipControls>().engine;
        Weapons shipWeapon = characterData.shipWeapon;
        //prefabManager.currentPrefab.GetComponent<ShipControls>().weapon;
        Weapons groundWeapon = characterData.groundWeapon;
        //null;
        if (gameManager.playerLocation == locationType.Ground) groundWeapon = prefabManager.currentPrefab.GetComponent<SimpleTankController>().weapon;
        if (engine != null) characterData.shipEngineName = engine.name;
        if (shipWeapon != null) characterData.shipWeaponName = shipWeapon.name;
        if (gameManager.playerLocation == locationType.Ground) characterData.groundWeaponName = groundWeapon.name;
        
        writer.WriteLine(CreateJSON(characterData));

        // foreach (NewCharacterData obj in objList) {
        // 	//WriteFile(obj);
         	//Debug.Log("Write -> " + CreateJSON(characterData));
        // 	writer.WriteLine(CreateJSON(obj));
        // }

        writer.Close();

	}

	public void ReadFile(string jsonFile ="") {
	// List<InventoryData> objList = new List<InventoryData>();
	 //inventory = new QI_Inventory();
	 if (jsonFile == "") jsonFile = jsonFileName;
	// Debug.Log("Loading -> " + jsonFile);
	string path = Application.persistentDataPath + "/" + jsonFile;
	//Read the text from directly from the test.txt file
	StreamReader reader = new StreamReader(path);
	string line = "";
	bool done = false;
	//reader
	//while ((line = reader.ReadLine()) != null){
        line = reader.ReadLine();
        characterData = CreateFromJSON(line);
        QI_ItemData item;
        item = inventoryManager.itemDatabase.GetItem(characterData.shipWeaponName);
        //if (characterData.shipWeaponName != "") 
        characterData.shipWeapon = item.ItemPrefab.gameObject.GetComponent<Weapons>();
        //if (characterData.shipEngineName != "") 
        characterData.shipEngine = inventoryManager.itemDatabase.GetItem(characterData.shipEngineName).ItemPrefab.gameObject.GetComponent<Engines>();
        //if (characterData.groundWeaponName != "") 
        characterData.groundWeapon = inventoryManager.itemDatabase.GetItem(characterData.groundWeaponName).ItemPrefab.gameObject.GetComponent<Weapons>();
        //Debug.Log("Read -> " + line);
        //NewCharacterData data = CreateFromJSON(line);
        //inventory.AddItem(itemDatabase.GetItem(data.name), data.amount);
        //objList.Add(CreateFromJSON(line));
        //}
        //.ReadToEnd());
        reader.Close();
	//return objList;
	}
    // Start is called before the first frame update
    void Start()
    {
        prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        
    }

    void Awake(){
        prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (prefabManager.currentPrefab.GetComponent<SimpleTankController>() != null){
            characterData.groundControls = prefabManager.currentPrefab.GetComponent<SimpleTankController>();
            characterData.groundControls.weapon = characterData.groundWeapon;
        }

        if (prefabManager.currentPrefab.GetComponent<ShipControls>() != null){
            characterData.shipControls = prefabManager.currentPrefab.GetComponent<ShipControls>();
            characterData.shipControls.engine = characterData.shipEngine;
            characterData.shipControls.weapon = characterData.shipWeapon;
        }
        //characterData.tankController = prefabManager.currentPrefab.GetComponent<SimpleTankController>();
        //characterData.shipControls = prefab.currentPrefab.GetComponent<ShipControls>();
        
    }
}
