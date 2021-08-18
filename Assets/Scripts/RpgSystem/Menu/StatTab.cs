using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPGSystem;

public class StatTab : MenuTab
{
    /// <summary>
    /// The item to be placed in the stat tabs(should just be a text)
    /// </summary>
    [Tooltip("The item to be placed in the stat tabs(should just be text)")]
    public GameObject StatUI;
    /// <summary>
    /// The buttons used to increase/decrease your player's stats
    /// </summary>
    public GameObject statButtonObject;
    public GameManager GameManager;

    /// <summary>
    /// The objects that hold all the created menu items
    /// These should all have an image as it's first child object, otherwise this won't work
    /// </summary>
    Transform[] ItemHolders;
    /// <summary>
    /// Holds an array of text items that display main stat names and the points put into them
    /// </summary>
    GameObject[] MainItems;
    /// <summary>
    /// Holds an array of text items that display sub stat names and their saved value
    /// </summary>
    GameObject[] SubItems;
    /// <summary>
    /// The space where buttons that can change stats are spawned
    /// </summary>
    Transform ButtonSpace;
    GameObject[] StatButtonList;

    /// <summary>
    /// Finds the tabs being used to hold items and fills them accordingly
    /// </summary>
    /// <param name="player">The character storing the information being displayed by the menu</param>
    public override void FillTab(Character player)
    {
        //Create an array to hold all of the main stats being displayed
        MainItems = new GameObject[player.CharacterStats.Length];

        //Create an array of buttons to match the size of MainItems
        StatButtonList = new GameObject[MainItems.Length];

        //Make another array to hold all the main/sub stat tabs
        Transform[] children = new Transform[transform.childCount];
        //The number of tabs holding menu items
        int itemHolderCount = 0;

        for (int i = 0; i < children.Length; ++i)
        {
            //Fill the array with the children's information
            children[i] = transform.GetChild(i);

            //Find every child that can hold the menu items
            if (children[i].gameObject.GetComponent<MenuItemHolder>() == true)
            {
                //Increment the item holder count so we know how large the array needs to be
                itemHolderCount++;
            }
            else
            {
                //Set the non-stat-holding object to take stat buttons
                ButtonSpace = children[i];

            }
        }

        //Resize the item holder array's size to be accurate
        ItemHolders = new Transform[itemHolderCount];
        //Reset the item holder count for re-use
        itemHolderCount = 0;

        for (int i = 0; i < children.Length; ++i)
        {
            //Find every child that can hold the menu items again
            if (children[i].GetComponent<MenuItemHolder>())
            {
                //Check we have a background image behind the text
                if (children[i].childCount != 0)
                {
                    //Save the transform of the images the items will appear in front of
                    ItemHolders[itemHolderCount] = children[i].GetChild(0);
                }
                itemHolderCount++;
            }
        }

        //Loop through every main stat the player has
        for (int i = 0; i < player.CharacterStats.Length; i++)
        {
            GameObject statUI = StatUI;

            //Change the text component to match the stat-specific information
            statUI.GetComponentInChildren<TextMeshProUGUI>().text =
                $"{player.CharacterStats[i].name}: " +
                $"\n{player.CharacterStats[i].statLevel}";

            //Write the information over the top of the first image we saved earlier
            MainItems[i] = Instantiate(statUI, ItemHolders[0]);

            if(ButtonSpace != null)
            {
                StatButtonList[i] = Instantiate(statButtonObject, ButtonSpace);
                StatButton statButton = StatButtonList[i].GetComponent<StatButton>();
                statButton.GM = GameManager;
                statButton.mainStat = GameManager.Player.CharacterStats[i];
            }
        }

        int currentStat = 0;
        StatInfo[] playerInfo = player.CharacterStatInfo;
        SubItems = new GameObject[playerInfo.Length];

        for (int i = 1; i < ItemHolders.Length; i++)
        {
            //Divide the displayed substats evenly among the Item Holders
            for (int e = 0; e < playerInfo.Length / (ItemHolders.Length - 1); e++)
            {
                GameObject statUI = StatUI;

                if (playerInfo[currentStat].PercentStat == false)
                {
                    //Change the text component to match the stat-specific information
                    statUI.GetComponentInChildren<TextMeshProUGUI>().text =
                        $"{player.CharacterStatInfo[currentStat].StatName}: " +
                        $"\n{(int)player.CharacterStatInfo[currentStat].StatValue}";
                }
                else
                {
                    //Change the text component to match the stat-specific information
                    statUI.GetComponentInChildren<TextMeshProUGUI>().text =
                        $"{player.CharacterStatInfo[currentStat].StatName}: " +
                        $"\n{(int)player.CharacterStatInfo[currentStat].StatValue}%";
                }

                //Write the information over the top of the images we saved earlier evenly
                SubItems[currentStat] = Instantiate(statUI, ItemHolders[i]);

                currentStat++;
            }
        }
    }

    /// <summary>
    /// Updates the information displayed to fit any changes made
    /// </summary>
    /// <param name="player">The character storing the information being displayed by the menu</param>
    public override void UpdateTab(Character player)
    {
        //Update the displayed main-stat information
        for (int i = 0; i < MainItems.Length; i++)
        {
            MainItems[i].GetComponentInChildren<TextMeshProUGUI>().text =
                $"{player.CharacterStats[i].name}: " +
                $"\n{player.CharacterStats[i].statLevel}";
        }

        //Update the displayed sub-stat information
        for (int i = 0; i < SubItems.Length; i++)
        {
            if (player.CharacterStatInfo[i].PercentStat == false)
            {
                //Change the text component to match the stat-specific information
                SubItems[i].GetComponentInChildren<TextMeshProUGUI>().text =
                    $"{player.CharacterStatInfo[i].StatName}: " +
                    $"\n{(int)player.CharacterStatInfo[i].StatValue}";
            }
            else
            {
                //Change the text component to match the stat-specific information
                SubItems[i].GetComponentInChildren<TextMeshProUGUI>().text =
                    $"{player.CharacterStatInfo[i].StatName}: " +
                    $"\n{(int)player.CharacterStatInfo[i].StatValue}%";
            }
        }
    }
}
