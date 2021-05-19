using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Character", order = 1)]
public class Character : ScriptableObject
{
    public MainStat[] CharacterStats;
    public int CharacterLevel = 1;
    public int SkillPointsPerLevel = 1;
    int totalSkillPoints;
    int spentSkillPoints = 0;
    int availableSkillPoints;

    // Start is called before the first frame update
    void Start()
    {
        totalSkillPoints = SkillPointsPerLevel * CharacterLevel;
        availableSkillPoints = totalSkillPoints - spentSkillPoints;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
