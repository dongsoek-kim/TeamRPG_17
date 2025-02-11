using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    internal class TownMoveScene : Scene
    {
        private ConsoleColor[] colors;
        public TownMoveScene()
        {
            colors = new ConsoleColor[Enum.GetValues(typeof(TownName)).Length];
            colors[(int)TownName.Elinia] = ConsoleColor.Green;
            colors[(int)TownName.Hannesys] = ConsoleColor.Red;
            colors[(int)TownName.CunningCity] = ConsoleColor.DarkGray;
        }
        public override void Update()
        {
            int length = Enum.GetValues(typeof(TownName)).Length;

            Console.Clear();
            Render.ColorWriteLine("이동해주실 도시를 선택해주세요\n", ConsoleColor.Cyan);
            Console.WriteLine($"현재 플레이어의 레벨 : {GameManager.Instance.player.level}");
            Console.WriteLine("─────────────────────────");
            for ( int i = 0; i < length; i++)
            {
                Render.ColorWrite($"{i + 1}. {GameManager.Instance.towns[i].name}", colors[i]);
                Render.ColorWriteLine($" | 도시레벨 : {GameManager.Instance.towns[i].entryLevel}", colors[i]);
            }    
            Console.WriteLine("─────────────────────────");
            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            if(intCommand > 0 && intCommand <= length)
            {
                // GameManager에 가지고있는 towns배열, 현재타운번호를 대입해서 플레이어가 입장 할 수 있는지 확인한다.
                if (GameManager.Instance.towns[intCommand - 1].CanEnterTown() == false)
                {
                    Console.WriteLine("레벨이 맞지 않아서 입장할 수 없습니다.");
                    Console.ReadKey(true); // 입력 대기
                    return;
                }

                // 게임매니저에 있는 현재 도시 설정후 로비씬
                GameManager.Instance.currentTown = GameManager.Instance.towns[intCommand - 1];
                GameManager.Instance.player.nowTown = (TownName)intCommand - 1;
                GameManager.Instance.ChangeScene(SceneName.LobbyScene);
            }
        }
    }
}
