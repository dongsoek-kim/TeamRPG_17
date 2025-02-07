using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class MonsterManager : Singleton<MonsterManager>
    {

        private List<Monster> monsters = new List<Monster> 
        {
            new Monster("슬라임", 1, 5, 5, 5),
            new Monster("고블린", 1, 15, 5, 5),
            new Monster("새끼 늑대", 1, 45, 8, 10),

            new Monster("거대한 쥐", 2, 60, 10, 12),
            new Monster("살벌한 거미", 2, 85, 12, 14),
            new Monster("그림자 골렘", 2, 120, 14, 20),

            new Monster("해골 병사", 3, 100, 19, 15),
            new Monster("파묻힌 유령", 3, 80, 25, 20),
            new Monster("폐허의 수호자", 3, 200, 10, 30),
        };

        public List<Monster> GetMonsterByLevel(int level)
        {
            return monsters.Where(m => m.Level == level).ToList();
        }
        public List<Monster> RandomMonsterSpawn(Dungeon dungeon)
        {
            Random rand = new Random();
            List<Monster> list = new List<Monster>();
            // 전투 개시 시 몬스터 랜덤 할당
            // 1~4마리 몬스터를 랜덤으로 선언?
            int[] arr = new int[rand.Next(1, 5)]; // 0~3 정해주고 몬스터 갯수

            List<Monster> encounter = this.GetMonsterByLevel(dungeon.Level);

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rand.Next(0, encounter.Count); // rand = 몬스터 종류를 정해주는거 // if  arr[0] = 2
                Monster newMonster = new Monster(encounter[arr[i]].Name, encounter[arr[i]].Level, encounter[arr[i]].MaxHp, encounter[arr[i]].Damage, encounter[arr[i]].Defense);
                list.Add(newMonster);
            }
            return list;
        }
    }
}
