using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGSystem;

public class InGameMenu : MonoBehaviour
{
    /// <summary>
    /// An array that holds all the different menus in the in-game menu
    /// </summary>
    public MenuTab[] tabs;
    public Character player;

    void Start()
    {
        //At run-time we want to find all the menus in the in-game menu for easier reference in other scripts and set only the first one to be active
        tabs = GetComponentsInChildren<MenuTab>();

        foreach (MenuTab tab in tabs)
        {
            tab.gameObject.SetActive(false);
        }

        if (tabs[0] != null)
            tabs[0].gameObject.SetActive(true);

        CreateInfo();
    }

    public void CreateInfo()
    {
        //Loop through all the menu tabs
        for (int i = 0; i < tabs.Length; i++)
        {
            if (tabs[i].isActiveAndEnabled)
            {
                //If the tab is active, fill it
                tabs[i].FillTab(player);
            }
        }
    }

    public void UpdateInfo()
    {
        //Loop through all the menu tabs
        for (int i = 0; i < tabs.Length; i++)
        {
            if (tabs[i].isActiveAndEnabled)
            {
                //If the tab is active, fill it
                tabs[i].UpdateTab(player);
            }
        }
    }
}
