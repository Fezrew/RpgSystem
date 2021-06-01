using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Creates a new character and starts a new game scene
    virtual public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    //Finds an already existing character
    virtual public void LoadGame()
    {

    }

    //Closes the application
    public void Quit()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
