using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public MainStat[] CharacterStats;
    public StatInfo[] CharacterStatInfo;
    public SkillTree CharacterSkillTree;
    public float Health;
    public int Damage;

    bool canMelee = false;
    Transform CharacterPosition;
    public float MeleeRange;
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

    public int[] statValues;

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

    public float GetStatValue(Stat stat)
    {
        return stats.ContainsKey(stat) ? stats[stat] : 0;
    }
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
            CharacterStats[i] = CharacterStats[i].Copy();
            if (i < statValues.Length)
            {
                CharacterStats[i].statLevel = statValues[i];
            }

            CharacterStats[i].ApplyTo(this);
        }
        StatTotalUpdate();
        UpdateInfo();
        Health = GetStatValue(Stat.MaxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        canMelee = Physics.CheckSphere(CharacterPosition.position, MeleeRange, CharacterMask);

        if (canMelee && Input.GetKeyDown(KeyCode.Q))
        {
            Attack();
        }
    }

    public void Attack()
    {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MeleeRange, CharacterMask))
        {
            hit.collider.gameObject.GetComponent<Character>().Health -= Damage;
            Debug.Log(gameObject.name + " attacked " + hit.collider.gameObject.name + " for " + Damage + " damage.\n" +
                hit.collider.gameObject.name + " has " + hit.collider.gameObject.GetComponent<Character>().Health + " health left.");
        }
    }

    public void StatTotalUpdate()
    {
        totalSkillPoints = SkillPointsPerLevel * CharacterLevel;
        availableSkillPoints = totalSkillPoints - spentSkillPoints;
    }

    public void UpdateInfo()
    {
        CharacterStatInfo = new StatInfo[statNames.Count];

        for (int i = 0; i < CharacterStatInfo.Length; i++)
        {
            StatInfo charInfo = new StatInfo();
            charInfo.CreateStatInfo(GetStatName((Stat)i), GetStatValue((Stat)i));
            CharacterStatInfo[i] = charInfo;
        }
    }
}


//Doesn't work but I want it for reference pile:

//stats.TryGetValue(Stat.Block, out GetStatValue());