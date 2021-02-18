using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Text;

[Serializable]
public class PlayerStats // : MonoBehaviour
{
public GameObject groundPrefab;
public GameObject spacePrefab;
public PlayerDataClass playerData = new PlayerDataClass();
public string name;
public string score;

    // Start is called before the first frame update
    PlayerStats()
   // void Start()
    {
        
	    //playerData.Load();
        Debug.Log("Created player stats");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
