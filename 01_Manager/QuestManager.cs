using Newtonsoft.Json;
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
        public KillQuest[] killQuests { get; private set; }     // 킬 퀘스트 배열
        public ItemQuest[] itemQuests { get; private set; }     // 아이템 퀘스트 배열

        private List<Quest> quests;                             // 킬 + 아이템 퀘스트 리스트

        public Quest? selectQuest { get; private set; }         // 현재 선택한 퀘스트 참조

        public string? newQuestString { get; set; }             // 신규 퀘스트 출력용 문자열 변수


        public void LoadQuest(string itemQuestJson, string killQuestJson)
        {
            killQuests = JsonConvert.DeserializeObject<KillQuest[]>(killQuestJson);
            itemQuests = JsonConvert.DeserializeObject<ItemQuest[]>(itemQuestJson);

            quests = new List<Quest>();
            for (int i = 0; i < killQuests.Length; i++)
                quests.Add(killQuests[i]);
            for(int i = 0; i < itemQuests.Length; i++)
                quests.Add(itemQuests[i]);
        }

        /// <summary>
        /// _town 마을의 수락가능/진행중 퀘스트 리스트 출력
        /// </summary>
        /// <param name="_town">출력 대상 퀘스트 마을 enum</param>
        public void ShowQuestList(TownName _town)
        {
            int questCount = 1;
            string questStateText;
            ConsoleColor questStateColor;

            foreach (Quest? quest in quests)
            {
                questStateText = "수락가능";
                questStateColor = ConsoleColor.Yellow;

                // null 예외  // 다른 마을일떄 
                if (quest == null || quest?.questTown != _town)
                    continue;

                // 이미 클리어한 퀘스트  // 접근 불가능한 퀘스트
                if (quest.questComplete || !quest.questAccess)
                    continue;

                // 퀘스트 수락한 퀘스트
                if(quest.questAccpet)
                {
                    questStateText = "진행중";
                    questStateColor = ConsoleColor.Magenta;
                }

                Console.Write($"{questCount++}. {quest.questTitle}");
                Render.ColorWrite($"  |  {questStateText}  ", questStateColor);

                // 반복퀘스트일때 추가 출력후 줄바꿈
                if (quest.questRepeatable)
                    Render.ColorWrite($"|  반복 퀘스트", ConsoleColor.DarkGray);
                Console.WriteLine();
            }
        }

        /// <summary>
        /// _town 마을의 완료한 퀘스트 리스트 출력
        /// </summary>
        /// <param name="_town">>출력 대상 퀘스트 마을 enum</param>
        public void ShowEndQuestList(TownName _town)
        {
            foreach(Quest? quest in quests)
            {
                // null / 현재마을퀘스트 X
                if (quest == null || quest?.questTown != _town)
                    continue;

                // 완료하지않은 퀘스트 X
                if (!quest.questComplete)
                    continue;

                Console.Write($"- {quest.questTitle}  |  ");
                Render.ColorWrite("완료\n", ConsoleColor.DarkGray);
            }
        }


        /// <summary>
        /// 선택된 퀘스트 정보를 출력
        /// </summary>
        public void ShowQuestInformation()
        {
            if (selectQuest == null)
                return;

            // 퀘스트 명 / 퀘스트 설명
            Render.ColorWriteLine($"\n{selectQuest.questTitle}",ConsoleColor.Cyan);    
            Render.AnimationWriteLine($"{selectQuest.questDescription}\n",2f, true);

            // 보상
            Render.ColorWriteLine("~~~~~보상~~~~~",ConsoleColor.DarkGray);
            selectQuest.ShowQuestReward();

            // 수락한 퀘스트일때
            if (selectQuest.questAccpet)
            {
                Render.ColorWriteLine($"\n~~~~~퀘스트 진행률~~~~~", ConsoleColor.DarkGray);
                selectQuest.QuestProgress();   // 퀘스트 진행도 확인
                Console.WriteLine("─────────────────────────");

                // 퀘스트 완료 가능하다면 퀘스트완료 선택지 추가
                if (selectQuest.QuestCheck())
                    Console.WriteLine($"1. 퀘스트 완료\n");
            }
            else
            {
                Console.WriteLine("\n─────────────────────────");
                Console.WriteLine("1. 퀘스트 수락\n");
            }
        }

        /// <summary>
        /// 퀘스트 선택 처리 메서드
        /// </summary>
        /// <param name="_town">현재 마을 enum</param>
        /// <param name="index">선택한 번호</param>
        /// <returns>퀘스트 선택 성공 여부</returns>
        public bool SelectQuest(TownName _town, int index)
        {
            int questCount = 1;

            foreach(Quest? quest in quests)
            {
                // null / 현재마을 X
                if (quest == null || quest.questTown != _town)
                    continue;

                // 이미 완료한 퀘스트 선택 X
                // 수락권한 없는 퀘스트 선택 X
                if (quest.questComplete || !quest.questAccess)
                    continue;

                if (questCount == index)
                {
                    selectQuest = quest;
                    return true;
                }

                questCount++;
            }

            return false;
        }

        /// <summary>
        /// 선택된 퀘스트의 완료 or 수락 처리
        /// </summary>
        /// <returns>완료 false // 수락 true 반환</returns>
        public bool QuestAccept()
        {
            if (selectQuest == null)
                return false;

            // 퀘스트를 수락한 상태일때
            if(selectQuest.questAccpet)
            {
                // 퀘스트 완료 성공
                if(selectQuest.QuestComplete())
                {
                    CheckPreQuest(selectQuest.questTitle);
                    return false;
                }
                
                //퀘스트 완료 실패
                return true;
            }

            // 퀘스트를 수락하지않은 상태일때
            else
            {
                // 선택된 퀘스트 수락 true
                selectQuest.questAccpet = true;
                return true;
            }
        }

        /// <summary>
        /// 사전 퀘스트 처리 메서드 
        /// </summary>
        /// <param name="_questTitle">완료한 퀘스트 타이틀</param>
        public void CheckPreQuest(string _questTitle)
        {
            foreach (Quest? quest in quests)
            {
                // null 예외
                if (quest == null)
                    continue;

                // 이미 퀘스트 권한 O / 사전퀘스트 X
                if (quest.questAccess || quest.preQuestTitle == null)
                    continue;

                if(quest.preQuestTitle.Equals(_questTitle))
                {
                    quest.questAccess = true;
                    newQuestString += $"- {quest.questTitle}\n";
                }
            }
        }

        /// <summary>
        /// 퀘스트 클리어 여부 반환 함수
        /// </summary>
        /// <param name="_questTitle">확인할 퀘스트 타이틀</param>
        /// <returns>클리어 여부 반환</returns>
        public bool IsClearQuest(string _questTitle)
        {
            foreach (Quest? quest in quests)
            {
                // null 예외
                if (quest == null)
                    continue;

                // 동일한 퀘스트명
                if(quest.questTitle.Equals(_questTitle))
                    return quest.questComplete;
            }

            return false;
        }

        /// <summary>
        /// 킬 퀘스트 진행도 업데이트 처리
        /// </summary>
        /// <param name="_monster">사먕한 몬스터</param>
        public void MonsterKillCount(Monster _monster)
        {
            // Killquest 중 수락한 퀘스트
            foreach (KillQuest quest in killQuests)
            {
                if(quest.questAccpet)
                    quest.QuestUpdate(_monster);
            }
        }
    }
}
