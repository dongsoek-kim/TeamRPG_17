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
        public string questItem;

        public override void QuestProgress()
        {
            string canloadItem = "아이템이없습니다.";
            if (GameManager.Instance.player.inventory.haveItem(questItem))
                canloadItem = "제출가능";
            Console.WriteLine($"{questItem} ( {canloadItem} )\n");
        }

        public override bool QuestComplete()
        {
            if (GameManager.Instance.player.inventory.haveItem(questItem))
            {
                questComplete = true;
                GameManager.Instance.player.inventory.DeleteItem(questItem);

                GameManager.Instance.player.AddExp(exp);
                GameManager.Instance.player.gold += gold;
                if (rewardItem != null)
                    GameManager.Instance.player.inventory.AddItem(rewardItem);
                return true;
            }

            return false;
        }

        public override bool QuestCheck()
        {
            // 아이템 가지고있으면 true
            // 없으면 false
            if (GameManager.Instance.player.inventory.haveItem(questItem))
                return true;
            else
                return false;
        }
    }
}
