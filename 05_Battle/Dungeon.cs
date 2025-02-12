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

        /// <summary>
        /// 던전 생성자. 던전 이름, 레벨, 던전 정보를 받아 생성
        /// </summary>
        /// <param name="dungeonName"> 던전 명 </param>
        /// <param name="level"> 던전 레벨 </param>
        /// <param name="dungeonInfo"> 던전 정보 </param>
        public Dungeon(string dungeonName, int level, string dungeonInfo)
        {
            DungeonName = dungeonName;
            Level = level;
            DungeonInfo = dungeonInfo;
        }
    }
}
