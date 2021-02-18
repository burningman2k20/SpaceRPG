


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
public class PlayerDataClass{

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

	public PlayerDataClass(){

	}

	public void Save(){

	}
	public void Load(){

	}
}
