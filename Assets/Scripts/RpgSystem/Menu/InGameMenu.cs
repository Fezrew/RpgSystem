using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenu : MonoBehaviour
{
    /// <summary>
    /// An array that holds all the different menus in the in-game menu
    /// </summary>
    public MenuTab[] tabs;
    /// <summary>
    /// A reference to the player's character. Useful for further reference by child objects
    /// </summary>
    public Character player;

    void Awake()
    {
        //At run-time we want to find all the menus in the in-game menu for easier reference in other scripts and set only the first one to be active
        tabs = GetComponentsInChildren<MenuTab>();

        foreach (MenuTab tab in tabs)
        {
            tab.gameObject.SetActive(false);
        }

        if (tabs[0] != null)
            tabs[0].gameObject.SetActive(true);

        UpdateInfo();
    }

    public void UpdateInfo()
    {
        //Loop through all the menu tabs
        for (int i = 0; i < tabs.Length; i++)
        {
            if (tabs[i].isActiveAndEnabled)
            {
                //If the tab is active, fill it
                tabs[i].FillTab();
            }
        }
    }
}
