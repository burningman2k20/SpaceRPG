using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    GameManager gameManager;

    public string sceneName = "Space";
    public void StartGame()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.gameStarted = true;
        SceneManager.LoadScene(sceneName);
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
