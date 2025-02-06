using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    internal class Monster
    {
        public string Name { get; set; }
        public int Level { get; private set; }
        public float Damage { get; private set; }
        public float Defense { get; private set; }
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }



        public Monster(string name, int level, int hp, float damage, float defense)
        {
            Name = name;
            Level = level;
            Damage = damage;
            Defense = defense;
            MaxHp = hp;
            CurrentHp = hp;
        }
    }
}
