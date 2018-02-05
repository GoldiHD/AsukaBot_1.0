namespace AsukaBot_1._0.Module.RPG.Logic
{
    public class Stats
    {

        private int SpendAbleStatPoints = 10;
        private Power MyPower;
        private Magic MyMagic;
        private Dexterity MyDexterity;
        private Intellegenc MyIntellegenc;
        private Vitallity MyVitallity;
        private Luck MyLuck;

        public Stats(Power myPower, Magic myMagic, Dexterity myDexterity, Intellegenc myIntellegenc, Vitallity myVitallity, Luck myLuck)
        {
            MyPower = myPower;
            MyMagic = myMagic;
            MyDexterity = myDexterity;
            MyIntellegenc = myIntellegenc;
            MyVitallity = myVitallity;
            MyLuck = myLuck;
        }

        public Power GetPower()
        {
            return MyPower;
        }

        public Magic GetMagic()
        {
            return MyMagic;
        }

        public Dexterity GetDexterity()
        {
            return MyDexterity;
        }

        public Intellegenc GetIntellegence()
        {
            return MyIntellegenc;
        }

        public Vitallity GetVitallity()
        {
            return MyVitallity;
        }

        public Luck GetLuck()
        {
            return MyLuck;
        }

        public int GetStatPoints()
        {
            return SpendAbleStatPoints;
        }

        public void SetStatsPoints(int newstatspoints)
        {
            SpendAbleStatPoints -= newstatspoints;
        }

        public void AddStatsPoints(int newstatspoints)
        {
            SpendAbleStatPoints += newstatspoints;
        }

    }

    public class Power
    {
        private int Lvl;
        private float DamageIncrease;


        public Power(int lvl)
        {
            Lvl = lvl;
            DamageIncrease = Lvl * 0.2f;
        }

        public int GetPowerLvl()
        {
            return Lvl;
        }

        public float GetDamgeModify()
        {
            return DamageIncrease;
        }

        public bool IncreaseStatLvl(int newlvlpoints)
        {
            if (newlvlpoints > 0)
            {
                Lvl += newlvlpoints;
                DamageIncrease = Lvl * 0.2f;
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class Magic
    {
        private int Lvl;
        private int MaxMana;
        private int Mana;
        private float SpellStrenght;
        public Magic(int lvl)
        {
            Lvl = lvl;
            SpellStrenght = Lvl * 0.2f;
            MaxMana = Lvl * 10;
            Mana = MaxMana;
        }

        public int GetMagicLvl()
        {
            return Lvl;
        }

        public bool IncreaseStatLvl(int newlvlpoints)
        {
            if (newlvlpoints > 0)
            {
                Lvl += newlvlpoints;
                SpellStrenght = Lvl * 0.2f;
                MaxMana = Lvl * 10;
                Mana = MaxMana;
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetMana()
        {
            return Mana;
        }
        public int GetMaxMana()
        {
            return MaxMana;
        }
    }

    public class Dexterity
    {
        private float DodgeChance;
        private int Lvl;
        private float CritChance;
        public Dexterity(int lvl)
        {
            Lvl = lvl;
            DodgeChance = Lvl * 0.3f;
            CritChance = Lvl * 0.2f;
        }

        public int GetDexterityLvl()
        {
            return Lvl;
        }

        public bool IncreaseStatLvl(int newlvlpoints)
        {
            if (newlvlpoints > 0)
            {
                Lvl += newlvlpoints;
                DodgeChance = Lvl * 0.3f;
                CritChance = Lvl * 0.2f;
                return true;
            }
            else
            {
                return false;
            }
        }

        public float GetDodgeChance()
        {
            return DodgeChance;
        }
    }

    public class Intellegenc
    {
        private int Lvl;
        private int Tier; /* 10 points equals another tier so 10 = 1  20 = 2 and so on*/
        private float MagicResistance;
        public Intellegenc(int lvl)
        {
            Lvl = lvl;
            Tier = Lvl;
            MagicResistance = Lvl * 0.2f;
        }

        public bool IncreaseStatLvl(int newlvlpoints)
        {
            if (newlvlpoints > 0)
            {
                Lvl += newlvlpoints;
                Tier = Lvl;
                MagicResistance = Lvl * 0.2f;
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetIntellegencLvl()
        {
            return Lvl;
        }
    }

    public class Vitallity
    {
        private int MaxHealth;
        private int Health;
        private int Lvl;
        public Vitallity(int lvl)
        {
            Lvl = lvl;
            MaxHealth = lvl * 10;
            Health = MaxHealth;
        }

        public int GetMyHealth()
        {
            return Health;
        }

        public bool IncreaseStatLvl(int newlvlpoints)
        {
            if (newlvlpoints > 0)
            {
                Lvl += newlvlpoints;
                MaxHealth = Lvl * 10;
                Health = MaxHealth;
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetMyMaxHealth()
        {
            return MaxHealth;
        }

        public void SetHealth(int newhealth)
        {
            if (MaxHealth < newhealth)
            {
                Health = MaxHealth;
            }
            else
            {
                Health = newhealth;
            }
        }

        public void GainFullHealth()
        {
            Health = MaxHealth;
        }

        public int GetVitallityLvl()
        {
            return Lvl;
        }
    }

    public class Luck
    {
        private float CritDamage;
        private int Lvl;
        private float LootQuality;

        public Luck(int lvl)
        {
            Lvl = lvl;
            CritDamage = Lvl * 0.2f;
            LootQuality = Lvl * 0.2f;
        }

        public bool IncreaseStatLvl(int newlvlpoints)
        {
            if (newlvlpoints > 0)
            {
                Lvl += newlvlpoints;
                CritDamage = Lvl * 0.2f;
                LootQuality = Lvl * 0.2f;
                return true;
            }
            else
            {
                return false;
            }
        }

        public int GetLuckLvl()
        {
            return Lvl;
        }
    }
}