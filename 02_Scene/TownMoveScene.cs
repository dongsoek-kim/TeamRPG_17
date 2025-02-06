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
            int length = Enum.GetValues(typeof(ItemName)).Length;

            Console.Clear();
            Console.WriteLine("도시 이름들어갈 곳");
            Console.WriteLine("이동해주실 도시를 선택해주세요\n");
            Console.WriteLine("─────────────────────────");
            for ( int i = 0; i < length; i++)
                Console.WriteLine($"{i + 1}. 마을 이름");
            
            Console.WriteLine("─────────────────────────");
            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            if(intCommand >= 0 && intCommand < length)
            {
                // 게임매니저 변수에 담기
                GameManager.Instance.ChangeScene(SceneName.LobbyScene);
            }
        }
    }
}
