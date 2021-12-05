using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class List : MonoBehaviour
{
	public GameObject list;
	public List<string> inventoryList = new List<string>();
	public QuantumTek.QuantumInventory.QI_Inventory inventory;
	public QuantumTek.QuantumInventory.QI_ItemDatabase itemDatabase;
	public Dictionary<string, QuantumTek.QuantumInventory.QI_ItemData> items = new Dictionary<string, QuantumTek.QuantumInventory.QI_ItemData>();

    // Start is called before the first frame update
    void Start()
    {
		list = GameObject.Find("listInventory");



    }

    // Update is called once per frame
    void Update()
    {

				foreach(QuantumTek.QuantumInventory.QI_ItemData entry in itemDatabase.Items)
		{

			if (inventory.GetStock(entry.name)>0){
				Debug.Log(entry);
				//list.GetComponent<Text>().text += itemDatabase.GetItem(entry.name).name + "\n";
				Debug.Log(entry.name + " -> " + inventory.GetStock(entry.name));
			}
				// do something with entry.Value or entry.Key
		}

    }

	void Awake(){

	}
}
