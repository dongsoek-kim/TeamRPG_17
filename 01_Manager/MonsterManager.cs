using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    internal class MonsterManager : Singleton<MonsterManager>
    {
        private List<Monster> monsters = new List<Monster>
        {
            new Monster("슬라임", 1, 5, 5, 5),
            new Monster("고블린", 1, 15, 5, 5),
            new Monster("거대한 오크", 2, 45, 12, 10),

        };

        public void RandomMonsterSpawn()
        {

        }
    }
}
