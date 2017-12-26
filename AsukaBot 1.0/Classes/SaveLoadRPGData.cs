using AsukaBot_1._0.Module.Music.Logic;
using AsukaBot_1._0.Module.RPG.Logic;
using AsukaBot_1._0.Module.RPG.Logic.Items;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AsukaBot_1._0.Classes
{
    class SaveLoadRPGData
    {
        private string FilePath = Directory.GetCurrentDirectory() + @"\assets\RPGXML.xml";
        private XmlTextWriter writer;
        private XmlTextReader reader;
        private RPG RPGGateWay;

        private void CheckForFile()
        {
            RPGGateWay = SingleTon.GetRPG();
            if (!(File.Exists(FilePath)))
            {
                File.Create(FilePath);
            }
        }

        public void SaveData()
        {
            CheckForFile();
            if(RPGGateWay == null)
            {
                RPGGateWay = new RPG();
            }
            writer = new XmlTextWriter(FilePath, Encoding.UTF8);
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("RPG");
            if (RPGGateWay.GetPlayers() != null)
            {
                foreach (Player element in RPGGateWay.GetPlayers())
                {
                    writer.WriteStartElement("User");
                    writer.WriteAttributeString("Username", element.GetPlayername());
                    writer.WriteStartElement("Stats");

                    writer.WriteStartElement("Power");
                    writer.WriteString(element.GetStats().GetPower().GetPowerLvl().ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Magic");
                    writer.WriteString(element.GetStats().GetMagic().GetMagicLvl().ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Dexterity");
                    writer.WriteString(element.GetStats().GetDexterity().GetDexterityLvl().ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Intelligence");
                    writer.WriteString(element.GetStats().GetIntellegence().GetIntellegencLvl().ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Vitallity");
                    writer.WriteString(element.GetStats().GetVitallity().GetVitallityLvl().ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Luck");
                    writer.WriteString(element.GetStats().GetLuck().GetLuckLvl().ToString());
                    writer.WriteEndElement();

                    writer.WriteEndElement();

                    writer.WriteStartElement("StatsPoints");
                    writer.WriteString(element.GetStats().GetStatPoints().ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("level");
                    writer.WriteString(element.GetPlayerLvl().ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("exp");
                    writer.WriteString(element.GetExpCurrent().ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("nextlvlexp");
                    writer.WriteString(element.GetExpForNextLvl().ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("hardcoremode");
                    writer.WriteString(element.GetHardcoreState().ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Armor");//redo
                    if (element.EquippedArmor[0] != null)
                    {
                        writer.WriteAttributeString("Head", element.EquippedArmor[0].Getname());
                    }

                    if (element.EquippedArmor[1] != null)
                    {
                        writer.WriteAttributeString("Chest", element.EquippedArmor[1].Getname());
                    }

                    if (element.EquippedArmor[2] != null)
                    {
                        writer.WriteAttributeString("Legs", element.EquippedArmor[2].Getname());
                    }
                    if (element.EquippedArmor[3] != null)
                    {
                        writer.WriteAttributeString("Hands", element.EquippedArmor[3].Getname());
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("Weapon");//redo
                    if (element.EquipedWeaponItem != null)
                    {
                        writer.WriteAttributeString("EquipedWeapon", element.EquipedWeaponItem.Getname());
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("AC");
                    writer.WriteString(element.GetAC().ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Gold");
                    writer.WriteString(element.GetInventory().GetGold().ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Items");
                    foreach (BaseItem item in element.GetInventory().GetTheInventory())
                    {
                        writer.WriteString(item.Getname() + ",");
                    }
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }

        public void LoadData()
        {
            CheckForFile();
            if (RPGGateWay == null)
            {
                RPGGateWay = new RPG();
            }
            reader = new XmlTextReader(FilePath);
            Player TempNewUserHolder = new Player();
            int[] StatsArray = new int[6];
            
            reader.WhitespaceHandling = WhitespaceHandling.None;
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        switch (reader.Name)
                        {
                            case "User":
                                TempNewUserHolder = new Player();
                                TempNewUserHolder.SetUsername(reader.GetAttribute("Username"));
                                break;

                            case "Power":
                                StatsArray[0] = Convert.ToInt32(reader.ReadString());
                                break;

                            case "Magic":
                                StatsArray[1] = Convert.ToInt32(reader.ReadString());
                                break;

                            case "Dexterity":
                                StatsArray[2] = Convert.ToInt32(reader.ReadString());
                                break;

                            case "Intelligence":
                                StatsArray[3] = Convert.ToInt32(reader.ReadString());
                                break;

                            case "Vitallity":
                                StatsArray[4] = Convert.ToInt32(reader.ReadString());
                                break;

                            case "Luck":
                                StatsArray[5] = Convert.ToInt32(reader.ReadString());
                                break;

                            case "StatsPoints":
                                TempNewUserHolder.GetStats().SetStatsPoints(10);
                                TempNewUserHolder.GetStats().AddStatsPoints(Convert.ToInt32(reader.ReadString()));
                                break;

                            case "level":
                                TempNewUserHolder.SetPlayerLevel(Convert.ToInt32(reader.ReadString()));
                                break;

                            case "exp":
                                TempNewUserHolder.SetPlayerCurrentExp(Convert.ToInt32(reader.ReadString()));
                                break;

                            case "nextlvlexp":
                                TempNewUserHolder.SetPlayerNextLevelXp(Convert.ToInt32(reader.ReadString()));
                                break;

                            case "Armor":
                                if (reader.GetAttribute("Head") != null)
                                {
                                    TempNewUserHolder.EquippedArmor[0] = (ArmorItem)RPGGateWay.CheckUpList.GetinventoryItemByName(reader.GetAttribute("Head"));
                                }
                                if (reader.GetAttribute("Chest") != null)
                                {
                                    TempNewUserHolder.EquippedArmor[1] = (ArmorItem)RPGGateWay.CheckUpList.GetinventoryItemByName(reader.GetAttribute("Chest"));
                                }
                                if (reader.GetAttribute("Legs") != null)
                                {
                                    TempNewUserHolder.EquippedArmor[2] = (ArmorItem)RPGGateWay.CheckUpList.GetinventoryItemByName(reader.GetAttribute("Legs"));
                                }
                                if (reader.GetAttribute("Hands") != null)
                                {
                                    TempNewUserHolder.EquippedArmor[3] = (ArmorItem)RPGGateWay.CheckUpList.GetinventoryItemByName(reader.GetAttribute("Hands"));
                                }
                                break;

                            case "Weapon":
                                if (reader.GetAttribute("EquipedWeapon") != null)
                                {
                                    TempNewUserHolder.EquipedWeaponItem = (WeaponsItem)RPGGateWay.CheckUpList.GetinventoryItemByName(reader.GetAttribute("EquipedWeapon"));
                                }
                                break;

                            case "AC":
                                if (TempNewUserHolder.GetAC() != Convert.ToInt32(reader.ReadString()))
                                {
                                    Console.WriteLine("Error in ac, user: " + TempNewUserHolder.GetPlayername());
                                }
                                break;

                            case "Gold":
                                TempNewUserHolder.GetInventory().SetGold(Convert.ToInt32(reader.ReadString()));
                                break;

                            case "hardcoremode":

                                if (reader.ReadString() == "True")
                                {
                                    TempNewUserHolder.SetHardcoreState(true);
                                }
                                break;

                            case "Items":
                                string temp;
                                temp = reader.ReadString();
                                string[] hello = temp.Split(',');
                                foreach (string itemname in hello)
                                {
                                    if (itemname != "")
                                    {
                                        TempNewUserHolder.GetInventory().GetTheInventory().Add(TempNewUserHolder.GetInventory().GetAllItemsList()[TempNewUserHolder.GetInventory().GetItemByName(itemname)]);
                                    }
                                }
                                break;
                        }
                        break;

                    case XmlNodeType.EndElement:
                        switch (reader.Name)
                        {
                            case "User":
                                RPGGateWay.AddPlayer(TempNewUserHolder);
                                break;

                            case "Stats":
                                TempNewUserHolder.SetStats(new Stats(new Power(StatsArray[0]), new Magic(StatsArray[1]), new Dexterity(StatsArray[2]), new Intellegenc(StatsArray[3]), new Vitallity(StatsArray[4]), new Luck(StatsArray[5])));
                                break;

                            case "Armor":
                                int b = 0;
                                if (TempNewUserHolder.EquippedArmor[0] != null)
                                {
                                    TempNewUserHolder.AddAC(TempNewUserHolder.EquippedArmor[0].GetAC());
                                }

                                if (TempNewUserHolder.EquippedArmor[1] != null)
                                {
                                    TempNewUserHolder.AddAC(TempNewUserHolder.EquippedArmor[1].GetAC());
                                }

                                if (TempNewUserHolder.EquippedArmor[2] != null)
                                {
                                    TempNewUserHolder.AddAC(TempNewUserHolder.EquippedArmor[3].GetAC());
                                }

                                if (TempNewUserHolder.EquippedArmor[3] != null)
                                {
                                    TempNewUserHolder.AddAC(TempNewUserHolder.EquippedArmor[4].GetAC());
                                }
                                break;

                        }
                        break;
                }

            }
            reader.Close();
        }
    }
}
