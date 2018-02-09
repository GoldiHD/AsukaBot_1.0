using System;
using System.Collections.Generic;
using AsukaBot_1._0.Classes.Services;
using AsukaBot_1._0.Module.RPG.Logic.Classes;

namespace AsukaBot_1._0.Module.RPG.Logic
{
    public class StatsModifer
    {
        private Player User;
        private MultipleKeyDictionary<Feats, bool, Feat> AllFeatsActive = new MultipleKeyDictionary<Feats, bool, Feat>();
        private static List<Feat> AllFeats;

        public StatsModifer(Player user)
        {
            User = user;
            AssignFeats();
            //create all feats

        }

        public void AssignFeats()
        {
            if (AllFeats == null)
            {
                AllFeats = new List<Feat>();
                AllFeats.Add(new FeatStats(FeatStatsType.Vitallity, Feats.Stats, "Tanker", 5));
                AllFeats.Add(new FeatStats(FeatStatsType.Power, Feats.Stats, "Hard Hitter", 5));
                AllFeats.Add(new FeatStats(FeatStatsType.Intellegence, Feats.Stats, "Smart ass", 5));
                AllFeats.Add(new FeatStats(FeatStatsType.Dexterity, Feats.Stats, "Nimble", 5));
                AllFeats.Add(new FeatStats(FeatStatsType.luck, Feats.Stats, "Uncanny Luck", 5));
                AllFeats.Add(new FeatStats(FeatStatsType.Magic, Feats.Stats, "Inborn Magic", 5));
            }
        }

        public void ActivateFeat(string input)
        {
            AssignFeats();
            foreach (Feat element in AllFeats)
            {
                if (element.GetName() == input)
                {
                    element.UseAblility(User);
                    AllFeatsActive.Add(element.GetFeatType(), true, element);
                    
                }
            }
        }

        public void DeactivateFeat()
        {

        }

        public void RemoveFeat()
        {

        }

    }

    public abstract class Feat
    {
        protected string Featname;
        protected Feats FeatType;
        protected ClassesType ClassLimit;

        public string GetName()
        {
            return Featname;
        }

        public Feats GetFeatType()
        {
            return FeatType;
        }

        abstract public void UseAblility(Player user);

    }

    public class FeatResistance : Feat
    {
        public override void UseAblility(Player user)
        {

        }
    }

    public class FeatAttack : Feat
    {
        public override void UseAblility(Player user)
        {

        }
    }

    public class FeatStats : Feat
    {
        private FeatStatsType Type;
        private int Streaght;

        public FeatStats(FeatStatsType type, Feats feattype, string name, int streanght)
        {
            FeatType = feattype;
            Featname = name;
            Type = type;
            Streaght = streanght;
        }

        public FeatStatsType getType()
        {
            return Type;
        }

        public int GetStreamght()
        {
            return Streaght;
        }

        public override void UseAblility(Player user)
        {
            switch (Type)
            {
                case FeatStatsType.Dexterity:
                    user.GetStats().GetDexterity().IncreaseStatsFeat(Streaght);
                    break;

                case FeatStatsType.Intellegence:
                    user.GetStats().GetIntellegence().IncreaseStatsFeat(Streaght);
                    break;

                case FeatStatsType.luck:
                    user.GetStats().GetLuck().IncreaseStatsFeat(Streaght);
                    break;

                case FeatStatsType.Magic:
                    user.GetStats().GetMagic().IncreaseStatsFeat(Streaght);
                    break;

                case FeatStatsType.Power:
                    user.GetStats().GetPower().IncreaseStatsFeat(Streaght);
                    break;

                case FeatStatsType.Vitallity:
                    user.GetStats().GetVitallity().IncreaseStatsFeat(Streaght);
                    break;
            }
        }


    }

    public enum Feats
    {
        Attack, Defense, Stats
    }

    public enum FeatStatsType
    {
        Power, luck, Vitallity, Dexterity, Intellegence, Magic
    }
}
