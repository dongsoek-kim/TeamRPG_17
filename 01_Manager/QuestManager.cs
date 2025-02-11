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
        // KillQuest의 Json파일, ItemQuest의 Json파일을 분리해서 관리
        public KillQuest[] killQuests { get; private set; }
        public ItemQuest[] itemQuests { get; private set; }

        public Quest? selectQuest { get; set; }

        public void LoadQuest(string itemQuestJson, string killQuestJson)
        {
            killQuests = JsonConvert.DeserializeObject<KillQuest[]>(killQuestJson);
            itemQuests = JsonConvert.DeserializeObject<ItemQuest[]>(itemQuestJson);
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

                if (quest.questComplete)
                    continue;

                questStateText = quest.questAccpet ? "진행중" : "수락가능";
                Console.Write($"{questCount++}. {quest.questTitle}  |  ");
                Render.ColorWrite($"{questStateText}\n",ConsoleColor.Cyan);
            }

            foreach (ItemQuest? quest in itemQuests)
            {
                if (quest == null || quest?.questTown != _town)
                    continue;

                if (quest.questComplete)
                    continue;

                questStateText = quest.questAccpet ? "진행중" : "수락가능";
                Console.Write($"{questCount++}. {quest.questTitle}  |  ");
                Render.ColorWrite($"{questStateText}\n", ConsoleColor.Cyan);
            }
        }

        public void ShowEndQuestList(TownName _town)
        {
            foreach(KillQuest? quest in killQuests)
            {
                // null / 현재마을퀘스트 X
                if (quest == null || quest?.questTown != _town)
                    continue;

                // 완료하지않은 퀘스트 X
                if (!quest.questComplete)
                    continue;

                Console.Write($"- {quest.questTitle}  |  ");
                Render.ColorWrite("완료\n", ConsoleColor.Cyan);
            }
            foreach (ItemQuest? quest in itemQuests)
            {
                // null / 현재마을퀘스트 X
                if (quest == null || quest?.questTown != _town)
                    continue;

                // 완료하지않은 퀘스트 X
                if (!quest.questComplete)
                    continue;

                Console.Write($"- {quest.questTitle}  |  ");
                Render.ColorWrite("완료\n", ConsoleColor.Cyan);
            }
        }


        /// <summary>
        /// 선택된 퀘스트의 정보를 출력해주는 메서드
        /// </summary>
        public void ShowQuestInformation()
        {
            if (selectQuest == null)
                return;

            Render.ColorWriteLine($"\n{selectQuest.questTitle}",ConsoleColor.Cyan);
            Console.WriteLine($"{selectQuest.questDescription}\n");   // 퀘스트 설명
            selectQuest.ShowQuestReward();                          // 퀘스트 보상

            // 수락한 퀘스트일때
            if (selectQuest.questAccpet)
            {
                Console.WriteLine($"\n~~~~~퀘스트 진행률~~~~~");
                selectQuest.QuestProgress();                // 퀘스트 진행도 확인

                // 퀘스트 완료 가능하다면 퀘스트완료 선택지 추가
                if(selectQuest.QuestCheck())
                    Console.WriteLine($"1. 퀘스트 완료\n");
            }
            else
            {
                Console.WriteLine("\n1. 퀘스트 수락\n");
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
                // null / 현재마을 X
                if (quest == null || quest.questTown != _town)
                    continue;

                // 이미 완료한 퀘스트 선택 X
                if (quest.questComplete)
                    continue;

                if (questCount == index)
                {
                    selectQuest = quest;
                    return true;
                }

                questCount++;
            }

            foreach (ItemQuest? quest in itemQuests)
            {
                // null / 현재마을 X
                if (quest == null || quest.questTown != _town)
                    continue;

                // 이미 완료한 퀘스트 선택 X
                if (quest.questComplete)
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
        /// 선택된 퀘스트의 수락 or 완료
        /// </summary>
        public bool QuestAccept()
        {
            if (selectQuest == null)
                return false;

            // 퀘스트를 수락한 상태일때
            if(selectQuest.questAccpet)
            {
                // 퀘스트 완료 성공
                if(selectQuest.QuestComplete())
                    return false;
                
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
