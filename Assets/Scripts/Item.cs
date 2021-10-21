using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameDataTypes;
using QuantumTek.QuantumInventory;

public class Item : MonoBehaviour
{
    public itemType item;
	public QI_ItemData itemData;
	public interactType interact;

	//InventoryManager inventoryManager;

	QI_ItemDatabase itemDatabase;
	//public QuantumTek.QuantumInventory.QI_Inventory inventory;
	//public QuantumTek.QuantumInventory.QI_ItemData itemData;

	//public QuantumTek.QuantumInventory.QI_ItemDatabase itemDatabase;

	GameObject trigger_object;
	GameManager gameManager;
	InventoryManager inventoryManager;
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
		trigger_object = other.gameObject;

		if (interact == interactType.Trigger) {
			inventoryManager.Pickup(itemData.Name);
			Destroy(gameObject);
			//Debug.Log(other.gameObject.name);
		}
    }
    // Start is called before the first frame update
    void Start()
    {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		//itemDatabase = inventoryManager.Find("InventoryManager").GetComponent<InventoryManager>();
		inventoryManager = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
		itemDatabase = inventoryManager.itemDatabase;
		//inventory = inventoryManager.inventory;
		//itemDatabase = inventoryManager.itemDatabase;
    }

    // Update is called once per frame
    void Update()
    {
		if (interact == interactType.Interactive){
			if (Input.GetKeyDown(KeyCode.Return)) {
				Debug.Log(trigger_object.gameObject.name);
			}
		}
		//inventory = GameObject.Find("InventoryHandler").GetComponent<QuantumTek.QuantumInventory.QI_Inventory>();

    }
}
