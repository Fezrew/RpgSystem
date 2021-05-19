using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sub-Stat", menuName = "Stat/Sub-Stat", order = 2)]
public class SubStat : ScriptableObject
{
    public int GrowthRate;
    public bool isPercentage = false;
    public int minimum;
    public int maximum;
    
    [HideInInspector]
    public MainStat mainStat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
