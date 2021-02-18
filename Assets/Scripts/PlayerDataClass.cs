using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Text;
using static GameDataTypes;

[Serializable]
public class playerData{
	
	[SerializeField]
	public GameObject myGameObject;
	[SerializeField]
	public Vector3 _position;

	[SerializeField]
	public Quaternion _rotation;

	[SerializeField]
	public Vector3 _scale;

	[SerializeField]
	public string _name;

	[SerializeField]
	public string _tag;

	[SerializeField]
	public string _groundPrefabName;

	[SerializeField]
	public string _spacePrefabName;

	[SerializeField]
	public locationType _location;

	[SerializeField]
	public string sceneName;

}
public class PlayerDataClass{

 	// [SerializeField]
	// public Vector3 _position;

	// [SerializeField]
	// public Quaternion _rotation;

	// [SerializeField]
	// public Vector3 _scale;

	// [SerializeField]
	// public string _name;

	// [SerializeField]
	// public string _tag;

	// [SerializeField]
	// public string _groundPrefabName;

	// [SerializeField]
	// public string _spacePrefabName;

	// [SerializeField]
	// public locationType _location;

	// [SerializeField]
	// public string sceneName;

	public playerData data = new playerData();
	public static string jsonSavePath;
	public PlayerDataClass() { 

	}

	public void Save(){

	}
	public void Load(){

	}
//}


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.IO;

// public class playerData{
//     public double points;        
//     public string name;
//     public Vector3 position;
//     public string selectedObject;

//     public Vector3 camPos;
// }
// public class PlayerStats {

//     public playerData data = new playerData();
//     public static string jsonSavePath;
//     public PlayerStats(string name) { data.name = name; }

//     public void setPosition(Vector3 position) { data.position = position; }

//     public Vector3 getPosition() { return data.position; }

//     public void setName(string name) { data.name = name; }
//     public string getName() { return data.name; }

//     public void setObject(GameObject _object){ data.selectedObject = _object.name; }
//     // public GameObject getObject() { 
//     //     return this.selectedObject; 
//     // }
public void LoadData() {
     Debug.Log(Application.persistentDataPath);
     jsonSavePath = Application.persistentDataPath + "/saveload.json";
     playerData gameSaving = JsonUtility.FromJson<playerData>( File.ReadAllText( jsonSavePath ) );
     Debug.Log("LoadData() called");
 
     //data.position = gameSaving.serializedPosition;
     
 
      //SceneManager.LoadScene( gameSaving.sceneName );
 
      //For testing purposes
      //Debug.Log( transform.position );
      //Debug.Log( transform.rotation );
      //Debug.Log( gameSaving.sceneName );
      //GameSaving.loaded = true;
 }

 public void SaveData() {
     Debug.Log(Application.persistentDataPath);
     Debug.Log("SaveData() called");
    //References
    //Scene scene = SceneManager.GetActiveScene();
     //playerData gameSaving = new playerData();
     //Scene Name
     //data.sceneName = scene.name;
 
     //Position
     //gameSaving.serializedPosition = player.transform.position;
    // GameSaving.position = gameSaving.serializedPosition;
 
     //Rotation
     //gameSaving.serializedRotation = player.transform.rotation;
     //GameSaving.rotation = gameSaving.serializedRotation;
     jsonSavePath= Application.persistentDataPath + "/saveload.json";
     string jsonData = JsonUtility.ToJson( data , true );
     File.WriteAllText( jsonSavePath , jsonData );
 
 }
 }