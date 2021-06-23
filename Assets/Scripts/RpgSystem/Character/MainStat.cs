using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Main-Stat", menuName = "Stat/Main-Stat", order = 1)]
public class MainStat : ScriptableObject
{
    public SubStat[] SubStats = new SubStat[2];
    public int statLevel = 0;
    public int statMax = 100;

    /// <summary>
    /// The rate that stat increases improve between levels
    /// </summary>
    float statIncreaseRate = 1.054412f;

    public float statPercentIncreaseRate = 80f;

    public MainStat Copy()
    {
        MainStat copy = Instantiate(this);
        copy.name = name;
        return copy;
    }

    public void ApplyTo(Character ch)
    {
        // eg because I have a Wisdom of 10, add more to my MaxMana
        foreach (SubStat subStat in SubStats)
        {
            ch.percentSubStats[subStat.stat] = subStat.isPercentage;

            if (!subStat.isPercentage)
            {
                ch.subStatValues[subStat.stat] = subStat.GrowthRate * Mathf.Pow(statIncreaseRate, statLevel);
            }
            else if (subStat.isPercentage)
            {
                //TODO: Make math work
                if (statLevel != 0)
                {
                    float subStatTotal = subStat.maximum * Mathf.Tan((Mathf.PI / 4 / (float)statMax) * (float)statLevel);
                    ch.subStatValues[subStat.stat] = subStatTotal;
                }
            }
        }
    }
}
