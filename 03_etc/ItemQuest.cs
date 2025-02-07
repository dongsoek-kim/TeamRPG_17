using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class ItemQuest : Quest
    {
        // 퀘스트완료에 필요한 아이템 enum
        public ItemName questItem;

        public ItemQuest(TownName _town, string _questTitle, string _questDescription, int _exp, int _gold
            , ItemName _questItem)
            : base(_town, _questTitle, _questDescription, _exp, _gold)
        {
            questItem = _questItem;
        }

        public override void QuestProgress()
        {
            // TODO: 인벤토리에서 questItem을 가지고있는지 확인해서 넣어주기
            Console.WriteLine($"{questItem} ( 미보유 / 보유 )");
        }
        public override void QuestComplete()
        {
            
        }
    }
}
