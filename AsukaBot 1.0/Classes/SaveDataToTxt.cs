using AsukaBot_1._0.Core;
using AsukaBot_1._0.Module.Games;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AsukaBot_1._0.Classes
{
    class SaveDataToTxt
    {
        private string FilePathBank = Directory.GetCurrentDirectory() + @"\assets\bank.xml";
        private string FilePathMusic = Directory.GetCurrentDirectory() + @"\assets\music.xml";

        private XmlTextWriter writer;
        private XmlTextReader reader;

        private void CheckForFile(string path)
        {
            if(!(File.Exists(path)))
            {
                File.Create(path);
                Console.WriteLine("No " + path+", was found but a new file have  been created");
            }
        }

        public void SaveData(SaveMode datatype)
        {
            switch(datatype)
            {
                case SaveMode.Bank:
                    GameBank bank = new GameBank();
                    writer = new XmlTextWriter(FilePathBank, Encoding.UTF8);
                    writer.WriteStartDocument(true);
                    writer.Formatting = Formatting.Indented;
                    writer.Indentation = 2;
                    writer.WriteStartElement("Bank");
                    if(bank.GetBankAccounts() != null)
                    {
                        writer.WriteStartElement("Bank");
                        foreach (BankAcountProfile element in bank.GetBankAccounts())
                        {
                            writer.WriteStartElement("Profile");

                            writer.WriteStartElement("username");
                            writer.WriteString(element.GetUsername());
                            writer.WriteEndElement();

                            writer.WriteStartElement("money");
                            writer.WriteString(element.GetCurrentAmountOfMoney().ToString());
                            writer.WriteEndElement();

                            writer.WriteEndElement();
                        }
                        writer.WriteEndDocument();
                        writer.Close();
                    }
                    break;

                case SaveMode.Music:

                    break;
            }
        }
    }

    public enum SaveMode
    {
        Bank, Music
    }
}
