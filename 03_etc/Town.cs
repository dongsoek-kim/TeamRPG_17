using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17._03_etc
{
    internal class Town
    {
        public TownName name { get; private set; }
        public string townDescription { get; private set; }
        public int entryLevel { get; private set; }


        public Town(TownName _name, string _townDescription, int _entryLevel)
        {
            name = _name;
            townDescription = _townDescription;
            entryLevel = _entryLevel;
        }
        
        public bool CanEnterTown()
        {
            return (GameManager.Instance.player.level >= EntryLevel);
        }
    }
}
