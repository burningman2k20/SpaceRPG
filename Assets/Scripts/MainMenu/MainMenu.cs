using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QuantumTek.QuantumUI;
using TMPro;

public class MainMenu : MonoBehaviour
{

    GameManager gameManager;
    CharacterManager characterManager;
    QUI_OptionList optionList;

    public string sceneName = "Space";

    public void ExitGame(){
        Application.Quit();
    }

    public void SetCharacterName(){
        TMPro.TMP_InputField inputField = GameObject.Find("NameField").GetComponent<TMPro.TMP_InputField>();
        characterManager.characterData.PlayerCharaterName = inputField.text;
        Debug.Log(characterManager.characterData.PlayerCharaterName);
    }
    public void StartGame()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.gameStarted = true;
        SceneManager.LoadScene(sceneName);
    }

    public void CreateGame()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
        gameManager.gameStarted = true;
        SceneManager.LoadScene(sceneName);
    }

	public void LoadGame(){
		
	}

    public void GetOptionList(){
        optionList = GameObject.Find("OptionList").GetComponent<QUI_OptionList>();
        Debug.Log(string.Format("{0} selected option", optionList.option));
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
