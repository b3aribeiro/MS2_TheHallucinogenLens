using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
   string sceneName;
   
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        
        sceneName = currentScene.name;
        
    }

    //for menu interface use

    public void StartGame()
    {
      SceneManager.LoadScene("Level_1");
    }

    public void ReturnMenu()
    {
      Cursor.lockState = CursorLockMode.None;
      SceneManager.LoadScene("MainMenu");
    }

     public void QuitGame()
    {
        Application.Quit();
    }

    //for in game coding
     private void OnTriggerEnter(Collider other) {
       if(other.gameObject.CompareTag("Player"))
       { 
        //+ pegou cristais +sceneName = "NOME"
        SceneManager.LoadScene("Level_3");
       }
   }

}
