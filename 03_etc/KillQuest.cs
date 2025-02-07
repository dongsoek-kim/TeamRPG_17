using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class KillQuest : Quest
    {
        // 수정필요 @@
        public string monsterName;  // 잡아야하는 몬스터 이름
        public int monsterCount;    // 잡아야하는 몬스터 수
        public int killCount;    // 잡아야하는 몬스터 수

        public KillQuest(TownName _town, string _questTitle, string _questDescription, int _exp, int _gold
            , string _monsterName, int _count) 
            : base(_town, _questTitle, _questDescription, _exp, _gold)
        {
            monsterName = _monsterName;
            monsterCount = _count;
            killCount = 0;
        }

        public override void QuestProgress()
        {
            Console.WriteLine($"{monsterName} ( {killCount} / {monsterCount})");
        }

        public override bool QuestComplete()
        {
            questComplete = true;
            return true;
        }

        public void QuestUpdate(string _targetName)
        {
            if (monsterName == _targetName)
            {
                killCount++;

                // 킬카운터가 퀘스트 요구치를 넘지않도록 예외처리
                if(killCount > monsterCount)
                {
                    killCount = monsterCount;
                }
            }
        }
    }
}
