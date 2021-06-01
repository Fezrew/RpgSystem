using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StatTab : MenuTab
{
    /// <summary>
    /// The item to be placed in the main stat tab
    /// </summary>
    public GameObject MainStatUI;
    /// <summary>
    /// The object that holds all the created items
    /// </summary>
    public Transform ItemHolder;

    public override void FillTab()
    {
        if (player)
        {
            for (int i = 0; i < player.CharacterStats.Length; i++)
            {
                GameObject statUI = MainStatUI;
                statUI.GetComponentInChildren<TextMeshProUGUI>().text = $"{player.CharacterStats[i].name}: {player.CharacterStats[i].statLevel}";
                Instantiate(statUI, ItemHolder);
            }
        }
    }
}
