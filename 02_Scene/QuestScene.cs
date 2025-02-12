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
        private bool questReward;
        public QuestScene() 
        {
            questInformation = false;
            questReward = false;
        }

        public override void Update()
        {
            if (questInformation)
                QuestInformation();

            else if(questReward) 
                QuestReward();      

            else
                QuestList();        

        }

        /// <summary>
        /// 퀘스트 리스트 출력
        /// </summary>
        public void QuestList()
        {
            Console.Clear();
            Render.ColorWriteLine("퀘스트", ConsoleColor.Cyan);
            Console.WriteLine("퀘스트 수락 및 완료 할 수 있습니다.");
            Console.WriteLine("─────────────────────────");
            Render.ColorWriteLine("진행가능한 퀘스트", ConsoleColor.Yellow);
            QuestManager.Instance.ShowQuestList((TownName)GameManager.Instance.currentTown.id);
            Console.WriteLine("─────────────────────────");
            Render.ColorWriteLine("완료한 퀘스트", ConsoleColor.DarkGray);
            QuestManager.Instance.ShowEndQuestList((TownName)GameManager.Instance.currentTown.id);
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
                    if(QuestManager.Instance.SelectQuest((TownName)GameManager.Instance.currentTown.id, intCommand))
                        questInformation = true;
                    break;
            }
        }

        /// <summary>
        /// 선택된 퀘스트의 정보 확인
        /// </summary>
        public void QuestInformation()
        {
            Console.Clear();
            Render.ColorWriteLine("퀘스트 정보", ConsoleColor.Cyan);
            Console.WriteLine("─────────────────────────");
            QuestManager.Instance.ShowQuestInformation();
            Console.WriteLine("0. 나가기");

            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch (intCommand)
            {
                // 퀘스트 목록으로 이동
                case 0:
                    questInformation = false;
                    break;

                // 현재 보고있는 퀘스트를 수락 또는 완료
                case 1:
                    if (!QuestManager.Instance.QuestAccept())
                    {
                        // 퀘스트 완료시 퀘스트 정보 false 보상 true
                        questInformation = false;
                        questReward = true;
                    }
                    break;
            }
        }
        
        /// <summary>
        /// 완료한 퀘스트의 보상 확인
        /// </summary>
        public void QuestReward()
        {
            Console.Clear();
            Render.ColorWriteLine("퀘스트 보상", ConsoleColor.Cyan);
            Console.WriteLine("─────────────────────────\n");
            QuestManager.Instance.selectQuest?.ShowQuestReward();
            Console.WriteLine("\n─────────────────────────");
            if(QuestManager.Instance.newQuestString != null)
            {
                Render.ColorWriteLine("신규퀘스트", ConsoleColor.Cyan);
                Console.Write($"\n{QuestManager.Instance.newQuestString}\n");
                QuestManager.Instance.newQuestString = null;

            }

            Console.WriteLine("─────────────────────────");
            Render.ColorWriteLine("Press Any Key to Continue", ConsoleColor.DarkGray);
            Console.ReadKey();

            questReward = false;
        }
    }
}
