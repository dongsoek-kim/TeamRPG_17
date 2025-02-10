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
        public string monsterName { get; private set; } // 잡아야하는 몬스터 이름
        public int monsterCount { get; private set; }   // 잡아야하는 몬스터 수
        public int killCount { get; private set; }      // 잡아야하는 몬스터 수

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
            Console.WriteLine($"{monsterName} - ( {killCount} / {monsterCount})\n");
        }

        public override bool QuestComplete()
        {
            // 몬스터 사냥횟수 충족
            if(killCount >= monsterCount)
            {
                questComplete = true;

                GameManager.Instance.player.AddExp(exp);
                GameManager.Instance.player.gold += gold;

                return true;
            }
            return false;
        }

        public void QuestUpdate(Monster _target)
        {
            // 죽은 몬스터가 해당 퀘스트의 목표 몬스터가 다르면 조기리턴
            if (_target.Name != monsterName)
                return;

            killCount++;

            // 킬카운터가 퀘스트 요구치를 넘지않도록 예외처리
            if (killCount > monsterCount)
                killCount = monsterCount;
        }

        public override bool QuestCheck()
        {
            // 몬스터수 충족
            if (killCount >= monsterCount)
                return true;

            return false;
        }
    }
}
