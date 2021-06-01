using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    /// <summary>
    /// All the characters main stats
    /// </summary>
    public MainStat[] CharacterStats;
    /// <summary>
    /// A list of numbers that change the level of the main stats and update the substats
    /// </summary>
    public int[] mainStatValues;
    /// <summary>
    /// Holds all of the substat values that are calculated at run-time
    /// </summary>
    public StatInfo[] CharacterStatInfo;
    /// <summary>
    /// Currently useless
    /// </summary>
    public SkillTree CharacterSkillTree;
    public float Health;
    public int Damage;

    /// <summary>
    /// Are you in melee range?
    /// </summary>
    bool canMelee = false;
    /// <summary>
    /// Decides at which range you are capable of meleeing an enemy at
    /// </summary>
    public float MeleeRange;
    /// <summary>
    /// Your position and the centre point of the radius check to see if enemies are in melee range
    /// </summary>
    Transform CharacterPosition;
    /// <summary>
    /// The layer that decides if you are a character and therefore target-able
    /// </summary>
    public LayerMask CharacterMask;

    /// <summary>
    /// The character's current level
    /// </summary>
    public int CharacterLevel = 1;
    /// <summary>
    /// The number of skill points gained per level
    /// </summary>
    public int SkillPointsPerLevel = 1;
    /// <summary>
    /// The number of skill points you the character currently has
    /// </summary>
    int totalSkillPoints;
    /// <summary>
    /// The number of skill points that have been put into a stat
    /// </summary>
    int spentSkillPoints = 0;
    /// <summary>
    /// The number of skill points you can currently spend
    /// </summary>
    int availableSkillPoints;


    public enum Stat
    {
        MaxHealth,
        PhysResist,
        MaxMana,
        MagResist,
        Block,
        DamReduce,
        Evade,
        CritReduce,
        MeleeAcc,
        MeleeCrit,
        RangedAcc,
        RangedCrit,
        MagAcc,
        MagDot,
        ItemDrop,
        CritChance
    }

    public Dictionary<Stat, float> stats = new Dictionary<Stat, float>();
    public Dictionary<Stat, string> statNames = new Dictionary<Stat, string>()
    {
        { Stat.MaxHealth, "Maximum Health" },
        { Stat.PhysResist, "Physical Damage Resist" },
        { Stat.MaxMana, "Maximum Mana" },
        { Stat.MagResist, "Magical Damage Resistance" },
        { Stat.Block, "Block Rating" },
        { Stat.DamReduce, "Damage Reduction" },
        { Stat.Evade, "Evasion Rating" },
        { Stat.CritReduce, "Enemy Crit Chance Reduction" },
        { Stat.MeleeAcc, "Melee Accuracy Rating" },
        { Stat.MeleeCrit, "Melee Crit Damage" },
        { Stat.RangedAcc, "Ranged Accuracy Rating" },
        { Stat.RangedCrit, "Ranged Crit Damage" },
        { Stat.MagAcc, "Magical Accuracy Rating" },
        { Stat.MagDot, "Magic Damage-Over-Time" },
        { Stat.ItemDrop, "Item Drop Chance" },
        { Stat.CritChance, "Critical Strike Chance" }
    };

    /// <summary>
    /// Returns the total value of the substat decided by the level of the mainstat
    /// </summary>
    /// <param name="stat"></param>
    /// <returns>The substat you wish to know the value of</returns>
    public float GetStatValue(Stat stat)
    {
        return stats.ContainsKey(stat) ? stats[stat] : 0;
    }
    /// <summary>
    /// Returns the name of a substat
    /// </summary>
    /// <param name="stat"></param>
    /// <returns>The substat you wish to know the value of</returns>
    public string GetStatName(Stat stat)
    {
        return statNames.ContainsKey(stat) ? statNames[stat] : "";
    }

    // Start is called before the first frame update
    void Start()
    {
        CharacterPosition = gameObject.transform;

        for (int i = 0; i < CharacterStats.Length; i++)
        {
            //Create a copy of all MainStats to avoid altering the scriptable objects in Assets
            CharacterStats[i] = CharacterStats[i].Copy();
        }

        //Update the available stat points
        StatTotalUpdate();

        //Update all stat totals
        UpdateStats();
        Health = GetStatValue(Stat.MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        //Is there an attack-able object in your range?
        canMelee = Physics.CheckSphere(CharacterPosition.position, MeleeRange, CharacterMask);

        //If you can melee and press the melee button, Attack
        if (canMelee && Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
    }

    public void UpdateCharacter()
    {
        StatTotalUpdate();
        UpdateStats();
    }

    public void Attack()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MeleeRange, CharacterMask))
        {
            //TODO: Apply damage through the correct formula
            hit.collider.gameObject.GetComponent<Character>().Health -= Damage;

            //Tell us who is hitting who for how much
            Debug.Log(gameObject.name + " attacked " + hit.collider.gameObject.name + " for " + Damage + " damage.\n" +
                hit.collider.gameObject.name + " has " + hit.collider.gameObject.GetComponent<Character>().Health + " health left.");
        }
    }

    /// <summary>
    /// Finds the amount of stats owned and used
    /// (e.g. I'm at level 40, but I have 20 points in VIT so I only have 20 points left)
    /// </summary>
    public void StatTotalUpdate()
    {
        totalSkillPoints = SkillPointsPerLevel * CharacterLevel;
        availableSkillPoints = totalSkillPoints - spentSkillPoints;
    }

    public void UpdateStats()
    {
        CharacterStatInfo = new StatInfo[statNames.Count];

        for (int i = 0; i < CharacterStats.Length; i++)
        {
            if (i < mainStatValues.Length)
            {
                CharacterStats[i].statLevel = mainStatValues[i];
            }

            CharacterStats[i].ApplyTo(this);
        }

        for (int i = 0; i < CharacterStatInfo.Length; i++)
        {
            StatInfo charInfo = new StatInfo();
            charInfo.CreateStatInfo(GetStatName((Stat)i), GetStatValue((Stat)i));
            CharacterStatInfo[i] = charInfo;
        }
    }
}