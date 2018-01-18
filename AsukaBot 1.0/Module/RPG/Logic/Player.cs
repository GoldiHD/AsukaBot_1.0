﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AsukaBot_1._0.Module.RPG.Logic.Items;
using AsukaBot_1._0.Module.RPG.Logic.Questing;
using AsukaBot_1._0.Module.RPG.Logic.Enemy;
using System.Diagnostics;
using AsukaBot_1._0.Module.RPG.Logic.Classes;

namespace AsukaBot_1._0.Module.RPG.Logic
{
    public class Player
    {
        private ClassBase MyClass;
        private Random RNG = new Random();
        private QuestManager questManager;
        private PlayerStates MyPlayerState = PlayerStates.Rest;
        private string Username;
        private Inventory MyInventory = new Inventory();
        private Stats MyStats = new Stats(new Power(1), new Magic(1), new Dexterity(1), new Intellegenc(1), new Vitallity(1), new Luck(1));
        private int Level = 1;
        private int expCurrent = 0;
        private int NextLvlExp = 10;
        private AttackType MyAttackType = AttackType.Melee;
        private const int BareHandedDamge = 1;
        public WeaponsItem EquipedWeaponItem;
        private MagicAttacks EquipedMagicAttack;
        public ArmorItem[] EquippedArmor = new ArmorItem[4]; //1 helmet, 2 Chest, 3 Legs, 4 Hands
        private int StatPointsLvlCounter = 0;
        private int AC = 0;
        private bool HardcoreMode = false;
        private Stopwatch MineSW = new Stopwatch();
        private Stopwatch ChopSW = new Stopwatch();
        private Stopwatch ForageSW = new Stopwatch();

        public Player() { }

        public int Mine()
        {
            if (MineSW.IsRunning)
            {
                if (MineSW.ElapsedMilliseconds > 300000)
                {
                    MineSW.Reset();
                    return 0;
                }
                else
                {
                    return (int)MineSW.ElapsedMilliseconds;
                }
            }
            else
            {
                MineSW.Start();
                return 0;
            }
        }

        public int Chop()
        {
            if (ChopSW.IsRunning)
            {
                if (ChopSW.ElapsedMilliseconds > 300000)
                {
                    ChopSW.Reset();
                    return 0;
                }
                else
                {
                    return (int)ChopSW.ElapsedMilliseconds;
                }
            }
            else
            {
                ChopSW.Start();
                return 0;
            }
        }

        public int Forage()
        {
            if (ForageSW.IsRunning)
            {
                if(ForageSW.ElapsedMilliseconds > 300000)
                {
                    ForageSW.Reset();
                    return 0;
                }
                else
                {
                    return (int)ForageSW.ElapsedMilliseconds;
                }
            }
            else
            {
                ForageSW.Start();
                return 0;
            }
        }

        public void SetPlayerLevel(int lvl)
        {
            Level = lvl;
        }

        public void SetPlayerCurrentExp(int exp)
        {
            expCurrent = exp;
        }

        public void SetPlayerNextLevelXp(int exp)
        {
            NextLvlExp = exp;
        }

        public Player(string username)
        {
            Username = username;
            MyPlayerState = PlayerStates.Rest;
            MyAttackType = AttackType.Melee;
        }

        public bool GetHardcoreState()
        {
            return HardcoreMode;
        }

        public string GetGeneralStats()
        {
            string data = "Username: " + Username + "\n" + "" + MyStats.GetVitallity().GetMyHealth() + "/" + MyStats.GetVitallity().GetMyMaxHealth() + "\n" + "Armor: " + AC + "\n" + "Lvl: " + Level + "\n" + "exp: " + expCurrent + "/" + NextLvlExp + "\n" + "Extra stats points: " + MyStats.GetStatPoints();
            return data;
        }

        public string UseConsumable(string Item)
        {

            Consumable ItemUsed = (Consumable)MyInventory.GetinventoryItemByName(Item);
            return ItemUsed.GetEffect().UseAblility(this);
        }

        public int GetExpCurrent()
        {
            return expCurrent;
        }

        public int GetExpForNextLvl()
        {
            return NextLvlExp;
        }

        public int GetAC()
        {
            return AC;
        }


        private void Levelup()
        {
            Level++;
        }

