using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class DungeonScene : Scene
    {
        private Dungeon currentDungeon;
        public override void Update()
        {
            int townLevel = GameManager.Instance.currentTown.entryLevel;

            currentDungeon = DungeonManager.Instance.GetDungeonByTownLevel(townLevel);

            DungeonMain();
        }

        /// <summary>
        /// 던전 메인 화면
        /// </summary>
        private void DungeonMain()
        {
            Console.Clear();
            Console.WriteLine("던전 정보");
            Console.WriteLine($"\n현재 던전의 레벨은 {currentDungeon.Level}입니다.\n");
            Console.WriteLine($"{currentDungeon.DungeonName} - {currentDungeon.DungeonInfo}\n\n");

            Render.ColorWriteLine("1. 입장하기", ConsoleColor.DarkRed);
            Console.WriteLine("0. 나가기");

            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch(intCommand)
            {
                case 0:
                    GameManager.Instance.ChangeScene(SceneName.LobbyScene);
                    break;
                case 1:
                    Battle battle = new Battle();
                    battle.StartBattle(currentDungeon);
                    break;
                   
            }
        }
    }
}
