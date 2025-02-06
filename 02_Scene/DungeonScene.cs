using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class DungeonScene : Scene
    {
        private bool onResult;
        private Random random;

        private int[] dungeonDefense;   // 던전 권장 방어력
        private int[] dungeonPrize;     // 던전 보상
        private int dungeonLevel;       // 선택된 던전 레벨

        public DungeonScene() 
        {
            random = new Random();
            onResult = false;

            dungeonDefense = new int[3];
            dungeonDefense[0] = 5;
            dungeonDefense[1] = 11;
            dungeonDefense[2] = 17;

            dungeonPrize = new int[3];
            dungeonPrize[0] = 1000;
            dungeonPrize[1] = 1700;
            dungeonPrize[2] = 2500;
        }

        public override void Update()
        {
            if (onResult)
                DungeonResult(dungeonLevel);    // 던전 결과
            else
                DungeonMain();                  // 던전 입장 전
        }

        private void DungeonMain()
        {
            Console.Clear();
            Console.WriteLine("던전입장");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("1. 쉬운 던전     | 빙어력 5 이상 권장");
            Console.WriteLine("2. 일반 던전     | 빙어력 11 이상 권장");
            Console.WriteLine("3. 어려운 던전    | 방어력 17 이상 권장");
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("0. 나가기");

            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch(intCommand)
            {
                case 0:
                    GameManager.Instance.ChangeScene(SceneName.LobbyScene);
                    break;
                case 1: 
                case 2: 
                case 3:
                    onResult = true;
                    dungeonLevel = intCommand;
                    break;
                   
            }
        }

        private void DungeonResult(int _dungeonLevel)
        {
            bool dungeonClear = true;

            Console.Clear();
            string dungeonTitle = "던전 클리어";
            string dungeonDescription = "축하합니다!!\n던전을 클리어 하였습니다.";

            int dungeon = dungeonDefense[_dungeonLevel - 1];            // 던전 권장 방어력
            int playerDefense = (int)GameManager.Instance.player.defense;                // 플레이어 방어력
            int itemDefense = GameManager.Instance.player.GetArmorStat();           // 플레이어 장비 방어력

            int hpOffset = dungeon - (playerDefense + itemDefense);     // hp감소량 offset
            int hpDecrease = random.Next(20 + hpOffset, 35 + hpOffset); // hp감소량

            int clearGold = dungeonPrize[_dungeonLevel - 1];

            // 0보다 클때 ( hp 감소량 증가 )
            // *권장 방어력보다 낮을때
            if(hpOffset > 0)
            {
                // 0 1 2 3 4
                if(random.Next(0, 1000) % 5 < 2)
                {
                    dungeonTitle = "던전 실패";
                    dungeonDescription = "던전을 실패 하였습니다.";
                    hpDecrease = GameManager.Instance.player.hp / 2;
                    dungeonClear = false;
                }
            }

            Console.WriteLine(dungeonTitle);
            Console.WriteLine(dungeonDescription);
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("[탐험 결과]");
            Console.WriteLine($"체력 {GameManager.Instance.player.hp} -> {GameManager.Instance.player.hp - hpDecrease}");
            if(dungeonClear)
                Console.WriteLine($"Gold {GameManager.Instance.player.gold} G -> {GameManager.Instance.player.gold + clearGold} G");
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("0. 나가기");

            //if (!GameManager.Instance.SceneInputCommand(out int intCommand))
            //    return;
            Console.ReadLine();

            GameManager.Instance.player.hp -= hpDecrease;
            GameManager.Instance.player.gold += clearGold;
            GameManager.Instance.player.AddExp();
            onResult = false;
            GameManager.Instance.ChangeScene(SceneName.LobbyScene);

        }
    }
}
