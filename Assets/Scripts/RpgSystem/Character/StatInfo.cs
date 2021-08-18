using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGSystem
{
    [System.Serializable]
    public struct StatInfo
    {
        public string StatName;
        public float StatValue;

        [HideInInspector]
        public bool PercentStat;

        public void CreateStatInfo(string name, float value, bool isPercentStat)
        {
            StatName = name;
            StatValue = value;
            PercentStat = isPercentStat;
        }
    }
}
