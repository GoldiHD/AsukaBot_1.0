using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsukaBot_1._0.Module.RPG.Logic.Items
{
    class WeaponMagicEffect
    {
        private Elements MyElement;

        public int GetExtraDamage()
        {
            return 0;
        }
    }



    public enum Elements
    {
        Fire, Water, Earth, Thunder
    }
}

