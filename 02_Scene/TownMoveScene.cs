using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17._02_Scene
{
    internal class TownMoveScene : Scene
    {
        public override void Update()
        {
            int length = Enum.GetValues(typeof(TownName)).Length;

            Console.Clear();
            Console.WriteLine("도시 이름들어갈 곳");
            Console.WriteLine("이동해주실 도시를 선택해주세요\n");
            Console.WriteLine("─────────────────────────");
            for ( int i = 0; i < length; i++)
            {
                Console.WriteLine($"{i + 1}. {GameManager.Instance.towns[i].name}");
            }    
            Console.WriteLine("─────────────────────────");
            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            if(intCommand >= 0 && intCommand < length)
            {
                // GameManager에 가지고있는 towns배열, 현재타운번호를 대입해서 플레이어가 입장 할 수 있는지 확인한다.
                if (GameManager.Instance.currentTown.CanEnterTown() == false)
                    return;

                // 게임매니저에 있는 현재 도시 설정후 로비씬
                GameManager.Instance.currentTown = GameManager.Instance.towns[intCommand - 1];
                GameManager.Instance.ChangeScene(SceneName.LobbyScene);
            }
        }
    }
}
