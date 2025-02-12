using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class MonsterManager : Singleton<MonsterManager>
    {
        /// <summary>
        /// 몬스터 리스트
        /// </summary>
        private List<Monster> monsters =
        [
            // 레벨 1 몬스터
            new("슬라임", 1, 5, 5, 5),
            new("고블린", 1, 15, 5, 5),
            new("새끼 늑대", 1, 45, 8, 10),
            new("나무 정령", 1, 30, 6, 8),
            new("야생 토끼", 1, 20, 7, 4),
            new("작은 박쥐", 1, 10, 4, 6),
            new("맹독 두꺼비", 1, 25, 9, 5),
            new("잿빛 들개", 1, 40, 7, 9),
            new("도깨비불", 1, 35, 6, 7),
            new("미니 골렘", 1, 50, 7, 12),

            // 레벨 2 몬스터
            new("거대한 쥐", 2, 60, 10, 12),
            new("살벌한 거미", 2, 85, 12, 14),
            new("그림자 골렘", 2, 120, 14, 20),
            new("독침 전갈", 2, 70, 13, 10),
            new("숲의 망령", 2, 95, 11, 16),
            new("불타는 해골", 2, 110, 15, 18),
            new("강철 멧돼지", 2, 130, 16, 15),
            new("얼음 정령", 2, 100, 12, 20),
            new("암흑의 기사", 2, 125, 17, 22),
            new("피에 굶주린 까마귀", 2, 90, 14, 12),

            // 레벨 3 몬스터
            new("해골 병사", 3, 100, 19, 15),
            new("파묻힌 유령", 3, 80, 25, 20),
            new("폐허의 수호자", 3, 200, 10, 30), 
            new("저주받은 기사", 3, 150, 22, 25),
            new("사령술사", 3, 120, 20, 18),
            new("불멸의 망령", 3, 160, 23, 27),
            new("검은 늑대왕", 3, 180, 24, 22),
            new("붉은 드레이크", 3, 170, 26, 28),
            new("죽음의 감시자", 3, 190, 21, 26),
            new("절망의 그림자", 3, 210, 27, 24)
        ];

        /// <summary>
        /// 레벨에 따른 몬스터 리스트 반환
        /// </summary>
        /// <param name="level"> 던전 레벨이 들어가야 함 </param>
        /// <returns></returns>
        public List<Monster> GetMonsterByLevel(int level)
        {
            return monsters.Where(m => m.Level == level).ToList();
        }

        /// <summary>
        /// 랜덤 몬스터 스폰
        /// </summary>
        /// <param name="dungeon"> 현재 던전의 정보 </param>
        /// <returns></returns>
        public List<Monster> RandomMonsterSpawn(Dungeon dungeon)
        {
            List<Monster> list = new List<Monster>();
            // 전투 개시 시 몬스터 랜덤 할당
            // 1~4마리 몬스터를 랜덤으로 선언?
            int[] arr = new int[RandomGenerator.Instance.Next(1, 5)]; // 0~3 정해주고 몬스터 갯수

            List<Monster> encounter = this.GetMonsterByLevel(dungeon.Level);

            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] = RandomGenerator.Instance.Next(0, encounter.Count); // rand = 몬스터 종류를 정해주는거 // if  arr[0] = 2
                Monster newMonster = new Monster(encounter[arr[i]].Name, encounter[arr[i]].Level, encounter[arr[i]].MaxHp, encounter[arr[i]].Damage, encounter[arr[i]].Defense);
                list.Add(newMonster);
            }
            return list;
        }
    }
}
