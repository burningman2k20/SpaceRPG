using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameDataTypes;

public class SpawnManager : MonoBehaviour
{
    GameManager gameManager;
    PrefabManager prefabManager;
    public GameObject[] SpawnPoints;
    public string spawnName;
    public string sceneName;
    int spawnIndex = -1;

    GameObject player;

     public int FindSpawnByName(string _name)
    {
		refreshSpawnPoints();
        for (int index = 0; index < SpawnPoints.Length; index++)
        {
            if (SpawnPoints[index].name == _name) return index;
        }
        return -1;
    }

    public GameObject findPlayer(){
    spawnIndex = FindSpawnByName(spawnName);
	GameObject[] found = GameObject.FindGameObjectsWithTag("Player");
	foreach ( GameObject tmp in found){
		if (tmp.GetComponent<ShipControls>() != null){
			if (tmp.GetComponent<ShipControls>().playerControl) return tmp;

		} else if (tmp.GetComponent<SimpleTankController>() != null){
			if (tmp.GetComponent<SimpleTankController>().playerControl) return tmp;
		}
	}
    switch (gameManager.playerLocation){
        case locationType.Space:
        // player = Instantiate(spacePrefab, SpawnPoints[spawnIndex].transform.position - hoverPlayer, SpawnPoints[spawnIndex].transform.rotation);
        // shipControls = player.GetComponent<ShipControls>();
        // shipControls.hoverHeight = hoverHeight;
        return player;
        //break;
        case locationType.Air:
        //player =  Instantiate(spacePrefab, SpawnPoints[spawnIndex].transform.position - hoverPlayer, SpawnPoints[spawnIndex].transform.rotation);
        return player;

        //break;
        case locationType.Ground:
       // player =  Instantiate(groundPrefab, SpawnPoints[spawnIndex].transform.position - besideShip, SpawnPoints[spawnIndex].transform.rotation);
        return player;
        //break;
    }
	return null;
}

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        prefabManager = GameObject.Find("PrefabManager").GetComponent<PrefabManager>();

    }

void refreshSpawnPoints(){
	SpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
}
    // Update is called once per frame
    void Update()
    {

    }

	void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
