using System.Collections.Generic;
using AsukaBot_1._0.Classes.Services;
using AsukaBot_1._0.Module.RPG.Logic.Classes;

namespace AsukaBot_1._0.Module.RPG.Logic
{
    public class StatsModifer
    {
        private Player User;
        public MultipleKeyDictionary<Feats, bool, Feat> AllFeatsActive = new MultipleKeyDictionary<Feats, bool, Feat>();
        public static Dictionary<Feats, Feat> AllFeats = new Dictionary<Feats, Feat>();

        public StatsModifer(Player user)
        {
            User = user;
            //create all feats
            if(AllFeats == null)
            {

            }
        }

    }

    public class Feat
    {
        protected string Featname;
        protected ClassesType ClassLimit;
    }

    public class FeatResistance : Feat
    {

    }

    public class FeatAttack : Feat
    {

    }

    public class FeatStats : Feat
    {
        private FeatStatsType Type;
        private int Streaght;

        public FeatStats(FeatStatsType type, int streanght)
        {
            Type = type;
            Streaght = streanght;
        }

        public FeatStatsType GetType()
        {
            return Type;
        }

        public int GetStreamght()
        {
            return Streaght;
        }
    }

    public enum Feats
    {
        Attack, Defense
    }

    public enum FeatStatsType
    {
        Power, luck, Vitallity, Dexterity, Intellegence, Magic
    }
}
