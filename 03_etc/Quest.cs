using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public abstract class Quest
    {
        // 퀘스트 데이터
        public TownName questTown;  // 퀘스트 위치
        public bool questComplete;  // 퀘스트 완료 여부
        public bool questAccpet;    // 퀘스트 수락 여부

        // 퀘스트 정보
        public string questTitle;       // 퀘스트명
        public string questDescription; // 퀘스트설명

        // 퀘스트 보상
        public int exp;     // 퀘스트 경험치 보상
        public int gold;    // 퀘스트 골드 보상
        // TODO: 퀘스트 아이템 보상 추가하기
        // 아이템 파트 어느정도 만들어졌을때 추가예정

        public Quest(TownName _town, string _questTitle, string _questDescription, int _exp, int _gold)
        {
            questTown = _town;
            questTitle = _questTitle;
            questDescription = _questDescription;
            gold = _gold;
            exp = _exp;
        }

        /// <summary>
        /// 퀘스트의 진행도를 출력해주는 메서드
        /// </summary>
        public abstract void QuestProgress();
        public abstract bool QuestComplete();
    }
}
