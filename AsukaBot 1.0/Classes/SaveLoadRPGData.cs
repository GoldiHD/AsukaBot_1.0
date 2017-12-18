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
        private RPG RPGGateWay;

        private void CheckForFile()
        {
            RPGGateWay = SingleTon.GetRPG();
            writer = new XmlTextWriter(FilePath, Encoding.UTF8);
            if (!(File.Exists(FilePath)))
            {
                File.Create(FilePath);
            }
        }

        public void SaveData()
        {
            CheckForFile();
            writer.WriteStartDocument(true);
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 2;
            writer.WriteStartElement("RPG");
            if (RPGGateWay.GetPlayers() != null)
            {
                foreach (Player element in RPGGateWay.GetPlayers())
                {
                    writer.WriteStartElement(element.GetPlayername());
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
                        writer.WriteString(element.EquippedArmor[0].Getname());
                    }

                    if (element.EquippedArmor[1] != null)
                    {
                        writer.WriteString(element.EquippedArmor[1].Getname());
                    }

                    if (element.EquippedArmor[2] != null)
                    {
                        writer.WriteString(element.EquippedArmor[2].Getname());
                    }
                    if (element.EquippedArmor[3] != null)
                    {
                        writer.WriteString(element.EquippedArmor[3].Getname());
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("Weapon");//redo
                    if (element.EquipedWeaponItem != null)
                    {
                        writer.WriteString(element.EquipedWeaponItem.Getname());
                    }
                    writer.WriteEndElement();

                    writer.WriteStartElement("AC");
                    writer.WriteString(element.GetAC().ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Gold");
                    writer.WriteString(element.GetInventory().GetGold().ToString());
                    writer.WriteEndElement();

                    writer.WriteStartElement("Items");
                    foreach(BaseItem item in element.GetInventory().GetTheInventory())
                    {
                        writer.WriteString(item.Getname());
                    }
                    writer.WriteEndElement();

                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
            writer.WriteEndDocument();
            writer.Close();
        }
    }
}
