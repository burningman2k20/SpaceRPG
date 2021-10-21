using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using QuantumTek.QuantumUI;

public class MainMenu : MonoBehaviour
{

    GameManager gameManager;
    QUI_OptionList optionList;

    public string sceneName = "Space";

    public void ExitGame(){
        Application.Quit();
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
