using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	public GameManager gameManager;

	void Awake(){

		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
