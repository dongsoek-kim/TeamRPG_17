using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class Town
    {
        public int id { get; private set; }
        public string name { get; private set; }
        public string townDescription { get; private set; }
        public int entryLevel { get; private set; }

        public int startItemIdx { get; private set; }
        public int count { get; private set; }

        /// <summary>
        /// 도시 객체 생성
        /// </summary>
        /// <param name="_name">도시 이름</param>
        /// <param name="_townDescription">도시 설명</param>
        /// <param name="_entryLevel">도시 입장 레벨</param>
        /// <param name="_startItemIdx">아이템 배열에서 얻어올 아이템 시작 인덱스</param>
        /// <param name="_count">몇개 출력을 원하는지</param>
        public Town(TownName _id, string _name, string _townDescription, int _entryLevel, int _startItemIdx, int _count)
        {
            id = (int)_id;
            name = _name;
            townDescription = _townDescription;
            entryLevel = _entryLevel;
            startItemIdx = _startItemIdx;
            count = _count;
        }

        /// <summary>
        /// 내가 가고자하는 마을에 갈 수 있는지 
        /// 레벨에 따라서 판단하는 메서드입니다.
        /// </summary>
        /// <returns>true or false</returns>
        public bool CanEnterTown()
        {
            return (GameManager.Instance.player.level >= entryLevel);
        }

        /// <summary>
        /// 현재 타운에서 상점에 있는 아이템을 가져올 수 있는지 
        /// 인덱스의 범위에 따라서 판단하는 메서드입니다.
        /// </summary>
        /// <returns>true or false</returns>
        public bool CanGetItem()
        {
            return (startItemIdx + count < ItemManager.Instance.itemLength);
        }
    }
}
