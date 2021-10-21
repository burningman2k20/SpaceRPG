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

    int DropButtonWidth = 45;
    int DropAllButtonWidth = 70;
    int EquipButtonWidth = 60;

    int MaxItemWidth = 120;
    int MaxTypeWidth = 120;

	int MaxScrollHeight = 200;
    int MaxScrollWidth = 475;

    Vector2 scrollPosition;


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
		scrollPosition = GUILayout.BeginScrollView(
            scrollPosition, GUILayout.Width(MaxScrollWidth), GUILayout.Height(MaxScrollHeight));
		foreach(QI_ItemData entry in inventoryManager.itemDatabase.Items)
		{
			
            // do something with entry.Value or entry.Key
            if (inventoryManager.inventory.GetStock(entry.Name) > 0)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Box(string.Format("{0}", inventoryManager.inventory.GetStock(entry.Name)), GUILayout.Width(35));
				GUILayout.Box(entry.Name, GUILayout.Width(MaxItemWidth));
                QI_ItemData item = inventoryManager.itemDatabase.GetItem(entry.Name);
                if (item.ItemPrefab != null)
                {
                    Projectile projectile = item.ItemPrefab.gameObject.GetComponent<Projectile>();
                    Weapons weapon = item.ItemPrefab.gameObject.GetComponent<Weapons>();
                    Engines engine = item.ItemPrefab.gameObject.GetComponent<Engines>();
                    ShipGenerator generator = item.ItemPrefab.gameObject.GetComponent<ShipGenerator>();
                    //ShipControls shipControls = prefabManager.currentPrefab.GetComponent<ShipControls>();
                    //SimpleTankController tankController = prefabManager.currentPrefab.GetComponent<SimpleTankController>();

                    if (projectile != null)
                    {
                        GUILayout.Box("Ammo");
                    }
					 if (generator != null)
                    {
                        GUILayout.Box("Ship Generator", GUILayout.Width(MaxTypeWidth));
                        if (characterManager.characterData.shipGenerator != generator)
                        {
                            if (GUILayout.Button("Equip", GUILayout.Width(EquipButtonWidth))){
								characterManager.characterData.shipGenerator = item.ItemPrefab.gameObject.GetComponent<ShipGenerator>();
								inventoryManager.Drop(entry.Name);
							}
                        }
                    }

					if (weapon != null){
                        if (weapon.weaponPlacement == WeaponPlacement.Ship)
                        {
                            GUILayout.Box("Ship Weapon", GUILayout.Width(MaxTypeWidth));
							if (characterManager.characterData.shipWeapon != weapon) 
								if (GUILayout.Button("Equip", GUILayout.Width(EquipButtonWidth))){
									characterManager.characterData.shipWeapon = item.ItemPrefab.gameObject.GetComponent<Weapons>();
									inventoryManager.Drop(entry.Name);
								}
                        }
                        if (weapon.weaponPlacement == WeaponPlacement.Person)
                        {
                            GUILayout.Box("Personal Weapon", GUILayout.Width(MaxTypeWidth));
                            if (characterManager.characterData.groundWeapon != weapon)
                            {
                                if (GUILayout.Button("Equip", GUILayout.Width(EquipButtonWidth))){
									characterManager.characterData.groundWeapon = item.ItemPrefab.gameObject.GetComponent<Weapons>();
									inventoryManager.Drop(entry.Name);
								}
                            }
                        }

                    }

					if (engine != null){
						GUILayout.Box("Engine", GUILayout.Width(MaxTypeWidth));
                        if (characterManager.characterData.shipEngine != engine)
                        {
                            if (GUILayout.Button("Equip", GUILayout.Width(EquipButtonWidth))){
								characterManager.characterData.shipEngine = item.ItemPrefab.gameObject.GetComponent<Engines>();
								inventoryManager.Drop(entry.Name);
							}
                        }
                    }

                   
                    //GUILayout.Box(item.ItemPrefab.gameObject.name, GUILayout.Width(MaxItemWidth));
                }

				 if (inventoryManager.inventory.GetStock(entry.Name) >= 1) {
					if (GUILayout.Button("Drop", GUILayout.Width(DropButtonWidth))){
						 inventoryManager.Drop(entry.Name, 1);
					}
				}
				if (inventoryManager.inventory.GetStock(entry.Name) > 1){
					if (GUILayout.Button("Drop All", GUILayout.Width(DropAllButtonWidth))){
						inventoryManager.Drop(entry.Name,inventoryManager.inventory.GetStock(entry.Name));
					}

				} else {
					GUILayout.Box("Drop All", GUILayout.Width(DropAllButtonWidth));
				}
				GUILayout.EndHorizontal();
            }
           
			 // End the scrollview we began above.
        	
			}
			GUILayout.EndScrollView();	
			
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