        public bool AddXP(int newxp)
        {
            expCurrent += newxp;
            if (NextLvlExp <= expCurrent)
            {
                expCurrent -= NextLvlExp;
                Levelup();
                NextLvlExp = (int)((NextLvlExp * 2) * 1.2f);
                StatPointsLvlCounter++;
                if (StatPointsLvlCounter == 5)
                {
                    MyStats.AddStatsPoints(5);
                    StatPointsLvlCounter = 0;
                }
                else
                {
                    MyStats.AddStatsPoints(2);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public void SetUsername(string username)
        {
            Username = username;
        }

        public QuestManager GetQuestManager()
        {
            return questManager;
        }

        public void TakeDamage(int damage)
        {
            MyStats.GetVitallity().SetHealth(damage);
        }

        public void SetQuestManager(QuestManager quest)
        {
            questManager = quest;
        }

        public PlayerStates GetPlayerState()
        {
            return MyPlayerState;
        }

        public void SetPlayerStates(PlayerStates NewValue)
        {
            MyPlayerState = NewValue;
        }

        public Stats GetStats()
        {
            return MyStats;
        }

        public void SetStats(Stats NewStats)
        {
            MyStats = NewStats;
        }

        public Inventory GetInventory()
        {
            return MyInventory;
        }

        public string GetPlayername()
        {
            return Username;
        }

        public int GetPlayerLvl()
        {
            return Level;
        }

        public string CounterAttack()
        {
            int DamgeAfterAc;
            int WeaponDamge;
            float DamgeModifyer;
            WeaponDamge = questManager.GetStoryMaker().GetEnemy().GetDamge();
            DamgeModifyer = questManager.GetStoryMaker().GetEnemy().GetCritchance();
            if (RNG.Next(1, 101) <= (DamgeModifyer * 100))
            {
                DamgeAfterAc = (int)((WeaponDamge * 2) - (AC / (Level * 1.5f)));
            }
            else
            {
                DamgeAfterAc = (int)(WeaponDamge - (AC / (Level * 1.5f)));
            }

            if (DamgeAfterAc < 0)
            {
                DamgeAfterAc = 0;
            }
            questManager.GetStoryMaker().GetEnemy().Attack(this, DamgeAfterAc);
            return DamgeAfterAc.ToString();
        }

        public string Attack()
        {
            int DamgeAfterAc = 0;
            int weaponDamge;
            float DamgeModifyer;
            switch (MyAttackType)
            {
                case AttackType.Melee:
                    if (EquipedWeaponItem == null)
                    {
                        weaponDamge = BareHandedDamge;
                    }
                    else
                    {
                        weaponDamge = EquipedWeaponItem.GetDamge();
                    }

                    DamgeModifyer = MyStats.GetPower().GetDamgeModify();

                    DamgeAfterAc = (int)((weaponDamge + (weaponDamge * (1 + DamgeModifyer))) - questManager.GetStoryMaker().GetEnemy().DamgeModify());
                    Console.WriteLine("AC: " + questManager.GetStoryMaker().GetEnemy().DamgeModify());
                    Console.WriteLine("Damage: " + weaponDamge + (weaponDamge * (1 + DamgeModifyer)));
                    if (DamgeAfterAc <= 0)
                    {
                        DamgeAfterAc = 0;
                    }
                    questManager.GetStoryMaker().GetEnemy().DealDamge(DamgeAfterAc);


                    break;

                case AttackType.Magic:
                    if (EquipedMagicAttack == null)
                    {
                        weaponDamge = BareHandedDamge;
                    }
                    break;
            }

            if (DamgeAfterAc == 0)
            {
                return "missed";
            }
            else
            {
                return DamgeAfterAc.ToString();
            }
        }

        public void AssignWeapon(object TheWeapon)
        {
            if (TheWeapon is WeaponsItem)
            {
                EquipedWeaponItem = (WeaponsItem)TheWeapon;
            }
            else if (TheWeapon is MagicAttacks)
            {
                EquipedMagicAttack = (MagicAttacks)TheWeapon;
            }
            else
            {
                Console.WriteLine("Error");
            }
        }

        /// <summary>
        /// only used for loading data
        /// </summary>
        /// <param name="MoreAc"></param>
        public void AddAC(int MoreAc)
        {
            AC += MoreAc;
        }

        /// <summary>
        /// only used for loading data
        /// </summary>
        /// <param name="inputState"></param>
        public void SetHardcoreState(bool inputState)
        {
            HardcoreMode = inputState;
        }
        public void AssignArmor(object TheArmor)
        {
            ArmorItem Item = (ArmorItem)TheArmor;

            switch (Item.GetArmorPos())
            {
                case Armorpiece.Head:
                    if (EquippedArmor[0] != null)
                    {
                        AC -= EquippedArmor[0].GetAC();
                        EquippedArmor[0] = Item;
                        AC += Item.GetAC();
                    }
                    else
                    {
                        EquippedArmor[0] = Item;
                        AC += Item.GetAC();
                    }
                    break;

                case Armorpiece.Chest:
                    if (EquippedArmor[1] != null)
                    {
                        AC -= EquippedArmor[1].GetAC();
                        EquippedArmor[1] = Item;
                        AC += Item.GetAC();
                    }
                    else
                    {
                        EquippedArmor[1] = Item;
                        AC += Item.GetAC();
                    }
                    break;

                case Armorpiece.Legs:
                    if (EquippedArmor[2] != null)
                    {
                        AC -= EquippedArmor[2].GetAC();
                        EquippedArmor[2] = Item;
                        AC += Item.GetAC();
                    }
                    else
                    {
                        EquippedArmor[2] = Item;
                        AC += Item.GetAC();
                        Console.WriteLine(AC);
                    }
                    break;

                case Armorpiece.Hands:
                    if (EquippedArmor[3] != null)
                    {
                        AC -= EquippedArmor[3].GetAC();
                        EquippedArmor[3] = Item;
                        AC += Item.GetAC();
                    }
                    else
                    {
                        EquippedArmor[3] = Item;
                        AC += Item.GetAC();
                    }
                    break;

                default:
                    Console.WriteLine("Error");
                    break;
            }

        }

        public void ManageAttack()
        {
            switch (MyAttackType)
            {
                case AttackType.Melee:
                    MyAttackType = AttackType.Magic;
                    break;

                case AttackType.Magic:
                    MyAttackType = AttackType.Melee;
                    break;
            }
        }

    }

    public enum PlayerStates
    {
        Pvp, Dungeon, Rest, CollabBoss, Encounter, Campaign
    }

    public enum AttackType
    {
        Melee, Magic
    }
}

