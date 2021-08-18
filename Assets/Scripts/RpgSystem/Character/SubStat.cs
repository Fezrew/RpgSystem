using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGSystem
{
    [CreateAssetMenu(fileName = "Sub-Stat", menuName = "Stat/Sub-Stat", order = 2)]
    public class SubStat : ScriptableObject
    {
        public Character.subStat stat;
        public int GrowthRate;
        public bool isPercentage = false;
        public int maximum;
    }
}