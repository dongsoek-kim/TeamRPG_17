using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class DungeonManager : Singleton<DungeonManager>
    {
        public string DungeonName { get; set; }
        public int Level { get; private set; }
        public float Damage { get; private set; }
    }
}
