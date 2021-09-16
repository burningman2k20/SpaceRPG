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
	GameManager gameManager;
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
    }
    // Start is called before the first frame update
    void Start()
    {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
