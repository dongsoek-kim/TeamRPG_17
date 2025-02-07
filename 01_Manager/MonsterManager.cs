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
            new Monster("슬라임", 1, 5, 5, 5), // 0
            new Monster("고블린", 1, 15, 5, 5), // 1
            new Monster("거대한 오크", 2, 45, 12, 10), //2

        };

        public List<Monster> RandomMonsterSpawn()
        {
            Random rand = new Random();
            List<Monster> list = new List<Monster>();
            // 전투 개시 시 몬스터 랜덤 할당
            // 1~4마리 몬스터를 랜덤으로 선언?
            int[] arr = new int[rand.Next(0, 4)]; // 0~3 정해주고 몬스터 갯수

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = rand.Next(0, 3); // rand = 몬스터 종류를 정해주는거 // if  arr[0] = 2
                list.Add(monsters[arr[i]]); // 됐어 나이스~
            }

            return list;
        }
    }
}
