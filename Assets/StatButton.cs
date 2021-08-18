using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPGSystem;

public class StatButton : MonoBehaviour
{
    /// <summary>
    /// The stat to be altered when pressing buttons
    /// </summary>
    [HideInInspector]
    public RPGSystem.MainStat mainStat;
    [HideInInspector]
    public GameManager GM;

    public void StatUp()
    {
        if (mainStat.statLevel < mainStat.statMax)
        {
            for (int i = 0; i < GM.Player.CharacterStats.Length; i++)
            {
                if (GM.Player.CharacterStats[i] == mainStat)
                {
                    GM.Player.mainStatValues[i]++;
                    GM.UpdateGame();
                }
            }
        }
    }

    public void StatDown()
    {
        if (mainStat.statLevel > mainStat.statMin)
        {
            for (int i = 0; i < GM.Player.CharacterStats.Length; i++)
            {
                if (GM.Player.CharacterStats[i] == mainStat)
                {
                    GM.Player.mainStatValues[i]--;
                    GM.UpdateGame();
                }
            }
        }
    }
}
