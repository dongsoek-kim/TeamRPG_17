﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public abstract class Quest
    {
        // 퀘스트 데이터
        public TownName questTown;      // 퀘스트 위치
        public bool questComplete;      // 퀘스트 완료 여부
        public bool questAccpet;        // 퀘스트 수락 여부
        public bool questRepeatable;    // 퀘스트 반복 가능여부

        // 퀘스트 정보
        public string questTitle;       // 퀘스트명
        public string questDescription; // 퀘스트설명

        // 연계퀘스트 데이터
        public bool questAccess;        // 퀘스트 접근 가능여부
        public string? preQuestTitle;   // 사전 퀘스트명

        // 퀘스트 보상
        public int exp;                 // 퀘스트 경험치 보상
        public int gold;                // 퀘스트 골드 보상
        public ItemName? rewardItem;    // 퀘스트 아이템 보상

        /// <summary>
        /// 퀘스트의 진행도를 출력해주는 메서드
        /// </summary>
        public abstract void QuestProgress();

        /// <summary>
        /// 퀘스트를 완료 처리 메서드
        /// </summary>
        /// <returns>완료 성공 여부 반환</returns>
        public abstract bool QuestComplete();

        /// <summary>
        /// 퀘스트 완료 가능여부 확인
        /// </summary>
        /// <returns>완료 가능여부 반환</returns>
        public abstract bool QuestCheck();

        /// <summary>
        /// 퀘스트 보상 출력  
        /// </summary>
        public virtual void ShowQuestReward()
        {
            Render.ColorWriteLine($"경험치 : {exp}\n골드 : {gold}",ConsoleColor.Yellow);
            if(rewardItem != null)
                Render.ColorWriteLine($"아이템 : {rewardItem}",ConsoleColor.Yellow);
        }
    }
}
