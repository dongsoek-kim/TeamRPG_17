using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17._03_etc
{
    internal class Town
    {
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
        public Town(string _name, string _townDescription, int _entryLevel, int _startItemIdx, int _count)
        {
            name = _name;
            townDescription = _townDescription;
            entryLevel = _entryLevel;
            startItemIdx = _startItemIdx;
            count = _count;
        }
        
        public bool CanEnterTown()
        {
            return (GameManager.Instance.player.level >= EntryLevel);
        }

        
        public bool CanGetItem()
        {
            // 예외 처리 : startItemIdx + count가 저장된 아이템 배열크기보다 크면
            return (startItemIdx + count < ItemManager.Instance.itemLength);
        }
    }
}
