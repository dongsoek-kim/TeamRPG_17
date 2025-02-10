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
        public string[] monsterName; // 잡아야하는 몬스터 이름
        public int[] monsterCount;   // 잡아야하는 몬스터 수
        public int[] killCount;      // 잡은 몬스터 수

        public override void QuestProgress()
        {
            for (int i = 0; i < monsterName.Length; i++)
            {
                Console.WriteLine($"{monsterName[i]} - ( {killCount[i]} / {monsterCount[i]} )");
            }
            Console.WriteLine();
        }

        public override bool QuestComplete()
        {
            if(QuestCheck())
            {
                // 퀘스트 완료
                questComplete = true;

                // 보상 지급
                GameManager.Instance.player.AddExp(exp);
                GameManager.Instance.player.gold += gold;
                if(rewardItem != null)
                    GameManager.Instance.player.inventory.AddItem(rewardItem);

                // 반복 가능한 퀘스트일때
                if(questRepeatable)
                {
                    questComplete = false;
                    questAccpet = false;
                }

                return true;
            }

            return false;
        }

        public void QuestUpdate(Monster _target)
        {
            for (int i = 0; i < monsterName.Length; i++)
            {
                if (_target.Name != monsterName[i])
                    continue;

                killCount[i]++;

                if (killCount[i] > monsterCount[i])
                    killCount[i] = monsterCount[i];
            }
        }

        public override bool QuestCheck()
        {
            for (int i = 0; i < monsterName.Length; i++)
            {
                if (killCount[i] >= monsterCount[i])
                    continue;

                return false;
            }

            return true;
        }
    }
}
