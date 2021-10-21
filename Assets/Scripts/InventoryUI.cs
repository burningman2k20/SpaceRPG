using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuantumTek.QuantumInventory;

public class InventoryUI : MonoBehaviour
{
	public GameManager gameManager;
	public UIPlacement uiManager;
	public InventoryManager inventoryManager;
	public CharacterManager characterManager;
	public PrefabManager prefabManager;

	void Awake(){

		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();
		uiManager = GameObject.Find("UIManager").GetComponent<UIPlacement>();
		inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
        characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

	void OnGUI(){
		if (!gameManager.gameStarted) return;
		GUILayout.BeginArea(uiManager.inventoryUIRect);
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Save")){
			inventoryManager.WriteFile("");
		}
		if (GUILayout.Button("Load")){
			inventoryManager.ReadFile("");

		}
		GUILayout.EndHorizontal();
		foreach(QI_ItemData entry in inventoryManager.itemDatabase.Items)
		{
            // do something with entry.Value or entry.Key
            if (inventoryManager.inventory.GetStock(entry.Name) > 0)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Box(string.Format("{0}", inventoryManager.inventory.GetStock(entry.Name)), GUILayout.MinWidth(50));
				GUILayout.Box(entry.Name);
                QI_ItemData item = inventoryManager.itemDatabase.GetItem(entry.Name);
                if (item.ItemPrefab != null)
                {
                    Projectile projectile = item.ItemPrefab.gameObject.GetComponent<Projectile>();
                    Weapons weapon = item.ItemPrefab.gameObject.GetComponent<Weapons>();
                    Engines engine = item.ItemPrefab.gameObject.GetComponent<Engines>();
                    //ShipControls shipControls = prefabManager.currentPrefab.GetComponent<ShipControls>();
                    //SimpleTankController tankController = prefabManager.currentPrefab.GetComponent<SimpleTankController>();

                    if (projectile != null)
                    {
                        GUILayout.Box("Ammo");
                    }

					if (weapon != null){
                        if (weapon.weaponPlacement == WeaponPlacement.Ship)
                        {
                            GUILayout.Box("Ship Weapon");
							if (characterManager.characterData.shipWeapon != weapon) 
								if (GUILayout.Button("Equip")){
									characterManager.characterData.shipWeapon = item.ItemPrefab.gameObject.GetComponent<Weapons>();
									inventoryManager.Drop(entry.Name);
								}
                        }
                        if (weapon.weaponPlacement == WeaponPlacement.Person)
                        {
                            GUILayout.Box("Personal Weapon");
                            if (characterManager.characterData.groundWeapon != weapon)
                            {
                                if (GUILayout.Button("Equip")){
									characterManager.characterData.groundWeapon = item.ItemPrefab.gameObject.GetComponent<Weapons>();
									inventoryManager.Drop(entry.Name);
								}
                            }
                        }

                    }

					if (engine != null){
						GUILayout.Box("Engine");
                        if (characterManager.characterData.shipEngine != engine)
                        {
                            if (GUILayout.Button("Equip")){
								characterManager.characterData.shipEngine = item.ItemPrefab.gameObject.GetComponent<Engines>();
								inventoryManager.Drop(entry.Name);
							}
                        }
                    }

                    // if (weapon != null)
                    // {
					// 		GUILayout.Box("Weapon");
                    //     switch (weapon.weaponPlacement)
                    //     {
                    //         case WeaponPlacement.Ship:
					
                    //     if (characterManager.characterData.shipWeapon != item.ItemPrefab.gameObject.GetComponent<Weapons>() && GUILayout.Button("Equip"))
                    //     {
                    //         characterManager.characterData.shipWeapon = item.ItemPrefab.gameObject.GetComponent<Weapons>();
                    //         //prefabManager.currentPrefab.GetComponent<ShipControls>().setWeapon(item);
                    //         inventoryManager.Drop(entry.Name);
                    //     }
                    //     else if (characterManager.characterData.shipWeapon == item.ItemPrefab.gameObject.GetComponent<Weapons>())
                    //     {
                    //         GUILayout.Box("Equip");
                    //         //prefabManager.currentPrefab.GetComponent<ShipControls>().setEngine(null);
                    //     }
                    //             break;

                    //         case WeaponPlacement.Person:
						
                    //     		if (characterManager.characterData.groundWeapon != item.ItemPrefab.gameObject.GetComponent<Weapons>()) if (GUILayout.Button("Equip"))
                    //     		{
                    //         //prefabManager.currentPrefab.GetComponent<SimpleTankController>().setWeapon(item);
                    //         		characterManager.characterData.groundWeapon = item.ItemPrefab.gameObject.GetComponent<Weapons>();
                    //         		inventoryManager.Drop(entry.Name);
                    //     		} 
					// 			else if (characterManager.characterData.groundWeapon == item.ItemPrefab.gameObject.GetComponent<Weapons>())
                    //     		{
                    //         		GUILayout.Box("Equip");
                    //         //prefabManager.currentPrefab.GetComponent<ShipControls>().setEngine(null);
                    //     		}
                    //             break;
                    //     }
                    // }
                
                    //item.ItemPrefab.gameObject.GetComponent<Engines>()
                    // if (characterManager.characterData.shipEngine != item.ItemPrefab.gameObject.GetComponent<Engines>() && GUILayout.Button("Equip"))
                    // {
					// 	 GUILayout.Box("Engine");
                    //     //prefabManager.currentPrefab.GetComponent<ShipControls>().setEngine(item);
                    //     characterManager.characterData.shipEngine = item.ItemPrefab.gameObject.GetComponent<Engines>();
                    //     inventoryManager.Drop(entry.Name);
                    // }
                    // else if (characterManager.characterData.shipEngine == item.ItemPrefab.gameObject.GetComponent<Engines>())
                    // {
					// 	GUILayout.Box("Engine");
                    //     GUILayout.Box("Equip");
                    //     //prefabManager.currentPrefab.GetComponent<ShipControls>().setEngine(null);
                    // }
                    //}
                    GUILayout.Box(item.ItemPrefab.gameObject.name);
                }

				 if (inventoryManager.inventory.GetStock(entry.Name) >= 1) {
					if (GUILayout.Button("Drop")){
						 inventoryManager.Drop(entry.Name, 1);
					}
				}
				if (inventoryManager.inventory.GetStock(entry.Name) > 1){
					if (GUILayout.Button("Drop All")){
						inventoryManager.Drop(entry.Name,inventoryManager.inventory.GetStock(entry.Name));
					}

				} else {
					GUILayout.Box("Drop All");
				}
				GUILayout.EndHorizontal();
            }
           
				
			}
			
			//GUILayout.Box(entry.Key);
			
			//GUILayout.Box(string.Format("{0}",inventoryManager.inventory.GetStock("ShipEngine1")));
			//GUILayout.Box(string.Format("{0}",inventoryManager.inventory.GetStock("Health Potion")));
			//if (GUILayout.Button("+Potion")) inventoryManager.PickupPotion("nothing");
			//if (GUILayout.Button("=Potion")) inventoryManager.DropPotion("nothing");
		GUILayout.EndArea();
	}
    // Update is called once per frame
    void Update()
    {

    }
}
