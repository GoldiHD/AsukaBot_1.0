using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Module.RPG.Logic.Classes
{
    public class ClassBase
    {
        protected Player User;
        protected string Classname;
        public string GetClassName()
        {
            return Classname;
        }
    }

    public class ClassMage : ClassBase
    {
        public ClassMage(Player user)
        {
            User = user;
            Classname = "Mage";
            User.GetFeatSystem().ActivateFeat("Smart ass");
        }
    }

    public class ClassRanger : ClassBase
    {
        public ClassRanger(Player user)
        {
            User = user;
            Classname = "Ranger";
            User.GetFeatSystem().ActivateFeat("Nimble");
        }
    }

    public class ClassRouge : ClassBase
    {
        public ClassRouge(Player user)
        {
            User = user;
            Classname = "Rouge";
            User.GetFeatSystem().ActivateFeat("Uncanny Luck");
        }
    }

    public class ClassCleric : ClassBase
    {
        public ClassCleric(Player user)
        {
            User = user;
            Classname = "Cleric";
            User.GetFeatSystem().ActivateFeat("Inborn Magic");
        }
    }

    public class ClassWarior : ClassBase
    {
        public ClassWarior(Player user)
        {
            User = user;
            Classname = "Warrior";
            User.GetFeatSystem().ActivateFeat("Hard Hitter");
        }
    }

    public class ClassPaladin : ClassBase
    {
        public ClassPaladin(Player user)
        {
            User = user;
            Classname = "Paladin";
            User.GetFeatSystem().ActivateFeat("Tanker");
        }
    }

    public class ClassLess : ClassBase
    {
        public ClassLess()
        {
            Classname = "Classless";
        }
    }

    public enum ClassesType
    {
        Classless, Warrior, Paladin, Cleric, Rouge, Ranger, Mages
    }
}
