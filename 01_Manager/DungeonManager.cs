using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class DungeonManager : Singleton<DungeonManager>
    {
        /// <summary>
        /// 던전 리스트
        /// </summary>
        private List<Dungeon> dungeons = new List<Dungeon>
        {
            new Dungeon("시작의 숲", 1, "작은 마물과 짐승이 있는 조금 평화로운 숲"),
            new Dungeon("어둠의 동굴", 2, "깊은 어둠이 감싸고 있는 동굴"),
            new Dungeon("고대의 폐허", 3, "고대 문명의 잔해가 남아있는 수수께끼의 폐허"),

        };

        /// <summary>
        /// Town의 레벨에 따른 던전 반환
        /// </summary>
        /// <param name="level"> 타운 레벨</param>
        /// <returns></returns>
        public Dungeon GetDungeonByTownLevel(int level)
        {
            return dungeons.OrderBy(d => Math.Abs(d.Level - level)).FirstOrDefault();
        }
    }
}
