using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabManager : MonoBehaviour
{
    public enum PrefabsType {
        Ground, Space
    }
    [Header("Player Prefabs")]
    [SerializeField] public GameObject[] playerPrefabs;
	[SerializeField] public GameObject currentPrefab;
    [SerializeField] public GameObject groundPrefab;
    [SerializeField] public GameObject spacePrefab;
    // Start is called before the first frame update
    void Start()
    {

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
