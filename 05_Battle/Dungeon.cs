using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class Dungeon
    {
        public string DungeonName { get; set; }
        public int Level { get; private set; }
        public string DungeonInfo { get; private set; }

        public Dungeon(string dungeonName, int level, string dungeonInfo)
        {
            DungeonName = dungeonName;
            Level = level;
            DungeonInfo = dungeonInfo;
        }
    }
}
