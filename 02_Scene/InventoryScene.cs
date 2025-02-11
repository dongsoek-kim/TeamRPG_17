using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamRPG_17
{
    internal class InventoryScene : Scene
    {
        private bool onEquip;
        int totalPage;
 
        int nowPage;
        public InventoryScene()
        {
            onEquip = false;
            nowPage = 0;
        }

        public override void Update()
        {
            if (onEquip)
                equipManagement();      // 장비 관리
            else
                InventoryPrint();       // 인벤토리
        }
        private void InventoryPrint()
        {
            Console.Clear();
            Render.ColorWriteLine($"인벤토리",ConsoleColor.Cyan);
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────────────────────");
            Render.ColorWriteLine("[아이템 목록]",ConsoleColor.Cyan);
            GameManager.Instance.player.inventory.ShowInventory(nowPage,out totalPage);
            Console.WriteLine($"{nowPage+1}/{totalPage}페이지");
            Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────────────────────");
            Render.ColorWriteLine("[장착중인 장비]                                             |            [포션갯수]",ConsoleColor.Cyan);
            Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────────────────────");
            GameManager.Instance.player.inventory.showPotion(nowPage);
            GameManager.Instance.player.inventory.showNowEquip();
            Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────────────────────");          
            Console.WriteLine("1. 장착 관리");
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
                    onEquip = true;
                    break;
                case 2:
                    if (totalPage-1 == nowPage)
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
                case 3:
                    if(nowPage==0)
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
        private void equipManagement()
        {
            Console.Clear();
            Render.ColorWriteLine("인벤토리 - 장착 관리",ConsoleColor.Cyan);
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────────────────────");
            Render.ColorWriteLine("[아이템 목록]",ConsoleColor.Cyan);
            GameManager.Instance.player.inventory.ShowEquip(nowPage,out totalPage);
            Console.WriteLine($"{nowPage + 1}/{totalPage}페이지");
            Console.WriteLine("────────────────────────────────────────────────────────────────────────────────────────────────────");
            Console.WriteLine("0. 나가기");
            ItemPage2();
            
            if (!GameManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch (intCommand)
            {
                case 0:
                    onEquip = false;
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
                    {   
                        if(intCommand>0&&intCommand<8) GameManager.Instance.player.inventory.Equipment(intCommand + (nowPage * 7));
                        break;
                    }
            }
        }
        private void ItemPage()
        {
            if(nowPage==0 && totalPage>0)
            {
                Console.WriteLine("2. 다음 페이지");
            }
            else if(nowPage>0 &&totalPage-1!=nowPage)
            {
                Console.WriteLine("2. 다음 페이지");
                Console.WriteLine("3. 이전 페이지");
            }
            else if(nowPage>0)
            {
                Console.WriteLine("3. 이전 페이지");
            }
        }
        private void ItemPage2()
        {
            if (nowPage == 0 && totalPage > 0)
            {
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
