using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    public class ShopScene : Scene
    {
        private int nowPage;
        private int totalPage;

        private bool onBuy;
        private bool onSell;

        private Shop shop;

        public ShopScene()
        {
            nowPage = 0;

            onBuy = false;
            onSell = false;

            shop = new Shop();
        }

        public override void Update()
        {
            if (onBuy)
                ShopBuy();      // 아이템 판매

            else if (onSell)        
                ShopSell();     // 아이템 구매

            else
                ShopMain();     // 상점 메인
        }

        private void ShopMain()
        {
            Console.Clear();
            Console.WriteLine("상점");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{GameManager.Instance.player.gold} G");
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("[아이템 목록]");
            shop.PrintItemList();
            Console.WriteLine("─────────────────────────");

            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");

            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch (intCommand)
            {
                case 0:
                    GameManager.Instance.ChangeScene(SceneName.LobbyScene);
                    break;
                case 1:
                    onBuy = true;
                    break;
                case 2:
                    onSell = true;
                    break;
            }
        }

        private void ShopBuy()
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 구매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{GameManager.Instance.player.gold} G");
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("[아이템 목록]");
            shop.PrintItemList(true);
            Console.WriteLine("─────────────────────────");

            Console.WriteLine("0. 나가기");

            shop.ShopMessage();
            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;
            
            switch (intCommand)
            {
                case 0:
                    onBuy = false;
                    break;

                default:
                    shop.BuyItem(GameManager.Instance.currentTown.startItemIdx + intCommand);
                    break;
            }
        }

        private void ShopSell()
        {
            Console.Clear();
            Console.WriteLine("상점 - 아이템 판매");
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{GameManager.Instance.player.gold} G");
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("[아이템 목록]");
            shop.SellItemList();
            Console.WriteLine("─────────────────────────");

            Console.WriteLine("0. 나가기");

            shop.ShopMessage();
            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch(intCommand)
            {
                case 0:
                    onSell = false;
                    break;
                default:
                    shop.SellItem(intCommand);
                    break;
            }
        }

        private void ItemPage()
        {
            if (nowPage == 0 && totalPage > 0)
            {
                Console.WriteLine("11. 다음 페이지");
            }
            else if (nowPage > 0 && totalPage - 1 != nowPage)
            {
                Console.WriteLine("11. 다음 페이지");
                Console.WriteLine("12. 이전 페이지");
            }
            else if (nowPage > 0)
            {
                Console.WriteLine("12. 이전 페이지");
            }
        }
    }
}
