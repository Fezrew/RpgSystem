using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Main-Stat", menuName = "Stat/Main-Stat", order = 1)]
public class MainStat : ScriptableObject
{
    public SubStat[] SubStats = new SubStat[2];
    public int statLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(SubStat stat in SubStats)
        {
            stat.mainStat = this;
        }
    }
}
