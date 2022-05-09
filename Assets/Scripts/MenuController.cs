using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public string sceneName;

    // Start is called before the first frame update
    public void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
    }

    // Update is called once per frame
    public void InitiateBtn()
    {
        SceneManager.LoadScene("Intro");
    }

    public void SimulationBegins()
    {
        SceneManager.LoadScene("Game");
    }
    
    public void ReturnMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ExitBtn()
    {
        Application.Quit();
    }
}
