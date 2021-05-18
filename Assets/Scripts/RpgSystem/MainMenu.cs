using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Scene GameScene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Creates a new character and starts a new game scene
    public void NewGame()
    {
        //SceneManager.LoadScene(GameScene);
    }

    //Finds an already existing character
    public void LoadGame()
    {

    }

    //Returns to the main menu scene
    public void ToMain()
    {

    }

    //Closes the application
    public void Quit()
    {
        Application.Quit();
    }
}
