using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPGSystem
{
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
        public int currentHealth;
        public int Damage;

        /// <summary>
        /// Are you in melee range?
        /// </summary>
        bool canMelee = false;
        /// <summary>
        /// Decides at which range you are capable of meleeing an enemy at
        /// </summary>
        public float MeleeRange;
        float RangedRange;
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

        public enum subStat
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

        public Dictionary<subStat, float> subStatValues = new Dictionary<subStat, float>();
        public Dictionary<subStat, bool> percentSubStats = new Dictionary<subStat, bool>();
        public Dictionary<subStat, string> subStatNames = new Dictionary<subStat, string>()
    {
        { subStat.MaxHealth, "Maximum Health" },
        { subStat.PhysResist, "Physical Damage Resist" },
        { subStat.MaxMana, "Maximum Mana" },
        { subStat.MagResist, "Magical Damage Resistance" },
        { subStat.Block, "Block Rating" },
        { subStat.DamReduce, "Damage Reduction" },
        { subStat.Evade, "Evasion Rating" },
        { subStat.CritReduce, "Enemy Crit Chance Reduction" },
        { subStat.MeleeAcc, "Melee Accuracy Rating" },
        { subStat.MeleeCrit, "Melee Crit Damage" },
        { subStat.RangedAcc, "Ranged Accuracy Rating" },
        { subStat.RangedCrit, "Ranged Crit Damage" },
        { subStat.MagAcc, "Magical Accuracy Rating" },
        { subStat.MagDot, "Magic Damage-Over-Time" },
        { subStat.ItemDrop, "Item Drop Chance" },
        { subStat.CritChance, "Critical Strike Chance" }
    };

        /// <summary>
        /// Returns the total value of the substat decided by the level of the mainstat
        /// </summary>
        /// <param name="stat">The substat that you want the value of</param>
        /// <returns>The value of the subastat you called in the function</returns>
        public float GetSubStatValue(subStat stat)
        {
            return subStatValues.ContainsKey(stat) ? subStatValues[stat] : 0;
        }
        /// <summary>
        /// Returns the name of a substat
        /// </summary>
        /// <param name="stat">The substat that you want the name of</param>
        /// <returns>The name of the stat you called in the function</returns>
        public string GetSubStatName(subStat stat)
        {
            return subStatNames.ContainsKey(stat) ? subStatNames[stat] : "";
        }

        /// <summary>
        /// Returns the value type of the stat in question
        /// </summary>
        /// <param name="stat">The substat that may be a percentage</param>
        /// <returns>The information of whether the stat is a percentage or not</returns>
        public bool GetPercentStat(subStat stat)
        {
            return percentSubStats.ContainsKey(stat) ? percentSubStats[stat] : false;
        }

        // Start is called before the first frame update
        void Awake()
        {
            CharacterPosition = gameObject.transform;

            for (int i = 0; i < CharacterStats.Length; i++)
            {
                //Create a copy of all MainStats to avoid altering the scriptable objects in Assets
                CharacterStats[i] = CharacterStats[i].Copy();
            }

            UpdateCharacter();

            currentHealth = (int)GetSubStatValue(subStat.MaxHealth);
            RangedRange = MeleeRange * 5;
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
            //Melee
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, MeleeRange, CharacterMask))
            {
                Character target = hit.collider.gameObject.GetComponent<Character>();
                float hitDamage = 0;

                Debug.Log(gameObject.name + " melee attacked " + target.name + " and...");

                //Does the attack hit?
                if (Random.Range(0, 100) <= GetSubStatValue(subStat.MeleeAcc) / target.GetSubStatValue(subStat.Evade) * 100)
                {
                    //Does the attack get blocked?
                    if (Random.Range(0, 100) <= GetSubStatValue(subStat.MeleeAcc) / target.GetSubStatValue(subStat.Block) * 100)
                    {
                        //Did the attack crit
                        if (Random.Range(0, 100) <= GetSubStatValue(subStat.CritChance) - target.GetSubStatValue(subStat.CritReduce))
                        {
                            Debug.Log($"The attack critically hit!");

                            hitDamage = Damage + GetSubStatValue(subStat.MeleeCrit) - ((Damage + GetSubStatValue(subStat.MeleeCrit)) * (target.GetSubStatValue(subStat.PhysResist) / 100));
                        }
                        //No
                        else
                        {
                            hitDamage = Damage - (Damage * (target.GetSubStatValue(subStat.PhysResist) / 100));
                        }
                    }
                    //If it was blocked
                    else
                    {
                        Debug.Log($"{target.name} blocked the attack!");

                        //Did the attack crit
                        if (Random.Range(0, 100) <= GetSubStatValue(subStat.CritChance) - target.GetSubStatValue(subStat.CritReduce))
                        {
                            Debug.Log($"The attack critically hit!");

                            hitDamage = (Damage + GetSubStatValue(subStat.MeleeCrit) - ((Damage + GetSubStatValue(subStat.MeleeCrit)) *
                                (target.GetSubStatValue(subStat.PhysResist) / 100)) - target.GetSubStatValue(subStat.DamReduce));
                        }
                        //No
                        else
                        {
                            hitDamage = (Damage - (Damage * (target.GetSubStatValue(subStat.PhysResist) / 100)) - target.GetSubStatValue(subStat.DamReduce));
                        }
                    }
                }
                //If it was dodged
                else
                {
                    Debug.Log($"{target.name} dodged!");
                }

                if (hitDamage >= 0)
                {
                    target.currentHealth -= (int)hitDamage;
                }
                hitDamage = hitDamage > 0 ? hitDamage : 0;
                Debug.Log($"The attack dealt {hitDamage} damage!");
                Debug.Log($"{target} has {target.currentHealth} health left!");
            }
            //Ranged
            else if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, RangedRange, CharacterMask))
            {
                Character target = hit.collider.gameObject.GetComponent<Character>();
                float hitDamage = 0;

                Debug.Log(gameObject.name + " ranged attacked " + target.name + " and...");

                //Does the attack hit?
                if (Random.Range(0, 100) <= GetSubStatValue(subStat.RangedAcc) / target.GetSubStatValue(subStat.Evade) * 100)
                {
                    //Does the attack get blocked?
                    if (Random.Range(0, 100) <= GetSubStatValue(subStat.RangedAcc) / target.GetSubStatValue(subStat.Block) * 100)
                    {
                        //Did the attack crit
                        if (Random.Range(0, 100) <= GetSubStatValue(subStat.CritChance) - target.GetSubStatValue(subStat.CritReduce))
                        {
                            Debug.Log($"The attack critically hit!");

                            hitDamage = Damage + GetSubStatValue(subStat.RangedCrit) - ((Damage + GetSubStatValue(subStat.RangedCrit)) * (target.GetSubStatValue(subStat.PhysResist) / 100));
                        }
                        //No
                        else
                        {
                            hitDamage = Damage - (Damage * (target.GetSubStatValue(subStat.PhysResist) / 100));
                        }
                    }
                    //If it was blocked
                    else
                    {
                        Debug.Log($"{target.name} blocked the attack!");

                        //Did the attack crit
                        if (Random.Range(0, 100) <= GetSubStatValue(subStat.CritChance) - target.GetSubStatValue(subStat.CritReduce))
                        {
                            Debug.Log($"The attack critically hit!");

                            hitDamage = (Damage + GetSubStatValue(subStat.RangedCrit) - ((Damage + GetSubStatValue(subStat.RangedCrit)) *
                                (target.GetSubStatValue(subStat.PhysResist) / 100)) - target.GetSubStatValue(subStat.DamReduce));
                        }
                        //No
                        else
                        {
                            hitDamage = (Damage - (Damage * (target.GetSubStatValue(subStat.PhysResist) / 100)) - target.GetSubStatValue(subStat.DamReduce));
                        }
                    }
                }
                //If it was dodged
                else
                {
                    Debug.Log($"{target.name} dodged!");
                }

                if (hitDamage >= 0)
                {
                    target.currentHealth -= (int)hitDamage;
                }

                hitDamage = hitDamage > 0 ? hitDamage : 0;
                Debug.Log($"The attack dealt {hitDamage} damage!");
                Debug.Log($"{target} has {target.currentHealth} health left!");
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

        /// <summary>
        /// Updates all the points put into main stats and the resulting sub stat values that change from it
        /// </summary>
        public void UpdateStats()
        {
            CharacterStatInfo = new StatInfo[subStatNames.Count];

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
                charInfo.CreateStatInfo(GetSubStatName((subStat)i), GetSubStatValue((subStat)i), GetPercentStat((subStat)i));
                CharacterStatInfo[i] = charInfo;
            }
        }
    }
}