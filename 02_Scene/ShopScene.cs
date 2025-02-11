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
        private int itemsPerPage; // 아이템 갯수

        private int startIndex; // 현재 상점 페이지에 따라서 시작위치가 바뀌는 아이템 인덱스 

        private bool onBuy;
        private bool onSell;

        private Shop shop;

        public ShopScene()
        {
            nowPage = 0;
            itemsPerPage = 7;

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
            shop.PrintItemList(itemsPerPage, nowPage, out startIndex, out totalPage);
            Console.WriteLine("─────────────────────────");

            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매\n");
            ItemPage();
            Console.WriteLine("0. 나가기");

            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch (intCommand)
            {
                case 0:
                    nowPage = 0;
                    GameManager.Instance.ChangeScene(SceneName.LobbyScene);
                    break;
                case 1:
                    nowPage = 0;
                    onBuy = true;
                    break;
                case 2:
                    nowPage = 0;
                    onSell = true;
                    break;
                case 11:
                    if (totalPage - 1 == nowPage)
                    {
                        Console.WriteLine("마지막 페이지입니다.");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        nowPage++;
                        break;
                    }
                case 12:
                    if (nowPage == 0)
                    {
                        Console.WriteLine("첫 페이지입니다.");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        nowPage--;
                        break;
                    }
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
            int itemCount = shop.PrintItemList(itemsPerPage, nowPage, out startIndex, out totalPage, true);
            Console.WriteLine("─────────────────────────");
            ItemPage();
            Console.WriteLine("0. 나가기");

            shop.ShopMessage();
            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch (intCommand)
            {
                case 0:
                    nowPage = 0;
                    onBuy = false;
                    break;
                case 11:
                    if (totalPage - 1 == nowPage)
                    {
                        Console.WriteLine("마지막 페이지입니다.");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        nowPage++;
                        break;
                    }
                case 12:
                    if (nowPage == 0)
                    {
                        Console.WriteLine("첫 페이지입니다.");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        nowPage--;
                        break;
                    }
                default:

                    if (intCommand < 0 || intCommand > itemCount - 1)
                    {
                        Render.ColorWriteLine("잘못입력하셨습니다.", ConsoleColor.Red);
                        Console.ReadKey(true);
                        return;
                    }

                    int index = startIndex + intCommand;
                    shop.BuyItem(index);
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
            int curPageItemCount = shop.SellItemList(itemsPerPage, nowPage, out totalPage); // 현재 페이지의 아이템의 개수 반환
            Console.WriteLine("─────────────────────────");
            ItemPage();
            Console.WriteLine("0. 나가기");

            shop.ShopMessage();
            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch (intCommand)
            {
                case 0:
                    nowPage = 0;
                    onSell = false;
                    break;
                case 11:
                    if (totalPage - 1 == nowPage)
                    {
                        Console.WriteLine("마지막 페이지입니다.");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        nowPage++;
                        break;
                    }
                case 12:
                    if (nowPage == 0)
                    {
                        Console.WriteLine("첫 페이지입니다.");
                        Console.ReadKey();
                        break;
                    }
                    else
                    {
                        nowPage--;
                        break;
                    }
                default:
                    shop.SellItem(nowPage * itemsPerPage + intCommand);
                    if (curPageItemCount - 1 <= 0 && nowPage > 0) // 현재 페이지 아이템 개수가 없으면 이전 페이지로
                    {
                        nowPage--;
                    }


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
