using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public MainStat[] CharacterStats;
    public SkillTree CharacterSkillTree;
    public int MaxHealth;
    public int Health;
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

    public float GetStat(Stat stat)
    {
        return stats.ContainsKey(stat) ? stats[stat] : 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        Health = MaxHealth;
        CharacterPosition = gameObject.transform;

        for (int i = 0; i < CharacterStats.Length; i++)
        {
            CharacterStats[i] = CharacterStats[i].Copy();
            CharacterStats[i].ApplyTo(this);
        }

        totalSkillPoints = SkillPointsPerLevel * CharacterLevel;
        availableSkillPoints = totalSkillPoints - spentSkillPoints;
    }

    // Update is called once per frame
    void Update()
    {
        canMelee = Physics.CheckSphere(CharacterPosition.position, MeleeRange, CharacterMask);

        if (canMelee && Input.GetKeyDown(KeyCode.Q))
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
    }

    public void Attack()
    {

    }
}
