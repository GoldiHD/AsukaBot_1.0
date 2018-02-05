using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Module.RPG.Logic.Classes
{
    public class ClassBase
    {
        protected string Classname;
        protected Stats PlayerStats;

        public Stats GetStats()
        {
            return PlayerStats;
        }

        public string GetClassName()
        {
            return Classname;
        }

        public void SetStats(Stats input)
        {
            PlayerStats = input;
        }
    }

    public class ClassMage : ClassBase
    {
        public ClassMage()
        {
            Classname = "Mage";
        }
    }

    public class ClassRanger : ClassBase
    {
        public ClassRanger()
        {
            Classname = "Ranger";
        }
    }

    public class ClassRouge : ClassBase
    {
        public ClassRouge()
        {
            Classname = "Rouge";
        }
    }

    public class ClassCleric : ClassBase
    {
        public ClassCleric()
        {
            Classname = "Cleric";
        }
    }

    public class ClassWarior : ClassBase
    {
        public ClassWarior()
        {
            Classname = "Warrior";
        }
    }

    public class ClassPaladin : ClassBase
    {
        public ClassPaladin()
        {
            Classname = "Paladin";
        }
    }

    public class ClassLess : ClassBase
    {
        public ClassLess(Player user)
        {
            Classname = "Classless";
        }
    }

    public enum ClassesType
    {
        Classless, Warrior, Paladin, Cleric, Rouge, Ranger, Mage
    }
}
