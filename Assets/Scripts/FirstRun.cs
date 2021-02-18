using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstRun : MonoBehaviour
{
    public string sceneName = "Space";
    // Start is called before the first frame update
    void Start()
    {

        SceneManager.LoadScene(sceneName);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
