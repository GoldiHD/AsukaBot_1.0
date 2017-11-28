using AsukaBot_1._0.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Classes
{
    class SaveDataToTxt
    {
        static private StreamReader Reader;
        static private StreamWriter Writer;
        private string FilePath = Directory.GetCurrentDirectory() + @"\assets\";

        /// <summary>
        /// add new data to txt save all the data
        /// </summary>
        /// <param name="Module"></param>
        /// <param name="Data"></param>
        public void SavaData(ModuleType Module, string Data)
        {
            string filepath = translatePath(Module);

        }
        /// <summary>
        /// loads data and changes it then resaves it 
        /// </summary>
        /// <param name="Module">What txt to work with</param>
        /// <param name="Data">the pure data</param>
        /// <param name="Where">What keyword it should use to find the data</param>
        public void ModifyData(ModuleType Module, string Data, string Where)
        {
            string filepath = translatePath(Module);
        }
        /// <summary>
        /// Load bank data, names and money
        /// </summary>
        /// <param name="Module">What module data it should load</param>
        /// <returns>list of all the text in the txt, if file does not exists it will be created and some default data will be send back</returns>
        public List<string>[] LoadBankData(ModuleType Module)
        {
            string filepath = translatePath(Module);
            if (File.Exists(FilePath))
            {
                Reader = new StreamReader(filepath);
                List<string>[] ReturnData = new List<string>[2];
                List<string> Names = new List<string>();
                List<string> Money = new List<string>();
                while (Reader.ReadLine() != null)
                {
                    Names.Add(Reader.ReadLine());
                    Money.Add(Reader.ReadLine());
                }
                ReturnData[0] = Names;
                ReturnData[1] = Money;
                return ReturnData;
            }
            else
            {
                return new List<string>[2] { new List<string> { "nan" }, new List<string> { "nan" } };
            }
        }
        /// <summary>
        /// Transform the enum into the path you need
        /// </summary>
        /// <param name="Check">the moduletype</param>
        /// <returns>string oof the path</returns>
        private string translatePath(ModuleType Check)
        {
            string path = FilePath;

            switch (Check)
            {
                case ModuleType.Bank:
                    path = "bank";
                    break;

                case ModuleType.DnD:
                    path = "music";
                    break;

                default:
                    Console.WriteLine("Error in defining a txt file");
                    break;
            }
            path += ".txt";
            if (!File.Exists(path))
            {
                using (Writer = File.CreateText(path))
                {

                }
                Writer.Close();
                Console.WriteLine("no file was found, so one have been created");
            }
            return path;
        }
    }
}
