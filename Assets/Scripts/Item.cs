using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameDataTypes;

public class Item : MonoBehaviour
{
    public itemType item;
	public interactType interact;
	public QuantumTek.QuantumInventory.QI_Inventory inventory;
	public QuantumTek.QuantumInventory.QI_ItemDatabase itemDatabase;

	GameObject trigger_object;
	GameManager gameManager;
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
		trigger_object = other.gameObject;

		if (interact == interactType.Trigger) {
			Debug.Log(other.gameObject.name);
		}
    }
    // Start is called before the first frame update
    void Start()
    {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		inventory = gameManager.inventory;
		itemDatabase = gameManager.itemDatabase;
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
