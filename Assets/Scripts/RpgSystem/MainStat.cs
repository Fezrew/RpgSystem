using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Main-Stat", menuName = "Stat/Main-Stat", order = 1)]
public class MainStat : ScriptableObject
{
    public SubStat[] SubStats = new SubStat[2];
    public int statLevel = 0;

    /// <summary>
    /// The rate that stat increases grow
    /// </summary>
    float statIncreaseRate = 1.054412f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public MainStat Copy()
    {
        MainStat copy = Instantiate(this);
        copy.name = name;
        Debug.Log("Copied MainStats");
        return copy;
    }

    public void ApplyTo(Character ch)
    {
        // eg because I have a Wisdom of 10, add more to my MaxMana
        foreach (SubStat subStat in SubStats)
        {
            ch.stats[subStat.stat] = subStat.GrowthRate * Mathf.Pow(statIncreaseRate, statLevel);
        }
    }
}
