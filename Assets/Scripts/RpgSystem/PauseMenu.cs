using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject Pause;
    public bool paused;

    // Start is called before the first frame update
    void Start()
    {
        Pause.SetActive(false);
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause.SetActive(!Pause.activeSelf);
        }
        if(Pause.activeSelf == true)
        {
            paused = true;
        }
        else
        {
            paused = false;
        }
    }

    //Un-pause the game
    public void Continue()
    {
        Pause.SetActive(false);
    }

    //Returns to the main menu scene
    public void ToMain()
    {
        SceneManager.LoadScene(0);
    }

    //Closes the application
    public void Quit()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
