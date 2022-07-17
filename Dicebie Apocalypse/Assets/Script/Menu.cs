using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject Howtoplay;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        Howtoplay.SetActive(true);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Howtoplay.SetActive(false);
        }
    }
}
