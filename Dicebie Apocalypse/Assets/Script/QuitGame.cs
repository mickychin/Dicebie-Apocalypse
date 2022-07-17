using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGame : MonoBehaviour
{
    public int count;
    public GameObject Quitting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Escape))
        {
            count = count + 1;
            Quitting.SetActive(true);
        }
        else
        {
            count = 1;
            Quitting.SetActive(false);
        }
        if(count > 400)
        {
            Debug.Log("QuitGame");
            SceneManager.LoadScene(0);
        }
    }
}
