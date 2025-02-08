using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class ItemQuest : Quest
    {
        // 퀘스트완료에 필요한 아이템 이름
        public string questItem;

        public ItemQuest(TownName _town, string _questTitle, string _questDescription, int _exp, int _gold
            , string _questItem)
            : base(_town, _questTitle, _questDescription, _exp, _gold)
        {
            questItem = _questItem;
        }

        public override void QuestProgress()
        {
            // TODO:
            // 인벤토리에서 questItem을 가지고있는지 확인
            // 가지고있다면 보유 / 없다면 미보유로 출력

            // 변수명을 못정하겠어요
            string canloadItem = "아이템이없습니다.";
            if (GameManager.Instance.player.inventory.haveItem(questItem))
                canloadItem = "제출가능";
            Console.WriteLine($"{questItem} ( {canloadItem} )");
        }
        public override bool QuestComplete()
        {
            // TODO :
            // 인벤토리에서 questItem을 가지고있는지 확인
            // 가지고있다면
            // questComplete = true 
            // 퀘스트보상 지급

            // 아이템 보유중일때
            if (GameManager.Instance.player.inventory.haveItem(questItem))
            {
                questComplete = true;
                GameManager.Instance.player.inventory.DeleteItem(questItem);
            }
            else
                questComplete = false;

            return questComplete;
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
