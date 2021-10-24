using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuantumTek.QuantumInventory;

public class CharacterUI : MonoBehaviour
{

	GameManager gameManager;
	InventoryManager inventoryManager;
	PrefabManager prefabManager;
    CharacterManager characterManager;
	UIPlacement uiPlacement;
	public ShipControls shipControls;
	public SimpleTankController groundControls;
	public Rect Placement;

	 int UnequipButtonWidth = 75;

	void OnGUI(){
		if (!gameManager.gameStarted) return;

		

		GUILayout.BeginArea(Placement);
		
		//if (characterManager.characterData.shipControls != null) {
		GUILayout.Box("Ship Data");
		if (characterManager.characterData.shipEngine != null){
			GUILayout.BeginHorizontal();
			GUILayout.Box(string.Format("{0}",characterManager.characterData.shipEngine));
			if (GUILayout.Button("Unequip", GUILayout.Width(UnequipButtonWidth))){
                string engine_name = characterManager.characterData.shipEngine.gameObject.name;
                //prefabManager.currentPrefab.GetComponent<ShipControls>().engine.gameObject.name;
                inventoryManager.inventory.AddItem(inventoryManager.itemDatabase.GetItem(engine_name),1);
                //Debug.Log(prefabManager.currentPrefab.GetComponent<ShipControls>().engine.gameObject.name);
                characterManager.characterData.shipEngine = null;
                //prefabManager.currentPrefab.GetComponent<ShipControls>().setEngine(null);
            }
			GUILayout.EndHorizontal();
		} else {
			GUILayout.Box("No engine mounted");
		}

		if (characterManager.characterData.shipGenerator != null){
			GUILayout.BeginHorizontal();
			GUILayout.Box(string.Format("{0}",characterManager.characterData.shipGenerator));
            GUILayout.Box(string.Format("{0}/{1}", characterManager.characterData.shipGenerator.getCurrentEnergy(characterManager.characterData), characterManager.characterData.shipGenerator.getMaxEnergy()));
            if (GUILayout.Button("Unequip", GUILayout.Width(UnequipButtonWidth))){
                string generator_name = characterManager.characterData.shipGenerator.gameObject.name;
                //prefabManager.currentPrefab.GetComponent<ShipControls>().engine.gameObject.name;
                inventoryManager.inventory.AddItem(inventoryManager.itemDatabase.GetItem(generator_name),1);
                //Debug.Log(prefabManager.currentPrefab.GetComponent<ShipControls>().engine.gameObject.name);
                characterManager.characterData.shipGenerator = null;
                //prefabManager.currentPrefab.GetComponent<ShipControls>().setEngine(null);
            }
			GUILayout.EndHorizontal();
		} else {
			GUILayout.Box("No Generator mounted");
		}

		if (characterManager.characterData.shipWeapon != null){
			GUILayout.BeginHorizontal();
			GUILayout.Box(string.Format("{0}",characterManager.characterData.shipWeapon));
			if (GUILayout.Button("Unequip", GUILayout.Width(UnequipButtonWidth))){

                string weapon_name = characterManager.characterData.shipWeapon.gameObject.name;
                //prefabManager.currentPrefab.GetComponent<ShipControls>().weapon.gameObject.name;
                inventoryManager.inventory.AddItem(inventoryManager.itemDatabase.GetItem(weapon_name),1);
				//Debug.Log(prefabManager.currentPrefab.GetComponent<ShipControls>().engine.gameObject.name);
				//prefabManager.currentPrefab.GetComponent<ShipControls>().setWeapon(null);
				characterManager.characterData.shipWeapon = null;
			}
			GUILayout.EndHorizontal();
		} else {
			GUILayout.Box("No Weapon mounted");
		}
		//}

		//if (characterManager.characterData.groundControls != null){
			GUILayout.Box("Character Data");
			if (characterManager.characterData.groundWeapon != null){
				GUILayout.BeginHorizontal();
				GUILayout.Box(string.Format("{0}",characterManager.characterData.groundWeapon));
			if (GUILayout.Button("Unequip", GUILayout.Width(UnequipButtonWidth))){
                string weapon_name = characterManager.characterData.groundWeapon.gameObject.name;
                //prefabManager.currentPrefab.GetComponent<SimpleTankController>().weapon.gameObject.name;
                inventoryManager.inventory.AddItem(inventoryManager.itemDatabase.GetItem(weapon_name),1);
				//Debug.Log(prefabManager.currentPrefab.GetComponent<ShipControls>().engine.gameObject.name);
				//prefabManager.currentPrefab.GetComponent<SimpleTankController>().setWeapon(null);
				characterManager.characterData.groundWeapon = null;
			}
			GUILayout.EndHorizontal();
		} else {
			GUILayout.Box("No Weapon mounted");
		}

		//}

		//Debug.Log(shipControls.engine);
		GUILayout.EndArea();
	}
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
		inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
		characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();



    }

    // Update is called once per frame
    void Update()
    {
		if (characterManager.characterData.shipControls == null) characterManager.characterData.shipControls = prefabManager.currentPrefab.gameObject.GetComponent<ShipControls>();
		if (characterManager.characterData.groundControls == null)  characterManager.characterData.groundControls = prefabManager.currentPrefab.gameObject.GetComponent<SimpleTankController>();


    }

	void Awake(){
        //shipControls = characterManager.characterData.shipControls;
		//groundControls = characterManager.characterData.groundControls;
        //shipControls = prefabManager.currentPrefab.gameObject.GetComponent<ShipControls>();
    }
}
