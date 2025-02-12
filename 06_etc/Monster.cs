using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class Monster
    {
        public string Name { get; set; }
        public int Level { get; private set; }
        public float Damage { get; private set; }
        public float Defense { get; private set; }
        public int MaxHp { get; set; }
        public int CurrentHp { get; set; }

        public bool IsDead => CurrentHp <= 0;


        /// <summary>
        /// 몬스터 생성자. 몬스터 이름, 레벨, 체력, 공격력, 방어력을 받아 생성
        /// </summary>
        /// <param name="name"> 이름 </param>
        /// <param name="level"> 레벨 </param>
        /// <param name="hp"> 체력 </param>
        /// <param name="damage"> 공격력 </param>
        /// <param name="defense"> 방어력 </param>
        public Monster(string name, int level, int hp, float damage, float defense)
        {
            Name = name;
            Level = level;
            Damage = damage;
            Defense = defense;
            MaxHp = hp;
            CurrentHp = hp;
        }

        /// <summary>
        /// 몬스터 정보를 반환하는 함수
        /// </summary>
        public string GetInfo()
        {
            return $"Lv.{this.Level}   {this.Name}";
        }
    }
}
