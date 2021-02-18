using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameDataTypes;
using static GameManager;

public class CharacterData
{
    GameManager gameManger;
    SelectionUI selectionUI;

    // constructor
    public CharacterData()
    {
        gameManger = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        selectionUI = GameObject.Find("UIManager").GetComponent<SelectionUI>();

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
