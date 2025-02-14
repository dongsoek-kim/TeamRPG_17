﻿using System;
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

        private bool onBuy; // 구매 페이지 여부
        private bool onSell; // 판매 페이지 여부

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
                ShopBuy();

            else if (onSell)
                ShopSell();

            else
                ShopMain();
        }

        /// <summary>
        /// 상점 페이지의 메인
        /// </summary>
        private void ShopMain()
        {
            Console.Clear();
            Render.ColorWriteLine("상점", ConsoleColor.Cyan);
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("─────────────────────────");
            Render.ColorWriteLine("[보유 골드]", ConsoleColor.Cyan);
            Render.ColorWriteLine($"{GameManager.Instance.player.gold} G", ConsoleColor.Yellow);
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
                case 8:
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
                case 9:
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

        /// <summary>
        /// 상점 아이템 구매
        /// </summary>
        private void ShopBuy()
        {
            Console.Clear();
            Render.ColorWriteLine("상점 - 아이템 구매", ConsoleColor.Cyan);
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("─────────────────────────");
            Render.ColorWriteLine("[보유 골드]", ConsoleColor.Cyan);
            Render.ColorWriteLine($"{GameManager.Instance.player.gold} G", ConsoleColor.Yellow);
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
                case 8:
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
                case 9:
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

        /// <summary>
        /// 상점 아이템 판매
        /// </summary>
        private void ShopSell()
        {
            Console.Clear();
            Render.ColorWriteLine("상점 - 아이템 판매", ConsoleColor.Cyan);
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.");
            Console.WriteLine("─────────────────────────");
            Render.ColorWriteLine("[보유 골드]", ConsoleColor.Cyan);
            Render.ColorWriteLine($"{GameManager.Instance.player.gold} G", ConsoleColor.Yellow);
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("[아이템 목록]");
            int curPageItemCount = shop.SellItemList(itemsPerPage, nowPage, out totalPage); // 현재 페이지의 아이템의 개수 반환
            Console.WriteLine("─────────────────────────");
            ItemPage(GameManager.Instance.player.inventory.inventory.Count(i => i != null) > itemsPerPage);
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
                case 8:
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
                case 9:
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
                    if (intCommand < 0 || intCommand > curPageItemCount)
                    {
                        Render.ColorWriteLine("잘못입력하셨습니다.", ConsoleColor.Red);
                        Console.ReadKey(true);
                        return;
                    }

                    shop.SellItem(nowPage * itemsPerPage + intCommand);
                    if (curPageItemCount - 1 <= 0 && nowPage > 0) // 현재 페이지 아이템 개수가 없으면 이전 페이지로
                    {
                        nowPage--;
                    }


                    break;
            }
        }

        /// <summary>
        /// 상점 메뉴 버튼 출력
        /// </summary>
        /// <param name="isActive">메뉴 버튼을 활성화 할것인지 여부</param>
        private void ItemPage(bool isActive = true)
        {
            if (nowPage == 0 && totalPage > 0)
            {
                if(isActive)
                    Console.WriteLine("8. 다음 페이지");
            }
            else if (nowPage > 0 && totalPage - 1 != nowPage)
            {
                Console.WriteLine("8. 다음 페이지");
                Console.WriteLine("9. 이전 페이지");
            }
            else if (nowPage > 0)
            {
                Console.WriteLine("9. 이전 페이지");
            }
        }
    }
}
