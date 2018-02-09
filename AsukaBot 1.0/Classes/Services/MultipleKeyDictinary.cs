using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Classes.Services
{
    public class MultipleKeyDictionary<TKey, TSubKey, TValue>
    {
        private List<TKey> Primary = new List<TKey>();
        private List<TSubKey> Secondary = new List<TSubKey>();
        private List<TValue> Value = new List<TValue>();

        /// <summary>
        /// Add new object to the dictionary
        /// </summary>
        /// <param name="key">The main key</param>
        /// <param name="subkey">The subkey</param>
        /// <param name="value"> Value of the dictionary object</param>
        public void Add(TKey key, TSubKey subkey, TValue value)
        {
            Primary.Add(key);
            Secondary.Add(subkey);
            Value.Add(value);
        }

        /// <summary>
        /// Check if the sizes of the dictionary fits
        /// </summary>
        /// <returns>true or false depending on wether it it all fits correctly</returns>
        public bool ErrorChecking()
        {
            if (Primary.Count == Secondary.Count && Secondary.Count == Value.Count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Find the location of a value
        /// </summary>
        /// <param name="ValueData">the value you wish to find</param>
        /// <returns>the index number of the value</returns>
        public int FindValueLocation(TValue ValueData)
        {
            for (int i = 0; i < Value.Count; i++)
            {
                if (Value[i].Equals(ValueData))
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// See if the dictionary contains the specif object with the correct keys
        /// </summary>
        /// <param name="key">main key</param>
        /// <param name="subkey">Subkey</param>
        /// <param name="value">dictionary object</param>
        /// <returns>if it's in the dictionary or not</returns>
        public bool Contains(TKey key, TSubKey subkey, TValue value)
        {
            for (int i = 0; i < Primary.Count; i++)
            {
                if(Primary[i].Equals(key) && Secondary[i].Equals(subkey) && Value[i].Equals(value))
                {
                    return true;
                }
            }
            return false;
        }


        /// <summary>
        /// if the main key exists in the dictionary
        /// </summary>
        /// <param name="key">main key you wanna look after</param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            if (Primary.Contains(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// if the subkey exists in the dictionary
        /// </summary>
        /// <param name="subkey">subkey you want to klook after</param>
        /// <returns></returns>
        public bool ContainsSubkey(TSubKey subkey)
        {
            if(Secondary.Contains(subkey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// if the dictionary object exists withing the dictionary
        /// </summary>
        /// <param name="value">the value you wanna look for in the dictionary</param>
        /// <returns></returns>
        public bool ContainsValue(TValue value)
        {
            if(Value.Contains(value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<TValue> GetAllValuesWith(TKey key)
        {
            List<TValue> ReturnList = new List<TValue>();
            for (int i = 0; i < Primary.Count; i++)
            {
                if (Primary[i].Equals(key))
                {
                    ReturnList.Add(Value[i]);
                }
            }
            return ReturnList;
        }

        public List<TValue> GetAllValuesWith(TKey key, TSubKey subkey)
        {
            List<TValue> ReturnList = new List<TValue>();
            for (int i = 0; i < Primary.Count; i++)
            {
                if (Primary[i].Equals(key) && Secondary[i].Equals(subkey))
                {
                    ReturnList.Add(Value[i]);
                }
            }
            return ReturnList;
        }

        /// <summary>
        /// clear the dictionary of all data
        /// </summary>
        public void Clear()
        {
            Primary.Clear();
            Secondary.Clear();
            Value.Clear();
        }

        /// <summary>
        /// remove data in posstion given
        /// </summary>
        /// <param name="Postion"></param>
        public void Remove(int Postion)
        {
            if(ErrorChecking())
            {
                Primary.RemoveAt(Postion);
                Secondary.RemoveAt(Postion);
                Value.RemoveAt(Postion);
            }
            else
            {
                throw new ArgumentException("The Dictionary is not propably aligned correctly");
            }
        }

        /// <summary>
        /// Get all the values from the values part of the dictionary
        /// </summary>
        /// <returns></returns>
        public List<TValue> GetAllValues()
        {
            return Value;
        }

        /// <summary>
        /// invalid
        /// </summary>
        /// <param name="key"></param>
        public void RemoveAllByKey(TKey key)
        {
            List<int> RemoveLocations = new List<int>();
            for(int i = 0; i < Primary.Count; i++)
            {

            }
        }

        /// <summary>
        /// invalid
        /// </summary>
        public void RemoveAllBySubkey()
        {

        }
    }
}
