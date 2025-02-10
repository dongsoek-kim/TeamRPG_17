using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class ItemQuest : Quest
    {
        // 퀘스트완료에 필요한 아이템
        public string[] questItem;

        public override void QuestProgress()
        {
            foreach (string item in questItem)
            {
                string canloadItem = "아이템이없습니다.";
                if (GameManager.Instance.player.inventory.haveItem(item))
                    canloadItem = "제출가능";
                Console.WriteLine($"{item} ( {canloadItem} )\n");
            }
        }

        public override bool QuestComplete()
        {
            if(QuestCheck())
            {
                // 아이템 반납 및 퀘스트완료
                questComplete = true;
                foreach(string item in questItem)
                    GameManager.Instance.player.inventory.DeleteItem(item);

                // 보상 지급
                GameManager.Instance.player.AddExp(exp);
                GameManager.Instance.player.gold += gold;
                if (rewardItem != null)
                    GameManager.Instance.player.inventory.AddItem(rewardItem);

                // 반복 가능한 퀘스트일때
                if (questRepeatable)
                {
                    questComplete = false;
                    questAccpet = false;
                }

                return true;
            }

            return false;
        }

        public override bool QuestCheck()
        {
            // 퀘스트 아이템 리스트에서 하나라도 부족하면 false
            // 퀘스트 아이템 전부 가지고있으면 true
            for(int i = 0; i < questItem.Length; i++)
            {
                if (GameManager.Instance.player.inventory.haveItem(questItem[i]))
                    continue;

                return false;
            }

            return true;
        }
    }
}
