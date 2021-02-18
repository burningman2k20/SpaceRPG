using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllers : MonoBehaviour
{
    //CharacterData charData;
    // Start is called before the first frame update
    void Start()
    {
        //charData = new CharacterData();

        // GameObject[] findPlayer = GameObject.FindGameObjectsWithTag("Controllers");
        // foreach (GameObject tmp in findPlayer)
        // {
        //     if (tmp) Destroy(tmp);
        //     //GameObject.Find("UIManager").GetComponent<SelectionUI>().FindSelectionObjects();
        // }

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
