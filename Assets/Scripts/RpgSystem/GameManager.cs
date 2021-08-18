using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGSystem;

public class GameManager : MonoBehaviour
{
    public Character Player;
    public InGameMenu GameMenu;

    // Start is called before the first frame update
    void Start()
    {
        Player.UpdateCharacter();
    }

    // Update is called once per frame
    public void UpdateGame()
    {
        Player.UpdateCharacter();
        GameMenu.UpdateInfo();
    }
}
