using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class QuestManager : Singleton<QuestManager>
    {
        // Json이 다형성을 지원하지않는다는 글을 보고 분리시켰습니다.
        // KillQuest의 Json파일, ItemQuest의 Json파일을 분리해서 관리할예정입니다.

        private KillQuest[] killQuests;
        private ItemQuest[] ItemQuests;

        public QuestManager()
        {
            
        }

        public void LoadQuestData()
        {

        }
    }
}
