using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class DungeonScene : Scene
    {

        public override void Update()
        {
            DungeonMain();
        }

        private void DungeonMain()
        {
            Console.Clear();
            Console.WriteLine("던전 정보");
            Console.WriteLine($"현재 던전의 레벨은 입니다.");


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
