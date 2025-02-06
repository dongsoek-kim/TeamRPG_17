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
        public InventoryScene()
        {
            onEquip = false;
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
            Console.WriteLine("인벤토리");
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("[아이템 목록]");
            Player.Instance.inventory.ShowInventory();
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("1. 장착 관리");
            Console.WriteLine("0. 나가기");

            if (!SceneManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch (intCommand)
            {
                case 0:
                    SceneManager.Instance.ChangeScene(SceneName.LobbyScene);
                    break;
                case 1:
                    onEquip = true;
                    break;

            }
        }
        private void equipManagement()
        {
            Console.Clear();
            Console.WriteLine("인벤토리 - 장착 관리");
            Console.WriteLine("보유중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("[아이템 목록]");
            Player.Instance.inventory.ShowEquip();
            Console.WriteLine("─────────────────────────");
            Console.WriteLine("0. 나가기");

            if (!SceneManager.Instance.SceneInputCommand(out int intCommand))
                return;

            switch (intCommand)
            {
                case 0:
                    onEquip = false;
                    break;

                default:
                    Player.Instance.inventory.Equipment(intCommand);
                    break;
            }
        }
    }
}
