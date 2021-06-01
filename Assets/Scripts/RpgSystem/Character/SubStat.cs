using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sub-Stat", menuName = "Stat/Sub-Stat", order = 2)]
public class SubStat : ScriptableObject
{
    public Character.Stat stat;
    public int GrowthRate;
    public bool isPercentage = false;
    public int maximum;
}
