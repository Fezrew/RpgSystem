using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct StatInfo
{
    [SerializeField]
    string statName;

    [SerializeField]
    float statValue;

    public void CreateStatInfo(string name, float value)
    {
        statName = name;
        statValue = value;
    }
}
