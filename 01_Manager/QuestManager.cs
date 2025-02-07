﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class QuestManager : Singleton<QuestManager>
    {
        // KillQuest의 Json파일, ItemQuest의 Json파일을 분리해서 관리
        public KillQuest[] killQuests { get; private set; }
        public ItemQuest[] itemQuests { get; private set; }

        public Quest? selectQuest;

        public QuestManager()
        {

        }

        public void LoadQuest(string itemQuestJson, string killQuestJson)
        {
            itemQuests = JsonConvert.DeserializeObject<ItemQuest[]>(itemQuestJson);
            killQuests = JsonConvert.DeserializeObject<KillQuest[]>(killQuestJson);
        }

        /// <summary>
        /// _town 마을의 퀘스트 리스트 출력
        /// </summary>
        /// <param name="_town"></param>
        public void ShowQuestList(TownName _town)
        {
            int questCount = 1;
            string questStateText;

            foreach(KillQuest? quest in killQuests)
            {
                if (quest == null || quest?.questTown != _town)
                    continue;

                questStateText = quest.questAccpet ? "진행중" : "수락가능";
                questStateText = quest.questComplete ? "완료" : questStateText;
                Console.WriteLine($"{questCount++}. {quest.questTitle}  |  {questStateText}");
            }

            foreach (ItemQuest? quest in itemQuests)
            {
                if (quest == null || quest?.questTown != _town)
                    continue;

                questStateText = quest.questAccpet ? "진행중" : "수락가능";
                questStateText = quest.questComplete ? "완료" : questStateText;
                Console.WriteLine($"{questCount++}. {quest.questTitle}  |  {questStateText}");
            }
        }


        /// <summary>
        /// 선택된 퀘스트의 정보를 출력해주는 메서드
        /// </summary>
        public void ShowQuestInformation()
        {
            if (selectQuest == null)
                return;

            Console.WriteLine($"-- {selectQuest.questTown} --");     // 퀘스트 진행 마을
            Console.WriteLine($"퀘스트 {selectQuest.questTitle}");   // 퀘스트 명
            Console.WriteLine($"{selectQuest.questDescription}");   // 퀘스트 설명

            // 수락한 퀘스트일때
            if (selectQuest.questAccpet)
            {
                Console.WriteLine($"----퀘스트진행도----");
                selectQuest.QuestProgress();                                // 퀘스트 진행도 확인
                Console.WriteLine($"\n1. 퀘스트 완료");
            }
            else
            {
                Console.WriteLine("\n1. 퀘스트 수락");
            }
        }

        /// <summary>
        /// 퀘스트 선택 메서드 정상적인 입력이라면 selectQuest에 저장됨
        /// </summary>
        public bool SelectQuest(TownName _town, int index)
        {
            int questCount = 1;

            foreach(KillQuest? quest in killQuests)
            {
                if (quest == null || quest.questTown != _town)
                    continue;

                if (questCount == index)
                {
                    selectQuest = quest;
                    if (quest.questComplete)
                        return false;

                    return true;
                }

                questCount++;
            }

            foreach (ItemQuest? quest in itemQuests)
            {
                if (quest == null || quest.questTown != _town)
                    continue;

                if (questCount == index)
                {
                    selectQuest = quest;
                    if (quest.questComplete)
                        return false;

                    return true;
                }

                questCount++;
            }
            return false;
        }

        /// <summary>
        /// 선택된 퀘스트의 수락 or 완료
        /// </summary>
        public bool SelectQuestAccept()
        {
            if (selectQuest == null)
                return false;

            if(selectQuest.questAccpet)
            {
                // 퀘스트 완료 성공시
                if(selectQuest.QuestComplete())
                    return false;
                
                //퀘스트 완료 실패시
                return true;
            }
            else
            {
                // 선택된 퀘스트 수락
                selectQuest.questAccpet = true;
                return true;
            }
        }

        public void MonsterKillCount(Monster _monster)
        { 
        
        }
    }
}
