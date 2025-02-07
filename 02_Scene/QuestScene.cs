using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class QuestScene : Scene
    {
        private bool questInformation;

        public QuestScene() 
        {
            questInformation = false;
        }

        public override void Update()
        {
            if (questInformation)
                QuestInformation(); // 선택된 퀘스트 정보 확인
            else
                QuestList();    // 퀘스트 리스트 출력

        }

        public void QuestList()
        {
            Console.Clear();
            Console.WriteLine("퀘스트");
            Console.WriteLine("퀘스트 수락 및 완료");
            Console.WriteLine("─────────────────────────");
            QuestManager.Instance.ShowQuestList(TownName.Elinia);
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("0. 나가기");

            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch(intCommand)
            {
                case 0:
                    GameManager.Instance.ChangeScene(SceneName.LobbyScene);
                    break;

                default:
                    if(QuestManager.Instance.SelectQuest(TownName.Elinia, intCommand))
                        questInformation = true;
                    break;
            }
        }

        public void QuestInformation()
        {
            Console.Clear();
            Console.WriteLine("퀘스트 정보");
            Console.WriteLine("─────────────────────────");
            QuestManager.Instance.ShowQuestInformation();
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("0. 나가기");

            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch (intCommand)
            {
                case 0:
                    questInformation = false;
                    break;

                default:
                    break;
            }
        }
    }
}
