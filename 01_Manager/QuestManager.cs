using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class QuestManager : Singleton<QuestManager>
    {
        // KillQuest의 Json파일, ItemQuest의 Json파일을 분리해서 관리
        private KillQuest[] killQuests;
        private ItemQuest[] itemQuests;

        public Quest selectQuest;

        public QuestManager()
        {
            LoadQuestData();    // 퀘스트 데이터 로드
        }

        public void LoadQuestData()
        {
            // 임시 퀘스트 데이터
            killQuests = new KillQuest[6];
            killQuests[0] = new KillQuest(
                TownName.Elinia, "테스트퀘스트1_kill", "테스트퀘스트1입니다.\n몬스터1 5마리",
                10, 10, "몬스터1", 5);

            killQuests[1] = new KillQuest(
                TownName.Elinia, "테스트퀘스트2_kill", "테스트퀘스트2입니다.\n몬스터2 10마리",
                20, 10, "몬스터2", 10);

            killQuests[2] = new KillQuest(
                TownName.Hannesys, "테스트퀘스트3_kill", "테스트퀘스트3입니다.\n몬스터3 15마리",
                30, 10, "몬스터3", 15);

            killQuests[3] = new KillQuest(
                TownName.Hannesys, "테스트퀘스트4_kill", "테스트퀘스트4입니다.\n몬스터4 20마리",
                40, 10, "몬스터4", 20);

            killQuests[4] = new KillQuest(
                TownName.CunningCity, "테스트퀘스트5_kill", "테스트퀘스트5입니다.\n몬스터5 25마리",
                50, 10, "몬스터5", 25);

            killQuests[5] = new KillQuest(
                TownName.CunningCity, "테스트퀘스트6_kill", "테스트퀘스트6입니다.\n몬스터6 30마리",
                60, 10, "몬스터6", 30);


            itemQuests = new ItemQuest[6];
            itemQuests[0] = new ItemQuest(
                TownName.Elinia, "테스트퀘스트1_item", "테스트퀘스트1입니다.\n아이템주세요",
                10, 10, ItemName.TrashArmor
                );

            itemQuests[1] = new ItemQuest(
                TownName.Elinia, "테스트퀘스트2_item", "테스트퀘스트2입니다.\n아이템주세요",
                20, 10, ItemName.IronArmor
                );

            itemQuests[2] = new ItemQuest(
                TownName.Hannesys, "테스트퀘스트3_item", "테스트퀘스트3입니다.\n아이템주세요",
                30, 10, ItemName.WoodenStick
                );

            itemQuests[3] = new ItemQuest(
                TownName.Hannesys, "테스트퀘스트4_item", "테스트퀘스트4입니다.\n아이템주세요",
                40, 10, ItemName.IronArmor
                );

            itemQuests[4] = new ItemQuest(
                TownName.CunningCity, "테스트퀘스트5_item", "테스트퀘스트5입니다.\n아이템주세요",
                50, 10, ItemName.BronzeAxe
                );

            itemQuests[5] = new ItemQuest(
                TownName.CunningCity, "테스트퀘스트6_item", "테스트퀘스트6입니다.\n아이템주세요",
                60, 10, ItemName.OldSword
                );
        }

        public void ShowQuestList(TownName _town)
        {
            int questCount = 1;

            // 킬 퀘스트 리스트 출력
            for (int i = 0; i < killQuests.Length; i++)
            {
                // killQuest[i]가 null이 아님 & 현재 마을의 퀘스트일때
                if (killQuests[i]?.questTown == _town)
                    Console.WriteLine($"{questCount++}. {killQuests[i].questTitle}");
            }

            // 아이템 퀘스트 출력
            for (int i = 0; i < itemQuests.Length; i++)
            {
                if (itemQuests[i]?.questTown == _town)
                    Console.WriteLine($"{questCount++}. {itemQuests[i].questTitle}");
            }
        }

        /// <summary>
        /// 선택된 퀘스트의 정보를 출력해주는 메서드
        /// </summary>
        public void ShowQuestInformation()
        {
            if (selectQuest == null)
                return;

            Console.WriteLine($"{selectQuest.questTown} 의 퀘스트");    // 퀘스트 진행 마을
            Console.WriteLine($"-- {selectQuest.questTitle} --");      // 퀘스트 명
            Console.WriteLine($"{selectQuest.questDescription}");      // 퀘스트 설명
            Console.WriteLine($"----퀘스트진행도----");   
            selectQuest.QuestProgress();                               // 퀘스트 진행도 확인
        }

        /// <summary>
        /// 퀘스트 선택 메서드 정상적인 입력이라면 selectQuest에 저장됨
        /// </summary>
        public bool SelectQuest(TownName _town, int index)
        {
            int questCount = 1;

            for(int i = 0; i < killQuests.Length; i++)
            {
                if (killQuests[i] == null || killQuests[i].questTown != _town)
                    continue;

                if(questCount == index)
                {
                    selectQuest = killQuests[i];
                    return true;
                }

                questCount++;
            }

            for (int i = 0; i < itemQuests.Length; i++)
            {
                if (itemQuests[i] == null || itemQuests[i].questTown != _town)
                    continue;

                if (questCount == index)
                {
                    selectQuest = itemQuests[i];
                    return true;
                }

                questCount++;
            }


            return false;
        }
    }
}
