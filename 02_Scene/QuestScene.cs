﻿using System;
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
            QuestManager.Instance.ShowQuestList((TownName)GameManager.Instance.currentTown.id);
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
                // 퀘스트 목록으로 이동
                case 0:
                    questInformation = false;
                    break;

                // 현재 보고있는 퀘스트를 수락 또는 완료
                // 퀘스트 수락 / 퀘스트 완료 실패 true
                // 퀘스트 완료 false
                case 1:
                    if (!QuestManager.Instance.SelectQuestAccept())
                        questInformation = false;
                    break;
            }
        }
    }
}
