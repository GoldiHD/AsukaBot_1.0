using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace AsukaBot_1._0.Module.Games
{
    public class DnDModule : ModuleBase<SocketCommandContext>
    {
        private List<Creature> AllCreaturesList = new List<Creature>();

        [Command("#settings")]
        public async Task MainSettigns()
        {
            
        }

        [Command("#GetCreature")]
        public async Task GetCreature(string MonsterName, int lvl = 1)
        {
            await Context.Channel.SendMessageAsync("not implamented yet");
        }

        /*
                          cbg.CreateCommand("#SetSettings").Parameter("IncreaseRate", ParameterType.Required).Parameter("XpFollow", ParameterType.Required).Do(async (e) =>
                {
                    await e.Channel.SendMessage("Settings have been changed");
                });

                cbg.CreateCommand("#ShowAllCreatures").Do(async (e) =>
                {
                    string AllCrreatureNames = "";
                    if (AllCreaturesList.Count > 0)
                    {
                        for (int i = 0; i < AllCreaturesList.Count; i++)
                        {
                            if (i == 0)
                            {
                                AllCrreatureNames = AllCreaturesList[i].Getname() + " ";
                            }
                            else
                            {
                                AllCrreatureNames += "," + AllCreaturesList[i].Getname() + " ";
                            }
                        }
                        await e.Channel.SendMessage(AllCrreatureNames);
                    }
                    else
                    {
                        await e.Channel.SendMessage("No Creatures have been created yet");
                    }
                });

                cbg.CreateCommand("#GMGetCreature").Do(async (e) =>
                {
                    //create Document with peoples access
                    //show creature with all stats
                });

                cbg.CreateCommand("#WipeCreature").Parameter("name", ParameterType.Required).Do(async (e) =>
                {
                    bool creatureExists = false;
                    if (e.GetArg("name") == "list")
                    {
                        AllCreaturesList = new List<Creature>();
                        await e.Channel.SendMessage(e.GetArg("name") + " is deleted");
                    }
                    else
                    {
                        for (int i = 0; i < AllCreaturesList.Count; i++)
                        {
                            if (AllCreaturesList[i].Getname().ToLower() == e.GetArg("name").ToLower())
                            {
                                creatureExists = true;
                                AllCreaturesList.RemoveAt(i);
                            }
                        }
                        if (creatureExists)
                        {
                            await e.Channel.SendMessage(e.GetArg("name") + " is deleted");
                        }
                        else
                        {
                            await e.Channel.SendMessage(e.GetArg("name") + " dosn't exists");
                        }
                    }

                });

                cbg.CreateCommand("#CreateCreature").Parameter("CreatureType", ParameterType.Required).Parameter("name", ParameterType.Required).Parameter("HP", ParameterType.Required).Parameter("CR", ParameterType.Required).Parameter("Str", ParameterType.Required).Parameter("Dex", ParameterType.Required).Parameter("Con", ParameterType.Required).Parameter("Int", ParameterType.Required).Parameter("Wis", ParameterType.Required).Parameter("Cha", ParameterType.Required).Parameter("XPgain", ParameterType.Required).Parameter("Type", ParameterType.Optional).Parameter("attacks", ParameterType.Multiple)
                 .Do(async (e) =>
                {
                    if (e.GetArg("CreatureType") == "light")
                    {
                        AllCreaturesList.Add(new Creature(e.GetArg("name"), e.GetArg("HP"), e.GetArg("CR"), e.GetArg("Str"), e.GetArg("Dex"), e.GetArg("Con"), e.GetArg("Int"), e.GetArg("Wis"), e.GetArg("Cha"), e.GetArg("XPgain")));
                    }
                    else if (e.GetArg("CreatureType") == "Heavy")
                    {
                        AllCreaturesList.Add(new Creature(e.GetArg("name"), e.GetArg("HP"), e.GetArg("CR"), e.GetArg("Str"), e.GetArg("Dex"), e.GetArg("Con"), e.GetArg("Int"), e.GetArg("Wis"), e.GetArg("Cha"), e.GetArg("XPgain")));
                    }
                    await e.Channel.SendMessage("Creature " + e.GetArg("name") + " created");
                });

                cbg.CreateCommand("#SetExtraData").Parameter("CreatureName", ParameterType.Required).Parameter("DataType", ParameterType.Required).Parameter("Data", ParameterType.Unparsed).Do(async (e) =>
                {
                    bool ChangedCreatureData = false;
                    bool FoundCreature = false;
                    for (int i = 0; i < AllCreaturesList.Count; i++)
                    {
                        if (e.GetArg("CreatureName").ToLower() == AllCreaturesList[i].Getname().ToLower())
                        {
                            ChangedCreatureData = true;
                            switch (e.GetArg("CreatureName").ToLower())
                            {
                                case "type":
                                    ChangedCreatureData = true;

                                    break;

                                case "attacks":
                                case "attack":
                                    ChangedCreatureData = true;

                                    break;

                                case "sence":
                                case "sences":
                                    ChangedCreatureData = true;

                                    break;



                                default:
                                    await e.Channel.SendMessage("Given data holder doesn't exists");
                                    break;
                            }
                        }
                    }
                    if (ChangedCreatureData == true && FoundCreature == true)
                    {
                        await e.Channel.SendMessage("Changes have been finshed");
                    }
                    else if (ChangedCreatureData == false && FoundCreature == true)
                    {
                        await e.Channel.SendMessage("the data type " + e.GetArg("DataType") + " was not found");
                    }
                    else if (ChangedCreatureData == false && FoundCreature == false)
                    {
                        await e.Channel.SendMessage("The creature " + e.GetArg("CreatureName") + " could not be found");
                    }
                });

                cbg.CreateCommand("#HereIsAssest").Parameter("CreatureName", ParameterType.Required).Do(async (e) =>
                        {
                            try
                            {
                                //Attachment TempAttacment = e.Message.Attachments[];
                                System.Diagnostics.Process.Start("www.google.dk");
                                await e.Channel.SendMessage("understood");
                            }
                            catch
                            {
                                Console.WriteLine("Erorr");
                            }
                        });
         */



        private void LoadCreatures()
        {
            List<Creature> temp = new List<Creature>();
            AllCreaturesList = temp;
        }

        private class Creature
        {
            private string Name = "";
            private string HP = "";
            private string Str = "";
            private string Dex = "";
            private string Con = "";
            private string Int = "";
            private string Wis = "";
            private string Cha = "";
            private CreatureTypes ThisCreatureTypes = CreatureTypes.Undefined;
            private string Sences = "";
            private string Languages = "";
            private string XPGain = "";
            private string Attacks = "";
            private string Size = "";
            private string Notes = "";
            private string CR = "";

            public Creature(string name, string hp, string cr, string str, string dex, string con, string intellegence, string wis, string cha, string xpGain)
            {
                this.Name = name;
                this.HP = hp;
                this.CR = cr;
                this.Str = str;
                this.Dex = dex;
                this.Con = con;
                this.Int = intellegence;
                this.Wis = wis;
                this.Cha = cha;
                this.XPGain = xpGain;
            }
            public Creature(string name, string hp, string cr, string str, string dex, string con, string intellegence, string wis, string cha, string xpGain, string type, string attacks, string senses, string languages, string Size, string Notes)
            {
                this.Name = name;
                this.HP = hp;
                this.CR = cr;
                this.Str = str;
                this.Dex = dex;
                this.Con = con;
                this.Int = intellegence;
                this.Wis = wis;
                this.Cha = cha;
                this.XPGain = xpGain;

                this.Sences = senses;


            }
            public void SetType(string type)
            {
                switch (type)
                {
                    case "dragon":
                        this.ThisCreatureTypes = CreatureTypes.Dragon;
                        break;
                    case "undead":
                        this.ThisCreatureTypes = CreatureTypes.Undead;
                        break;
                    case "demon":
                        this.ThisCreatureTypes = CreatureTypes.Demon;
                        break;
                    case "human":
                        this.ThisCreatureTypes = CreatureTypes.Human;
                        break;
                    case "elf":
                        this.ThisCreatureTypes = CreatureTypes.Elf;
                        break;
                    case "animal":
                        this.ThisCreatureTypes = CreatureTypes.Animal;
                        break;

                    default:
                        this.ThisCreatureTypes = CreatureTypes.Undefined;
                        break;
                }
            }
            public void SetAttacks(string attacks)
            {
                this.Attacks = attacks;
            }
            public List<string> GetAll()
            {
                List<string> All = new List<string>();
                All.Add(Name);
                All.Add(Str);
                return All;
            }
            public string Getname()
            {
                return Name;
            }
            public enum CreatureTypes
            {
                Dragon, Undead, Demon, Human, Elf, Animal, Undefined
            }

        }
    }
}
