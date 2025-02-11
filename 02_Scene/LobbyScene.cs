using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class LobbyScene : Scene
    {
        public override void Update()
        {
            Console.Clear();

            Console.WriteLine($"{GameManager.Instance.currentTown.name} 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.\n");
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("1. 상태보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 상점");
            Console.WriteLine("4. 던전입장");
            Console.WriteLine("5. 휴식하기");
            Console.WriteLine("6. 도시선택");
            Console.WriteLine("7. 퀘스트");
            Console.WriteLine("8. 저장하기");
            Console.WriteLine("─────────────────────────");
            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch(intCommand)
            {
                case 1:
                    GameManager.Instance.ChangeScene(SceneName.StatScene);
                    break;
                case 2:
                    GameManager.Instance.ChangeScene(SceneName.InventoryScene);
                    break;
                case 3:
                    GameManager.Instance.ChangeScene(SceneName.ShopScene);
                    break;
                case 4:
                    GameManager.Instance.ChangeScene(SceneName.DungeonScene);
                    break;
                case 5:
                    GameManager.Instance.ChangeScene(SceneName.SleepScene);
                    break;
                case 6:
                    GameManager.Instance.ChangeScene(SceneName.TownMoveScene);
                    break;
                case 7:
                    GameManager.Instance.ChangeScene(SceneName.QusetScene);
                    break;
                case 8:
                    Console.Clear();
                    Console.WriteLine("게임을 저장합니다.");
                    DataManager.SaveGameData(
                        GameManager.Instance.player,
                        GameManager.Instance.player.inventory, 
                        DataManager.currentSlot);    
                    Console.ReadKey();
                    break;
            }
        }
    }
}
