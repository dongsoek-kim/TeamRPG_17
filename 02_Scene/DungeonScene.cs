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

        private void DungeonMain()
        {
            Console.Clear();
            Console.WriteLine("던전 정보");
            Console.WriteLine($"\n현재 던전의 레벨은 {currentDungeon.Level}입니다.\n");
            Console.WriteLine($"{currentDungeon.DungeonName} - {currentDungeon.DungeonInfo}\n\n");

            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 입장하기");

            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch(intCommand)
            {
                case 0:
                    GameManager.Instance.ChangeScene(SceneName.LobbyScene);
                    break;
                case 1:
                    BattleScene battle = new BattleScene();
                    battle.StartBattle();
                    break;
                   
            }
        }

        private void DungeonResult(int _dungeonLevel)
        {
            bool dungeonClear = true;

            Console.Clear();

        }
    }
}
