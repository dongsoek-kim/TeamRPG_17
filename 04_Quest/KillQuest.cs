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

        /// <summary>
        /// 퀘스트의 진행도를 출력해주는 메서드
        /// </summary>
        public override void QuestProgress()
        {
            for (int i = 0; i < monsterName.Length; i++)
            {
                Console.WriteLine($"{monsterName[i]} - ( {killCount[i]} / {monsterCount[i]} )");
            }
            Console.WriteLine();
        }

        /// <summary>
        /// 퀘스트를 완료 처리 메서드
        /// </summary>
        /// <returns>완료 성공 여부 반환</returns>
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
        
        /// <summary>
        /// 사냥 퀘스트의 진행도 업데이트 
        /// </summary>
        /// <param name="_target">확인할 사망한 몬스터</param>
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

        /// <summary>
        /// 퀘스트 완료 가능여부 확인
        /// </summary>
        /// <returns>완료 가능여부 반환</returns>
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
